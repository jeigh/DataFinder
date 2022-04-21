Imports GuidFinder.ClassLibrary

Class SearchWindow
    Private Shared Sub DisplayGuidResultsViewModel(ByVal theVm As ViewModels.ResultsViewModel)
        Dim blah As New ResultsGrid(theVm)
        blah.Show()
    End Sub

    Private Shared Sub DisplayGuidCountsViewModel(ByVal theVM As ViewModels.ColumnsViewModel)
        Dim blah As New GuidColumns(theVM)
        blah.Show()
    End Sub

    Public Sub New()
        InitializeComponent()
        SetViewModel()
    End Sub

    Private Sub SetViewModel()
        Dim GuidResultsViewModelViewSource As GuidFinder.ViewModels.SearchViewModel = CType(Me.FindResource("theViewModel"), ViewModels.SearchViewModel)
        With GuidResultsViewModelViewSource
            .DisplayResultsToUserImpl = AddressOf DisplayGuidResultsViewModel
            .DisplayCountsToUserImpl = AddressOf DisplayGuidCountsViewModel
        End With
    End Sub
End Class
