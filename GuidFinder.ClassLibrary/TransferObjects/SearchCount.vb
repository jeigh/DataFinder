Namespace TransferObjects
    Public Class SearchCount

        Public Property TableSchema As String
        Public Property TableName As String
        Public Property ColumnName As String
        Public Property DataType As String
        Public Property RecordCount As Integer

        Public Sub New(ByVal col As Column)
            ColumnName = col.ColumnName
            DataType = col.DataType
            TableName = col.TableName
            TableSchema = col.TableSchema
        End Sub

        Public Sub New()
        End Sub



    End Class
End Namespace