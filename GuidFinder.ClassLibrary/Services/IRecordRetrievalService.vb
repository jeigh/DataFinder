Namespace Services
    Public Interface IRecordRetrievalService
        Function RetrieveColumnList(databaseServerName As String, databaseName As String, username As String, password As String) As IEnumerable(Of TransferObjects.Column)
        Function RetrieveRecordByValue(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal param1 As T, useLikeOperator As Boolean, ByVal searchableColumn As TransferObjects.Column) As TransferObjects.SearchResult
        Function RetrieveRecordCountByValue(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal param1 As T, useLikeOperator As Boolean, ByVal searchableColumn As TransferObjects.Column) As TransferObjects.SearchCount
        Function RetrieveRecordCountsByValue(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal uniqueidentifier As T, useLikeOperator As Boolean, ByVal SearchThese As List(Of TransferObjects.Column)) As List(Of TransferObjects.SearchCount)
        Function RetrieveRecordsByValue(Of T)(databaseServerName As String, databaseName As String, username As String, password As String, ByVal uniqueidentifier As T, useLikeOperator As Boolean, ByVal SearchThese As List(Of TransferObjects.Column)) As List(Of TransferObjects.SearchResult)
    End Interface
End Namespace