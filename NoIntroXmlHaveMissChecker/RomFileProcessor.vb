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

Public Class RomFileProcessor
    Public Function CreateDataTable() As DataTable
        Dim fileDataTable As New DataTable()
        fileDataTable.Columns.Add("File Name", GetType(String))
        fileDataTable.Columns.Add("File Size", GetType(Long))
        fileDataTable.Columns.Add("File Type", GetType(String))

        For Each romHeaderType In RomHeader.GetSupportedHeaderTypes()
            Dim prefix As String = RomHeader.GetPrefix(romHeaderType)
            For Each prop In romHeaderType.GetProperties()
                If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" AndAlso prop.Name <> "Extensions" Then
                    fileDataTable.Columns.Add(prefix & prop.Name, GetType(String))
                End If
            Next
        Next

        Return fileDataTable
    End Function

    Public Function ProcessFile(fileName As String, fileSize As Long, fileType As String, fileStream As Stream) As Dictionary(Of String, Object)
        Dim result As New Dictionary(Of String, Object)
        result("File Name") = fileName
        result("File Size") = fileSize
        result("File Type") = fileType

        Dim extension As String = Path.GetExtension(fileName).ToLower()
        Dim romHeader As RomHeader = Nothing
        romHeader = RomHeader.GetRomHeaderFromExtension(extension)

        If romHeader IsNot Nothing Then
            Dim bytesRead As Integer = fileStream.Read(romHeader.HeaderBytes, 0, romHeader.HeaderSize)
            If bytesRead = romHeader.HeaderSize Then
                romHeader.SetHeaderInfo()

                For Each prop In romHeader.GetType().GetProperties()
                    If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" AndAlso prop.Name <> "Extensions" Then
                        Dim columnName As String = romHeader.Prefix & prop.Name
                        Dim columnValue As Object = prop.GetValue(romHeader)
                        result(columnName) = If(columnValue Is Nothing, String.Empty, columnValue.ToString())
                    End If
                Next
            End If
        End If
        Return result
    End Function

    Public Function RemoveUnnecessaryColumns(fileDataTable As DataTable) As DataTable
        ' 必要なカラム名のリストを作成
        Dim requiredColumns As New List(Of String) From {"File Name", "File Size", "File Type"}

        ' ROMヘッダークラスの拡張子を取得
        Dim romExtensions As New HashSet(Of String)
        For Each romHeaderType In RomHeader.GetSupportedHeaderTypes()
            Dim extensions As String() = RomHeader.GetExtensions(romHeaderType)
            For Each extension In extensions
                romExtensions.Add(extension)
            Next
        Next

        For Each row As DataRow In fileDataTable.Rows
            Dim fileName As String = CStr(row("File Name"))
            Dim extension As String = Path.GetExtension(fileName).ToLower()

            If romExtensions.Contains(extension) Then
                Dim romHeaderType As Type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(Function(t) GetType(RomHeader).IsAssignableFrom(t) AndAlso RomHeader.GetExtensions(t).Contains(extension))
                If romHeaderType IsNot Nothing Then
                    Dim prefix As String = RomHeader.GetPrefix(romHeaderType)

                    ' 必要な列をrequiredColumnsに追加
                    For Each prop In romHeaderType.GetProperties()
                        If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" AndAlso prop.Name <> "Extensions" Then
                            requiredColumns.Add(prefix & prop.Name)
                        End If
                    Next
                End If
            End If
        Next

        ' 不要なカラムを削除
        For Each column As DataColumn In fileDataTable.Columns.Cast(Of DataColumn)().ToArray()
            If Not requiredColumns.Contains(column.ColumnName) Then
                fileDataTable.Columns.Remove(column)
            End If
        Next

        Return fileDataTable
    End Function
End Class