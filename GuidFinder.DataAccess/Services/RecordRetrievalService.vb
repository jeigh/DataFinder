Imports GuidFinder.ClassLibrary
Imports Unity

Namespace Services

    Public Class RecordRetrievalService
        Implements GuidFinder.ClassLibrary.Services.IRecordRetrievalService

        Private ReadOnly _serviceContainer As IUnityContainer
        Private ReadOnly _genericSqlGatewayInstance As ClassLibrary.Gateways.IGenericSqlGateway

        Private Sub New()
        End Sub

        Public Sub New(sc As IUnityContainer)
            _serviceContainer = sc

            _genericSqlGatewayInstance = _serviceContainer.Resolve(Of ClassLibrary.Gateways.IGenericSqlGateway)()
        End Sub

        Public Function RetrieveColumnList(databaseServerName As String, databaseName As String, username As String, password As String) As IEnumerable(Of TransferObjects.Column) Implements GuidFinder.ClassLibrary.Services.IRecordRetrievalService.RetrieveColumnList
            Return _genericSqlGatewayInstance.RetrieveColumnList(databaseServerName, databaseName, username, password)
        End Function

        Public Function RetrieveRecordCountsByValue(Of FT)(
            databaseServerName As String,
            databaseName As String,
            username As String,
            password As String,
            ByVal searchValue As FT,
            useLikeOperator As Boolean,
            ByVal SearchThese As List(Of TransferObjects.Column)
        ) As List(Of TransferObjects.SearchCount) Implements ClassLibrary.Services.IRecordRetrievalService.RetrieveRecordCountsByValue
            Dim returnables As New List(Of TransferObjects.SearchCount)
            Try
                For Each col As TransferObjects.Column In SearchThese
                    returnables.Add(_genericSqlGatewayInstance.QueryColumnForValueCount(Of FT)(databaseServerName, databaseName, username, password, searchValue, useLikeOperator, col))
                Next
            Catch ex As System.Data.SqlClient.SqlException
                If Not ex.ErrorCode = -2146232060 Then
                    Throw
                End If
            Catch ex As Exception
                Throw
            End Try

            Return returnables
        End Function

        Public Function RetrieveRecordsByValue(Of FT)(
            databaseServerName As String,
            databaseName As String,
            username As String,
            password As String,
            ByVal searchValue As FT,
            useLikeOperator As Boolean,
            ByVal SearchThese As List(Of TransferObjects.Column)
        ) As List(Of TransferObjects.SearchResult) Implements ClassLibrary.Services.IRecordRetrievalService.RetrieveRecordsByValue
            Dim returnable As New List(Of TransferObjects.SearchResult)
            For Each cd As TransferObjects.Column In SearchThese
                Dim dt As DataTable = _genericSqlGatewayInstance.QueryColumnForValue(Of FT)(databaseServerName, databaseName, username, password, searchValue, useLikeOperator, cd)
                If dt.Rows.Count > 0 Then
                    Dim gsr As New TransferObjects.SearchResult(cd)

                    gsr.Results = dt
                    gsr.Value = searchValue

                    returnable.Add(gsr)
                End If
            Next

            Return returnable
        End Function

        Public Function RetrieveRecordCountByValue(Of FT)(
            databaseServerName As String,
            databaseName As String,
            username As String,
            password As String,
            ByVal searchValue As FT,
            useLikeOperator As Boolean,
            ByVal searchableColumn As ClassLibrary.TransferObjects.Column
        ) As ClassLibrary.TransferObjects.SearchCount Implements ClassLibrary.Services.IRecordRetrievalService.RetrieveRecordCountByValue
            Dim returnable As ClassLibrary.TransferObjects.SearchCount = Nothing

            Try
                returnable = _genericSqlGatewayInstance.QueryColumnForValueCount(Of FT)(databaseServerName, databaseName, username, password, searchValue, useLikeOperator, searchableColumn)
            Catch ex As System.Data.SqlClient.SqlException
                If Not ex.ErrorCode = -2146232060 Then
                    Throw
                End If
            Catch ex As Exception
                Throw
            End Try

            Return returnable
        End Function



        Public Function RetrieveRecordByValue(Of FT)(
            databaseServerName As String,
            databaseName As String,
            username As String,
            password As String,
            ByVal searchValue As FT,
            useLikeOperator As Boolean,
            ByVal cd As ClassLibrary.TransferObjects.Column
        ) As ClassLibrary.TransferObjects.SearchResult Implements ClassLibrary.Services.IRecordRetrievalService.RetrieveRecordByValue
            Dim dt As DataTable = _genericSqlGatewayInstance.QueryColumnForValue(Of FT)(databaseServerName, databaseName, username, password, searchValue, useLikeOperator, cd)
            Dim gsr As TransferObjects.SearchResult = Nothing

            If dt.Rows.Count > 0 Then
                gsr = New TransferObjects.SearchResult(cd)

                gsr.Results = dt
                gsr.Value = searchValue
            End If
            Return gsr
        End Function



    End Class



End Namespace
