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

Imports System.Windows.Forms

Public Class Utility

    Public Sub SetCheckedListBoxItems(checkedListBox As CheckedListBox, items As List(Of String))
        checkedListBox.DataSource = items
    End Sub

    Public Sub SetCheckedListBoxItemsFromCsv(checkedListBox As CheckedListBox, csvItems As String)
        Dim items As String() = csvItems.Split(","c)
        checkedListBox.Items.Clear()
        checkedListBox.Items.AddRange(items)
    End Sub

    Public Overloads Sub SetCheckedListBoxCheckedItems(checkedListBox As CheckedListBox, items As List(Of String))
        If items Is Nothing Then
            items = New List(Of String)()
        End If

        For i As Integer = 0 To checkedListBox.Items.Count - 1
            checkedListBox.SetItemChecked(i, False)
        Next

        Dim itemsToCheck As New List(Of String)()
        For Each item As Object In checkedListBox.Items
            If items.Contains(ExtractValue(item.ToString())) Then
                itemsToCheck.Add(item.ToString())
            End If
        Next

        For i As Integer = 0 To checkedListBox.Items.Count - 1
            Dim itemValue As String = checkedListBox.Items(i).ToString()
            If itemsToCheck.Contains(itemValue) Then
                checkedListBox.SetItemChecked(i, True)
            End If
        Next
    End Sub

    Public Overloads Function GetCheckedListBoxItems(checkedListBox As CheckedListBox) As List(Of String)
        Dim values As New List(Of String)()
        For Each item As Object In checkedListBox.Items
            values.Add(ExtractValue(item))
        Next
        Return values
    End Function

    Public Function GetCheckedListBoxUncheckedItems(checkedListBox As CheckedListBox) As List(Of String)
        Dim items As New List(Of String)()
        For Each item As Object In checkedListBox.Items
            If Not checkedListBox.CheckedItems.Contains(item) Then
                items.Add(item.ToString())
            End If
        Next
        Return items
    End Function

    Public Overloads Function GetCheckedListBoxCheckedItems(checkedListBox As CheckedListBox) As List(Of String)
        Dim checkedItems As CheckedListBox.CheckedItemCollection = checkedListBox.CheckedItems
        Dim items As New List(Of String)()
        For Each item As Object In checkedItems
            items.Add(item.ToString())
        Next
        Dim values As New List(Of String)()
        For Each item As String In items
            values.Add(ExtractValue(item))
        Next
        Return values
    End Function

    Public Sub SetButtonEnabledAndText(button As Button, enabled As Boolean, text As String)
        button.Enabled = enabled
        button.Text = text
    End Sub

    Public Sub SetToolStripButtonEnabledAndText(button As ToolStripButton, enabled As Boolean, text As String)
        button.Enabled = enabled
        button.Text = text
    End Sub

    Public Function ListToCSV(ByVal stringList As List(Of String)) As String
        Dim csvString As String = String.Join(",", stringList)
        Return csvString
    End Function

    Public Function CSVToList(ByVal csvString As String) As List(Of String)
        Dim lines As String() = csvString.Split(Environment.NewLine)
        Dim csvList As New List(Of String)

        For Each line As String In lines
            Dim values As String() = line.Split(",")
            For Each value As String In values
                csvList.Add(value.Trim())
            Next
        Next

        Return csvList
    End Function

    Private Function ExtractValue(itemText As String) As String
        Dim separatorIndex As Integer = InStr(itemText, " - ")
        If separatorIndex > 0 Then
            Dim value As String = Left(itemText, separatorIndex - 1)
            Return value
        Else
            Return itemText
        End If
    End Function

    Public Sub SetClipboardText(text As String)
        If Not String.IsNullOrEmpty(text) Then
            Clipboard.SetText(text)
        End If
    End Sub
End Class
