<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppMainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppMainWindow))
        ToolStripComparisonTools = New ToolStrip()
        ToolStripButtonStartComparison = New ToolStripButton()
        ToolStripProgressBarComparisonProgress = New ToolStripProgressBar()
        ToolStripLabelComparisonProgressStatus = New ToolStripLabel()
        ToolStripRomFolderSelection = New ToolStrip()
        ToolStripButtonSelectRomFolder = New ToolStripButton()
        ToolStripTextBoxSelectedRomFolderPath = New ToolStripTextBox()
        ToolStripProgressBarRomFolderLoadProgress = New ToolStripProgressBar()
        ToolStripLabelRomFolderLoadProgressStatus = New ToolStripLabel()
        ToolStripNoIntroXmlSelection = New ToolStrip()
        ToolStripButtonSelectNoIntroXmlFile = New ToolStripButton()
        ToolStripTextBoxSelectedNoIntroXmlFilePath = New ToolStripTextBox()
        ToolStripProgressBarNoIntroXmlLoadProgress = New ToolStripProgressBar()
        ToolStripLabelNoIntroXmlLoadProgressStatus = New ToolStripLabel()
        StatusStripMainWindow = New StatusStrip()
        ToolStripProgressBarOverallProgress = New ToolStripProgressBar()
        ToolStripStatusLabelOverallStatus = New ToolStripStatusLabel()
        TabPageConsoleOutput = New TabPage()
        TextBoxConsoleOutputText = New TextBox()
        TabPageMissingRomsList = New TabPage()
        DataGridViewMissingRomsTable = New DataGridView()
        TabPageAvailableRomsList = New TabPage()
        DataGridViewAvailableRomsTable = New DataGridView()
        TabPageYourRomsList = New TabPage()
        DataGridViewYourRomsTable = New DataGridView()
        TabPageNoIntroXmlViewer = New TabPage()
        DataGridViewNoIntroXmlTable = New DataGridView()
        TabPageConfigurationSettings = New TabPage()
        GroupBoxComparisonFilterSettings = New GroupBox()
        SplitContainerFilterSettingsPanels = New SplitContainer()
        GroupBoxRegionFilterSettings = New GroupBox()
        CheckedListBoxRegionFilters = New CheckedListBox()
        SplitContainerAdditionalFilterSettingsPanels = New SplitContainer()
        GroupBoxLanguageFilterSettings = New GroupBox()
        CheckedListBoxLanguageFilters = New CheckedListBox()
        SplitContainerMiscFilterSettingsPanels = New SplitContainer()
        GroupBoxOtherFilterSettings = New GroupBox()
        CheckedListBoxOtherFilters = New CheckedListBox()
        GroupBoxSpecialComparisonSettings = New GroupBox()
        ButtonDeselectAllComparisonFilters = New Button()
        ButtonSelectAllComparisonFilters = New Button()
        ButtonApplyEnglishComparisonSettings = New Button()
        ButtonApplyJapaneseComparisonSettings = New Button()
        CheckBoxEnableDebugLoggingMode = New CheckBox()
        CheckBoxExcludeUnknownLanguageRoms = New CheckBox()
        CheckBoxExcludeUnknownRegionRoms = New CheckBox()
        TabControlMainInterface = New TabControl()
        ToolStripComparisonTools.SuspendLayout()
        ToolStripRomFolderSelection.SuspendLayout()
        ToolStripNoIntroXmlSelection.SuspendLayout()
        StatusStripMainWindow.SuspendLayout()
        TabPageConsoleOutput.SuspendLayout()
        TabPageMissingRomsList.SuspendLayout()
        CType(DataGridViewMissingRomsTable, ComponentModel.ISupportInitialize).BeginInit()
        TabPageAvailableRomsList.SuspendLayout()
        CType(DataGridViewAvailableRomsTable, ComponentModel.ISupportInitialize).BeginInit()
        TabPageYourRomsList.SuspendLayout()
        CType(DataGridViewYourRomsTable, ComponentModel.ISupportInitialize).BeginInit()
        TabPageNoIntroXmlViewer.SuspendLayout()
        CType(DataGridViewNoIntroXmlTable, ComponentModel.ISupportInitialize).BeginInit()
        TabPageConfigurationSettings.SuspendLayout()
        GroupBoxComparisonFilterSettings.SuspendLayout()
        CType(SplitContainerFilterSettingsPanels, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerFilterSettingsPanels.Panel1.SuspendLayout()
        SplitContainerFilterSettingsPanels.Panel2.SuspendLayout()
        SplitContainerFilterSettingsPanels.SuspendLayout()
        GroupBoxRegionFilterSettings.SuspendLayout()
        CType(SplitContainerAdditionalFilterSettingsPanels, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerAdditionalFilterSettingsPanels.Panel1.SuspendLayout()
        SplitContainerAdditionalFilterSettingsPanels.Panel2.SuspendLayout()
        SplitContainerAdditionalFilterSettingsPanels.SuspendLayout()
        GroupBoxLanguageFilterSettings.SuspendLayout()
        CType(SplitContainerMiscFilterSettingsPanels, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerMiscFilterSettingsPanels.Panel1.SuspendLayout()
        SplitContainerMiscFilterSettingsPanels.Panel2.SuspendLayout()
        SplitContainerMiscFilterSettingsPanels.SuspendLayout()
        GroupBoxOtherFilterSettings.SuspendLayout()
        GroupBoxSpecialComparisonSettings.SuspendLayout()
        TabControlMainInterface.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStripComparisonTools
        ' 
        ToolStripComparisonTools.Items.AddRange(New ToolStripItem() {ToolStripButtonStartComparison, ToolStripProgressBarComparisonProgress, ToolStripLabelComparisonProgressStatus})
        ToolStripComparisonTools.Location = New Point(0, 50)
        ToolStripComparisonTools.Name = "ToolStripComparisonTools"
        ToolStripComparisonTools.Size = New Size(800, 25)
        ToolStripComparisonTools.TabIndex = 12
        ToolStripComparisonTools.Text = "ToolStrip1"
        ' 
        ' ToolStripButtonStartComparison
        ' 
        ToolStripButtonStartComparison.AutoSize = False
        ToolStripButtonStartComparison.BackColor = Color.GreenYellow
        ToolStripButtonStartComparison.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonStartComparison.ImageTransparentColor = Color.Magenta
        ToolStripButtonStartComparison.Name = "ToolStripButtonStartComparison"
        ToolStripButtonStartComparison.Size = New Size(128, 22)
        ToolStripButtonStartComparison.Text = "Start Comparison"
        ' 
        ' ToolStripProgressBarComparisonProgress
        ' 
        ToolStripProgressBarComparisonProgress.Name = "ToolStripProgressBarComparisonProgress"
        ToolStripProgressBarComparisonProgress.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelComparisonProgressStatus
        ' 
        ToolStripLabelComparisonProgressStatus.Name = "ToolStripLabelComparisonProgressStatus"
        ToolStripLabelComparisonProgressStatus.Size = New Size(24, 22)
        ToolStripLabelComparisonProgressStatus.Tag = ""
        ToolStripLabelComparisonProgressStatus.Text = "0/0"
        ' 
        ' ToolStripRomFolderSelection
        ' 
        ToolStripRomFolderSelection.Items.AddRange(New ToolStripItem() {ToolStripButtonSelectRomFolder, ToolStripTextBoxSelectedRomFolderPath, ToolStripProgressBarRomFolderLoadProgress, ToolStripLabelRomFolderLoadProgressStatus})
        ToolStripRomFolderSelection.Location = New Point(0, 25)
        ToolStripRomFolderSelection.Name = "ToolStripRomFolderSelection"
        ToolStripRomFolderSelection.Size = New Size(800, 25)
        ToolStripRomFolderSelection.TabIndex = 12
        ToolStripRomFolderSelection.Text = "ToolStrip1"
        ' 
        ' ToolStripButtonSelectRomFolder
        ' 
        ToolStripButtonSelectRomFolder.AutoSize = False
        ToolStripButtonSelectRomFolder.BackColor = Color.GreenYellow
        ToolStripButtonSelectRomFolder.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonSelectRomFolder.ImageTransparentColor = Color.Magenta
        ToolStripButtonSelectRomFolder.Name = "ToolStripButtonSelectRomFolder"
        ToolStripButtonSelectRomFolder.Size = New Size(128, 22)
        ToolStripButtonSelectRomFolder.Text = "Select ROM Folder"
        ' 
        ' ToolStripTextBoxSelectedRomFolderPath
        ' 
        ToolStripTextBoxSelectedRomFolderPath.BorderStyle = BorderStyle.FixedSingle
        ToolStripTextBoxSelectedRomFolderPath.Name = "ToolStripTextBoxSelectedRomFolderPath"
        ToolStripTextBoxSelectedRomFolderPath.ReadOnly = True
        ToolStripTextBoxSelectedRomFolderPath.Size = New Size(400, 25)
        ' 
        ' ToolStripProgressBarRomFolderLoadProgress
        ' 
        ToolStripProgressBarRomFolderLoadProgress.Name = "ToolStripProgressBarRomFolderLoadProgress"
        ToolStripProgressBarRomFolderLoadProgress.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelRomFolderLoadProgressStatus
        ' 
        ToolStripLabelRomFolderLoadProgressStatus.Name = "ToolStripLabelRomFolderLoadProgressStatus"
        ToolStripLabelRomFolderLoadProgressStatus.Size = New Size(24, 22)
        ToolStripLabelRomFolderLoadProgressStatus.Text = "0/0"
        ' 
        ' ToolStripNoIntroXmlSelection
        ' 
        ToolStripNoIntroXmlSelection.Items.AddRange(New ToolStripItem() {ToolStripButtonSelectNoIntroXmlFile, ToolStripTextBoxSelectedNoIntroXmlFilePath, ToolStripProgressBarNoIntroXmlLoadProgress, ToolStripLabelNoIntroXmlLoadProgressStatus})
        ToolStripNoIntroXmlSelection.Location = New Point(0, 0)
        ToolStripNoIntroXmlSelection.Name = "ToolStripNoIntroXmlSelection"
        ToolStripNoIntroXmlSelection.Size = New Size(800, 25)
        ToolStripNoIntroXmlSelection.TabIndex = 13
        ToolStripNoIntroXmlSelection.Text = "ToolStrip2"
        ' 
        ' ToolStripButtonSelectNoIntroXmlFile
        ' 
        ToolStripButtonSelectNoIntroXmlFile.AutoSize = False
        ToolStripButtonSelectNoIntroXmlFile.BackColor = Color.GreenYellow
        ToolStripButtonSelectNoIntroXmlFile.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonSelectNoIntroXmlFile.ImageTransparentColor = Color.Magenta
        ToolStripButtonSelectNoIntroXmlFile.Name = "ToolStripButtonSelectNoIntroXmlFile"
        ToolStripButtonSelectNoIntroXmlFile.Size = New Size(128, 22)
        ToolStripButtonSelectNoIntroXmlFile.Text = "Select No-Intro XML"
        ' 
        ' ToolStripTextBoxSelectedNoIntroXmlFilePath
        ' 
        ToolStripTextBoxSelectedNoIntroXmlFilePath.BorderStyle = BorderStyle.FixedSingle
        ToolStripTextBoxSelectedNoIntroXmlFilePath.Name = "ToolStripTextBoxSelectedNoIntroXmlFilePath"
        ToolStripTextBoxSelectedNoIntroXmlFilePath.ReadOnly = True
        ToolStripTextBoxSelectedNoIntroXmlFilePath.Size = New Size(400, 25)
        ' 
        ' ToolStripProgressBarNoIntroXmlLoadProgress
        ' 
        ToolStripProgressBarNoIntroXmlLoadProgress.Name = "ToolStripProgressBarNoIntroXmlLoadProgress"
        ToolStripProgressBarNoIntroXmlLoadProgress.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelNoIntroXmlLoadProgressStatus
        ' 
        ToolStripLabelNoIntroXmlLoadProgressStatus.Name = "ToolStripLabelNoIntroXmlLoadProgressStatus"
        ToolStripLabelNoIntroXmlLoadProgressStatus.Size = New Size(24, 22)
        ToolStripLabelNoIntroXmlLoadProgressStatus.Text = "0/0"
        ' 
        ' StatusStripMainWindow
        ' 
        StatusStripMainWindow.Items.AddRange(New ToolStripItem() {ToolStripProgressBarOverallProgress, ToolStripStatusLabelOverallStatus})
        StatusStripMainWindow.Location = New Point(0, 428)
        StatusStripMainWindow.Name = "StatusStripMainWindow"
        StatusStripMainWindow.Size = New Size(800, 22)
        StatusStripMainWindow.TabIndex = 0
        StatusStripMainWindow.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBarOverallProgress
        ' 
        ToolStripProgressBarOverallProgress.BackColor = Color.Orange
        ToolStripProgressBarOverallProgress.ForeColor = Color.Blue
        ToolStripProgressBarOverallProgress.Name = "ToolStripProgressBarOverallProgress"
        ToolStripProgressBarOverallProgress.Size = New Size(100, 16)
        ' 
        ' ToolStripStatusLabelOverallStatus
        ' 
        ToolStripStatusLabelOverallStatus.Name = "ToolStripStatusLabelOverallStatus"
        ToolStripStatusLabelOverallStatus.Size = New Size(88, 17)
        ToolStripStatusLabelOverallStatus.Text = "Have: 0, Miss: 0"
        ' 
        ' TabPageConsoleOutput
        ' 
        TabPageConsoleOutput.Controls.Add(TextBoxConsoleOutputText)
        TabPageConsoleOutput.Location = New Point(4, 24)
        TabPageConsoleOutput.Name = "TabPageConsoleOutput"
        TabPageConsoleOutput.Padding = New Padding(3)
        TabPageConsoleOutput.Size = New Size(792, 325)
        TabPageConsoleOutput.TabIndex = 4
        TabPageConsoleOutput.Text = "Console"
        TabPageConsoleOutput.UseVisualStyleBackColor = True
        ' 
        ' TextBoxConsoleOutputText
        ' 
        TextBoxConsoleOutputText.Dock = DockStyle.Fill
        TextBoxConsoleOutputText.Location = New Point(3, 3)
        TextBoxConsoleOutputText.Multiline = True
        TextBoxConsoleOutputText.Name = "TextBoxConsoleOutputText"
        TextBoxConsoleOutputText.ScrollBars = ScrollBars.Both
        TextBoxConsoleOutputText.Size = New Size(786, 319)
        TextBoxConsoleOutputText.TabIndex = 0
        ' 
        ' TabPageMissingRomsList
        ' 
        TabPageMissingRomsList.Controls.Add(DataGridViewMissingRomsTable)
        TabPageMissingRomsList.Location = New Point(4, 24)
        TabPageMissingRomsList.Name = "TabPageMissingRomsList"
        TabPageMissingRomsList.Padding = New Padding(3)
        TabPageMissingRomsList.Size = New Size(792, 325)
        TabPageMissingRomsList.TabIndex = 3
        TabPageMissingRomsList.Text = "Miss"
        TabPageMissingRomsList.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewMissingRomsTable
        ' 
        DataGridViewMissingRomsTable.AllowUserToAddRows = False
        DataGridViewMissingRomsTable.AllowUserToDeleteRows = False
        DataGridViewMissingRomsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewMissingRomsTable.Dock = DockStyle.Fill
        DataGridViewMissingRomsTable.Location = New Point(3, 3)
        DataGridViewMissingRomsTable.Name = "DataGridViewMissingRomsTable"
        DataGridViewMissingRomsTable.ReadOnly = True
        DataGridViewMissingRomsTable.Size = New Size(786, 319)
        DataGridViewMissingRomsTable.TabIndex = 0
        ' 
        ' TabPageAvailableRomsList
        ' 
        TabPageAvailableRomsList.Controls.Add(DataGridViewAvailableRomsTable)
        TabPageAvailableRomsList.Location = New Point(4, 24)
        TabPageAvailableRomsList.Name = "TabPageAvailableRomsList"
        TabPageAvailableRomsList.Padding = New Padding(3)
        TabPageAvailableRomsList.Size = New Size(792, 325)
        TabPageAvailableRomsList.TabIndex = 2
        TabPageAvailableRomsList.Text = "Have"
        TabPageAvailableRomsList.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewAvailableRomsTable
        ' 
        DataGridViewAvailableRomsTable.AllowUserToAddRows = False
        DataGridViewAvailableRomsTable.AllowUserToDeleteRows = False
        DataGridViewAvailableRomsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewAvailableRomsTable.Dock = DockStyle.Fill
        DataGridViewAvailableRomsTable.Location = New Point(3, 3)
        DataGridViewAvailableRomsTable.Name = "DataGridViewAvailableRomsTable"
        DataGridViewAvailableRomsTable.ReadOnly = True
        DataGridViewAvailableRomsTable.Size = New Size(786, 319)
        DataGridViewAvailableRomsTable.TabIndex = 0
        ' 
        ' TabPageYourRomsList
        ' 
        TabPageYourRomsList.Controls.Add(DataGridViewYourRomsTable)
        TabPageYourRomsList.Location = New Point(4, 24)
        TabPageYourRomsList.Name = "TabPageYourRomsList"
        TabPageYourRomsList.Padding = New Padding(3)
        TabPageYourRomsList.Size = New Size(792, 325)
        TabPageYourRomsList.TabIndex = 0
        TabPageYourRomsList.Text = "ROMs"
        TabPageYourRomsList.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewYourRomsTable
        ' 
        DataGridViewYourRomsTable.AllowUserToAddRows = False
        DataGridViewYourRomsTable.AllowUserToDeleteRows = False
        DataGridViewYourRomsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewYourRomsTable.Dock = DockStyle.Fill
        DataGridViewYourRomsTable.Location = New Point(3, 3)
        DataGridViewYourRomsTable.Name = "DataGridViewYourRomsTable"
        DataGridViewYourRomsTable.ReadOnly = True
        DataGridViewYourRomsTable.Size = New Size(786, 319)
        DataGridViewYourRomsTable.TabIndex = 0
        ' 
        ' TabPageNoIntroXmlViewer
        ' 
        TabPageNoIntroXmlViewer.Controls.Add(DataGridViewNoIntroXmlTable)
        TabPageNoIntroXmlViewer.Location = New Point(4, 24)
        TabPageNoIntroXmlViewer.Name = "TabPageNoIntroXmlViewer"
        TabPageNoIntroXmlViewer.Padding = New Padding(3)
        TabPageNoIntroXmlViewer.Size = New Size(792, 325)
        TabPageNoIntroXmlViewer.TabIndex = 1
        TabPageNoIntroXmlViewer.Text = "No-Intro"
        TabPageNoIntroXmlViewer.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewNoIntroXmlTable
        ' 
        DataGridViewNoIntroXmlTable.AllowUserToAddRows = False
        DataGridViewNoIntroXmlTable.AllowUserToDeleteRows = False
        DataGridViewNoIntroXmlTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewNoIntroXmlTable.Dock = DockStyle.Fill
        DataGridViewNoIntroXmlTable.Location = New Point(3, 3)
        DataGridViewNoIntroXmlTable.Name = "DataGridViewNoIntroXmlTable"
        DataGridViewNoIntroXmlTable.ReadOnly = True
        DataGridViewNoIntroXmlTable.Size = New Size(786, 319)
        DataGridViewNoIntroXmlTable.TabIndex = 0
        ' 
        ' TabPageConfigurationSettings
        ' 
        TabPageConfigurationSettings.Controls.Add(GroupBoxComparisonFilterSettings)
        TabPageConfigurationSettings.Location = New Point(4, 24)
        TabPageConfigurationSettings.Name = "TabPageConfigurationSettings"
        TabPageConfigurationSettings.Padding = New Padding(3)
        TabPageConfigurationSettings.Size = New Size(792, 325)
        TabPageConfigurationSettings.TabIndex = 5
        TabPageConfigurationSettings.Text = "Config"
        TabPageConfigurationSettings.UseVisualStyleBackColor = True
        ' 
        ' GroupBoxComparisonFilterSettings
        ' 
        GroupBoxComparisonFilterSettings.Controls.Add(SplitContainerFilterSettingsPanels)
        GroupBoxComparisonFilterSettings.Dock = DockStyle.Fill
        GroupBoxComparisonFilterSettings.Location = New Point(3, 3)
        GroupBoxComparisonFilterSettings.Name = "GroupBoxComparisonFilterSettings"
        GroupBoxComparisonFilterSettings.Size = New Size(786, 319)
        GroupBoxComparisonFilterSettings.TabIndex = 0
        GroupBoxComparisonFilterSettings.TabStop = False
        GroupBoxComparisonFilterSettings.Text = "Comparison Filters (Unchecked items will be excluded during comparison)"
        ' 
        ' SplitContainerFilterSettingsPanels
        ' 
        SplitContainerFilterSettingsPanels.Dock = DockStyle.Fill
        SplitContainerFilterSettingsPanels.FixedPanel = FixedPanel.None
        SplitContainerFilterSettingsPanels.IsSplitterFixed = False
        SplitContainerFilterSettingsPanels.Location = New Point(3, 19)
        SplitContainerFilterSettingsPanels.Name = "SplitContainerFilterSettingsPanels"
        ' 
        ' SplitContainerFilterSettingsPanels.Panel1
        ' 
        SplitContainerFilterSettingsPanels.Panel1.Controls.Add(GroupBoxRegionFilterSettings)
        ' 
        ' SplitContainerFilterSettingsPanels.Panel2
        ' 
        SplitContainerFilterSettingsPanels.Panel2.Controls.Add(SplitContainerAdditionalFilterSettingsPanels)
        SplitContainerFilterSettingsPanels.Size = New Size(780, 297)
        SplitContainerFilterSettingsPanels.SplitterDistance = 150
        SplitContainerFilterSettingsPanels.TabIndex = 4
        ' 
        ' GroupBoxRegionFilterSettings
        ' 
        GroupBoxRegionFilterSettings.Controls.Add(CheckedListBoxRegionFilters)
        GroupBoxRegionFilterSettings.Dock = DockStyle.Fill
        GroupBoxRegionFilterSettings.Location = New Point(0, 0)
        GroupBoxRegionFilterSettings.Name = "GroupBoxRegionFilterSettings"
        GroupBoxRegionFilterSettings.Size = New Size(150, 297)
        GroupBoxRegionFilterSettings.TabIndex = 1
        GroupBoxRegionFilterSettings.TabStop = False
        GroupBoxRegionFilterSettings.Text = "Region Filters"
        ' 
        ' CheckedListBoxRegionFilters
        ' 
        CheckedListBoxRegionFilters.Dock = DockStyle.Fill
        CheckedListBoxRegionFilters.FormattingEnabled = True
        CheckedListBoxRegionFilters.Location = New Point(3, 19)
        CheckedListBoxRegionFilters.Name = "CheckedListBoxRegionFilters"
        CheckedListBoxRegionFilters.Size = New Size(144, 275)
        CheckedListBoxRegionFilters.TabIndex = 0
        ' 
        ' SplitContainerAdditionalFilterSettingsPanels
        ' 
        SplitContainerAdditionalFilterSettingsPanels.Dock = DockStyle.Fill
        SplitContainerAdditionalFilterSettingsPanels.FixedPanel = FixedPanel.None
        SplitContainerAdditionalFilterSettingsPanels.IsSplitterFixed = False
        SplitContainerAdditionalFilterSettingsPanels.Location = New Point(0, 0)
        SplitContainerAdditionalFilterSettingsPanels.Name = "SplitContainerAdditionalFilterSettingsPanels"
        ' 
        ' SplitContainerAdditionalFilterSettingsPanels.Panel1
        ' 
        SplitContainerAdditionalFilterSettingsPanels.Panel1.Controls.Add(GroupBoxLanguageFilterSettings)
        ' 
        ' SplitContainerAdditionalFilterSettingsPanels.Panel2
        ' 
        SplitContainerAdditionalFilterSettingsPanels.Panel2.Controls.Add(SplitContainerMiscFilterSettingsPanels)
        SplitContainerAdditionalFilterSettingsPanels.Size = New Size(626, 297)
        SplitContainerAdditionalFilterSettingsPanels.SplitterDistance = 230
        SplitContainerAdditionalFilterSettingsPanels.TabIndex = 4
        ' 
        ' GroupBoxLanguageFilterSettings
        ' 
        GroupBoxLanguageFilterSettings.Controls.Add(CheckedListBoxLanguageFilters)
        GroupBoxLanguageFilterSettings.Dock = DockStyle.Fill
        GroupBoxLanguageFilterSettings.Location = New Point(0, 0)
        GroupBoxLanguageFilterSettings.Name = "GroupBoxLanguageFilterSettings"
        GroupBoxLanguageFilterSettings.Size = New Size(230, 297)
        GroupBoxLanguageFilterSettings.TabIndex = 2
        GroupBoxLanguageFilterSettings.TabStop = False
        GroupBoxLanguageFilterSettings.Text = "Language Filters"
        ' 
        ' CheckedListBoxLanguageFilters
        ' 
        CheckedListBoxLanguageFilters.Dock = DockStyle.Fill
        CheckedListBoxLanguageFilters.FormattingEnabled = True
        CheckedListBoxLanguageFilters.Location = New Point(3, 19)
        CheckedListBoxLanguageFilters.Name = "CheckedListBoxLanguageFilters"
        CheckedListBoxLanguageFilters.Size = New Size(224, 275)
        CheckedListBoxLanguageFilters.TabIndex = 1
        ' 
        ' SplitContainerMiscFilterSettingsPanels
        ' 
        SplitContainerMiscFilterSettingsPanels.Dock = DockStyle.Fill
        SplitContainerMiscFilterSettingsPanels.FixedPanel = FixedPanel.None
        SplitContainerMiscFilterSettingsPanels.IsSplitterFixed = False
        SplitContainerMiscFilterSettingsPanels.Location = New Point(0, 0)
        SplitContainerMiscFilterSettingsPanels.Name = "SplitContainerMiscFilterSettingsPanels"
        ' 
        ' SplitContainerMiscFilterSettingsPanels.Panel1
        ' 
        SplitContainerMiscFilterSettingsPanels.Panel1.Controls.Add(GroupBoxOtherFilterSettings)
        ' 
        ' SplitContainerMiscFilterSettingsPanels.Panel2
        ' 
        SplitContainerMiscFilterSettingsPanels.Panel2.Controls.Add(GroupBoxSpecialComparisonSettings)
        SplitContainerMiscFilterSettingsPanels.Size = New Size(392, 297)
        SplitContainerMiscFilterSettingsPanels.SplitterDistance = 150
        SplitContainerMiscFilterSettingsPanels.TabIndex = 0
        ' 
        ' GroupBoxOtherFilterSettings
        ' 
        GroupBoxOtherFilterSettings.Controls.Add(CheckedListBoxOtherFilters)
        GroupBoxOtherFilterSettings.Dock = DockStyle.Fill
        GroupBoxOtherFilterSettings.Location = New Point(0, 0)
        GroupBoxOtherFilterSettings.Name = "GroupBoxOtherFilterSettings"
        GroupBoxOtherFilterSettings.Size = New Size(150, 297)
        GroupBoxOtherFilterSettings.TabIndex = 3
        GroupBoxOtherFilterSettings.TabStop = False
        GroupBoxOtherFilterSettings.Text = "Other Filters"
        ' 
        ' CheckedListBoxOtherFilters
        ' 
        CheckedListBoxOtherFilters.Dock = DockStyle.Fill
        CheckedListBoxOtherFilters.FormattingEnabled = True
        CheckedListBoxOtherFilters.Location = New Point(3, 19)
        CheckedListBoxOtherFilters.Name = "CheckedListBoxOtherFilters"
        CheckedListBoxOtherFilters.Size = New Size(144, 275)
        CheckedListBoxOtherFilters.TabIndex = 2
        ' 
        ' GroupBoxSpecialComparisonSettings
        ' 
        GroupBoxSpecialComparisonSettings.Controls.Add(ButtonDeselectAllComparisonFilters)
        GroupBoxSpecialComparisonSettings.Controls.Add(ButtonSelectAllComparisonFilters)
        GroupBoxSpecialComparisonSettings.Controls.Add(ButtonApplyEnglishComparisonSettings)
        GroupBoxSpecialComparisonSettings.Controls.Add(ButtonApplyJapaneseComparisonSettings)
        GroupBoxSpecialComparisonSettings.Controls.Add(CheckBoxEnableDebugLoggingMode)
        GroupBoxSpecialComparisonSettings.Controls.Add(CheckBoxExcludeUnknownLanguageRoms)
        GroupBoxSpecialComparisonSettings.Controls.Add(CheckBoxExcludeUnknownRegionRoms)
        GroupBoxSpecialComparisonSettings.Dock = DockStyle.Fill
        GroupBoxSpecialComparisonSettings.FlatStyle = FlatStyle.System
        GroupBoxSpecialComparisonSettings.Location = New Point(0, 0)
        GroupBoxSpecialComparisonSettings.Name = "GroupBoxSpecialComparisonSettings"
        GroupBoxSpecialComparisonSettings.Size = New Size(238, 297)
        GroupBoxSpecialComparisonSettings.TabIndex = 4
        GroupBoxSpecialComparisonSettings.TabStop = False
        GroupBoxSpecialComparisonSettings.Text = "Special"
        ' 
        ' ButtonDeselectAllComparisonFilters
        ' 
        ButtonDeselectAllComparisonFilters.Location = New Point(7, 101)
        ButtonDeselectAllComparisonFilters.Name = "ButtonDeselectAllComparisonFilters"
        ButtonDeselectAllComparisonFilters.Size = New Size(103, 23)
        ButtonDeselectAllComparisonFilters.TabIndex = 6
        ButtonDeselectAllComparisonFilters.Text = "Nothing"
        ButtonDeselectAllComparisonFilters.UseVisualStyleBackColor = True
        ' 
        ' ButtonSelectAllComparisonFilters
        ' 
        ButtonSelectAllComparisonFilters.Location = New Point(7, 72)
        ButtonSelectAllComparisonFilters.Name = "ButtonSelectAllComparisonFilters"
        ButtonSelectAllComparisonFilters.Size = New Size(103, 23)
        ButtonSelectAllComparisonFilters.TabIndex = 5
        ButtonSelectAllComparisonFilters.Text = "All"
        ButtonSelectAllComparisonFilters.UseVisualStyleBackColor = True
        ' 
        ' ButtonApplyEnglishComparisonSettings
        ' 
        ButtonApplyEnglishComparisonSettings.Location = New Point(116, 72)
        ButtonApplyEnglishComparisonSettings.Name = "ButtonApplyEnglishComparisonSettings"
        ButtonApplyEnglishComparisonSettings.Size = New Size(104, 23)
        ButtonApplyEnglishComparisonSettings.TabIndex = 4
        ButtonApplyEnglishComparisonSettings.Text = "English"
        ButtonApplyEnglishComparisonSettings.UseVisualStyleBackColor = True
        ' 
        ' ButtonApplyJapaneseComparisonSettings
        ' 
        ButtonApplyJapaneseComparisonSettings.Location = New Point(116, 101)
        ButtonApplyJapaneseComparisonSettings.Name = "ButtonApplyJapaneseComparisonSettings"
        ButtonApplyJapaneseComparisonSettings.Size = New Size(103, 23)
        ButtonApplyJapaneseComparisonSettings.TabIndex = 3
        ButtonApplyJapaneseComparisonSettings.Text = "Japanese"
        ButtonApplyJapaneseComparisonSettings.UseVisualStyleBackColor = True
        ' 
        ' CheckBoxEnableDebugLoggingMode
        ' 
        CheckBoxEnableDebugLoggingMode.AutoSize = True
        CheckBoxEnableDebugLoggingMode.Location = New Point(6, 130)
        CheckBoxEnableDebugLoggingMode.Name = "CheckBoxEnableDebugLoggingMode"
        CheckBoxEnableDebugLoggingMode.Size = New Size(133, 19)
        CheckBoxEnableDebugLoggingMode.TabIndex = 2
        CheckBoxEnableDebugLoggingMode.Text = "Enable Debug Mode"
        CheckBoxEnableDebugLoggingMode.UseVisualStyleBackColor = True
        ' 
        ' CheckBoxExcludeUnknownLanguageRoms
        ' 
        CheckBoxExcludeUnknownLanguageRoms.AutoSize = True
        CheckBoxExcludeUnknownLanguageRoms.Location = New Point(6, 47)
        CheckBoxExcludeUnknownLanguageRoms.Name = "CheckBoxExcludeUnknownLanguageRoms"
        CheckBoxExcludeUnknownLanguageRoms.Size = New Size(181, 19)
        CheckBoxExcludeUnknownLanguageRoms.TabIndex = 1
        CheckBoxExcludeUnknownLanguageRoms.Text = "Exclude Unknown Languages"
        CheckBoxExcludeUnknownLanguageRoms.UseVisualStyleBackColor = True
        ' 
        ' CheckBoxExcludeUnknownRegionRoms
        ' 
        CheckBoxExcludeUnknownRegionRoms.AutoSize = True
        CheckBoxExcludeUnknownRegionRoms.Location = New Point(6, 22)
        CheckBoxExcludeUnknownRegionRoms.Name = "CheckBoxExcludeUnknownRegionRoms"
        CheckBoxExcludeUnknownRegionRoms.Size = New Size(166, 19)
        CheckBoxExcludeUnknownRegionRoms.TabIndex = 0
        CheckBoxExcludeUnknownRegionRoms.Text = "Exclude Unknown Regions"
        CheckBoxExcludeUnknownRegionRoms.UseVisualStyleBackColor = True
        ' 
        ' TabControlMainInterface
        ' 
        TabControlMainInterface.Controls.Add(TabPageConfigurationSettings)
        TabControlMainInterface.Controls.Add(TabPageNoIntroXmlViewer)
        TabControlMainInterface.Controls.Add(TabPageYourRomsList)
        TabControlMainInterface.Controls.Add(TabPageAvailableRomsList)
        TabControlMainInterface.Controls.Add(TabPageMissingRomsList)
        TabControlMainInterface.Controls.Add(TabPageConsoleOutput)
        TabControlMainInterface.Dock = DockStyle.Fill
        TabControlMainInterface.Location = New Point(0, 75)
        TabControlMainInterface.Name = "TabControlMainInterface"
        TabControlMainInterface.SelectedIndex = 0
        TabControlMainInterface.Size = New Size(800, 353)
        TabControlMainInterface.TabIndex = 1
        ' 
        ' AppMainWindow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(TabControlMainInterface)
        Controls.Add(StatusStripMainWindow)
        Controls.Add(ToolStripComparisonTools)
        Controls.Add(ToolStripRomFolderSelection)
        Controls.Add(ToolStripNoIntroXmlSelection)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "AppMainWindow"
        Text = "No-Intro Xml HaveM iss Checker"
        ToolStripComparisonTools.ResumeLayout(False)
        ToolStripComparisonTools.PerformLayout()
        ToolStripRomFolderSelection.ResumeLayout(False)
        ToolStripRomFolderSelection.PerformLayout()
        ToolStripNoIntroXmlSelection.ResumeLayout(False)
        ToolStripNoIntroXmlSelection.PerformLayout()
        StatusStripMainWindow.ResumeLayout(False)
        StatusStripMainWindow.PerformLayout()
        TabPageConsoleOutput.ResumeLayout(False)
        TabPageConsoleOutput.PerformLayout()
        TabPageMissingRomsList.ResumeLayout(False)
        CType(DataGridViewMissingRomsTable, ComponentModel.ISupportInitialize).EndInit()
        TabPageAvailableRomsList.ResumeLayout(False)
        CType(DataGridViewAvailableRomsTable, ComponentModel.ISupportInitialize).EndInit()
        TabPageYourRomsList.ResumeLayout(False)
        CType(DataGridViewYourRomsTable, ComponentModel.ISupportInitialize).EndInit()
        TabPageNoIntroXmlViewer.ResumeLayout(False)
        CType(DataGridViewNoIntroXmlTable, ComponentModel.ISupportInitialize).EndInit()
        TabPageConfigurationSettings.ResumeLayout(False)
        GroupBoxComparisonFilterSettings.ResumeLayout(False)
        SplitContainerFilterSettingsPanels.Panel1.ResumeLayout(False)
        SplitContainerFilterSettingsPanels.Panel2.ResumeLayout(False)
        CType(SplitContainerFilterSettingsPanels, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerFilterSettingsPanels.ResumeLayout(False)
        GroupBoxRegionFilterSettings.ResumeLayout(False)
        SplitContainerAdditionalFilterSettingsPanels.Panel1.ResumeLayout(False)
        SplitContainerAdditionalFilterSettingsPanels.Panel2.ResumeLayout(False)
        CType(SplitContainerAdditionalFilterSettingsPanels, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerAdditionalFilterSettingsPanels.ResumeLayout(False)
        GroupBoxLanguageFilterSettings.ResumeLayout(False)
        SplitContainerMiscFilterSettingsPanels.Panel1.ResumeLayout(False)
        SplitContainerMiscFilterSettingsPanels.Panel2.ResumeLayout(False)
        CType(SplitContainerMiscFilterSettingsPanels, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerMiscFilterSettingsPanels.ResumeLayout(False)
        GroupBoxOtherFilterSettings.ResumeLayout(False)
        GroupBoxSpecialComparisonSettings.ResumeLayout(False)
        GroupBoxSpecialComparisonSettings.PerformLayout()
        TabControlMainInterface.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ToolStripRomFolderSelection As ToolStrip
    Friend WithEvents ToolStripNoIntroXmlSelection As ToolStrip
    Friend WithEvents StatusStripMainWindow As StatusStrip
    Friend WithEvents ToolStripTextBoxSelectedNoIntroXmlFilePath As ToolStripTextBox
    Friend WithEvents ToolStripButtonSelectNoIntroXmlFile As ToolStripButton
    Friend WithEvents ToolStripTextBoxSelectedRomFolderPath As ToolStripTextBox
    Friend WithEvents ToolStripButtonSelectRomFolder As ToolStripButton
    Friend WithEvents ToolStripProgressBarRomFolderLoadProgress As ToolStripProgressBar
    Friend WithEvents ToolStripLabelRomFolderLoadProgressStatus As ToolStripLabel
    Friend WithEvents ToolStripProgressBarNoIntroXmlLoadProgress As ToolStripProgressBar
    Friend WithEvents ToolStripLabelNoIntroXmlLoadProgressStatus As ToolStripLabel
    Friend WithEvents ToolStripComparisonTools As ToolStrip
    Friend WithEvents ToolStripButtonStartComparison As ToolStripButton
    Friend WithEvents ToolStripProgressBarComparisonProgress As ToolStripProgressBar
    Friend WithEvents ToolStripLabelComparisonProgressStatus As ToolStripLabel
    Friend WithEvents ToolStripStatusLabelOverallStatus As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBarOverallProgress As ToolStripProgressBar
    Friend WithEvents TabPageConsoleOutput As TabPage
    Friend WithEvents TextBoxConsoleOutputText As TextBox
    Friend WithEvents TabPageMissingRomsList As TabPage
    Friend WithEvents DataGridViewMissingRomsTable As DataGridView
    Friend WithEvents TabPageAvailableRomsList As TabPage
    Friend WithEvents DataGridViewAvailableRomsTable As DataGridView
    Friend WithEvents TabPageYourRomsList As TabPage
    Friend WithEvents DataGridViewYourRomsTable As DataGridView
    Friend WithEvents TabPageNoIntroXmlViewer As TabPage
    Friend WithEvents DataGridViewNoIntroXmlTable As DataGridView
    Friend WithEvents TabPageConfigurationSettings As TabPage
    Friend WithEvents GroupBoxComparisonFilterSettings As GroupBox
    Friend WithEvents SplitContainerFilterSettingsPanels As SplitContainer
    Friend WithEvents GroupBoxRegionFilterSettings As GroupBox
    Friend WithEvents CheckedListBoxRegionFilters As CheckedListBox
    Friend WithEvents SplitContainerAdditionalFilterSettingsPanels As SplitContainer
    Friend WithEvents GroupBoxLanguageFilterSettings As GroupBox
    Friend WithEvents CheckedListBoxLanguageFilters As CheckedListBox
    Friend WithEvents SplitContainerMiscFilterSettingsPanels As SplitContainer
    Friend WithEvents GroupBoxOtherFilterSettings As GroupBox
    Friend WithEvents CheckedListBoxOtherFilters As CheckedListBox
    Friend WithEvents GroupBoxSpecialComparisonSettings As GroupBox
    Friend WithEvents CheckBoxEnableDebugLoggingMode As CheckBox
    Friend WithEvents CheckBoxExcludeUnknownLanguageRoms As CheckBox
    Friend WithEvents CheckBoxExcludeUnknownRegionRoms As CheckBox
    Friend WithEvents TabControlMainInterface As TabControl
    Friend WithEvents ButtonApplyEnglishComparisonSettings As Button
    Friend WithEvents ButtonApplyJapaneseComparisonSettings As Button
    Friend WithEvents ButtonDeselectAllComparisonFilters As Button
    Friend WithEvents ButtonSelectAllComparisonFilters As Button

End Class
