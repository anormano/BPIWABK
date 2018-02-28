using System;
using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Mobile;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security.ClientServer.Remoting;

namespace BPIWABK.Mobile
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class BPIWABKMobileApplication : MobileApplication
    {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule module2;
        private BPIWABK.Module.BPIWABKModule module3;
        private BPIWABK.Module.Mobile.BPIWABKMobileModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule;
        private DevExpress.ExpressApp.CloneObject.Mobile.CloneObjectMobileModule cloneObjectMobileModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.ConditionalAppearance.Mobile.ConditionalAppearanceMobileModule conditionalAppearanceMobileModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
        private DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule fileAttachmentsMobileModule;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2 reportsMobileModuleV2;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Mobile.ValidationMobileModule validationMobileModule;
        private SecurityStrategyComplex securityStrategyComplex1;
        private AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;

        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static BPIWABKMobileApplication()
        {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
        }
        private void InitializeDefaults()
        {
            LinkNewObjectToParentImmediately = false;
        }
        #endregion

        public BPIWABKMobileApplication()
        {

            SecurityAdapterHelper.Enable();
            Tracing.Initialize();
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            InitializeComponent();
            InitializeDefaults();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            //SecurityAdapterHelper.Enable();
            //string connectionString = "tcp://localhost:8082/DataServer";
            //Hashtable t = new Hashtable();
            //t.Add("secure", true);
            //t.Add("tokenImpersonationLevel", "impersonation");
            //TcpChannel channel = new TcpChannel(t, null, null);
            //if (ChannelServices.GetChannel(channel.ChannelName) == null)
            //{
            //    ChannelServices.RegisterChannel(channel, true);
            //}
            //Tracing.Initialize();
            //IDataServer clientDataServer = (IDataServer)Activator.GetObject(typeof(RemoteSecuredDataServer), connectionString);
            //ServerSecurityClient securityClient = new ServerSecurityClient(clientDataServer, new ClientInfoFactory());
            //Security = securityClient;
            //CreateCustomObjectSpaceProvider +=
            //    delegate (object _sender, CreateCustomObjectSpaceProviderEventArgs args)
            //    {
            //        args.ObjectSpaceProvider = new DataServerObjectSpaceProvider(
            //            clientDataServer, securityClient);
            //    };
            //InitializeComponent();
        }

        protected override void SetLogonParametersForUIBuilder(object logonParameters)
        {
            base.SetLogonParametersForUIBuilder(logonParameters);
            ((AuthenticationStandardLogonParameters)logonParameters).UserName = "Admin";
            ((AuthenticationStandardLogonParameters)logonParameters).Password = "";
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection)
        {
            System.Web.HttpApplicationState application = (System.Web.HttpContext.Current != null) ? System.Web.HttpContext.Current.Application : null;
            IXpoDataStoreProvider dataStoreProvider = null;
            if (application != null && application["DataStoreProvider"] != null)
            {
                dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            }
            else
            {
                dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, true);
                if (application != null)
                {
                    application["DataStoreProvider"] = dataStoreProvider;
                }
            }
            return dataStoreProvider;
            //         IXpoDataStoreProvider dataStoreProvider = null;
            //         if(!String.IsNullOrEmpty(connectionString)) {
            //             dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString);
            //         }
            //         else if(connection != null) {
            //             dataStoreProvider = new ConnectionDataStoreProvider(connection);
            //         }
            //return dataStoreProvider;
        }

        private void BPIWABKMobileApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
            //string message = "The application cannot connect to the specified database.";

            //if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
            //             message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            //         }
            //throw new InvalidOperationException(message);
        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule();
            this.module3 = new BPIWABK.Module.BPIWABKModule();
            this.module4 = new BPIWABK.Module.Mobile.BPIWABKMobileModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.cloneObjectModule = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            this.cloneObjectMobileModule = new DevExpress.ExpressApp.CloneObject.Mobile.CloneObjectMobileModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.conditionalAppearanceMobileModule = new DevExpress.ExpressApp.ConditionalAppearance.Mobile.ConditionalAppearanceMobileModule();
            this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
            this.fileAttachmentsMobileModule = new DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule();
            this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsMobileModuleV2 = new DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationMobileModule = new DevExpress.ExpressApp.Validation.Mobile.ValidationMobileModule();
            this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // auditTrailModule
            // 
            this.auditTrailModule.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            // 
            // dashboardsModule
            // 
            this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
            // 
            // pivotChartModuleBase
            // 
            this.pivotChartModuleBase.DataAccessMode = DevExpress.ExpressApp.CollectionSourceDataAccessMode.Client;
            this.pivotChartModuleBase.ShowAdditionalNavigation = false;
            // 
            // reportsModuleV2
            // 
            this.reportsModuleV2.EnableInplaceReports = true;
            this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(BPIWABK.Module.BusinessObjects.Administrative.Peran);
            this.securityStrategyComplex1.UserType = typeof(BPIWABK.Module.BusinessObjects.Administrative.Pegawai);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // BPIWABKMobileApplication
            // 
            this.ApplicationName = "BPIWABK";
            this.Modules.Add(this.module1);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.auditTrailModule);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.cloneObjectModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.dashboardsModule);
            this.Modules.Add(this.pivotChartModuleBase);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.reportsModuleV2);
            this.Modules.Add(this.treeListEditorsModuleBase);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.cloneObjectMobileModule);
            this.Modules.Add(this.conditionalAppearanceMobileModule);
            this.Modules.Add(this.fileAttachmentsMobileModule);
            this.Modules.Add(this.reportsMobileModuleV2);
            this.Modules.Add(this.validationMobileModule);
            this.Modules.Add(this.module4);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.BPIWABKMobileApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
