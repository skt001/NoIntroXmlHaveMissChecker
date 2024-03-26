<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        ToolStripCompare = New ToolStrip()
        ToolStripButtonCompare = New ToolStripButton()
        ToolStripProgressBarCompare = New ToolStripProgressBar()
        ToolStripLabelCompare = New ToolStripLabel()
        ToolStripRomFolder = New ToolStrip()
        ToolStripButtonFolder = New ToolStripButton()
        ToolStripTextBoxFolder = New ToolStripTextBox()
        ToolStripProgressBarRomFolder = New ToolStripProgressBar()
        ToolStripLabelRomFolder = New ToolStripLabel()
        ToolStripNoIntroXml = New ToolStrip()
        ToolStripButtonXml = New ToolStripButton()
        ToolStripTextBoxXml = New ToolStripTextBox()
        ToolStripProgressBarNoIntroXml = New ToolStripProgressBar()
        ToolStripLabelNoIntroXml = New ToolStripLabel()
        TextBoxUrl = New TextBox()
        LabelUrl = New Label()
        TextBoxAuthor = New TextBox()
        LabelAuthor = New Label()
        TextBoxDate = New TextBox()
        LabelDate = New Label()
        TextBoxVersion = New TextBox()
        LabelVersion = New Label()
        TextBoxDescription = New TextBox()
        LabelDescription = New Label()
        TextBoxName = New TextBox()
        LabelName = New Label()
        TabControlMain = New TabControl()
        TabPageYourRoms = New TabPage()
        DataGridViewFolder = New DataGridView()
        TabPageNoIntroXml = New TabPage()
        DataGridViewXml = New DataGridView()
        TabPageHaveList = New TabPage()
        DataGridViewHave = New DataGridView()
        TabPageMissList = New TabPage()
        DataGridViewMiss = New DataGridView()
        TabPageConsole = New TabPage()
        TextBoxConsole = New TextBox()
        StatusStripMain = New StatusStrip()
        ToolStripProgressBarMain = New ToolStripProgressBar()
        ToolStripStatusLabelMain = New ToolStripStatusLabel()
        GroupBoxHeaderInformation = New GroupBox()
        ToolStripCompare.SuspendLayout()
        ToolStripRomFolder.SuspendLayout()
        ToolStripNoIntroXml.SuspendLayout()
        TabControlMain.SuspendLayout()
        TabPageYourRoms.SuspendLayout()
        CType(DataGridViewFolder, ComponentModel.ISupportInitialize).BeginInit()
        TabPageNoIntroXml.SuspendLayout()
        CType(DataGridViewXml, ComponentModel.ISupportInitialize).BeginInit()
        TabPageHaveList.SuspendLayout()
        CType(DataGridViewHave, ComponentModel.ISupportInitialize).BeginInit()
        TabPageMissList.SuspendLayout()
        CType(DataGridViewMiss, ComponentModel.ISupportInitialize).BeginInit()
        TabPageConsole.SuspendLayout()
        StatusStripMain.SuspendLayout()
        GroupBoxHeaderInformation.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStripCompare
        ' 
        ToolStripCompare.Items.AddRange(New ToolStripItem() {ToolStripButtonCompare, ToolStripProgressBarCompare, ToolStripLabelCompare})
        ToolStripCompare.Location = New Point(0, 50)
        ToolStripCompare.Name = "ToolStripCompare"
        ToolStripCompare.Size = New Size(800, 25)
        ToolStripCompare.TabIndex = 12
        ToolStripCompare.Text = "ToolStrip1"
        ' 
        ' ToolStripButtonCompare
        ' 
        ToolStripButtonCompare.AutoSize = False
        ToolStripButtonCompare.BackColor = Color.GreenYellow
        ToolStripButtonCompare.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonCompare.Image = CType(resources.GetObject("ToolStripButtonCompare.Image"), Image)
        ToolStripButtonCompare.ImageTransparentColor = Color.Magenta
        ToolStripButtonCompare.Name = "ToolStripButtonCompare"
        ToolStripButtonCompare.Size = New Size(100, 22)
        ToolStripButtonCompare.Text = "Start Compere"
        ' 
        ' ToolStripProgressBarCompare
        ' 
        ToolStripProgressBarCompare.Name = "ToolStripProgressBarCompare"
        ToolStripProgressBarCompare.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelCompare
        ' 
        ToolStripLabelCompare.Name = "ToolStripLabelCompare"
        ToolStripLabelCompare.Size = New Size(24, 22)
        ToolStripLabelCompare.Tag = ""
        ToolStripLabelCompare.Text = "0/0"
        ' 
        ' ToolStripRomFolder
        ' 
        ToolStripRomFolder.Items.AddRange(New ToolStripItem() {ToolStripButtonFolder, ToolStripTextBoxFolder, ToolStripProgressBarRomFolder, ToolStripLabelRomFolder})
        ToolStripRomFolder.Location = New Point(0, 25)
        ToolStripRomFolder.Name = "ToolStripRomFolder"
        ToolStripRomFolder.Size = New Size(800, 25)
        ToolStripRomFolder.TabIndex = 12
        ToolStripRomFolder.Text = "ToolStrip1"
        ' 
        ' ToolStripButtonFolder
        ' 
        ToolStripButtonFolder.AutoSize = False
        ToolStripButtonFolder.BackColor = Color.GreenYellow
        ToolStripButtonFolder.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonFolder.Image = CType(resources.GetObject("ToolStripButtonFolder.Image"), Image)
        ToolStripButtonFolder.ImageTransparentColor = Color.Magenta
        ToolStripButtonFolder.Name = "ToolStripButtonFolder"
        ToolStripButtonFolder.Size = New Size(100, 22)
        ToolStripButtonFolder.Text = "Rom Folder"
        ' 
        ' ToolStripTextBoxFolder
        ' 
        ToolStripTextBoxFolder.BorderStyle = BorderStyle.FixedSingle
        ToolStripTextBoxFolder.Name = "ToolStripTextBoxFolder"
        ToolStripTextBoxFolder.ReadOnly = True
        ToolStripTextBoxFolder.Size = New Size(400, 25)
        ' 
        ' ToolStripProgressBarRomFolder
        ' 
        ToolStripProgressBarRomFolder.Name = "ToolStripProgressBarRomFolder"
        ToolStripProgressBarRomFolder.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelRomFolder
        ' 
        ToolStripLabelRomFolder.Name = "ToolStripLabelRomFolder"
        ToolStripLabelRomFolder.Size = New Size(24, 22)
        ToolStripLabelRomFolder.Text = "0/0"
        ' 
        ' ToolStripNoIntroXml
        ' 
        ToolStripNoIntroXml.Items.AddRange(New ToolStripItem() {ToolStripButtonXml, ToolStripTextBoxXml, ToolStripProgressBarNoIntroXml, ToolStripLabelNoIntroXml})
        ToolStripNoIntroXml.Location = New Point(0, 0)
        ToolStripNoIntroXml.Name = "ToolStripNoIntroXml"
        ToolStripNoIntroXml.Size = New Size(800, 25)
        ToolStripNoIntroXml.TabIndex = 13
        ToolStripNoIntroXml.Text = "ToolStrip2"
        ' 
        ' ToolStripButtonXml
        ' 
        ToolStripButtonXml.AutoSize = False
        ToolStripButtonXml.BackColor = Color.GreenYellow
        ToolStripButtonXml.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButtonXml.Image = CType(resources.GetObject("ToolStripButtonXml.Image"), Image)
        ToolStripButtonXml.ImageTransparentColor = Color.Magenta
        ToolStripButtonXml.Name = "ToolStripButtonXml"
        ToolStripButtonXml.Size = New Size(100, 22)
        ToolStripButtonXml.Text = "NoIntro XML"
        ' 
        ' ToolStripTextBoxXml
        ' 
        ToolStripTextBoxXml.BorderStyle = BorderStyle.FixedSingle
        ToolStripTextBoxXml.Name = "ToolStripTextBoxXml"
        ToolStripTextBoxXml.ReadOnly = True
        ToolStripTextBoxXml.Size = New Size(400, 25)
        ' 
        ' ToolStripProgressBarNoIntroXml
        ' 
        ToolStripProgressBarNoIntroXml.Name = "ToolStripProgressBarNoIntroXml"
        ToolStripProgressBarNoIntroXml.Size = New Size(100, 22)
        ' 
        ' ToolStripLabelNoIntroXml
        ' 
        ToolStripLabelNoIntroXml.Name = "ToolStripLabelNoIntroXml"
        ToolStripLabelNoIntroXml.Size = New Size(24, 22)
        ToolStripLabelNoIntroXml.Text = "0/0"
        ' 
        ' TextBoxUrl
        ' 
        TextBoxUrl.Location = New Point(113, 129)
        TextBoxUrl.Name = "TextBoxUrl"
        TextBoxUrl.ReadOnly = True
        TextBoxUrl.Size = New Size(650, 23)
        TextBoxUrl.TabIndex = 11
        ' 
        ' LabelUrl
        ' 
        LabelUrl.Location = New Point(12, 133)
        LabelUrl.Name = "LabelUrl"
        LabelUrl.Size = New Size(100, 15)
        LabelUrl.TabIndex = 10
        LabelUrl.Text = "Url"
        ' 
        ' TextBoxAuthor
        ' 
        TextBoxAuthor.Location = New Point(113, 106)
        TextBoxAuthor.Name = "TextBoxAuthor"
        TextBoxAuthor.ReadOnly = True
        TextBoxAuthor.Size = New Size(650, 23)
        TextBoxAuthor.TabIndex = 9
        ' 
        ' LabelAuthor
        ' 
        LabelAuthor.Location = New Point(12, 110)
        LabelAuthor.Name = "LabelAuthor"
        LabelAuthor.Size = New Size(100, 15)
        LabelAuthor.TabIndex = 8
        LabelAuthor.Text = "Author"
        ' 
        ' TextBoxDate
        ' 
        TextBoxDate.Location = New Point(113, 83)
        TextBoxDate.Name = "TextBoxDate"
        TextBoxDate.ReadOnly = True
        TextBoxDate.Size = New Size(650, 23)
        TextBoxDate.TabIndex = 7
        ' 
        ' LabelDate
        ' 
        LabelDate.Location = New Point(12, 64)
        LabelDate.Name = "LabelDate"
        LabelDate.Size = New Size(100, 15)
        LabelDate.TabIndex = 6
        LabelDate.Text = "Date"
        ' 
        ' TextBoxVersion
        ' 
        TextBoxVersion.Location = New Point(113, 60)
        TextBoxVersion.Name = "TextBoxVersion"
        TextBoxVersion.ReadOnly = True
        TextBoxVersion.Size = New Size(650, 23)
        TextBoxVersion.TabIndex = 5
        ' 
        ' LabelVersion
        ' 
        LabelVersion.Location = New Point(12, 87)
        LabelVersion.Name = "LabelVersion"
        LabelVersion.Size = New Size(100, 15)
        LabelVersion.TabIndex = 4
        LabelVersion.Text = "Version"
        ' 
        ' TextBoxDescription
        ' 
        TextBoxDescription.Location = New Point(113, 37)
        TextBoxDescription.Name = "TextBoxDescription"
        TextBoxDescription.ReadOnly = True
        TextBoxDescription.Size = New Size(650, 23)
        TextBoxDescription.TabIndex = 3
        ' 
        ' LabelDescription
        ' 
        LabelDescription.Location = New Point(12, 41)
        LabelDescription.Name = "LabelDescription"
        LabelDescription.Size = New Size(100, 15)
        LabelDescription.TabIndex = 2
        LabelDescription.Text = "Description"
        ' 
        ' TextBoxName
        ' 
        TextBoxName.Location = New Point(113, 14)
        TextBoxName.Name = "TextBoxName"
        TextBoxName.ReadOnly = True
        TextBoxName.Size = New Size(650, 23)
        TextBoxName.TabIndex = 1
        ' 
        ' LabelName
        ' 
        LabelName.Location = New Point(12, 18)
        LabelName.Name = "LabelName"
        LabelName.Size = New Size(100, 15)
        LabelName.TabIndex = 0
        LabelName.Text = "Name"
        ' 
        ' TabControlMain
        ' 
        TabControlMain.Controls.Add(TabPageYourRoms)
        TabControlMain.Controls.Add(TabPageNoIntroXml)
        TabControlMain.Controls.Add(TabPageHaveList)
        TabControlMain.Controls.Add(TabPageMissList)
        TabControlMain.Controls.Add(TabPageConsole)
        TabControlMain.Dock = DockStyle.Fill
        TabControlMain.Location = New Point(0, 234)
        TabControlMain.Name = "TabControlMain"
        TabControlMain.SelectedIndex = 0
        TabControlMain.Size = New Size(800, 194)
        TabControlMain.TabIndex = 1
        ' 
        ' TabPageYourRoms
        ' 
        TabPageYourRoms.Controls.Add(DataGridViewFolder)
        TabPageYourRoms.Location = New Point(4, 24)
        TabPageYourRoms.Name = "TabPageYourRoms"
        TabPageYourRoms.Padding = New Padding(3)
        TabPageYourRoms.Size = New Size(792, 166)
        TabPageYourRoms.TabIndex = 0
        TabPageYourRoms.Text = "Your Roms"
        TabPageYourRoms.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewFolder
        ' 
        DataGridViewFolder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewFolder.Dock = DockStyle.Fill
        DataGridViewFolder.Location = New Point(3, 3)
        DataGridViewFolder.Name = "DataGridViewFolder"
        DataGridViewFolder.ReadOnly = True
        DataGridViewFolder.Size = New Size(786, 160)
        DataGridViewFolder.TabIndex = 0
        ' 
        ' TabPageNoIntroXml
        ' 
        TabPageNoIntroXml.Controls.Add(DataGridViewXml)
        TabPageNoIntroXml.Location = New Point(4, 24)
        TabPageNoIntroXml.Name = "TabPageNoIntroXml"
        TabPageNoIntroXml.Padding = New Padding(3)
        TabPageNoIntroXml.Size = New Size(792, 166)
        TabPageNoIntroXml.TabIndex = 1
        TabPageNoIntroXml.Text = "NoIntro XML"
        TabPageNoIntroXml.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewXml
        ' 
        DataGridViewXml.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewXml.Dock = DockStyle.Fill
        DataGridViewXml.Location = New Point(3, 3)
        DataGridViewXml.Name = "DataGridViewXml"
        DataGridViewXml.ReadOnly = True
        DataGridViewXml.Size = New Size(786, 160)
        DataGridViewXml.TabIndex = 0
        ' 
        ' TabPageHaveList
        ' 
        TabPageHaveList.Controls.Add(DataGridViewHave)
        TabPageHaveList.Location = New Point(4, 24)
        TabPageHaveList.Name = "TabPageHaveList"
        TabPageHaveList.Padding = New Padding(3)
        TabPageHaveList.Size = New Size(792, 166)
        TabPageHaveList.TabIndex = 2
        TabPageHaveList.Text = "Have List"
        TabPageHaveList.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewHave
        ' 
        DataGridViewHave.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewHave.Dock = DockStyle.Fill
        DataGridViewHave.Location = New Point(3, 3)
        DataGridViewHave.Name = "DataGridViewHave"
        DataGridViewHave.ReadOnly = True
        DataGridViewHave.Size = New Size(786, 160)
        DataGridViewHave.TabIndex = 0
        ' 
        ' TabPageMissList
        ' 
        TabPageMissList.Controls.Add(DataGridViewMiss)
        TabPageMissList.Location = New Point(4, 24)
        TabPageMissList.Name = "TabPageMissList"
        TabPageMissList.Padding = New Padding(3)
        TabPageMissList.Size = New Size(792, 166)
        TabPageMissList.TabIndex = 3
        TabPageMissList.Text = "Miss List"
        TabPageMissList.UseVisualStyleBackColor = True
        ' 
        ' DataGridViewMiss
        ' 
        DataGridViewMiss.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewMiss.Dock = DockStyle.Fill
        DataGridViewMiss.Location = New Point(3, 3)
        DataGridViewMiss.Name = "DataGridViewMiss"
        DataGridViewMiss.ReadOnly = True
        DataGridViewMiss.Size = New Size(786, 160)
        DataGridViewMiss.TabIndex = 0
        ' 
        ' TabPageConsole
        ' 
        TabPageConsole.Controls.Add(TextBoxConsole)
        TabPageConsole.Location = New Point(4, 24)
        TabPageConsole.Name = "TabPageConsole"
        TabPageConsole.Padding = New Padding(3)
        TabPageConsole.Size = New Size(792, 166)
        TabPageConsole.TabIndex = 4
        TabPageConsole.Text = "Console"
        TabPageConsole.UseVisualStyleBackColor = True
        ' 
        ' TextBoxConsole
        ' 
        TextBoxConsole.Dock = DockStyle.Fill
        TextBoxConsole.Location = New Point(3, 3)
        TextBoxConsole.Multiline = True
        TextBoxConsole.Name = "TextBoxConsole"
        TextBoxConsole.ReadOnly = True
        TextBoxConsole.Size = New Size(786, 160)
        TextBoxConsole.TabIndex = 0
        ' 
        ' StatusStripMain
        ' 
        StatusStripMain.Items.AddRange(New ToolStripItem() {ToolStripProgressBarMain, ToolStripStatusLabelMain})
        StatusStripMain.Location = New Point(0, 428)
        StatusStripMain.Name = "StatusStripMain"
        StatusStripMain.Size = New Size(800, 22)
        StatusStripMain.TabIndex = 0
        StatusStripMain.Text = "StatusStrip1"
        ' 
        ' ToolStripProgressBarMain
        ' 
        ToolStripProgressBarMain.BackColor = Color.Orange
        ToolStripProgressBarMain.ForeColor = Color.Blue
        ToolStripProgressBarMain.Name = "ToolStripProgressBarMain"
        ToolStripProgressBarMain.Size = New Size(100, 16)
        ' 
        ' ToolStripStatusLabelMain
        ' 
        ToolStripStatusLabelMain.Name = "ToolStripStatusLabelMain"
        ToolStripStatusLabelMain.Size = New Size(79, 17)
        ToolStripStatusLabelMain.Text = "Have:0 Miss:0"
        ' 
        ' GroupBoxHeaderInformation
        ' 
        GroupBoxHeaderInformation.Controls.Add(TextBoxUrl)
        GroupBoxHeaderInformation.Controls.Add(LabelUrl)
        GroupBoxHeaderInformation.Controls.Add(TextBoxAuthor)
        GroupBoxHeaderInformation.Controls.Add(LabelAuthor)
        GroupBoxHeaderInformation.Controls.Add(TextBoxDate)
        GroupBoxHeaderInformation.Controls.Add(LabelDate)
        GroupBoxHeaderInformation.Controls.Add(TextBoxVersion)
        GroupBoxHeaderInformation.Controls.Add(LabelVersion)
        GroupBoxHeaderInformation.Controls.Add(TextBoxDescription)
        GroupBoxHeaderInformation.Controls.Add(LabelDescription)
        GroupBoxHeaderInformation.Controls.Add(TextBoxName)
        GroupBoxHeaderInformation.Controls.Add(LabelName)
        GroupBoxHeaderInformation.Dock = DockStyle.Top
        GroupBoxHeaderInformation.Location = New Point(0, 75)
        GroupBoxHeaderInformation.Name = "GroupBoxHeaderInformation"
        GroupBoxHeaderInformation.Size = New Size(800, 159)
        GroupBoxHeaderInformation.TabIndex = 12
        GroupBoxHeaderInformation.TabStop = False
        GroupBoxHeaderInformation.Text = "Header Infomation"
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(TabControlMain)
        Controls.Add(StatusStripMain)
        Controls.Add(GroupBoxHeaderInformation)
        Controls.Add(ToolStripCompare)
        Controls.Add(ToolStripRomFolder)
        Controls.Add(ToolStripNoIntroXml)
        Name = "FormMain"
        Text = "No-Intro Xml HaveM iss Checker"
        ToolStripCompare.ResumeLayout(False)
        ToolStripCompare.PerformLayout()
        ToolStripRomFolder.ResumeLayout(False)
        ToolStripRomFolder.PerformLayout()
        ToolStripNoIntroXml.ResumeLayout(False)
        ToolStripNoIntroXml.PerformLayout()
        TabControlMain.ResumeLayout(False)
        TabPageYourRoms.ResumeLayout(False)
        CType(DataGridViewFolder, ComponentModel.ISupportInitialize).EndInit()
        TabPageNoIntroXml.ResumeLayout(False)
        CType(DataGridViewXml, ComponentModel.ISupportInitialize).EndInit()
        TabPageHaveList.ResumeLayout(False)
        CType(DataGridViewHave, ComponentModel.ISupportInitialize).EndInit()
        TabPageMissList.ResumeLayout(False)
        CType(DataGridViewMiss, ComponentModel.ISupportInitialize).EndInit()
        TabPageConsole.ResumeLayout(False)
        TabPageConsole.PerformLayout()
        StatusStripMain.ResumeLayout(False)
        StatusStripMain.PerformLayout()
        GroupBoxHeaderInformation.ResumeLayout(False)
        GroupBoxHeaderInformation.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents LabelName As Label
    Friend WithEvents TextBoxName As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents LabelUrl As Label
    Friend WithEvents TextBoxAuthor As TextBox
    Friend WithEvents LabelAuthor As Label
    Friend WithEvents TextBoxDate As TextBox
    Friend WithEvents LabelDate As Label
    Friend WithEvents TextBoxVersion As TextBox
    Friend WithEvents LabelVersion As Label
    Friend WithEvents TextBoxDescription As TextBox
    Friend WithEvents LabelDescription As Label
    Friend WithEvents ToolStripRomFolder As ToolStrip
    Friend WithEvents ToolStripNoIntroXml As ToolStrip
    Friend WithEvents StatusStripMain As StatusStrip
    Friend WithEvents TabControlMain As TabControl
    Friend WithEvents TabPageYourRoms As TabPage
    Friend WithEvents DataGridViewFolder As DataGridView
    Friend WithEvents TabPageNoIntroXml As TabPage
    Friend WithEvents DataGridViewXml As DataGridView
    Friend WithEvents ToolStripTextBoxXml As ToolStripTextBox
    Friend WithEvents ToolStripButtonXml As ToolStripButton
    Friend WithEvents ToolStripTextBoxFolder As ToolStripTextBox
    Friend WithEvents ToolStripButtonFolder As ToolStripButton
    Friend WithEvents ToolStripProgressBarRomFolder As ToolStripProgressBar
    Friend WithEvents ToolStripLabelRomFolder As ToolStripLabel
    Friend WithEvents ToolStripProgressBarNoIntroXml As ToolStripProgressBar
    Friend WithEvents ToolStripLabelNoIntroXml As ToolStripLabel
    Friend WithEvents TabPageHaveList As TabPage
    Friend WithEvents DataGridViewHave As DataGridView
    Friend WithEvents TabPageMissList As TabPage
    Friend WithEvents DataGridViewMiss As DataGridView
    Friend WithEvents ToolStripCompare As ToolStrip
    Friend WithEvents ToolStripButtonCompare As ToolStripButton
    Friend WithEvents ToolStripProgressBarCompare As ToolStripProgressBar
    Friend WithEvents ToolStripLabelCompare As ToolStripLabel
    Friend WithEvents ToolStripStatusLabelMain As ToolStripStatusLabel
    Friend WithEvents GroupBoxHeaderInformation As GroupBox
    Friend WithEvents ToolStripProgressBarMain As ToolStripProgressBar
    Friend WithEvents TabPageConsole As TabPage
    Friend WithEvents TextBoxConsole As TextBox

End Class
