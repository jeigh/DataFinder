Imports Unity
Imports Unity.Lifetime


Public Interface IServiceRegistry
    Sub AddInterfaceRegistration(Of TInterface, TClass As TInterface)()
    ReadOnly Property ServiceContainer() As IUnityContainer
    Sub RegisterServices()
End Interface

Public Class ServiceRegistry
    Implements IServiceRegistry

    Private _serviceContainer As IUnityContainer

    Public ReadOnly Property ServiceContainer() As IUnityContainer Implements IServiceRegistry.ServiceContainer
        Get
            Return _serviceContainer
        End Get
    End Property

    Public Sub RegisterServices() Implements IServiceRegistry.RegisterServices
        AddInterfaceRegistration(Of ClassLibrary.Services.IRecordRetrievalService, DataAccess.Services.RecordRetrievalService)()
        AddInterfaceRegistration(Of ClassLibrary.Gateways.IGenericSqlGateway, DataAccess.Gateways.GenericSqlGateway)()
        AddInterfaceRegistration(Of ClassLibrary.Services.IColumnService, DataAccess.Services.ColumnService)()
    End Sub

    Public Sub AddInterfaceRegistration(Of TInterface, TClass As TInterface)() Implements IServiceRegistry.AddInterfaceRegistration
        _serviceContainer.RegisterType(Of TInterface, TClass)(New ContainerControlledLifetimeManager())
    End Sub

    Public Sub New()
        _serviceContainer = New UnityContainer()
        RegisterServices()
    End Sub
End Class

