' Project: NoIntroXmlHaveMissChecker
' File: MainForm.vb
'
' Copyright (c) 2024 skt001
'
' This source code is licensed under the MIT license. See LICENSE.txt for details.
'
' Created: 2024-03-24
' Author: skt001
' Description: Main form class providing functionality to compare and validate files in a folder against an XML (DAT) file.

Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Public Class FormMain
    ' Setting the ForeColor and BackColor of ToolStripProgressBarMain does not reflect the expected behavior.
    ' The progress bar does not display the "Have" portion in blue and the "Miss" portion in orange.
    ' For now, we will comment out these lines and revisit this issue later.

    ' ToolStripProgressBarMain の ForeColor と BackColor を設定しても、期待した動作が反映されません。
    ' プログレスバーは、"Have" の部分を青で、"Miss" の部分をオレンジで表示しません。
    ' 現時点では、これらの行をコメントアウトし、後で再検討することにします。
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add any necessary initialization code here

        ' Hide the Console tab on form load
        TabPageConsole.Parent = Nothing

        ' Set the ForeColor and BackColor of ToolStripProgressBarMain
        'ToolStripProgressBarMain.ForeColor = Color.Blue ' "Have" portion color
        'ToolStripProgressBarMain.BackColor = Color.Orange ' "Miss" portion color

        ' Hide the Console tab on form load
        TabPageConsole.Parent = Nothing
    End Sub

    Private Sub ToolStripButtonXml_Click(sender As Object, e As EventArgs) Handles ToolStripButtonXml.Click
        LoadDatFile()
    End Sub

    Private Sub ToolStripButtonFolder_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFolder.Click
        LoadFolderFiles()
    End Sub

    Private Sub ToolStripButtonCompare_Click(sender As Object, e As EventArgs) Handles ToolStripButtonCompare.Click
        If DataGridViewFolder.DataSource Is Nothing OrElse DataGridViewXml.DataSource Is Nothing Then
            MessageBox.Show("Please load both DAT file and folder before comparing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        CompareFiles()
    End Sub

    Private Sub LoadDatFile()
        Dim openFileDialog As New OpenFileDialog() With {
        .Filter = "DAT Files (*.dat)|*.dat|All Files (*.*)|*.*",
        .Title = "Select a DAT file"
    }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ToolStripButtonXml.Enabled = False

            Dim datFilePath As String = openFileDialog.FileName
            ToolStripTextBoxXml.Text = datFilePath

            Try
                ' Load and process the DAT file
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(datFilePath)

                DisplayHeaderInfo(xmlDoc)
                DisplayGameData(xmlDoc)
            Catch ex As Exception
                ' Display an error dialog for critical errors during file loading
                MessageBox.Show($"Error loading DAT file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ToolStripButtonXml.Enabled = True
            End Try
        End If
    End Sub

    Private Sub LoadFolderFiles()
        Dim folderBrowserDialog As New FolderBrowserDialog() With {
        .Description = "Select a folder"
    }

        If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
            ToolStripButtonFolder.Enabled = False

            Dim folderPath As String = folderBrowserDialog.SelectedPath
            ToolStripTextBoxFolder.Text = folderPath

            Try
                ' Load and process the folder files
                DisplayFolderFiles(folderPath)
            Catch ex As Exception
                ' Display an error dialog for critical errors during folder loading
                MessageBox.Show($"Error loading folder files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ToolStripButtonFolder.Enabled = True
            End Try
        End If
    End Sub

    Private Sub DisplayHeaderInfo(xmlDoc As XmlDocument)
        Dim headerNode As XmlNode = xmlDoc.SelectSingleNode("//header")

        If headerNode Is Nothing Then
            MessageBox.Show("Header information not found in the XML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        TextBoxName.Text = If(headerNode.SelectSingleNode("name") IsNot Nothing, headerNode.SelectSingleNode("name").InnerText, "")
        TextBoxDescription.Text = If(headerNode.SelectSingleNode("description") IsNot Nothing, headerNode.SelectSingleNode("description").InnerText, "")
        TextBoxVersion.Text = If(headerNode.SelectSingleNode("version") IsNot Nothing, headerNode.SelectSingleNode("version").InnerText, "")
        TextBoxDate.Text = If(headerNode.SelectSingleNode("date") IsNot Nothing, headerNode.SelectSingleNode("date").InnerText, "")
        TextBoxAuthor.Text = If(headerNode.SelectSingleNode("author") IsNot Nothing, headerNode.SelectSingleNode("author").InnerText, "")
        TextBoxUrl.Text = If(headerNode.SelectSingleNode("url") IsNot Nothing, headerNode.SelectSingleNode("url").InnerText, "")
    End Sub

    Private Sub DisplayGameData(xmlDoc As XmlDocument)
        Dim gameDataTable As New DataTable()
        gameDataTable.Columns.Add("Name", GetType(String))
        gameDataTable.Columns.Add("Description", GetType(String))
        gameDataTable.Columns.Add("File Name", GetType(String))
        gameDataTable.Columns.Add("Size", GetType(Long))
        gameDataTable.Columns.Add("CRC", GetType(String))
        gameDataTable.Columns.Add("MD5", GetType(String))
        gameDataTable.Columns.Add("SHA1", GetType(String))

        Dim gameNodes As XmlNodeList = xmlDoc.SelectNodes("//game")

        ToolStripProgressBarNoIntroXml.Maximum = gameNodes.Count
        ToolStripProgressBarNoIntroXml.Value = 0

        For Each gameNode As XmlNode In gameNodes
            Dim romNode As XmlNode = gameNode.SelectSingleNode("rom")

            If romNode Is Nothing Then
                MessageBox.Show("File name (rom element) not found for a game entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim row As DataRow = gameDataTable.NewRow()
            row("Name") = If(gameNode.Attributes("name") IsNot Nothing, gameNode.Attributes("name").Value, "")
            row("Description") = If(gameNode.SelectSingleNode("description") IsNot Nothing, gameNode.SelectSingleNode("description").InnerText, "")
            row("File Name") = romNode.Attributes("name").Value
            row("Size") = If(romNode.Attributes("size") IsNot Nothing, CLng(romNode.Attributes("size").Value), 0)
            row("CRC") = If(romNode.Attributes("crc") IsNot Nothing, romNode.Attributes("crc").Value, "")
            row("MD5") = If(romNode.Attributes("md5") IsNot Nothing, romNode.Attributes("md5").Value, "")
            row("SHA1") = If(romNode.Attributes("sha1") IsNot Nothing, romNode.Attributes("sha1").Value, "")
            gameDataTable.Rows.Add(row)

            ToolStripProgressBarNoIntroXml.Value += 1
            ToolStripLabelNoIntroXml.Text = $"{ToolStripProgressBarNoIntroXml.Value} / {ToolStripProgressBarNoIntroXml.Maximum}"
            Application.DoEvents() ' Update UI
        Next

        DataGridViewXml.DataSource = gameDataTable

        ' Set the file size column to right-align with thousands separator
        DataGridViewXml.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewXml.Columns("Size").DefaultCellStyle.Format = "#,0"
    End Sub

    Private Sub DisplayFolderFiles(folderPath As String)
        Dim fileList As New List(Of FileInfo)()

        Try
            ' Get all files in the specified folder and its subfolders
            Dim directory As New DirectoryInfo(folderPath)
            GetAllFiles(directory, fileList)
        Catch ex As Exception
            MessageBox.Show("Error accessing the folder: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim fileDataTable As New DataTable()
        fileDataTable.Columns.Add("File Name", GetType(String))
        fileDataTable.Columns.Add("File Size", GetType(Long))
        fileDataTable.Columns.Add("File Type", GetType(String))

        ' Set the count of ToolStripProgressBarRomFolder to the total number of zip files and uncompressed files
        ToolStripProgressBarRomFolder.Maximum = fileList.Where(Function(f) f.Extension.ToLower() <> ".zip").Count() + fileList.Where(Function(f) f.Extension.ToLower() = ".zip").Count()
        ToolStripProgressBarRomFolder.Value = 0

        For Each fileInfo As FileInfo In fileList
            If fileInfo.Extension.ToLower() <> ".zip" Then
                Dim row As DataRow = fileDataTable.NewRow()
                row("File Name") = fileInfo.Name ' Use only the file name
                row("File Size") = fileInfo.Length ' Display file size in bytes
                row("File Type") = "Regular File"
                fileDataTable.Rows.Add(row)
                ToolStripProgressBarRomFolder.Value += 1
            Else
                ' Don't count entries in the zip file, only count the zip file itself as one item
                DisplayZipContents(fileInfo.FullName, fileDataTable)
                ToolStripProgressBarRomFolder.Value += 1
            End If

            ToolStripLabelRomFolder.Text = $"{ToolStripProgressBarRomFolder.Value} / {ToolStripProgressBarRomFolder.Maximum}"
            Application.DoEvents() ' Update UI
        Next

        DataGridViewFolder.DataSource = fileDataTable

        ' Set the file size column to right-align with thousands separator
        DataGridViewFolder.Columns("File Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewFolder.Columns("File Size").DefaultCellStyle.Format = "#,0"
    End Sub

    Private Sub GetAllFiles(directory As DirectoryInfo, ByRef fileList As List(Of FileInfo))
        Try
            ' Add all files in the current directory to the list
            fileList.AddRange(directory.GetFiles())

            ' Recursively call the method for each subdirectory
            For Each subDirectory As DirectoryInfo In directory.GetDirectories()
                GetAllFiles(subDirectory, fileList)
            Next
        Catch ex As Exception
            ' Ignore any errors that occur when accessing files or folders
            ' You can log the error or display a message if needed
        End Try
    End Sub

    Private Sub DisplayZipContents(zipFilePath As String, fileDataTable As DataTable)
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(zipFilePath)
                For Each entry As ZipArchiveEntry In archive.Entries
                    Dim row As DataRow = fileDataTable.NewRow()
                    row("File Name") = entry.FullName ' Remove extra spaces
                    row("File Size") = entry.Length ' Display file size in bytes
                    row("File Type") = "ZIP Content"
                    fileDataTable.Rows.Add(row)

                    ' Don't count entries in the zip file
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error reading the ZIP file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 【画面更新について】
    ' このプログラムでは、処理中の画面更新にApplication.DoEvents()を使用しています。
    ' これは、処理中もUIの応答性を維持するためです。
    ' バックグラウンドスレッドやInvoke/BeginInvokeを使用する方法もありますが、
    ' このプログラムではあえてApplication.DoEvents()を使用しています。
    ' 理由は、プログラムの簡潔さと、過去のプログラミング経験に基づく判断からです。
    ' ただし、Application.DoEvents()の使用には注意が必要です。
    ' 長時間の処理中にApplication.DoEvents()を呼び出すと、予期しない動作が発生する可能性があります。
    ' 将来的に、バックグラウンドスレッドを使用する方法に切り替えることも検討しています。

    ' [Regarding screen updates]
    ' In this program, Application.DoEvents() is used for screen updates during processing.
    ' This is to maintain UI responsiveness while the processing is ongoing.
    ' Alternative methods, such as using background threads or Invoke/BeginInvoke, are available,
    ' but this program intentionally uses Application.DoEvents().
    ' The reason is based on the simplicity of the program and judgment from past programming experiences.
    ' However, caution is necessary when using Application.DoEvents().
    ' Calling Application.DoEvents() during long-running processes may lead to unexpected behavior.
    ' In the future, switching to a method using background threads is also being considered.

    ' The ProgressBarStyle does not have a paused mode. ProgressBarStyle.Pause does not exist.
    ' Both conditions in the if statement set the style to ProgressBarStyle.Continuous, making the if statement meaningless.
    ' However, we will keep the if statement for now.

    ' ProgressBarStyle には、一時停止モードがありません。ProgressBarStyle.Pause は存在しません。
    ' if 文の両方の条件で、スタイルを ProgressBarStyle.Continuous に設定しているため、if 文は意味がありません。
    ' ただし、現時点では if 文を残しておきます。

    Private Sub CompareFiles()
        If DataGridViewFolder.DataSource Is Nothing OrElse DataGridViewXml.DataSource Is Nothing Then
            MessageBox.Show("Please load both DAT file and folder before comparing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ToolStripStatusLabelMain.Text = "Sorting folder files..."
        Dim folderFilesView As DataView = DirectCast(DataGridViewFolder.DataSource, DataTable).DefaultView
        folderFilesView.Sort = "File Name"

        ToolStripStatusLabelMain.Text = "Sorting XML files..."
        Dim xmlFilesView As DataView = DirectCast(DataGridViewXml.DataSource, DataTable).DefaultView
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

        ToolStripProgressBarCompare.Maximum = xmlFilesView.Count
        ToolStripProgressBarCompare.Value = 0

        Dim matchCount As Integer = 0
        Dim missCount As Integer = 0

        ToolStripStatusLabelMain.Text = "Comparing files..."

        FileComparison.CompareFilesInFolderAndXml(Me, folderFilesView, xmlFilesView, matchedFilesTable, missingFilesTable, matchCount, missCount)

        DataGridViewHave.DataSource = matchedFilesTable
        DataGridViewMiss.DataSource = missingFilesTable

        DataGridViewHave.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewHave.Columns("Size").DefaultCellStyle.Format = "#,0"
        DataGridViewMiss.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewMiss.Columns("Size").DefaultCellStyle.Format = "#,0"

        If xmlFilesView.Count > 0 Then
            Dim havePercentage As Double = (matchCount / xmlFilesView.Count) * 100
            ToolStripProgressBarMain.Maximum = 100
            ToolStripProgressBarMain.Value = CInt(havePercentage)

            If havePercentage < 100 Then
                ToolStripProgressBarMain.Style = ProgressBarStyle.Continuous
            Else
                ToolStripProgressBarMain.Style = ProgressBarStyle.Continuous
            End If
        Else
            ToolStripProgressBarMain.Value = 0
            ToolStripProgressBarMain.Maximum = 100
        End If

        ToolStripStatusLabelMain.Text = $"Have: {matchCount} Miss: {missCount}"

        MessageBox.Show($"Comparison complete.{Environment.NewLine}Have: {matchCount}{Environment.NewLine}Miss: {missCount}", "Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DataGridViewMiss_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewMiss.CellContentClick
        ToolStripStatusLabelMain.Text = "Processing cell click..."
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridViewMiss.Rows(e.RowIndex)
            Dim columnName As String = DataGridViewMiss.Columns(e.ColumnIndex).Name

            If {"Name", "File Name", "CRC", "MD5", "SHA1"}.Contains(columnName) Then
                Dim cell As DataGridViewCell = row.Cells(columnName)
                If cell IsNot Nothing AndAlso cell.Value IsNot Nothing Then
                    ToolStripStatusLabelMain.Text = "Searching for value..."
                    Dim backgroundWorker As New System.ComponentModel.BackgroundWorker()
                    AddHandler backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
                    backgroundWorker.RunWorkerAsync(cell.Value.ToString())
                End If
            End If
        End If
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        Dim value As String = e.Argument.ToString()
        SearchValue(value)
    End Sub

    Private Sub SearchValue(value As String)
        Dim searchUrl As String = $"https://www.google.com/search?q={Uri.EscapeDataString(value)}"

        Try
            Process.Start(New ProcessStartInfo() With {
        .FileName = searchUrl,
        .UseShellExecute = True
    })
            ToolStripStatusLabelMain.Text = "Search completed."
        Catch ex As Exception
            MessageBox.Show($"An error occurred while opening the URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ToolStripStatusLabelMain.Text = "Error occurred during search."
        End Try
    End Sub

    Private Sub TabControlMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControlMain.SelectedIndexChanged
        If TabControlMain.SelectedTab Is TabPageMissList Then
            ' Automatically adjust column widths
            DataGridViewMiss.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

            ' Convert "Name", "File Name", "CRC", "MD5", "SHA1" cells to button cells
            For Each row As DataGridViewRow In DataGridViewMiss.Rows
                For Each columnName As String In {"Name", "File Name", "CRC", "MD5", "SHA1"}
                    Dim cell As DataGridViewCell = row.Cells(columnName)
                    If cell.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(cell.Value.ToString()) Then
                        Dim buttonCell As New DataGridViewButtonCell()
                        buttonCell.Value = cell.Value
                        row.Cells(columnName) = buttonCell
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub AppendToConsole(message As String)
        ' Append the error message to the Console tab
        TextBoxConsole.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}")

        ' Display the Console tab if it's not already visible
        If TabControlMain.SelectedTab IsNot TabPageConsole Then
            TabControlMain.SelectedTab = TabPageConsole
        End If
    End Sub
End Class
