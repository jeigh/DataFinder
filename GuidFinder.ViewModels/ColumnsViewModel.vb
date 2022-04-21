Imports System.Collections.ObjectModel
Imports GuidFinder.ClassLibrary

Public Class ColumnsViewModel
    Inherits ViewModelBase

    Public Property Columns As New ObservableCollection(Of TransferObjects.SearchCount)

    Private Sub New()
    End Sub

    Public Sub New(ByVal paramGuidColumns As IEnumerable(Of TransferObjects.SearchCount))
        Columns.Clear()

        For Each col As TransferObjects.SearchCount In paramGuidColumns
            Columns.Add(col)
        Next

        NotifyPropertyChanged("Columns")

    End Sub
End Class
