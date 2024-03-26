' Project: NoIntroXmlHaveMissChecker
' File: FileComparison.vb
'
' Copyright (c) 2024 skt001
'
' This source code is licensed under the MIT license. See LICENSE.txt for details.
'
' Created: 2024-03-25
' Author: skt001
' Description: Standard module containing methods for comparing files and displaying file information.

Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Module FileComparison

    Public Sub CompareFilesInFolderAndXml(formInstance As FormMain, folderFilesView As DataView, xmlFilesView As DataView, matchedFilesTable As DataTable, missingFilesTable As DataTable, ByRef matchCount As Integer, ByRef missCount As Integer)
        Dim totalCount As Integer = xmlFilesView.Count

        formInstance.ToolStripProgressBarMain.ForeColor = Color.Blue
        formInstance.ToolStripProgressBarMain.BackColor = Color.Orange

        For currentIndex As Integer = 0 To totalCount - 1
            Dim fileName As String = Path.GetFileName(xmlFilesView(currentIndex)("File Name").ToString().Trim())
            Dim matchedRows As DataRowView() = folderFilesView.FindRows(New Object() {fileName})

            If matchedRows.Length > 0 Then
                For Each folderRow As DataRowView In matchedRows
                    Dim newRow As DataRow = matchedFilesTable.NewRow()
                    newRow("Name") = xmlFilesView(currentIndex)("Name")
                    newRow("Description") = xmlFilesView(currentIndex)("Description")
                    newRow("File Name") = xmlFilesView(currentIndex)("File Name")
                    newRow("Size") = xmlFilesView(currentIndex)("Size")
                    newRow("CRC") = xmlFilesView(currentIndex)("CRC")
                    newRow("MD5") = xmlFilesView(currentIndex)("MD5")
                    newRow("SHA1") = xmlFilesView(currentIndex)("SHA1")
                    matchedFilesTable.Rows.Add(newRow)
                Next
                matchCount += 1
            Else
                Dim newRow As DataRow = missingFilesTable.NewRow()
                newRow("Name") = xmlFilesView(currentIndex)("Name")
                newRow("Description") = xmlFilesView(currentIndex)("Description")
                newRow("File Name") = xmlFilesView(currentIndex)("File Name")
                newRow("Size") = xmlFilesView(currentIndex)("Size")
                newRow("CRC") = xmlFilesView(currentIndex)("CRC")
                newRow("MD5") = xmlFilesView(currentIndex)("MD5")
                newRow("SHA1") = xmlFilesView(currentIndex)("SHA1")
                missingFilesTable.Rows.Add(newRow)
                missCount += 1
            End If

            formInstance.ToolStripProgressBarMain.Maximum = totalCount
            formInstance.ToolStripProgressBarMain.Value = currentIndex + 1
            formInstance.ToolStripProgressBarCompare.Maximum = totalCount
            formInstance.ToolStripProgressBarCompare.Value = currentIndex + 1
            formInstance.ToolStripLabelCompare.Text = $"{currentIndex + 1} / {totalCount}"
            formInstance.ToolStripStatusLabelMain.Text = $"Have: {matchCount} Miss: {missCount}"

            Application.DoEvents()
        Next
    End Sub

    Public Sub FillGameDataGridView(xmlDoc As XmlDocument, dataGridView As DataGridView, progressBar As ToolStripProgressBar, progressLabel As ToolStripLabel)
        Dim gameDataTable As New DataTable()
        gameDataTable.Columns.Add("Name", GetType(String))
        gameDataTable.Columns.Add("Description", GetType(String))
        gameDataTable.Columns.Add("File Name", GetType(String))
        gameDataTable.Columns.Add("Size", GetType(Long))
        gameDataTable.Columns.Add("CRC", GetType(String))
        gameDataTable.Columns.Add("MD5", GetType(String))
        gameDataTable.Columns.Add("SHA1", GetType(String))

        Dim gameNodes As XmlNodeList = xmlDoc.SelectNodes("//game")

        progressBar.Maximum = gameNodes.Count
        progressBar.Value = 0

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

            progressBar.Value += 1
            progressLabel.Text = $"{progressBar.Value} / {progressBar.Maximum}"
            Application.DoEvents()
        Next

        dataGridView.DataSource = gameDataTable

        dataGridView.Columns("Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dataGridView.Columns("Size").DefaultCellStyle.Format = "#,0"
    End Sub

    Public Sub FillFolderDataGridView(folderPath As String, dataGridView As DataGridView, progressBar As ToolStripProgressBar, progressLabel As ToolStripLabel)
        Dim fileList As New List(Of FileInfo)()

        Try
            Dim directory As New DirectoryInfo(folderPath)
            GetAllFiles(directory, fileList)
        Catch ex As Exception
            ' Log the error message to the Console tab for non-critical errors
            Dim formInstance As FormMain = DirectCast(dataGridView.FindForm(), FormMain)
            formInstance.AppendToConsole($"Error accessing folder: {ex.Message}")
        End Try

        Dim fileDataTable As New DataTable()
        fileDataTable.Columns.Add("File Name", GetType(String))
        fileDataTable.Columns.Add("File Size", GetType(Long))
        fileDataTable.Columns.Add("File Type", GetType(String))

        progressBar.Maximum = fileList.Where(Function(f) f.Extension.ToLower() <> ".zip").Count() + fileList.Where(Function(f) f.Extension.ToLower() = ".zip").Count()
        progressBar.Value = 0

        For Each fileInfo As FileInfo In fileList
            If fileInfo.Extension.ToLower() <> ".zip" Then
                Dim row As DataRow = fileDataTable.NewRow()
                row("File Name") = fileInfo.Name
                row("File Size") = fileInfo.Length
                row("File Type") = "Regular File"
                fileDataTable.Rows.Add(row)
                progressBar.Value += 1
            Else
                AddZipContentsToDataTable(fileInfo.FullName, fileDataTable)
                progressBar.Value += 1
            End If

            progressLabel.Text = $"{progressBar.Value} / {progressBar.Maximum}"
            Application.DoEvents()
        Next

        dataGridView.DataSource = fileDataTable

        dataGridView.Columns("File Size").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dataGridView.Columns("File Size").DefaultCellStyle.Format = "#,0"
    End Sub

    Private Sub GetAllFiles(directory As DirectoryInfo, ByRef fileList As List(Of FileInfo))
        Try
            fileList.AddRange(directory.GetFiles())

            For Each subDirectory As DirectoryInfo In directory.GetDirectories()
                GetAllFiles(subDirectory, fileList)
            Next
        Catch ex As Exception
            ' Log the error or display a message if needed
            ' For now, just ignore the error and continue
        End Try
    End Sub

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
            MessageBox.Show("Error reading the ZIP file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module