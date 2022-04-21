Imports System.Collections.ObjectModel
Imports GuidFinder.ClassLibrary

Public Class ResultsViewModel
    Public Property Records As DataTable
    Public Property TableName As String

    Private Sub New()
    End Sub

    Public Sub New(ByVal param As TransferObjects.SearchResult)
        Records = param.Results
        TableName = String.Format("[{0}].[{1}]", param.TableSchema, param.TableName)
    End Sub

End Class
