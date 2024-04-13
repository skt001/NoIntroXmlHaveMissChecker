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
Imports System.Reflection

Public Class LocalizeMessages
    Private Const CONFIG_FILE_NAME As String = "localize_settings.ini"
    Private Shared _localizedStrings As Dictionary(Of String, String)

    Public Shared Property FORM_TITLE As String = "No-Intro XML HaveMiss Checker"
    Public Shared Property FORM_FONT_NAME As String = "Consolas"
    Public Shared Property FORM_FONT_SIZE As String = "8"

    Public Shared Property GROUP_COMPARISON_FILTERS As String = "Comparison Filters (Unchecked items will be excluded during comparison)"
    Public Shared Property GROUP_REGION_FILTERS As String = "Region Filters"
    Public Shared Property GROUP_LANGUAGE_FILTERS As String = "Language Filters"
    Public Shared Property GROUP_OTHER_FILTERS As String = "Other Filters"

    Public Shared Property GROUP_SPECIAL_SETTINGS As String = "Special"
    Public Shared Property BUTTON_APPLY_ENGLISH_SETTINGS As String = "English"
    Public Shared Property BUTTON_APPLY_JAPANESE_SETTINGS As String = "Japanese"
    Public Shared Property BUTTON_SELECT_ALL_FILTERS As String = "All"
    Public Shared Property BUTTON_DESELECT_ALL_FILTERS As String = "Nothing"
    Public Shared Property CHECKBOX_ENABLE_DEBUG_MODE As String = "Enable Debug Mode"
    Public Shared Property CHECKBOX_EXCLUDE_UNKNOWN_LANGUAGES As String = "Exclude Unknown Languages"
    Public Shared Property CHECKBOX_EXCLUDE_UNKNOWN_REGIONS As String = "Exclude Unknown Regions"

    Public Shared Property LABEL_SELECT_DAT_FILE As String = "Select No-Intro XML"
    Public Shared Property LABEL_SELECT_ROM_FOLDER As String = "Select ROM Folder"
    Public Shared Property LABEL_START_COMPARISON As String = "Start Comparison"
    Public Shared Property LABEL_NOW_LOADING As String = "Loading ..."

    Public Shared Property TAB_CONFIGURATION_SETTINGS As String = "Config"
    Public Shared Property TAB_CONSOLE_OUTPUT As String = "Console"

    Public Shared Property TAB_NOINTRO_XML As String = "No-Intro"
    Public Shared Property TAB_YOUR_ROMS As String = "ROMs"
    Public Shared Property TAB_AVAILABLE_ROMS As String = "Have"
    Public Shared Property TAB_MISSING_ROMS As String = "Miss"

    Public Shared Property TITLE_ERROR As String = "Error"
    Public Shared Property TITLE_INFO As String = "Information"

    Public Shared Property MSG_ERROR_ADDING_ROW As String = "An error occurred while adding a row to the data table."
    Public Shared Property MSG_ERROR_COMPARING_FILES As String = "An error occurred while comparing files."
    Public Shared Property MSG_ERROR_FILE_NOT_FOUND As String = "The specified file was not found."
    Public Shared Property MSG_ERROR_INVALID_DATA As String = "The data format is invalid."
    Public Shared Property MSG_ERROR_LOADING_DAT As String = "An error occurred while loading the DAT file."
    Public Shared Property MSG_ERROR_LOADING_FILES As String = "Cannot process before loading files."
    Public Shared Property MSG_ERROR_LOADING_FOLDER As String = "An error occurred while loading the folder."
    Public Shared Property MSG_ERROR_LOGGING As String = "An error occurred while logging."
    Public Shared Property MSG_ERROR_MESSAGEBOX As String = "An error occurred while showing the message box."
    Public Shared Property MSG_ERROR_MISSING_ROMS_TABLE As String = "An error occurred while handling the missing ROMs table."
    Public Shared Property MSG_ERROR_PARSING_XML As String = "An error occurred while parsing the XML file."
    Public Shared Property MSG_ERROR_SELECTING_DATFILE As String = "An error occurred while selecting the DAT file."
    Public Shared Property MSG_ERROR_SELECTING_ROMFOLDER As String = "An error occurred while selecting the ROM folder."
    Public Shared Property MSG_INFO_COMPARISON_COMPLETE As String = "Comparison complete."
    Public Shared Property MSG_ERROR_BACKGROUND_WORKER As String = "An error occurred in the background worker."
    Public Shared Property MSG_ERROR_ACCESSING_FOLDER As String = "An error occurred while accessing the folder."
    Public Shared Property MSG_ERROR_PROCESSING_FILE As String = "An error occurred while processing a file."
    Public Shared Property MSG_ERROR_READING_ZIP As String = "An error occurred while reading a ZIP file."
    Public Shared Property MSG_ERROR_ACCESSING_SUBDIRECTORY As String = "An error occurred while accessing a subdirectory."
    Public Shared Property MSG_ERROR_SEARCHING_GOOGLE As String = "An error occurred while performing a Google search."
    Public Shared Property MSG_ERROR_READING_INIFILE As String = "An error occurred while reading the INI file."
    Public Shared Property MSG_ERROR_WRITING_INIFILE As String = "An error occurred while writing the INI file."

    Public Shared Sub LoadLocalizedStrings()
        _localizedStrings = New Dictionary(Of String, String)()

        ' Set default (English) values
        SetDefaultEnglishValues()

        If File.Exists(ConfigFilePath) Then
            Dim lines As String() = File.ReadAllLines(ConfigFilePath)

            Dim currentSection As String = String.Empty

            For Each line As String In lines
                If line.Trim() = String.Empty Then Continue For

                If line.StartsWith("[") AndAlso line.EndsWith("]") Then
                    currentSection = line.Substring(1, line.Length - 2)
                Else
                    Dim parts As String() = line.Split("=")
                    If parts.Length = 2 Then
                        Dim key As String = parts(0).Trim()
                        Dim value As String = parts(1).Trim()

                        If currentSection = "LocalizedStrings" Then
                            _localizedStrings(key) = value
                        End If
                    End If
                End If
            Next

            UpdateLocalizedProperties()

            ' Add new properties to the ini file if any
            AddNewPropertiesToIniFile()
        Else
            ' If the file doesn't exist, create the ini file with default values
            CreateIniFileWithDefaultValues()
        End If
    End Sub

    Private Shared Sub SetDefaultEnglishValues()
        For Each prop As PropertyInfo In GetType(LocalizeMessages).GetProperties(BindingFlags.Public Or BindingFlags.Static)
            _localizedStrings(prop.Name) = prop.GetValue(Nothing).ToString()
        Next
    End Sub

    Private Shared Sub CreateIniFileWithDefaultValues()
        Dim defaultContent As String = "[LocalizedStrings]" & vbCrLf & vbCrLf

        For Each entry As KeyValuePair(Of String, String) In _localizedStrings
            defaultContent &= $"{entry.Key}={entry.Value}" & vbCrLf
        Next

        File.WriteAllText(ConfigFilePath, defaultContent)
    End Sub

    Private Shared Sub AddNewPropertiesToIniFile()
        Dim newProperties As New Dictionary(Of String, String)()

        For Each prop As PropertyInfo In GetType(LocalizeMessages).GetProperties(BindingFlags.Public Or BindingFlags.Static)
            If Not _localizedStrings.ContainsKey(prop.Name) Then
                newProperties(prop.Name) = prop.GetValue(Nothing).ToString()
            End If
        Next

        If newProperties.Any() Then
            Dim iniContent As String = File.ReadAllText(ConfigFilePath)

            For Each entry As KeyValuePair(Of String, String) In newProperties
                iniContent &= $"{entry.Key}={entry.Value}" & vbCrLf
            Next

            File.WriteAllText(ConfigFilePath, iniContent)
        End If
    End Sub

    Private Shared Sub UpdateLocalizedProperties()
        For Each prop As PropertyInfo In GetType(LocalizeMessages).GetProperties(BindingFlags.Public Or BindingFlags.Static)
            If _localizedStrings.ContainsKey(prop.Name) Then
                prop.SetValue(Nothing, _localizedStrings(prop.Name))
            End If
        Next
    End Sub

    Private Shared ReadOnly Property ConfigFilePath As String
        Get
            Return Path.Combine(Application.StartupPath, CONFIG_FILE_NAME)
        End Get
    End Property
End Class