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

Public Class SettingsCurrent
    Private Const INI_FILE_PATH As String = "current_settings.ini"
    Private Const SECTION_CURRENT_SETTINGS As String = "CurrentSettings"
    Private Const LAST_USED_DATFILE As String = "LastUsedDatFile"
    Private Const LAST_USED_FOLDER As String = "LastUsedFolder"
    Private Const SELECTED_REGIONS As String = "SelectedRegions"
    Private Const SELECTED_LANGUAGES As String = "SelectedLanguages"
    Private Const SELECTED_OTHERS As String = "SelectedOthers"

    Private Const EXCLUDE_UNKNOWN_REGIONS As String = "ExcludeUnknownRegions"
    Private Const EXCLUDE_UNKNOWN_LANGUAGES As String = "ExcludeUnknownLanguages"

    Private Const WIN_TOP As String = "Top"
    Private Const WIN_LEFT As String = "Left"
    Private Const WIN_WIDTH As String = "Width"
    Private Const WIN_HEIGHT As String = "Height"

    Private Const DEFAULT_REGION_SPLITTER_DISTANCE As Integer = 130
    Private Const DEFAULT_LANGUAGE_SPLITTER_DISTANCE As Integer = 130
    Private Const DEFAULT_OTHER_SPLITTER_DISTANCE As Integer = 130

    Private Const REGION_SPLITTER_DISTANCE As String = "RegionSplitterDistance"
    Private Const LANGUAGE_SPLITTER_DISTANCE As String = "LanguageSplitterDistance"
    Private Const OTHER_SPLITTER_DISTANCE As String = "OtherSplitterDistance"

    Private ReadOnly _iniFilePath As String

    Public Sub New()
        _iniFilePath = Path.Combine(Application.StartupPath, INI_FILE_PATH)
    End Sub

    Public Function ReadLastUsedDatFile(defaultValue As String) As String
        Return ReadSetting(LAST_USED_DATFILE, defaultValue)
    End Function

    Public Sub WriteLastUsedDatFile(value As String)
        WriteSetting(LAST_USED_DATFILE, value)
    End Sub

    Public Function ReadLastUsedFolder(defaultValue As String) As String
        Return ReadSetting(LAST_USED_FOLDER, defaultValue)
    End Function

    Public Sub WriteLastUsedFolder(value As String)
        WriteSetting(LAST_USED_FOLDER, value)
    End Sub

    Public Function LoadSelectedRegions(stringList As List(Of String)) As List(Of String)
        Dim csvString As String = String.Join(",", stringList)
        Dim lines As String() = ReadSetting(SELECTED_REGIONS, csvString).Split(Environment.NewLine)
        Dim csvList As New List(Of String)

        For Each line As String In lines
            Dim values As String() = line.Split(",")
            For Each value As String In values
                csvList.Add(value.Trim())
            Next
        Next

        Return csvList
    End Function

    Public Sub SaveSelectedRegionsToFile(stringList As List(Of String))
        WriteSetting(SELECTED_REGIONS, String.Join(",", stringList))
    End Sub

    Public Function LoadSelectedLanguages(stringList As List(Of String)) As List(Of String)
        Dim csvString As String = String.Join(",", stringList)
        Dim lines As String() = ReadSetting(SELECTED_LANGUAGES, csvString).Split(Environment.NewLine)
        Dim csvList As New List(Of String)

        For Each line As String In lines
            Dim values As String() = line.Split(",")
            For Each value As String In values
                csvList.Add(value.Trim())
            Next
        Next

        Return csvList
    End Function

    Public Sub SaveSelectedLanguagesToFile(stringList As List(Of String))
        WriteSetting(SELECTED_LANGUAGES, String.Join(",", stringList))
    End Sub

    Public Function LoadSelectedOthers(stringList As List(Of String)) As List(Of String)
        Dim csvString As String = String.Join(",", stringList)
        Dim lines As String() = ReadSetting(SELECTED_OTHERS, csvString).Split(Environment.NewLine)
        Dim csvList As New List(Of String)

        For Each line As String In lines
            Dim values As String() = line.Split(",")
            For Each value As String In values
                csvList.Add(value.Trim())
            Next
        Next

        Return csvList
    End Function

    Public Sub SaveSelectedOthersToFile(stringList As List(Of String))
        WriteSetting(SELECTED_OTHERS, String.Join(",", stringList))
    End Sub

    Public Function LoadExcludeUnknownRegions(defaultValue As Boolean) As Boolean
        Return ReadBooleanSetting(EXCLUDE_UNKNOWN_REGIONS, defaultValue)
    End Function

    Public Sub SaveExcludeUnknownRegionsToFile(value As Boolean)
        WriteBooleanSetting(EXCLUDE_UNKNOWN_REGIONS, value)
    End Sub

    Public Function LoadExcludeUnknownLanguages(defaultValue As Boolean) As Boolean
        Return ReadBooleanSetting(EXCLUDE_UNKNOWN_LANGUAGES, defaultValue)
    End Function

    Public Sub SaveExcludeUnknownLanguagesToFile(value As Boolean)
        WriteBooleanSetting(EXCLUDE_UNKNOWN_LANGUAGES, value)
    End Sub

    Public Function ReadWindowSizeTop(defaultValue As String) As String
        Return ReadSetting(WIN_TOP, defaultValue)
    End Function

    Public Sub WriteWindowSizeTop(value As String)
        WriteSetting(WIN_TOP, value)
    End Sub

    Public Function ReadWindowSizeLeft(defaultValue As String) As String
        Return ReadSetting(WIN_LEFT, defaultValue)
    End Function

    Public Sub WriteWindowSizeLeft(value As String)
        WriteSetting(WIN_LEFT, value)
    End Sub

    Public Function ReadWindowSizeHeight(defaultValue As String) As String
        Return ReadSetting(WIN_HEIGHT, defaultValue)
    End Function

    Public Sub WriteWindowSizeHeight(value As String)
        WriteSetting(WIN_HEIGHT, value)
    End Sub

    Public Function ReadWindowSizeWidth(defaultValue As String) As String
        Return ReadSetting(WIN_WIDTH, defaultValue)
    End Function

    Public Sub WriteWindowSizeWidth(value As String)
        WriteSetting(WIN_WIDTH, value)
    End Sub

    Public Function ReadRegionSplitterDistance() As Integer
        Return Integer.Parse(ReadSetting(REGION_SPLITTER_DISTANCE, DEFAULT_REGION_SPLITTER_DISTANCE.ToString()))
    End Function

    Public Sub WriteRegionSplitterDistance(value As Integer)
        WriteSetting(REGION_SPLITTER_DISTANCE, value.ToString())
    End Sub

    Public Function ReadLanguageSplitterDistance() As Integer
        Return Integer.Parse(ReadSetting(LANGUAGE_SPLITTER_DISTANCE, DEFAULT_LANGUAGE_SPLITTER_DISTANCE.ToString()))
    End Function

    Public Sub WriteLanguageSplitterDistance(value As Integer)
        WriteSetting(LANGUAGE_SPLITTER_DISTANCE, value.ToString())
    End Sub

    Public Function ReadOtherSplitterDistance() As Integer
        Return Integer.Parse(ReadSetting(OTHER_SPLITTER_DISTANCE, DEFAULT_OTHER_SPLITTER_DISTANCE.ToString()))
    End Function

    Public Sub WriteOtherSplitterDistance(value As Integer)
        WriteSetting(OTHER_SPLITTER_DISTANCE, value.ToString())
    End Sub

    Private Function ReadSetting(key As String, defaultValue As String) As String
        Return ReadIniValue(SECTION_CURRENT_SETTINGS, key, defaultValue)
    End Function

    Private Sub WriteSetting(key As String, value As String)
        WriteIniValue(SECTION_CURRENT_SETTINGS, key, value)
    End Sub

    Private Function ReadBooleanSetting(key As String, defaultValue As Boolean) As Boolean
        Dim value As String = ReadSetting(key, defaultValue.ToString())
        Return Boolean.Parse(value)
    End Function

    Private Sub WriteBooleanSetting(key As String, value As Boolean)
        WriteSetting(key, value.ToString())
    End Sub

    Private Function ReadIniValue(section As String, key As String, defaultValue As String) As String
        Dim value As String = defaultValue

        Try
            If File.Exists(_iniFilePath) Then
                Dim iniContent As String() = File.ReadAllLines(_iniFilePath)
                Dim sectionFound As Boolean = False

                For Each line As String In iniContent
                    If line.Trim() = $"[{section}]" Then
                        sectionFound = True
                    ElseIf sectionFound AndAlso line.StartsWith($"{key}=") Then
                        value = line.Substring($"{key}=".Length).Trim()
                        Exit For
                    ElseIf sectionFound AndAlso line.StartsWith("[") Then
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(LocalizeMessages.MSG_ERROR_READING_INIFILE)
        End Try
        Return value
    End Function

    Private Sub WriteIniValue(section As String, key As String, value As String)
        Dim iniContent As New List(Of String)()

        Try
            If File.Exists(_iniFilePath) Then
                iniContent.AddRange(File.ReadAllLines(_iniFilePath))
            End If

            Dim sectionIndex As Integer = iniContent.IndexOf($"[{section}]")

            If sectionIndex = -1 Then
                iniContent.Add($"[{section}]")
                iniContent.Add($"{key}={value}")
            Else
                Dim keyIndex As Integer = -1
                Dim endIndex As Integer = iniContent.Count

                For i As Integer = sectionIndex + 1 To iniContent.Count - 1
                    If iniContent(i).StartsWith("[") Then
                        endIndex = i
                        Exit For
                    ElseIf iniContent(i).StartsWith($"{key}=") Then
                        keyIndex = i
                    End If
                Next

                If keyIndex = -1 Then
                    iniContent.Insert(endIndex, $"{key}={value}")
                Else
                    iniContent(keyIndex) = $"{key}={value}"
                End If
            End If

            File.WriteAllLines(_iniFilePath, iniContent)
        Catch ex As Exception
            Throw New Exception(LocalizeMessages.MSG_ERROR_WRITING_INIFILE)
        End Try
    End Sub
End Class