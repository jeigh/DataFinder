Imports GuidFinder.ClassLibrary
Imports Microsoft.Practices.Unity
Imports GuidFinder.ClassLibrary.Gateways

Namespace Gateways
    Public Class GenericSqlGateway
        Implements IGenericSqlGateway
        Private Const csTemplate As String = "Server={0};Database={1};Trusted_Connection={2};"
        Private Const csUnPwTemplate As String = "User Id={0};Password={1};"

        Private Function BuildConnectionString(databaseServerName As String, databaseName As String, username As String, Password As String) As String
            Dim isTrustedConnection As String
            If String.IsNullOrWhiteSpace(username) Then isTrustedConnection = "True" Else isTrustedConnection = "False"

            Dim returnable As String = String.Format(csTemplate, databaseServerName, databaseName, isTrustedConnection)
            If isTrustedConnection = "False" Then
                Dim csUnPw As String = String.Format(csUnPwTemplate, username, Password)
                returnable = returnable & csUnPw
            End If

            Return returnable
        End Function

        Public Function RetrieveColumnList(databaseServerName As String, databaseName As String, username As String, password As String) As List(Of TransferObjects.Column) Implements IGenericSqlGateway.RetrieveColumnList
            Dim RetrieveColumnListQuery As String = "SELECT T.TABLE_TYPE, C.TABLE_SCHEMA, C.TABLE_NAME, C.COLUMN_NAME, C.DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS C INNER JOIN INFORMATION_SCHEMA.TABLES T ON T.TABLE_NAME = C.TABLE_NAME;"

            Dim theDatatable As New DataTable()
            Dim ConnectionString As String = BuildConnectionString(databaseServerName, databaseName, username, password)
            Using theAdapter As New SqlClient.SqlDataAdapter(RetrieveColumnListQuery, ConnectionString)
                theAdapter.Fill(theDatatable)
            End Using

            Dim returnable As List(Of TransferObjects.Column) = (
                From aRow As Object In theDatatable.Rows
                Select New TransferObjects.Column With {
                    .ColumnName = CType(aRow, System.Data.DataRow)("COLUMN_NAME").ToString,
                    .DataType = CType(aRow, System.Data.DataRow)("DATA_TYPE").ToString,
                    .TableName = CType(aRow, System.Data.DataRow)("TABLE_NAME").ToString,
                    .TableSchema = CType(aRow, System.Data.DataRow)("TABLE_SCHEMA").ToString,
                    .TableType = CType(aRow, System.Data.DataRow)("TABLE_TYPE").ToString
                }).ToList

            Return returnable
        End Function

        Public Function QueryColumnForValue(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal SearchCriteria As T, useLikeOperator As Boolean, ByVal cd As TransferObjects.Column) As DataTable Implements IGenericSqlGateway.QueryColumnForValue
            Dim RetrieveRowsQueryTemplate As String

            If useLikeOperator Then
                RetrieveRowsQueryTemplate = "SELECT * FROM [{0}].[{1}] WHERE [{2}] like '{3}'"
            Else
                RetrieveRowsQueryTemplate = "SELECT * FROM [{0}].[{1}] WHERE [{2}] = '{3}'"
            End If

            Dim RetrieveRowsQuery As String = String.Format(RetrieveRowsQueryTemplate, cd.TableSchema, cd.TableName, cd.ColumnName, SearchCriteria.ToString)

            Dim theDatatable As New DataTable()
            Dim ConnectionString As String = BuildConnectionString(databaseServerName, databaseName, username, password)
            Using theAdapter As New SqlClient.SqlDataAdapter(RetrieveRowsQuery, ConnectionString)
                theAdapter.Fill(theDatatable)
            End Using

            Return theDatatable
        End Function

        Public Function QueryColumnForValueCount(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal SearchCriteria As T, useLikeOperator As Boolean, ByVal cd As TransferObjects.Column) As TransferObjects.SearchCount Implements IGenericSqlGateway.QueryColumnForValueCount
            Dim RetrieveRowsQueryTemplate As String
            If useLikeOperator Then
                RetrieveRowsQueryTemplate = "SELECT Count(*) as RECORD_COUNT FROM [{0}].[{1}] WHERE [{2}] like '{3}'"
            Else
                RetrieveRowsQueryTemplate = "SELECT Count(*) as RECORD_COUNT FROM [{0}].[{1}] WHERE [{2}] = '{3}'"
            End If

            Dim RetrieveRowsQuery As String = String.Format(RetrieveRowsQueryTemplate, cd.TableSchema, cd.TableName, cd.ColumnName, SearchCriteria.ToString)

            Dim theDatatable As New DataTable()
            Dim ConnectionString As String = BuildConnectionString(databaseServerName, databaseName, username, password)
            Using theAdapter As New SqlClient.SqlDataAdapter(RetrieveRowsQuery, ConnectionString)
                theAdapter.Fill(theDatatable)
            End Using

            Dim returnable As New TransferObjects.SearchCount

            If theDatatable.Rows.Count = 1 Then
                With returnable
                    .ColumnName = cd.ColumnName
                    .DataType = cd.DataType
                    .RecordCount = CInt(theDatatable.Rows(0)("RECORD_COUNT"))
                    .TableName = cd.TableName
                    .TableSchema = cd.TableSchema
                End With
            End If

            Return returnable
        End Function
    End Class

End Namespace