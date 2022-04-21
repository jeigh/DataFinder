Imports System.Windows.Input
Imports GuidFinder.ClassLibrary
Imports System.ComponentModel
Imports Microsoft.Practices.Unity.UnityContainerExtensions
Imports GuidFinder.ClassLibrary.Services
Imports Microsoft.Practices.Unity
Imports Unity

Namespace Commands
    Public MustInherit Class SearchCommandBase
        Implements ICommand

        Private ReadOnly _columnService As IColumnService
        Private ReadOnly _recordRetrievalService As IRecordRetrievalService

        Public Sub New(ByVal vm As SearchViewModel, sc As IUnityContainer)
            ViewModelContext = vm


            _sc = sc

            _columnService = ServiceContainer.Resolve(Of IColumnService)()
            _recordRetrievalService = ServiceContainer.Resolve(Of IRecordRetrievalService)()

        End Sub

        Private theBackgroundWorker As BackgroundWorker
        Private _sc As IUnityContainer

        Protected Sub UpdateProgress(i As Integer)
            theBackgroundWorker.ReportProgress(i)
        End Sub

        Protected ReadOnly Property ServiceContainer As IUnityContainer
            Get
                Return _sc
            End Get
        End Property

        Public Overridable Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
            Return True
        End Function

        Protected Function RetrieveRelevantColumns() As IEnumerable(Of TransferObjects.Column)
            Dim allColumns As IEnumerable(Of TransferObjects.Column) = _recordRetrievalService.RetrieveColumnList(ViewModelContext.DatabaseServerName, ViewModelContext.DatabaseName, ViewModelContext.UserName, ViewModelContext.Password)

            Dim returnable As New List(Of TransferObjects.Column)

            If ViewModelContext.FirstSearch.IncludeUniqueIdentifierColumns Then
                returnable.AddRange(_columnService.RetrieveColumnsMatchingDataType(Enumerations.SearchableDataType.SearchableGuid, allColumns))
            End If

            If ViewModelContext.FirstSearch.IncludeVarcharColumns Then
                returnable.AddRange(_columnService.RetrieveColumnsMatchingDataType(Enumerations.SearchableDataType.SearchableVarchar, allColumns))
            End If

            If ViewModelContext.FirstSearch.IncludeNVarcharColumns Then
                returnable.AddRange(_columnService.RetrieveColumnsMatchingDataType(Enumerations.SearchableDataType.SearchableNVarchar, allColumns))
            End If

            If ViewModelContext.FirstSearch.IncludeIntegerColumns Then
                returnable.AddRange(_columnService.RetrieveColumnsMatchingDataType(Enumerations.SearchableDataType.SearchableInteger, allColumns))
            End If


            If Not ViewModelContext.IncludeViews Then
                Dim removables As List(Of TransferObjects.Column) = (From r In returnable Where r.TableType = "VIEW" Select r).ToList
                removables.ForEach(AddressOf returnable.Remove)
            End If

            Return returnable
        End Function

        Protected MustOverride Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Protected MustOverride Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Try
                ViewModelContext.ProgressValue = e.ProgressPercentage
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub

        Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
            If ViewModelContext Is Nothing Then Throw New Exception("ViewModelContext is undefined")

            Try
                With ViewModelContext
                    .ProgressMax = 100
                    .ProgressMin = 0
                    .ProgressValue = 0
                    .ProgressVisibility = Windows.Visibility.Visible
                End With

                theBackgroundWorker = New BackgroundWorker()

                With theBackgroundWorker
                    .WorkerReportsProgress = True

                    AddHandler .DoWork, AddressOf backgroundWorker_DoWork
                    AddHandler .ProgressChanged, AddressOf backgroundWorker_ProgressChanged
                    AddHandler .RunWorkerCompleted, AddressOf backgroundWorker_RunWorkerCompleted

                    .RunWorkerAsync()
                End With
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString)
            End Try
        End Sub

        Public Event CanExecuteChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements System.Windows.Input.ICommand.CanExecuteChanged

        Protected Property ViewModelContext As SearchViewModel = Nothing

        Private Sub New()
        End Sub
    End Class
End Namespace

