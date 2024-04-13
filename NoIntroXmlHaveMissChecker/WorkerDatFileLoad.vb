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
Imports System.Xml

Public Class WorkerDatFileLoad
    Private WithEvents _backgroundWorker As New BackgroundWorker()

    Public Sub New()
        AddHandler _backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _backgroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
        _backgroundWorker.WorkerReportsProgress = True
    End Sub

    Public Sub LoadData(filePath As String)
        If Not _backgroundWorker.IsBusy Then
            CurrentState = LoadingState.Loading
            _backgroundWorker.RunWorkerAsync(filePath)
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim filePath As String = CStr(e.Argument)
        Dim gameDataTable As DataTable = Nothing

        Try
            gameDataTable = LoadDatFile(filePath)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_LOADING_DAT)
        End Try

        e.Result = gameDataTable
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        RaiseEvent ProgressChanged(e.ProgressPercentage, CStr(e.UserState))
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            RaiseEvent CriticalErrorOccurred(e.Error, LocalizeMessages.MSG_ERROR_BACKGROUND_WORKER)
        ElseIf e.Cancelled Then
            RaiseEvent LoadingCancelled()
        Else
            CurrentState = LoadingState.Completed
            RaiseEvent LoadingCompleted(e.Result)
        End If
    End Sub

    Private Function LoadDatFile(filePath As String) As DataTable
        Dim gameDataTable As New DataTable()
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(filePath)

            _backgroundWorker.ReportProgress(0, $"0 / {xmlDoc.SelectNodes("//game").Count}")
            gameDataTable = ParseXmlDocument(xmlDoc)
        Catch ex As Exception
            If TypeOf ex Is FileNotFoundException Then
                RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_FILE_NOT_FOUND)
            Else
                RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_PARSING_XML)
            End If
        End Try

        Return gameDataTable
    End Function

    Private Function ParseXmlDocument(xmlDoc As XmlDocument) As DataTable
        Dim gameDataTable As DataTable = CreateGameDataTable()
        Dim gameNodes As XmlNodeList = xmlDoc.SelectNodes("//game")

        For i As Integer = 0 To gameNodes.Count - 1
            Dim gameNode As XmlNode = gameNodes(i)
            Try
                AddGameRowToDataTable(gameNode, gameDataTable)
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_ADDING_ROW)
            End Try

            Dim progressPercentage As Integer = CInt((i + 1) / gameNodes.Count * 100)
            Dim userState As String = $"{i + 1} / {gameNodes.Count}"
            _backgroundWorker.ReportProgress(progressPercentage, userState)
        Next

        Return gameDataTable
    End Function

    Private Function CreateGameDataTable() As DataTable
        Dim gameDataTable As New DataTable()
        gameDataTable.Columns.Add("Name", GetType(String))
        gameDataTable.Columns.Add("Description", GetType(String))
        gameDataTable.Columns.Add("File Name", GetType(String))
        gameDataTable.Columns.Add("Size", GetType(Long))
        gameDataTable.Columns.Add("CRC", GetType(String))
        gameDataTable.Columns.Add("MD5", GetType(String))
        gameDataTable.Columns.Add("SHA1", GetType(String))
        Return gameDataTable
    End Function

    Private Sub AddGameRowToDataTable(gameNode As XmlNode, gameDataTable As DataTable)
        Dim romNode As XmlNode = gameNode.SelectSingleNode("rom")
        If romNode Is Nothing Then
            RaiseEvent ErrorOccurred(New Exception(), LocalizeMessages.MSG_ERROR_INVALID_DATA)
            Return
        End If

        Dim row As DataRow = gameDataTable.NewRow()
        row("Name") = If(gameNode.Attributes("name")?.Value, String.Empty)
        row("Description") = If(gameNode.SelectSingleNode("description")?.InnerText, String.Empty)
        row("File Name") = romNode.Attributes("name").Value
        row("Size") = If(romNode.Attributes("size")?.Value, 0L)
        row("CRC") = If(romNode.Attributes("crc")?.Value, String.Empty)
        row("MD5") = If(romNode.Attributes("md5")?.Value, String.Empty)
        row("SHA1") = If(romNode.Attributes("sha1")?.Value, String.Empty)
        gameDataTable.Rows.Add(row)
    End Sub

    Public Event ProgressChanged(progress As Integer, message As String)
    Public Event InformationReceived(message As String)
    Public Event ErrorOccurred(ex As Exception, message As String)
    Public Event CriticalErrorOccurred(ex As Exception, message As String)
    Public Event LoadingCompleted(result As DataTable)
    Public Event LoadingCancelled()

    Public Enum LoadingState
        NotStarted
        Loading
        Completed
    End Enum

    Public Property CurrentState As LoadingState = LoadingState.NotStarted
End Class