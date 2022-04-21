Imports GuidFinder.ClassLibrary

Namespace Gateways
    Public Interface IGenericSqlGateway
        Function RetrieveColumnList(ByVal databaseServerName As String, ByVal databaseName As String, username As String, password As String) As List(Of TransferObjects.Column)
        Function QueryColumnForValue(Of T)(ByVal databaseServerName As String, ByVal databaseName As String, username As String, password As String, ByVal SearchCriteria As T, ByVal useLikeOperator As Boolean, ByVal cd As TransferObjects.Column) As DataTable
        Function QueryColumnForValueCount(Of T)(ByVal databaseServerName As String, ByVal databaseName As String, username As String, password As String, ByVal SearchCriteria As T, ByVal useLikeOperator As Boolean, ByVal cd As TransferObjects.Column) As TransferObjects.SearchCount
    End Interface

End Namespace
