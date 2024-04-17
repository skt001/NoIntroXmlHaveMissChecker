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

Public Class AppMainWindow

    Private WithEvents _workerdatFileLoad As New WorkerDatFileLoad()
    Private WithEvents _workerRomFolderLoad As New WorkerRomFolderLoad()
    Private WithEvents _workerComparison As New WorkerComparison()
    Private WithEvents _workerGoogleSearch As New WorkerGoogleSearch()
    Private _utility As New Utility()
    Private _settingsFilters As New SettingsFilters()
    Private _settingsCurrent As New SettingsCurrent()

    Private Sub AppMainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckBoxEnableDebugLoggingMode.Checked = True

        LocalizeMessages.LoadLocalizedStrings()
        SetLocalizedUIText()

        Me.Top = _settingsCurrent.ReadWindowSizeTop(Me.Top)
        Me.Left = _settingsCurrent.ReadWindowSizeLeft(Me.Left)
        Me.Height = _settingsCurrent.ReadWindowSizeHeight(Me.Height)
        Me.Width = _settingsCurrent.ReadWindowSizeWidth(Me.Width)
        Me.SplitContainerFilterSettingsPanels.SplitterDistance = _settingsCurrent.ReadRegionSplitterDistance()
        Me.SplitContainerAdditionalFilterSettingsPanels.SplitterDistance = _settingsCurrent.ReadLanguageSplitterDistance()
        Me.SplitContainerMiscFilterSettingsPanels.SplitterDistance = _settingsCurrent.ReadOtherSplitterDistance()

        'load checkbox list
        _utility.SetCheckedListBoxItems(CheckedListBoxRegionFilters, _settingsFilters.LoadRegions())
        _utility.SetCheckedListBoxItems(CheckedListBoxLanguageFilters, _settingsFilters.LoadLanguages())
        _utility.SetCheckedListBoxItems(CheckedListBoxOtherFilters, _settingsFilters.LoadOthers())

        'check all items
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, _utility.GetCheckedListBoxItems(CheckedListBoxRegionFilters))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, _utility.GetCheckedListBoxItems(CheckedListBoxLanguageFilters))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, _utility.GetCheckedListBoxItems(CheckedListBoxOtherFilters))
        CheckBoxExcludeUnknownRegionRoms.Checked = False
        CheckBoxExcludeUnknownLanguageRoms.Checked = False

        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, _settingsCurrent.LoadSelectedRegions(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters)))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, _settingsCurrent.LoadSelectedLanguages(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters)))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, _settingsCurrent.LoadSelectedOthers(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters)))
        CheckBoxExcludeUnknownRegionRoms.Checked = _settingsCurrent.LoadExcludeUnknownRegions(CheckBoxExcludeUnknownRegionRoms.Checked)
        CheckBoxExcludeUnknownLanguageRoms.Checked = _settingsCurrent.LoadExcludeUnknownLanguages(CheckBoxExcludeUnknownLanguageRoms.Checked)
    End Sub

    Private Sub SetLocalizedUIText()
        Me.Text = LocalizeMessages.FORM_TITLE

        Dim fontSize As Integer
        If Integer.TryParse(LocalizeMessages.FORM_FONT_SIZE, fontSize) Then
            Me.Font = New Font(LocalizeMessages.FORM_FONT_NAME, fontSize)
        Else
            Me.Font = New Font(LocalizeMessages.FORM_FONT_NAME, 8) ' デフォルトのフォントサイズを使用
        End If

        ToolStripButtonSelectNoIntroXmlFile.Text = LocalizeMessages.LABEL_SELECT_DAT_FILE
        ToolStripButtonSelectRomFolder.Text = LocalizeMessages.LABEL_SELECT_ROM_FOLDER
        ToolStripButtonStartComparison.Text = LocalizeMessages.LABEL_START_COMPARISON

        GroupBoxComparisonFilterSettings.Text = LocalizeMessages.GROUP_COMPARISON_FILTERS
        GroupBoxRegionFilterSettings.Text = LocalizeMessages.GROUP_REGION_FILTERS
        GroupBoxLanguageFilterSettings.Text = LocalizeMessages.GROUP_LANGUAGE_FILTERS
        GroupBoxOtherFilterSettings.Text = LocalizeMessages.GROUP_OTHER_FILTERS
        GroupBoxSpecialComparisonSettings.Text = LocalizeMessages.GROUP_SPECIAL_SETTINGS

        ButtonApplyEnglishComparisonSettings.Text = LocalizeMessages.BUTTON_APPLY_ENGLISH_SETTINGS
        ButtonApplyJapaneseComparisonSettings.Text = LocalizeMessages.BUTTON_APPLY_JAPANESE_SETTINGS
        ButtonSelectAllComparisonFilters.Text = LocalizeMessages.BUTTON_SELECT_ALL_FILTERS
        ButtonDeselectAllComparisonFilters.Text = LocalizeMessages.BUTTON_DESELECT_ALL_FILTERS

        CheckBoxEnableDebugLoggingMode.Text = LocalizeMessages.CHECKBOX_ENABLE_DEBUG_MODE
        CheckBoxExcludeUnknownLanguageRoms.Text = LocalizeMessages.CHECKBOX_EXCLUDE_UNKNOWN_LANGUAGES
        CheckBoxExcludeUnknownRegionRoms.Text = LocalizeMessages.CHECKBOX_EXCLUDE_UNKNOWN_REGIONS

        TabPageConfigurationSettings.Text = LocalizeMessages.TAB_CONFIGURATION_SETTINGS
        TabPageConsoleOutput.Text = LocalizeMessages.TAB_CONSOLE_OUTPUT
        TabPageNoIntroXmlViewer.Text = LocalizeMessages.TAB_NOINTRO_XML
        TabPageYourRomsList.Text = LocalizeMessages.TAB_YOUR_ROMS
        TabPageAvailableRomsList.Text = LocalizeMessages.TAB_AVAILABLE_ROMS
        TabPageMissingRomsList.Text = LocalizeMessages.TAB_MISSING_ROMS
    End Sub

    Private Sub ChangeTitle(ByVal situation As String)
        Me.Text = $"{LocalizeMessages.FORM_TITLE} ({situation})"
    End Sub

    Private Sub RestoreTitle()
        Me.Text = LocalizeMessages.FORM_TITLE
    End Sub

    Private Sub ToolStripButtonSelectNoIntroXmlFile_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSelectNoIntroXmlFile.Click
        Try
            If ToolStripButtonSelectNoIntroXmlFile.Enabled = False Then
                Exit Sub
            Else
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, False, LocalizeMessages.LABEL_NOW_LOADING)
            End If

            If _workerdatFileLoad.CurrentState = WorkerDatFileLoad.LoadingState.Loading Then
                Exit Sub
            End If

            Dim lastUsedDatFile As String = _settingsCurrent.ReadLastUsedDatFile("")
            Dim initialDirectory As String = If(String.IsNullOrEmpty(lastUsedDatFile), "", Path.GetDirectoryName(lastUsedDatFile))
            Dim initialFileName As String = If(String.IsNullOrEmpty(lastUsedDatFile), "", Path.GetFileName(lastUsedDatFile))

            Dim openFileDialog As New OpenFileDialog() With {
                    .Filter = "DAT Files (*.dat)|*.dat|All Files (*.*)|*.*",
                    .Title = "Select a DAT file",
                    .RestoreDirectory = True,
                    .InitialDirectory = initialDirectory,
                    .FileName = initialFileName
                    }
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ToolStripTextBoxSelectedNoIntroXmlFilePath.Text = openFileDialog.FileName
                ChangeTitle(Path.GetFileName(openFileDialog.FileName))
                _settingsCurrent.WriteLastUsedDatFile(openFileDialog.FileName)
                Try
                    _workerdatFileLoad.LoadData(openFileDialog.FileName)
                Catch
                    ToolStripTextBoxSelectedNoIntroXmlFilePath.Text = String.Empty
                    RestoreTitle()
                End Try
            Else
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, True, LocalizeMessages.LABEL_SELECT_DAT_FILE)
                System.Threading.Thread.Sleep(1000)
            End If
        Catch ex As Exception
            Logger.LogMessage($"Error selecting No-Intro XML file: {ex.Message}")
            Logger.ShowErrorMessageBox(LocalizeMessages.MSG_ERROR_SELECTING_DATFILE)
            _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, True, LocalizeMessages.LABEL_SELECT_DAT_FILE)
        End Try
    End Sub

    Private Sub WorkerDatFileLoad_InformationReceived(message As String) Handles _workerdatFileLoad.InformationReceived
        Logger.LogMessage(message)
    End Sub

    Private Sub WorkerDatFileLoad_ErrorOccurred(ex As Exception, message As String) Handles _workerdatFileLoad.ErrorOccurred
        Logger.LogMessage($"Error loading DAT file: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
    End Sub

    Private Sub WorkerDatFileLoad_CriticalErrorOccurred(ex As Exception, message As String) Handles _workerdatFileLoad.CriticalErrorOccurred
        Logger.LogMessage($"Critical error loading DAT file: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, True, LocalizeMessages.LABEL_SELECT_DAT_FILE)
    End Sub

    Private Sub WorkerDatFileLoad_LoadingCompleted(result As DataTable) Handles _workerdatFileLoad.LoadingCompleted
        If result IsNot Nothing AndAlso _workerdatFileLoad.CurrentState = WorkerDatFileLoad.LoadingState.Completed Then
            DisplayDatFileLoadingResult(result)
        End If
    End Sub

    Private Sub WorkerDatFileLoad_LoadingCancelled() Handles _workerdatFileLoad.LoadingCancelled
        Logger.LogMessage("DAT file loading cancelled.")
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, True, LocalizeMessages.LABEL_SELECT_DAT_FILE)
    End Sub

    Private Sub WorkerDatFileLoad_ProgressChanged(progress As Integer, message As String) Handles _workerdatFileLoad.ProgressChanged
        ToolStripProgressBarNoIntroXmlLoadProgress.Value = progress
        ToolStripLabelNoIntroXmlLoadProgressStatus.Text = message
    End Sub

    Private Sub DisplayDatFileLoadingResult(gameDataTable As DataTable)
        If gameDataTable IsNot Nothing Then
            DataGridViewNoIntroXmlTable.DataSource = gameDataTable
        End If
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectNoIntroXmlFile, True, LocalizeMessages.LABEL_SELECT_DAT_FILE)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub ToolStripButtonSelectRomFolder_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSelectRomFolder.Click
        Try
            If ToolStripButtonSelectRomFolder.Enabled = False Then
                Exit Sub
            Else
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, False, LocalizeMessages.LABEL_NOW_LOADING)
            End If

            If _workerRomFolderLoad.CurrentState = WorkerRomFolderLoad.LoadingState.Loading Then
                Exit Sub
            End If

            Dim lastUsedFolder As String = _settingsCurrent.ReadLastUsedFolder("")

            Dim folderBrowserDialog As New FolderBrowserDialog() With {
                .Description = "Select a folder",
                .SelectedPath = If(Directory.Exists(lastUsedFolder), lastUsedFolder, "")
                }
            If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
                ToolStripTextBoxSelectedRomFolderPath.Text = folderBrowserDialog.SelectedPath
                _settingsCurrent.WriteLastUsedFolder(folderBrowserDialog.SelectedPath)
                Try
                    _workerRomFolderLoad.LoadData(folderBrowserDialog.SelectedPath)
                Catch
                    ToolStripTextBoxSelectedRomFolderPath.Text = String.Empty
                End Try
            Else
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, True, LocalizeMessages.LABEL_SELECT_ROM_FOLDER)
                System.Threading.Thread.Sleep(1000)
            End If
        Catch ex As Exception
            Logger.LogMessage($"Error selecting ROM folder: {ex.Message}")
            Logger.ShowErrorMessageBox(LocalizeMessages.MSG_ERROR_SELECTING_ROMFOLDER)
            _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, True, LocalizeMessages.LABEL_SELECT_ROM_FOLDER)
        End Try
    End Sub

    Private Sub WorkerRomFolderLoad_InformationReceived(message As String) Handles _workerRomFolderLoad.InformationReceived
        Logger.LogMessage(message)
    End Sub

    Private Sub WorkerRomFolderLoad_ErrorOccurred(ex As Exception, message As String) Handles _workerRomFolderLoad.ErrorOccurred
        Logger.LogMessage($"Error loading ROM folder: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
    End Sub

    Private Sub WorkerRomFolderLoad_CriticalErrorOccurred(ex As Exception, message As String) Handles _workerRomFolderLoad.CriticalErrorOccurred
        Logger.LogMessage($"Critical error loading ROM folder: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, True, LocalizeMessages.LABEL_SELECT_ROM_FOLDER)
    End Sub

    Private Sub WorkerRomFolderLoad_LoadingCompleted(result As DataTable) Handles _workerRomFolderLoad.LoadingCompleted
        If result IsNot Nothing AndAlso _workerRomFolderLoad.CurrentState = WorkerRomFolderLoad.LoadingState.Completed Then
            DisplayRomFolderLoadingResult(result)
        End If
    End Sub

    Private Sub WorkerRomFolderLoad_LoadingCancelled() Handles _workerRomFolderLoad.LoadingCancelled
        Logger.LogMessage("ROM folder loading cancelled.")
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, True, LocalizeMessages.LABEL_SELECT_ROM_FOLDER)
    End Sub

    Private Sub WorkerRomFolderLoad_ProgressChanged(progress As Integer, message As String) Handles _workerRomFolderLoad.ProgressChanged
        ToolStripProgressBarRomFolderLoadProgress.Value = progress
        ToolStripLabelRomFolderLoadProgressStatus.Text = message
    End Sub

    Private Sub DisplayRomFolderLoadingResult(fileDataTable As DataTable)
        If fileDataTable IsNot Nothing Then
            DataGridViewYourRomsTable.DataSource = fileDataTable
        End If
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonSelectRomFolder, True, LocalizeMessages.LABEL_SELECT_ROM_FOLDER)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub ToolStripButtonStartComparison_Click(sender As Object, e As EventArgs) Handles ToolStripButtonStartComparison.Click
        Try
            If ToolStripButtonStartComparison.Enabled = False Then
                Exit Sub
            Else
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, False, LocalizeMessages.LABEL_NOW_LOADING)
            End If

            If _workerdatFileLoad.CurrentState = WorkerDatFileLoad.LoadingState.Loading OrElse
                    _workerRomFolderLoad.CurrentState = WorkerRomFolderLoad.LoadingState.Loading OrElse
                    _workerComparison.CurrentState = WorkerRomFolderLoad.LoadingState.Loading OrElse
                    _workerdatFileLoad.CurrentState = WorkerDatFileLoad.LoadingState.NotStarted OrElse
                    _workerRomFolderLoad.CurrentState = WorkerRomFolderLoad.LoadingState.NotStarted OrElse
                    DataGridViewNoIntroXmlTable.DataSource Is Nothing OrElse
                    DataGridViewYourRomsTable.DataSource Is Nothing Then

                Logger.LogMessage("Cannot process before loading files.")
                Logger.ShowErrorMessageBox(LocalizeMessages.MSG_ERROR_LOADING_FILES)
                _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
                System.Threading.Thread.Sleep(1000)
                Exit Sub
            End If

            Dim folderFilesTable As DataTable = DirectCast(DataGridViewYourRomsTable.DataSource, DataTable).Copy()
            Dim xmlFilesTable As DataTable = DirectCast(DataGridViewNoIntroXmlTable.DataSource, DataTable).Copy()
            Dim allRegions As New List(Of String)()
            Dim allLanguages As New List(Of String)()
            Dim includeRegions As New List(Of String)()
            Dim includeLanguages As New List(Of String)()
            Dim excludeOtherItems As New List(Of String)()

            allRegions = _utility.GetCheckedListBoxItems(CheckedListBoxRegionFilters)
            includeRegions = _utility.GetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters)

            allLanguages = _utility.GetCheckedListBoxItems(CheckedListBoxLanguageFilters)
            includeLanguages = _utility.GetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters)

            excludeOtherItems = _utility.GetCheckedListBoxUncheckedItems(CheckedListBoxOtherFilters)

            Dim excludeUnknownRegions As Boolean = CheckBoxExcludeUnknownRegionRoms.Checked
            Dim excludeUnknownLanguages As Boolean = CheckBoxExcludeUnknownLanguageRoms.Checked

            DataGridViewAvailableRomsTable.DataSource = Nothing
            DataGridViewMissingRomsTable.DataSource = Nothing

            Dim comparisonParameters As New WorkerComparison.ComparisonParameters(
                folderFilesTable, xmlFilesTable,
                allRegions, allLanguages,
                includeRegions, includeLanguages,
                excludeOtherItems, excludeUnknownRegions, excludeUnknownLanguages
            )
            _workerComparison.StartComparison(comparisonParameters)

            If DataGridViewMissingRomsTable.Columns.Contains("[G]") = False Then
                Dim searchColumn As New DataGridViewButtonColumn With {
                    .Name = "[G]",
                    .UseColumnTextForButtonValue = True,
                    .Text = "[G]"
                }
                DataGridViewMissingRomsTable.Columns.Insert(0, searchColumn)
            End If

            If DataGridViewMissingRomsTable.Columns.Contains("[C]") = False Then
                Dim copyColumn As New DataGridViewButtonColumn With {
                    .Name = "[C]",
                    .UseColumnTextForButtonValue = True,
                    .Text = "[C]"
                }
                DataGridViewMissingRomsTable.Columns.Insert(1, copyColumn)
            End If
        Catch ex As Exception
            Logger.LogMessage($"Error starting comparison: {ex.Message}")
            Logger.ShowErrorMessageBox(LocalizeMessages.MSG_ERROR_COMPARING_FILES)
            _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
        End Try
    End Sub

    Private Sub WorkerComparison_InformationReceived(message As String) Handles _workerComparison.InformationReceived
        Logger.LogMessage(message)
    End Sub

    Private Sub WorkerComparison_ErrorOccurred(ex As Exception, message As String) Handles _workerComparison.ErrorOccurred
        Logger.LogMessage($"Error comparing files: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
    End Sub

    Private Sub WorkerComparison_CriticalErrorOccurred(ex As Exception, message As String) Handles _workerComparison.CriticalErrorOccurred
        Logger.LogMessage($"Critical error comparing files: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
    End Sub

    Private Sub WorkerComparison_ComparisonCompleted(result As WorkerComparison.ComparisonSummary) Handles _workerComparison.ComparisonCompleted
        If result IsNot Nothing AndAlso _workerComparison.CurrentState = WorkerComparison.ComparisonState.Completed Then
            DisplayComparisonResult(result)
        End If
    End Sub

    Private Sub WorkerComparison_ComparisonCancelled() Handles _workerComparison.ComparisonCancelled
        Logger.LogMessage("Comparison cancelled.")
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
    End Sub

    Private Sub WorkerComparison_ProgressUpdated(progress As Integer, message As String) Handles _workerComparison.ProgressUpdated
        ToolStripProgressBarComparisonProgress.Value = progress
        ToolStripLabelComparisonProgressStatus.Text = message
    End Sub

    Private Sub DisplayComparisonResult(result As WorkerComparison.ComparisonSummary)
        DataGridViewAvailableRomsTable.DataSource = result.MatchedFilesTable
        DataGridViewMissingRomsTable.DataSource = result.MissingFilesTable

        DataGridViewAvailableRomsTable.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewAvailableRomsTable.Columns("Size").DefaultCellStyle.Format = "#,0"
        DataGridViewMissingRomsTable.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewMissingRomsTable.Columns("Size").DefaultCellStyle.Format = "#,0"

        ToolStripProgressBarOverallProgress.Maximum = 100
        Dim totalCount As Integer = result.MatchCount + result.MissCount
        If totalCount > 0 Then
            Dim collectionRate As Double = result.MatchCount / totalCount * 100
            ToolStripProgressBarOverallProgress.Value = CInt(collectionRate)
        Else
            ToolStripProgressBarOverallProgress.Value = 0
        End If
        ToolStripStatusLabelOverallStatus.Text = $"Have: {result.MatchCount} Miss: {result.MissCount}"

        Dim targetTabPage As TabPage = TabPageMissingRomsList
        If targetTabPage IsNot Nothing Then
            TabControlMainInterface.SelectedTab = targetTabPage
        End If

        If targetTabPage.Controls.Count > 0 AndAlso TypeOf targetTabPage.Controls(0) Is DataGridView Then
            Dim dgv As DataGridView = DirectCast(targetTabPage.Controls(0), DataGridView)
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        End If

        MessageBox.Show(LocalizeMessages.MSG_INFO_COMPARISON_COMPLETE, LocalizeMessages.TITLE_INFO, MessageBoxButtons.OK, MessageBoxIcon.Information)

        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub ButtonSelectAllComparisonFilters_Click(sender As Object, e As EventArgs) Handles ButtonSelectAllComparisonFilters.Click
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, _utility.GetCheckedListBoxItems(CheckedListBoxRegionFilters))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, _utility.GetCheckedListBoxItems(CheckedListBoxLanguageFilters))
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, _utility.GetCheckedListBoxItems(CheckedListBoxOtherFilters))
        CheckBoxExcludeUnknownRegionRoms.Checked = False
        CheckBoxExcludeUnknownLanguageRoms.Checked = False
    End Sub

    Private Sub ButtonDeselectAllComparisonFilters_Click(sender As Object, e As EventArgs) Handles ButtonDeselectAllComparisonFilters.Click
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, Nothing)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, Nothing)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, Nothing)
        CheckBoxExcludeUnknownRegionRoms.Checked = True
        CheckBoxExcludeUnknownLanguageRoms.Checked = True
    End Sub

    Private Sub ButtonApplyJapaneseComparisonSettings_Click(sender As Object, e As EventArgs) Handles ButtonApplyJapaneseComparisonSettings.Click
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, JapaneseRegions)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, JapaneseLanguages)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, _utility.GetCheckedListBoxItems(CheckedListBoxOtherFilters))
        CheckBoxExcludeUnknownRegionRoms.Checked = True
        CheckBoxExcludeUnknownLanguageRoms.Checked = False
    End Sub

    Private Sub ButtonApplyEnglishComparisonSettings_Click(sender As Object, e As EventArgs) Handles ButtonApplyEnglishComparisonSettings.Click
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters, EnglishRegions)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters, EnglishLanguages)
        _utility.SetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters, _utility.GetCheckedListBoxItems(CheckedListBoxOtherFilters))
        CheckBoxExcludeUnknownRegionRoms.Checked = True
        CheckBoxExcludeUnknownLanguageRoms.Checked = False
    End Sub

    Private ReadOnly Property JapaneseRegions As List(Of String) = New List(Of String) From {"Japan", "World"}
    Private ReadOnly Property JapaneseLanguages As List(Of String) = New List(Of String) From {"Ja"}
    Private ReadOnly Property EnglishRegions As List(Of String) = New List(Of String) From {"World", "USA", "United Kingdom", "Europe"}
    Private ReadOnly Property EnglishLanguages As List(Of String) = New List(Of String) From {"En"}

    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)

        _settingsCurrent.SaveSelectedRegionsToFile(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxRegionFilters))
        _settingsCurrent.SaveSelectedLanguagesToFile(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxLanguageFilters))
        _settingsCurrent.SaveSelectedOthersToFile(_utility.GetCheckedListBoxCheckedItems(CheckedListBoxOtherFilters))
        _settingsCurrent.SaveExcludeUnknownRegionsToFile(CheckBoxExcludeUnknownRegionRoms.Checked)
        _settingsCurrent.SaveExcludeUnknownLanguagesToFile(CheckBoxExcludeUnknownLanguageRoms.Checked)

        _settingsCurrent.WriteWindowSizeTop(Me.Top)
        _settingsCurrent.WriteWindowSizeLeft(Me.Left)
        _settingsCurrent.WriteWindowSizeHeight(Me.Height)
        _settingsCurrent.WriteWindowSizeWidth(Me.Width)

        _settingsCurrent.WriteRegionSplitterDistance(SplitContainerFilterSettingsPanels.SplitterDistance)
        _settingsCurrent.WriteLanguageSplitterDistance(SplitContainerAdditionalFilterSettingsPanels.SplitterDistance)
        _settingsCurrent.WriteOtherSplitterDistance(SplitContainerMiscFilterSettingsPanels.SplitterDistance)
    End Sub

    Private Sub DataGridViewMissingRomsTable_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewMissingRomsTable.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewMissingRomsTable.Rows(e.RowIndex)
            Dim columnName As String = DataGridViewMissingRomsTable.Columns(e.ColumnIndex).Name

            If columnName = "[G]" Then
                Dim searchValue As String = row.Cells("Name").Value.ToString()
                _utility.SetClipboardText(searchValue)
                searchValue = $"""{searchValue}"""
                _workerGoogleSearch.SearchAsync(searchValue)
            ElseIf columnName = "[C]" Then
                Dim clipboardValue As String = row.Cells("Name").Value.ToString()
                _utility.SetClipboardText(clipboardValue)
            End If
        End If
    End Sub

    Private Sub WorkerGoogleSearch_InformationReceived(message As String) Handles _workerGoogleSearch.InformationReceived
        Logger.LogMessage(message)
    End Sub

    Private Sub WorkerGoogleSearch_ErrorOccurred(ex As Exception, message As String) Handles _workerGoogleSearch.ErrorOccurred
        Logger.LogMessage($"Error searching google: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
    End Sub

    Private Sub WorkerGoogleSearch_CriticalErrorOccurred(ex As Exception, message As String) Handles _workerGoogleSearch.CriticalErrorOccurred
        Logger.LogMessage($"Critical error searching google: {ex.Message}")
        Logger.ShowErrorMessageBox(message)
        _utility.SetToolStripButtonEnabledAndText(ToolStripButtonStartComparison, True, LocalizeMessages.LABEL_START_COMPARISON)
    End Sub

    Private Sub AppMainWindow_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("For those who don't want to bother")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("Just use the preset 'All' in the initial state")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("Behavior of region and language filters")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("For example, let's say you have the following ROM file names.")
        Logger.LogMessage(" Game Name (Japan).rom")
        Logger.LogMessage(" Game Name (USA, Europe).rom")
        Logger.LogMessage(" Game Name (Japan, USA).rom")
        Logger.LogMessage(" Game Name.rom")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("Case 1: Uncheck 'Japan'")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage(" Game Name (Japan).rom       - Excluded")
        Logger.LogMessage(" Game Name (USA, Europe).rom - Not excluded")
        Logger.LogMessage(" Game Name (Japan, USA).rom  - Not excluded")
        Logger.LogMessage(" Game Name.rom               - Not excluded")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("Case 2: Uncheck 'Japan' and 'USA'")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage(" Game Name (Japan).rom       - Excluded")
        Logger.LogMessage(" Game Name (USA, Europe).rom - Not excluded")
        Logger.LogMessage(" Game Name (Japan, USA).rom  - Excluded")
        Logger.LogMessage(" Game Name.rom               - Not excluded")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("Case 3: Uncheck all regions")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage(" Game Name (Japan).rom       - Excluded")
        Logger.LogMessage(" Game Name (USA, Europe).rom - Excluded")
        Logger.LogMessage(" Game Name (Japan, USA).rom  - Excluded")
        Logger.LogMessage(" Game Name.rom               - Not excluded")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("Behavior of other filters")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage("For example, let's say you have the following ROM file names.")
        Logger.LogMessage(" Game Name (Japan) [Beta].rom")
        Logger.LogMessage(" Game Name (USA) (Proto).rom")
        Logger.LogMessage(" Game Name (Europe) [Beta, Proto].rom")
        Logger.LogMessage(" Game Name (Japan).rom")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("Case 1: Uncheck 'Beta'")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage(" Game Name (Japan) [Beta].rom          - Excluded")
        Logger.LogMessage(" Game Name (USA) (Proto).rom           - Not excluded")
        Logger.LogMessage(" Game Name (Europe) [Beta, Proto].rom  - Excluded")
        Logger.LogMessage(" Game Name (Japan).rom                 - Not excluded")
        Logger.LogMessage(String.Empty)
        Logger.LogMessage("Case 2: Uncheck 'Beta' and 'Proto'")
        Logger.LogMessage("-------------------------------------------------------------------------")
        Logger.LogMessage(" Game Name (Japan) [Beta].rom          - Excluded")
        Logger.LogMessage(" Game Name (USA) (Proto).rom           - Excluded")
        Logger.LogMessage(" Game Name (Europe) [Beta, Proto].rom  - Excluded")
        Logger.LogMessage(" Game Name (Japan).rom                 - Not excluded")

        CheckBoxEnableDebugLoggingMode.Checked = False
    End Sub

    Private Class Logger
        Public Shared Sub LogMessage(message As String)
            Try
                Dim logMessage As String = $"{Date.Now:yyyy-MM-dd HH:mm:ss} - {message}"
                Debug.WriteLine(logMessage)
                If AppMainWindow.Instance.CheckBoxEnableDebugLoggingMode.Checked Then
                    AppendToConsole(AppMainWindow.Instance.TextBoxConsoleOutputText, logMessage)
                End If
            Catch ex As Exception
                Dim callerInfo As String = GetCallerInfo()
                Debug.WriteLine($"{callerInfo} - Error logging message: {ex.Message}")
            End Try
        End Sub

        Public Shared Sub ShowErrorMessageBox(message As String)
            Try
                LogMessage($"Showing Error MessageBox: {message}")
                MessageBox.Show(message, LocalizeMessages.TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                Dim callerInfo As String = GetCallerInfo()
                Debug.WriteLine($"{callerInfo} - Error showing error message box: {ex.Message}")
            End Try
        End Sub

        Public Shared Sub ShowInfoMessageBox(message As String)
            Try
                LogMessage($"Showing Info MessageBox: {message}")
                MessageBox.Show(message, LocalizeMessages.TITLE_INFO, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                Dim callerInfo As String = GetCallerInfo()
                Debug.WriteLine($"{callerInfo} - Error showing infomation message box: {ex.Message}")
            End Try
        End Sub

        Private Shared Sub AppendToConsole(textbox As TextBox, message As String)
            Try
                SelectConsoleTab()
                If textbox.InvokeRequired Then
                    Dim callerInfo As String = GetCallerInfo()
                    textbox.BeginInvoke(Sub() AppendToConsole(textbox, message))
                Else
                    Dim callerInfo As String = GetCallerInfo()
                    If textbox.TextLength > 0 Then
                        textbox.AppendText(Environment.NewLine)
                    End If
                    textbox.AppendText(message)
                End If
            Catch ex As Exception
                Dim callerInfo As String = GetCallerInfo()
                Debug.WriteLine($"{callerInfo} - Error appending to console: {ex.Message}")
            End Try
        End Sub

        Private Shared Sub SelectConsoleTab()
            Try
                If AppMainWindow.Instance.TabControlMainInterface.InvokeRequired Then
                    Dim callerInfo As String = GetCallerInfo()
                    AppMainWindow.Instance.TabControlMainInterface.BeginInvoke(Sub() SelectConsoleTab())
                Else
                    Dim callerInfo As String = GetCallerInfo()
                    AppMainWindow.Instance.TabControlMainInterface.SelectedTab = AppMainWindow.Instance.TabPageConsoleOutput
                End If
            Catch ex As Exception
                Dim callerInfo As String = GetCallerInfo()
                Debug.WriteLine($"{callerInfo} - Error selecting console tab: {ex.Message}")
            End Try
        End Sub

        Private Shared Function GetCallerInfo() As String
            Dim stackTrace As StackTrace = New StackTrace()
            Dim callingMethodName As String = stackTrace.GetFrame(2).GetMethod().Name
            Dim callingClassName As String = stackTrace.GetFrame(2).GetMethod().DeclaringType.Name

            Return $"[{callingClassName}.{callingMethodName}]"
        End Function
    End Class

    Public Shared ReadOnly Property Instance As AppMainWindow
        Get
            Return CType(Application.OpenForms("AppMainWindow"), AppMainWindow)
        End Get
    End Property

End Class
