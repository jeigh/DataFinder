Imports GuidFinder.ViewModels
Imports GuidFinder.ClassLibrary


Public Class GuidColumns
    Public Sub New(ByRef vm As ViewModels.ColumnsViewModel)
        InitializeComponent()

        TheViewModel = vm
    End Sub

    Private Property TheViewModel As ViewModels.ColumnsViewModel

    Private Sub New()
        InitializeComponent()
    End Sub

    Private Function RetrieveViewSource() As List(Of ViewModels.ColumnsViewModel)
        Dim returnable As New List(Of ViewModels.ColumnsViewModel)

        returnable.Add(TheViewModel)

        Return returnable
    End Function

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Try
            Dim GuidColumnsViewModelViewSource As System.Windows.Data.CollectionViewSource = CType(Me.FindResource("GuidColumnsViewModelViewSource"), System.Windows.Data.CollectionViewSource)
            GuidColumnsViewModelViewSource.Source = RetrieveViewSource()
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
