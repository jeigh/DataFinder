Imports GuidFinder.ClassLibrary
Imports GuidFinder.ClassLibrary.Enumerations
Imports GuidFinder.ClassLibrary.Services
Imports Unity

Namespace Services
    Public Class ColumnService
        Implements IColumnService

        Private Sub New()
        End Sub

        Public Sub New(sc As IUnityContainer)
            _recordRetrievalService = sc.Resolve(Of ClassLibrary.Services.IRecordRetrievalService)()
        End Sub

        Private _recordRetrievalService As ClassLibrary.Services.IRecordRetrievalService

        Public Function RetrieveColumnsMatchingDataType(ByVal dt As SearchableDataType, ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column) Implements IColumnService.RetrieveColumnsMatchingDataType
            Select Case dt
                Case SearchableDataType.SearchableGuid
                    Return RetrieveGuidColumns(cols)
                Case SearchableDataType.SearchableInteger
                    Return RetrieveIntegerColumns(cols)
                Case SearchableDataType.SearchableVarchar
                    Return RetrieveVarcharColumns(cols)
                Case SearchableDataType.SearchableNVarchar
                    Return RetrieveNVarcharColumns(cols)
                Case Else
                    Throw New NotImplementedException()
            End Select
        End Function

        Private Function RetrieveVarcharColumns(ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column)
            Return (From c In cols Where c.DataType.ToLower = "varchar" Or c.DataType.ToLower = "char" Select c)
        End Function

        Private Function RetrieveNVarcharColumns(ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column)
            Return (From c In cols Where c.DataType.ToLower = "nvarchar" Select c)
        End Function

        Private Function RetrieveGuidColumns(ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column)
            Return (From c In cols Where c.DataType.ToLower = "uniqueidentifier" Select c)
        End Function

        Private Function RetrieveIntegerColumns(ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column)
            Return (From c In cols Where c.DataType.ToLower = "int" OrElse c.DataType.ToLower = "bigint" Select c)
        End Function


    End Class




End Namespace
