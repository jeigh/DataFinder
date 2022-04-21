Imports System.Windows.Input
Imports GuidFinder.ClassLibrary
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.UnityContainerExtensions
Imports Unity


Namespace Commands
    Public Class SearchCommand
        Inherits SearchCommandBase

        Public Sub New(ByVal vm As SearchViewModel, sc As IUnityContainer)
            MyBase.New(vm, sc)

            _recordRetrievalService = sc.Resolve(Of Services.IRecordRetrievalService)()
        End Sub

        Protected Overrides Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
            Try
                Dim i As Integer = 0

                Dim relevantColumns As IEnumerable(Of TransferObjects.Column) = RetrieveRelevantColumns()

                For Each searchableColumn In relevantColumns
                    Dim percentageToReport As Integer = CInt(i * 100 / relevantColumns.Count)
                    UpdateProgress(percentageToReport)

                    Dim newSearchResult As TransferObjects.SearchResult = _recordRetrievalService.RetrieveRecordByValue(ViewModelContext.DatabaseServerName, ViewModelContext.DatabaseName, ViewModelContext.UserName, ViewModelContext.Password, ViewModelContext.FirstSearch.SearchText.Trim, ViewModelContext.FirstSearch.UseLikeOperator, searchableColumn)
                    If newSearchResult IsNot Nothing AndAlso newSearchResult.Results.Rows.Count > 0 Then
                        Results.Add(newSearchResult)
                    End If
                    i += 1
                Next
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub

        Protected Overrides Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
            Try
                For Each result As TransferObjects.SearchResult In Results
                    ViewModelContext.DisplayResultsToUserImpl(New ResultsViewModel(result))
                Next

                With ViewModelContext
                    .ProgressMax = 100
                    .ProgressMin = 0
                    .ProgressValue = 100
                    .ProgressVisibility = Windows.Visibility.Hidden
                End With
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub

        Private ReadOnly _recordRetrievalService As Services.IRecordRetrievalService
        Private _results As List(Of TransferObjects.SearchResult)

        Public ReadOnly Property Results() As List(Of TransferObjects.SearchResult)
            Get
                If _results Is Nothing Then
                    _results = New List(Of TransferObjects.SearchResult)
                End If
                Return _results
            End Get
        End Property
    End Class
End Namespace