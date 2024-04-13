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
Imports System.IO.Compression

Public Class WorkerRomFolderLoad
    Private WithEvents _backgroundWorker As New BackgroundWorker()

    Public Sub New()
        AddHandler _backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _backgroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
        _backgroundWorker.WorkerReportsProgress = True
    End Sub

    Public Sub LoadData(folderPath As String)
        If Not _backgroundWorker.IsBusy Then
            CurrentState = LoadingState.Loading
            _backgroundWorker.RunWorkerAsync(folderPath)
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim folderPath As String = CStr(e.Argument)
        Dim fileDataTable As DataTable = Nothing

        Try
            fileDataTable = LoadFolderFiles(folderPath)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_LOADING_FOLDER)
        End Try

        e.Result = fileDataTable
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

    Private Function LoadFolderFiles(folderPath As String) As DataTable
        Dim fileList As New List(Of FileInfo)()
        Try
            Dim directory As New DirectoryInfo(folderPath)
            GetAllFiles(directory, fileList)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_ACCESSING_FOLDER)
            Return Nothing
        End Try

        Dim fileDataTable As New DataTable()
        fileDataTable.Columns.Add("File Name", GetType(String))
        fileDataTable.Columns.Add("File Size", GetType(Long))
        fileDataTable.Columns.Add("File Type", GetType(String))

        _backgroundWorker.ReportProgress(0, $"0 / {fileList.Count}")

        Try
            For Each fileInfo As FileInfo In fileList
                Try
                    If fileInfo.Extension.ToLower() <> ".zip" Then
                        Dim row As DataRow = fileDataTable.NewRow()
                        row("File Name") = fileInfo.Name
                        row("File Size") = fileInfo.Length
                        row("File Type") = "Regular File"
                        fileDataTable.Rows.Add(row)
                    Else
                        AddZipContentsToDataTable(fileInfo.FullName, fileDataTable)
                    End If

                    Dim progressPercentage As Integer = CInt(fileDataTable.Rows.Count / fileList.Count * 100)
                    If fileDataTable.Rows.Count Mod 100 = 0 OrElse
                        fileList.Count - fileDataTable.Rows.Count <= 100 Then
                        Dim userState As String = $"{fileDataTable.Rows.Count} / {fileList.Count}"
                        _backgroundWorker.ReportProgress(progressPercentage, userState)
                    End If
                Catch ex As Exception
                    RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_PROCESSING_FILE)
                End Try
            Next
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_ACCESSING_FOLDER)
            Return Nothing
        End Try

        Return fileDataTable
    End Function

    Private Sub AddZipContentsToDataTable(zipFilePath As String, fileDataTable As DataTable)
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(zipFilePath)
                For Each entry As ZipArchiveEntry In archive.Entries
                    Dim row As DataRow = fileDataTable.NewRow()
                    row("File Name") = entry.FullName
                    row("File Size") = entry.Length
                    row("File Type") = "ZIP Content"
                    fileDataTable.Rows.Add(row)
                Next
            End Using
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_READING_ZIP)
        End Try
    End Sub

    Private Sub GetAllFiles(directory As DirectoryInfo, ByRef fileList As List(Of FileInfo))
        Try
            fileList.AddRange(directory.GetFiles())

            For Each subDirectory As DirectoryInfo In directory.GetDirectories()
                GetAllFiles(subDirectory, fileList)
            Next
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_ACCESSING_SUBDIRECTORY)
        End Try
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