
Namespace Services
    Public Interface IColumnService
        Function RetrieveColumnsMatchingDataType(ByVal dt As GuidFinder.ClassLibrary.Enumerations.SearchableDataType, ByVal cols As IEnumerable(Of TransferObjects.Column)) As IEnumerable(Of TransferObjects.Column)
    End Interface

End Namespace
