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

Imports System.IO

Public Class SettingsFilters
    Private Const INI_FILE_PATH As String = "filter_settings.ini"

    Private Const SECTION_REGION As String = "Region"
    Private Const SECTION_LANGUAGE As String = "Language"
    Private Const SECTION_OTHER As String = "Other"

    Private ReadOnly _iniFilePath As String

    Public Sub New()
        _iniFilePath = Path.Combine(Application.StartupPath, INI_FILE_PATH)
    End Sub

    Public Function LoadRegions() As List(Of String)
        Return ReadSectionValues(SECTION_REGION)
    End Function

    Public Function LoadLanguages() As List(Of String)
        Return ReadSectionValues(SECTION_LANGUAGE)
    End Function

    Public Function LoadOthers() As List(Of String)
        Return ReadSectionValues(SECTION_OTHER)
    End Function

    Private Function ReadSectionValues(sectionName As String) As List(Of String)
        Dim values As New List(Of String)()

        If File.Exists(_iniFilePath) Then
            Dim iniContent As String() = File.ReadAllLines(_iniFilePath)
            Dim sectionFound As Boolean = False
            Dim currentSection As String = ""

            For Each line As String In iniContent
                Dim trimmedLine As String = line.Trim()
                If line.Trim() = $"[{sectionName}]" Then
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2)
                    sectionFound = True
                ElseIf sectionFound AndAlso line.StartsWith("[") Then
                    Exit For
                ElseIf sectionFound Then
                    If currentSection = SECTION_LANGUAGE Then
                        Dim itemParts As String() = trimmedLine.Split(","c)
                        If itemParts.Length = 3 Then
                            Dim code As String = itemParts(0).Trim()
                            Dim english As String = itemParts(1).Trim()
                            Dim native As String = itemParts(2).Trim()
                            values.Add($"{code} - {english} ({native})")
                        End If
                    Else
                        If Not String.IsNullOrWhiteSpace(line) Then
                            values.Add(line.Trim())
                        End If
                    End If
                End If
            Next
        End If

        Return values
    End Function
End Class