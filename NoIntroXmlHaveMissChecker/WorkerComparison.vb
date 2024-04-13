' MIT License
' 
' Copyright (c) 2024 skt001
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions

Public Class WorkerComparison
    Private WithEvents _backgroundWorker As New BackgroundWorker()

    Private Const EXCLUDED_ITEM_PATTERN As String = "(\[{0}\]|\({0}( \d+)?\))"
    Private Const UNKNOWN_ITEM_PATTERN As String = "\(([^)]+)\)"

    Public Sub New()
        _backgroundWorker.WorkerReportsProgress = True
        AddHandler _backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _backgroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
    End Sub

    Public Sub StartComparison(comparisonParameters As ComparisonParameters)
        If Not _backgroundWorker.IsBusy Then
            CurrentState = ComparisonState.Running
            _backgroundWorker.RunWorkerAsync(comparisonParameters)
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim parameters As ComparisonParameters = DirectCast(e.Argument, ComparisonParameters)
        Dim result As ComparisonSummary = Nothing

        Try
            result = CompareFiles(parameters)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_COMPARING_FILES)
        End Try

        e.Result = result
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        RaiseEvent ProgressUpdated(e.ProgressPercentage, CStr(e.UserState))
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            RaiseEvent CriticalErrorOccurred(e.Error, LocalizeMessages.MSG_ERROR_BACKGROUND_WORKER)
        ElseIf e.Cancelled Then
            RaiseEvent ComparisonCancelled()
        Else
            CurrentState = ComparisonState.Completed
            RaiseEvent ComparisonCompleted(e.Result)
        End If
    End Sub

    Private Function CompareFiles(parameters As ComparisonParameters) As ComparisonSummary
        _backgroundWorker.ReportProgress(0, "0/0")

        Dim folderFilesView As DataView = parameters.FolderFilesTable.DefaultView
        folderFilesView.Sort = "File Name"

        Dim xmlFilesView As DataView = parameters.XmlFilesTable.DefaultView
        xmlFilesView.Sort = "File Name"

        Dim matchedFilesTable As New DataTable()
        matchedFilesTable.Columns.Add("Name", GetType(String))
        matchedFilesTable.Columns.Add("Description", GetType(String))
        matchedFilesTable.Columns.Add("File Name", GetType(String))
        matchedFilesTable.Columns.Add("Size", GetType(Long))
        matchedFilesTable.Columns.Add("CRC", GetType(String))
        matchedFilesTable.Columns.Add("MD5", GetType(String))
        matchedFilesTable.Columns.Add("SHA1", GetType(String))

        Dim missingFilesTable As New DataTable()
        missingFilesTable.Columns.Add("Name", GetType(String))
        missingFilesTable.Columns.Add("Description", GetType(String))
        missingFilesTable.Columns.Add("File Name", GetType(String))
        missingFilesTable.Columns.Add("Size", GetType(Long))
        missingFilesTable.Columns.Add("CRC", GetType(String))
        missingFilesTable.Columns.Add("MD5", GetType(String))
        missingFilesTable.Columns.Add("SHA1", GetType(String))

        Dim matchCount As Integer = 0
        Dim missCount As Integer = 0

        For currentIndex As Integer = 0 To xmlFilesView.Count - 1
            Dim fileName As String = Path.GetFileName(xmlFilesView(currentIndex)("File Name").ToString().Trim())

            If IsExcludedByFilter(fileName, parameters) Then
                'RaiseEvent InformationReceived("IsExcludedByFilter: " & fileName)
                Continue For
            End If

            If Not CanIncludeItem(fileName, parameters) Then
                Continue For
            End If

            Dim matchedRows As DataRowView() = folderFilesView.FindRows(New Object() {fileName})

            If matchedRows.Length > 0 Then
                For Each folderRow As DataRowView In matchedRows
                    Dim newRow As DataRow = matchedFilesTable.NewRow()
                    newRow("Name") = xmlFilesView(currentIndex)("Name")
                    newRow("Description") = xmlFilesView(currentIndex)("Description")
                    newRow("File Name") = xmlFilesView(currentIndex)("File Name")
                    newRow("Size") = xmlFilesView(currentIndex)("Size")
                    newRow("CRC") = xmlFilesView(currentIndex)("CRC")
                    newRow("MD5") = xmlFilesView(currentIndex)("MD5")
                    newRow("SHA1") = xmlFilesView(currentIndex)("SHA1")

                    matchedFilesTable.Rows.Add(newRow)
                Next
                matchCount += 1
            Else
                Dim newRow As DataRow = missingFilesTable.NewRow()

                newRow("Name") = xmlFilesView(currentIndex)("Name")
                newRow("Description") = xmlFilesView(currentIndex)("Description")
                newRow("File Name") = xmlFilesView(currentIndex)("File Name")
                newRow("Size") = xmlFilesView(currentIndex)("Size")
                newRow("CRC") = xmlFilesView(currentIndex)("CRC")
                newRow("MD5") = xmlFilesView(currentIndex)("MD5")
                newRow("SHA1") = xmlFilesView(currentIndex)("SHA1")

                missingFilesTable.Rows.Add(newRow)
                missCount += 1
            End If

            _backgroundWorker.ReportProgress(CInt((currentIndex + 1) / xmlFilesView.Count * 100),
                                             $"{currentIndex + 1} / {xmlFilesView.Count}")
        Next

        Return New ComparisonSummary(matchedFilesTable, missingFilesTable, matchCount, missCount)
    End Function

    Private Function IsExcludedByFilter(fileName As String, parameters As ComparisonParameters) As Boolean
        If IsExcludedItem(fileName, parameters.ExcludeOtherItems) Then
            Return True
        End If

        If parameters.ExcludeUnknownRegions AndAlso Not ContainsKnownItems(fileName, parameters.IncludeRegions) Then
            Return True
        End If

        If parameters.ExcludeUnknownLanguages AndAlso Not ContainsKnownItems(fileName, parameters.IncludeLanguages) Then
            Return True
        End If

        Return False
    End Function

    Private Function CanIncludeItem(fileName As String, parameters As ComparisonParameters) As Boolean
        If Not ContainsAnyItem(fileName, parameters.IncludeRegions, parameters.AllRegions) Then
            'RaiseEvent InformationReceived("Regions: " & fileName)
            Return False
        End If

        If Not ContainsAnyItem(fileName, parameters.IncludeLanguages, parameters.AllLanguages) Then
            'RaiseEvent InformationReceived("Languages: " & fileName)
            Return False
        End If

        Return True
    End Function

    Private Function ContainsAnyItem(fileName As String, includeItems As List(Of String), allItems As List(Of String)) As Boolean
        Dim matches As MatchCollection = Regex.Matches(fileName, UNKNOWN_ITEM_PATTERN)
        Dim allMatches As New List(Of String)()
        Dim allMatchesCsv As String

        For Each match As Match In matches
            allMatches.Add(match.Groups(1).Value)
        Next
        allMatchesCsv = String.Join(",", allMatches)

        'RaiseEvent InformationReceived(fileName)

        Dim filteredItems As List(Of String) = Split(allMatchesCsv, ",").Select(Function(item) item.Trim()).ToList()

        If Not filteredItems.Intersect(allItems).Any() Then
            Return True
        End If

        If filteredItems.Intersect(includeItems).Any() Then
            'RaiseEvent InformationReceived(" Hit Tags: " & String.Join(",", includeItems))
            Return True
        End If

        Return False
    End Function

    Private Function IsExcludedItem(fileName As String, excludedItems As List(Of String)) As Boolean
        For Each excludedItem As String In excludedItems
            If Regex.IsMatch(fileName, String.Format(EXCLUDED_ITEM_PATTERN, Regex.Escape(excludedItem))) Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Function ContainsKnownItems(fileName As String, knownItems As List(Of String)) As Boolean
        Dim matches As MatchCollection = Regex.Matches(fileName, UNKNOWN_ITEM_PATTERN)
        For Each match As Match In matches
            Dim items As String() = match.Groups(1).Value.Split(","c)
            For Each item As String In items
                If knownItems.Contains(item.Trim()) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Public Class ComparisonParameters
        Public ReadOnly FolderFilesTable As DataTable
        Public ReadOnly XmlFilesTable As DataTable
        Public ReadOnly AllRegions As List(Of String)
        Public ReadOnly AllLanguages As List(Of String)
        Public ReadOnly IncludeRegions As List(Of String)
        Public ReadOnly IncludeLanguages As List(Of String)
        Public ReadOnly ExcludeOtherItems As List(Of String)
        Public ReadOnly ExcludeUnknownRegions As Boolean
        Public ReadOnly ExcludeUnknownLanguages As Boolean

        Public Sub New(folderFilesTable As DataTable, xmlFilesTable As DataTable,
                       allRegions As List(Of String), allLanguages As List(Of String),
                       includeRegions As List(Of String), includeLanguages As List(Of String),
                       excludeOtherItems As List(Of String), excludeUnknownRegions As Boolean,
                       excludeUnknownLanguages As Boolean)
            Me.FolderFilesTable = folderFilesTable
            Me.XmlFilesTable = xmlFilesTable
            Me.AllRegions = allRegions
            Me.AllLanguages = allLanguages
            Me.IncludeRegions = includeRegions
            Me.IncludeLanguages = includeLanguages
            Me.ExcludeOtherItems = excludeOtherItems
            Me.ExcludeUnknownRegions = excludeUnknownRegions
            Me.ExcludeUnknownLanguages = excludeUnknownLanguages
        End Sub
    End Class

    Public Class ComparisonSummary
        Public ReadOnly MatchedFilesTable As DataTable
        Public ReadOnly MissingFilesTable As DataTable
        Public ReadOnly MatchCount As Integer
        Public ReadOnly MissCount As Integer

        Public Sub New(matchedFilesTable As DataTable, missingFilesTable As DataTable,
                       matchCount As Integer, missCount As Integer)
            Me.MatchedFilesTable = matchedFilesTable
            Me.MissingFilesTable = missingFilesTable
            Me.MatchCount = matchCount
            Me.MissCount = missCount
        End Sub
    End Class

    Public Event ProgressUpdated(progress As Integer, message As String)
    Public Event InformationReceived(message As String)
    Public Event ErrorOccurred(ex As Exception, message As String)
    Public Event CriticalErrorOccurred(ex As Exception, message As String)
    Public Event ComparisonCompleted(result As ComparisonSummary)
    Public Event ComparisonCancelled()

    Public Enum ComparisonState
        NotStarted
        Running
        Completed
    End Enum

    Public Property CurrentState As ComparisonState = ComparisonState.NotStarted
End Class