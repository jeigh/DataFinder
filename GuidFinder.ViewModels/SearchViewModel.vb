Imports System.ComponentModel
Imports System.Windows.Input

Public Class SearchViewModel
    Inherits ViewModelBase

    Private _secondSearch As New SearchableViewModel
    Private _firstSearch As New SearchableViewModel
    Private _includeViews As Boolean
    Private _databaseName As String
    Private _databaseServerName As String
    Private _progressValue As Integer = 0
    Private _progressVisibility As System.Windows.Visibility = System.Windows.Visibility.Collapsed
    Private _progressMax As Integer = 10
    Private _progressMin As Integer = 0

    Public DisplayResultsToUserImpl As Action(Of ViewModels.ResultsViewModel)
    Public DisplayCountsToUserImpl As Action(Of ColumnsViewModel)

    Public Sub New()
    End Sub

    Private _serviceRegistry As DataAccess.IServiceRegistry
    Private ReadOnly Property ServiceRegistry As DataAccess.IServiceRegistry
        Get
            If _serviceRegistry Is Nothing Then
                _serviceRegistry = New DataAccess.ServiceRegistry()
                _serviceRegistry.RegisterServices()
            End If
            Return _serviceRegistry
        End Get
    End Property

    Public ReadOnly Property GuidCountCommandImpl() As ICommand
        Get
            Return New Commands.CountCommand(Me, ServiceRegistry.ServiceContainer)
        End Get
    End Property

    Public ReadOnly Property GuidSearchCommandImpl() As ICommand
        Get
            Return New Commands.SearchCommand(Me, ServiceRegistry.ServiceContainer)
        End Get
    End Property

    Public ReadOnly Property DoubleGuidSearchCommand() As ICommand
        Get
            Return New Commands.DoubleSearchCommand(Me, ServiceRegistry.ServiceContainer)
        End Get
    End Property

    Public Property ProgressValue() As Integer
        Get
            Return _progressValue
        End Get
        Set(ByVal value As Integer)
            _progressValue = value
            NotifyPropertyChanged("ProgressValue")
        End Set
    End Property

    Public Property ProgressVisibility() As System.Windows.Visibility
        Get
            Return _progressVisibility
        End Get
        Set(ByVal value As System.Windows.Visibility)
            _progressVisibility = value
            NotifyPropertyChanged("ProgressVisibility")
        End Set
    End Property

    Public Property ProgressMax() As Integer
        Get
            Return _progressMax
        End Get
        Set(ByVal value As Integer)
            _progressMax = value
            NotifyPropertyChanged("ProgressMax")
        End Set
    End Property

    Public Property ProgressMin() As Integer
        Get
            Return _progressMin
        End Get
        Set(ByVal value As Integer)
            _progressMin = value
            NotifyPropertyChanged("ProgressMin")
        End Set
    End Property


    Public Property DatabaseServerName() As String
        Get
            Return _databaseServerName
        End Get
        Set(ByVal value As String)
            _databaseServerName = value
            NotifyPropertyChanged("DatabaseServerName")
        End Set
    End Property

    Public Property DatabaseName() As String
        Get
            Return _databaseName
        End Get
        Set(ByVal value As String)
            _databaseName = value
            NotifyPropertyChanged("DatabaseName")
        End Set
    End Property

    Public Property IncludeViews() As Boolean
        Get
            Return _includeViews
        End Get
        Set(ByVal value As Boolean)
            _includeViews = value
            NotifyPropertyChanged("IncludeViews")
        End Set
    End Property

    Public Property FirstSearch() As SearchableViewModel
        Get
            Return _firstSearch
        End Get
        Set(ByVal value As SearchableViewModel)
            _firstSearch = value
            NotifyPropertyChanged("FirstSearch")
        End Set
    End Property

    Private _userName As String 
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
            NotifyPropertyChanged("UserName")
        End Set
    End Property
    Private _password As String 
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
            NotifyPropertyChanged("Password")
        End Set
    End Property

    Public Property SecondSearch() As SearchableViewModel
        Get
            Return _secondSearch
        End Get
        Set(ByVal value As SearchableViewModel)
            _secondSearch = value
            NotifyPropertyChanged("SecondSearch")
        End Set
    End Property





End Class
