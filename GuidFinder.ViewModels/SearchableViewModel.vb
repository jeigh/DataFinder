

Public Class SearchableViewModel
    Inherits ViewModelBase

    Private _useLikeOperator As Boolean
    Private _includeIntegerColumns As Boolean
    Private _includeNVarcharColumns As Boolean
    Private _includeVarcharColumns As Boolean
    Private _includeUniqueIdentifierColumns As Boolean
    Private _searchText As String = String.Empty

    Public Property SearchText() As String
        Get
            Return _searchText
        End Get
        Set(ByVal value As String)
            _searchText = value
            NotifyPropertyChanged("SearchText")
        End Set
    End Property
    Public Property IncludeUniqueIdentifierColumns() As Boolean
        Get
            Return _includeUniqueIdentifierColumns
        End Get
        Set(ByVal value As Boolean)
            _includeUniqueIdentifierColumns = value
            NotifyPropertyChanged("IncludeUniqueIdentifierColumns")
        End Set
    End Property
    Public Property IncludeVarcharColumns() As Boolean
        Get
            Return _includeVarcharColumns
        End Get
        Set(ByVal value As Boolean)
            _includeVarcharColumns = value
            NotifyPropertyChanged("IncludeVarcharColumns")
        End Set
    End Property
    Public Property IncludeNVarcharColumns() As Boolean
        Get
            Return _includeNVarcharColumns
        End Get
        Set(ByVal value As Boolean)
            _includeNVarcharColumns = value
            NotifyPropertyChanged("IncludeNVarcharColumns")
        End Set
    End Property
    Public Property IncludeIntegerColumns() As Boolean
        Get
            Return _includeIntegerColumns
        End Get
        Set(ByVal value As Boolean)
            _includeIntegerColumns = value
            NotifyPropertyChanged("IncludeIntegerColumns")
        End Set
    End Property
    Public Property UseLikeOperator() As Boolean
        Get
            Return _useLikeOperator
        End Get
        Set(ByVal value As Boolean)
            _useLikeOperator = value
            NotifyPropertyChanged("UseLikeOperator")
        End Set
    End Property

End Class
