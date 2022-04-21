Imports GuidFinder.ClassLibrary
Imports System.ComponentModel
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.UnityContainerExtensions
Imports Unity


Namespace Commands
    Public Class CountCommand
        Inherits SearchCommandBase

        Public Sub New(ByVal vm As SearchViewModel, sc As IUnityContainer)
            MyBase.New(vm, sc)

            _recordRetrievalService = sc.Resolve(Of Services.IRecordRetrievalService)()
        End Sub

        Protected Overrides Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Try
                Results.Clear()
                Dim i As Integer = 0

                Dim relevantColumns As IEnumerable(Of TransferObjects.Column) = RetrieveRelevantColumns()

                For Each searchableColumn In relevantColumns
                    Dim percentageToReport As Integer = CInt(i * 100 / relevantColumns.Count)
                    UpdateProgress(percentageToReport)

                    Dim newSearchCount As TransferObjects.SearchCount = _recordRetrievalService.RetrieveRecordCountByValue(ViewModelContext.DatabaseServerName, ViewModelContext.DatabaseName, ViewModelContext.UserName, ViewModelContext.Password, ViewModelContext.FirstSearch.SearchText.Trim, ViewModelContext.FirstSearch.UseLikeOperator, searchableColumn)
                    If newSearchCount IsNot Nothing AndAlso newSearchCount.RecordCount > 0 Then
                        Results.Add(newSearchCount)
                    End If
                    i += 1
                Next
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try

        End Sub

        Protected Overrides Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Try
                Dim vmResults As ColumnsViewModel = New ViewModels.ColumnsViewModel(Results)

                With ViewModelContext
                    .ProgressMax = 100
                    .ProgressMin = 0
                    .ProgressValue = 100
                    .ProgressVisibility = Windows.Visibility.Hidden
                    .DisplayCountsToUserImpl(vmResults)
                End With
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub

        Private ReadOnly _recordRetrievalService As Services.IRecordRetrievalService
        Private _results As List(Of TransferObjects.SearchCount)

        Public ReadOnly Property Results() As List(Of TransferObjects.SearchCount)
            Get
                If _results Is Nothing Then
                    _results = New List(Of TransferObjects.SearchCount)
                End If
                Return _results
            End Get
        End Property
    End Class
End Namespace