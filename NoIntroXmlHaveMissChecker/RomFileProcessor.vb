Imports System.IO
Imports System.Reflection

Public Class RomFileProcessor
    Public Function CreateDataTable() As DataTable
        Dim fileDataTable As New DataTable()
        fileDataTable.Columns.Add("File Name", GetType(String))
        fileDataTable.Columns.Add("File Size", GetType(Long))
        fileDataTable.Columns.Add("File Type", GetType(String))

        ' RomHeaderクラスの派生クラスのプロパティ名にプレフィックスを付けて動的に列を追加
        For Each romHeaderType In {GetType(NesRomHeader), GetType(SnesRomHeader), GetType(N64RomHeader), GetType(GbaRomHeader), GetType(NdsRomHeader), GetType(N3dsRomHeader)}
            Dim prefix As String = CType(Activator.CreateInstance(romHeaderType), RomHeader).Prefix
            For Each prop In romHeaderType.GetProperties()
                ' 特殊なプロパティを除外
                If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" Then
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

        Select Case extension
            Case ".nes"
                romHeader = New NesRomHeader()
            Case ".smc", ".sfc"
                romHeader = New SnesRomHeader()
            Case ".n64", ".v64", ".z64"
                romHeader = New N64RomHeader()
            Case ".gba"
                romHeader = New GbaRomHeader()
            Case ".nds"
                romHeader = New NdsRomHeader()
            Case ".3ds"
                romHeader = New N3dsRomHeader()
        End Select

        If romHeader IsNot Nothing Then
            Dim bytesRead As Integer = fileStream.Read(romHeader.HeaderBytes, 0, romHeader.HeaderSize)
            If bytesRead = romHeader.HeaderSize Then
                romHeader.SetHeaderInfo()

                For Each prop In romHeader.GetType().GetProperties()
                    If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" Then
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

        ' fileDataTable内のファイル名の拡張子を取得
        Dim extensions As New HashSet(Of String)
        For Each row As DataRow In fileDataTable.Rows
            Dim fileName As String = CStr(row("File Name"))
            Dim extension As String = Path.GetExtension(fileName).ToLower()
            extensions.Add(extension)
        Next

        ' 使用されている拡張子に対応するROMヘッダークラスのプレフィックス付きのプロパティ名を必要なカラム名のリストに追加
        For Each romHeaderType In {GetType(NesRomHeader), GetType(SnesRomHeader), GetType(N64RomHeader), GetType(GbaRomHeader), GetType(NdsRomHeader), GetType(N3dsRomHeader)}
            Dim prefix As String = CType(Activator.CreateInstance(romHeaderType), RomHeader).Prefix
            Dim extensionProperty As PropertyInfo = romHeaderType.GetProperty("Extension")
            If extensionProperty IsNot Nothing Then
                Dim extensionValue As String = CStr(extensionProperty.GetValue(Nothing))
                If extensions.Contains(extensionValue) Then
                    For Each prop In romHeaderType.GetProperties()
                        ' 特殊なプロパティを除外
                        If prop.Name <> "HeaderSize" AndAlso prop.Name <> "HeaderBytes" AndAlso prop.Name <> "Prefix" AndAlso prop.Name <> "Extension" Then
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