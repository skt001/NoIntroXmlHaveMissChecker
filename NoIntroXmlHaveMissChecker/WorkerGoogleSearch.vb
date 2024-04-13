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

Public Class WorkerGoogleSearch
    Private ReadOnly _backgroundWorker As BackgroundWorker

    Public Sub New()
        _backgroundWorker = New BackgroundWorker()
        AddHandler _backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
    End Sub

    Public Sub SearchAsync(query As String)
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.RunWorkerAsync(query)
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim query As String = e.Argument.ToString()
        Try
            SearchGoogle(query)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, LocalizeMessages.MSG_ERROR_SEARCHING_GOOGLE)
        End Try
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            RaiseEvent CriticalErrorOccurred(e.Error, LocalizeMessages.MSG_ERROR_BACKGROUND_WORKER)
        Else
            RaiseEvent SearchCompleted()
        End If
    End Sub

    Private Sub SearchGoogle(query As String)
        Dim searchUrl As String = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}"
        Process.Start(New ProcessStartInfo With {
            .FileName = searchUrl,
            .UseShellExecute = True
        })
    End Sub

    Public Event InformationReceived(message As String)
    Public Event ErrorOccurred(ex As Exception, message As String)
    Public Event CriticalErrorOccurred(ex As Exception, message As String)
    Public Event SearchCompleted()
End Class