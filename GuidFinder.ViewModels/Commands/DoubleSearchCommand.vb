Imports GuidFinder.ClassLibrary
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.UnityContainerExtensions
Imports Unity

Namespace Commands
    Public Class DoubleSearchCommand
        Inherits SearchCommandBase

        Public Sub New(ByVal vm As SearchViewModel, sc As IUnityContainer)
            MyBase.New(vm, sc)

            _recordRetrievalService = sc.Resolve(Of Services.IRecordRetrievalService)()
        End Sub

        Private _filteredResults As List(Of TransferObjects.SearchResult)
        Private ReadOnly _recordRetrievalService As Services.IRecordRetrievalService

        Public ReadOnly Property FilteredResults() As List(Of TransferObjects.SearchResult)
            Get
                If _filteredResults Is Nothing Then
                    _filteredResults = New List(Of TransferObjects.SearchResult)
                End If
                Return _filteredResults
            End Get
        End Property

        Protected Overrides Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
            Try
                For Each result As TransferObjects.SearchResult In FilteredResults
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

        Protected Overrides Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
            Try
                Dim i As Integer = 0

                Dim secondSearchText As String = ViewModelContext.SecondSearch.SearchText.Trim.ToUpper
                Dim unfilteredResults As New List(Of TransferObjects.SearchResult)

                Dim relevantColumns As IEnumerable(Of TransferObjects.Column) = RetrieveRelevantColumns()

                For Each searchableColumn In relevantColumns
                    Dim percentageToReport As Integer = CInt(i * 100 / relevantColumns.Count)
                    UpdateProgress(percentageToReport)

                    Dim newSearchResult As TransferObjects.SearchResult = _recordRetrievalService.RetrieveRecordByValue(ViewModelContext.DatabaseServerName, ViewModelContext.DatabaseName, ViewModelContext.UserName, ViewModelContext.Password, ViewModelContext.FirstSearch.SearchText.Trim, ViewModelContext.FirstSearch.UseLikeOperator, searchableColumn)
                    If newSearchResult IsNot Nothing AndAlso newSearchResult.Results.Rows.Count > 0 Then
                        unfilteredResults.Add(newSearchResult)
                    End If
                    i += 1
                Next

                For Each thisSearchResult As TransferObjects.SearchResult In unfilteredResults
                    Dim IncludeThisDataTable As Boolean = False
                    Dim removable As New List(Of System.Data.DataRow)

                    For Each drow As System.Data.DataRow In thisSearchResult.Results.Rows
                        Dim IncludeThisRecord As Boolean = False

                        For Each cell As Object In drow.ItemArray
                            If cell.ToString.ToUpper = secondSearchText Then
                                IncludeThisRecord = True
                            End If
                        Next

                        If IncludeThisRecord = True Then
                            IncludeThisDataTable = True
                        Else
                            removable.Add(drow)
                        End If
                    Next

                    If IncludeThisDataTable Then
                        removable.ForEach(AddressOf thisSearchResult.Results.Rows.Remove)
                        FilteredResults.Add(thisSearchResult)
                    End If
                Next
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub
    End Class

End Namespace