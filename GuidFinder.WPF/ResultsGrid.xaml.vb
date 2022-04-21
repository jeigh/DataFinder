Imports GuidFinder.ViewModels

Public Class ResultsGrid
    Private Property GridDataSource As ViewModels.ResultsViewModel

    Public Sub New(ByVal results As ResultsViewModel)
        InitializeComponent()

        GridDataSource = results
        Me.Title = results.TableName
    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Try
            Dim GuidResultsViewModelViewSource As System.Windows.Data.CollectionViewSource = CType(Me.FindResource("GuidResultsViewModelViewSource"), System.Windows.Data.CollectionViewSource)

            Dim theList As New List(Of ResultsViewModel)
            theList.Add(GridDataSource)

            GuidResultsViewModelViewSource.Source = theList
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
