Namespace TransferObjects
    Public Class SearchResult

        Private Sub New()
        End Sub

        Public Sub New(ByVal col As Column)
            ColumnName = col.ColumnName
            DataType = col.DataType
            TableName = col.TableName
            TableSchema = col.TableSchema
        End Sub

        Public Property TableSchema As String
        Public Property TableName As String
        Public Property ColumnName As String
        Public Property DataType As String
        Public Property Value As Object
        Public Property Results As DataTable
    End Class
End Namespace
