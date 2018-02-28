using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.ExpressApp.Security.ClientServer;
using System.Configuration;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using DevExpress.ExpressApp.Security.ClientServer.Remoting;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.MiddleTier;
using BPIWABK.Module.BusinessObjects.Administrative;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;

namespace BPIWABK.ApplicationServer
{
    class Program
    {
        private static void serverApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
            e.Updater.Update();
            e.Handled = true;
        }
        private static void serverApplication_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e)
        {
            e.ObjectSpaceProvider = new XPObjectSpaceProvider(e.ConnectionString, e.Connection);
        }
        static void Main(string[] args)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                ValueManager.ValueManagerType = typeof(MultiThreadValueManager<>).GetGenericTypeDefinition();

                ServerApplication serverApplication = new ServerApplication();
                serverApplication.ApplicationName = "BPIWABK";
                serverApplication.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
                if (System.Diagnostics.Debugger.IsAttached && serverApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
                {
                    serverApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                }

                serverApplication.Modules.BeginInit();
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Security.SecurityModule());
                serverApplication.Modules.Add(new BPIWABK.Module.BPIWABKModule());
                serverApplication.Modules.Add(new BPIWABK.Module.Win.BPIWABKWindowsFormsModule());
                serverApplication.Modules.Add(new BPIWABK.Module.Web.BPIWABKAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.AuditTrail.AuditTrailModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Chart.ChartModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Chart.Web.ChartAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.CloneObject.CloneObjectModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Dashboards.DashboardsModule() { DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData) });
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.HtmlPropertyEditor.Win.HtmlPropertyEditorWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotChart.Win.PivotChartWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotGrid.PivotGridModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2() { ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2) });
                serverApplication.Modules.Add(new DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Validation.ValidationModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule());
                serverApplication.Modules.EndInit();

                serverApplication.DatabaseVersionMismatch += new EventHandler<DatabaseVersionMismatchEventArgs>(serverApplication_DatabaseVersionMismatch);
                serverApplication.CreateCustomObjectSpaceProvider += new EventHandler<CreateCustomObjectSpaceProviderEventArgs>(serverApplication_CreateCustomObjectSpaceProvider);

                serverApplication.ConnectionString = connectionString;

                Console.WriteLine("Mempersiapkan...");
                serverApplication.Setup();
                Console.WriteLine("Memeriksa kompatibilitas...");
                serverApplication.CheckCompatibility();
                serverApplication.Dispose();
                WcfDataServerHelper.AddKnownType(typeof(ExportPermissionRequest));
                Console.WriteLine("Memulai layanan...");
                //ServerPermissionPolicyRequestProcessor.UseAutoAssociationPermission = true;
                QueryRequestSecurityStrategyHandler securityProviderHandler = delegate ()
                {
                    SecurityStrategyComplex security = new SecurityStrategyComplex(
                        typeof(Pegawai), typeof(Peran), new AuthenticationStandard());
                    security.CustomizeRequestProcessors +=
                        delegate (object sender, CustomizeRequestProcessorsEventArgs e)
                        {
                            List<IOperationPermission> result = new List<IOperationPermission>();
                            if (security != null)
                            {
                                Pegawai user = security.User as Pegawai;
                                if (user != null)
                                {
                                    foreach (Peran role in user.Roles)
                                    {
                                        if (role.CanExport)
                                        {
                                            result.Add(new ExportPermission());
                                        }
                                    }
                                }
                            }

                            IPermissionDictionary permissionDictionary = new PermissionDictionary((IEnumerable<IOperationPermission>)result);
                            e.Processors.Add(typeof(ExportPermissionRequest), new ExportPermissionRequestProcessor(permissionDictionary));
                            
                        };
                    return security;
                };
                SecuredDataServer dataServer = new SecuredDataServer(connectionString, XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary, securityProviderHandler);
                RemoteSecuredDataServer.Initialize(dataServer);

                //"Authentication with the TCP Channel" at http://msdn.microsoft.com/en-us/library/59hafwyt(v=vs.80).aspx

                IDictionary t = new Hashtable();
                t.Add("port", 8082);
                //t.Add("secure", true);
                //t.Add("impersonate", false);

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Test"]) == false)
                {
                    Console.WriteLine("Test");
                }

                TcpChannel channel = new TcpChannel(t, null, null);
                ChannelServices.RegisterChannel(channel, true);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteSecuredDataServer), "DataServer", WellKnownObjectMode.Singleton);
                Console.WriteLine("Layanan telah dimulai...");
                Console.WriteLine("");
                Console.WriteLine("PERINGATAN: aplikasi Analisis Beban Kerja tidak dapat digunakan apabila layanan di hentikan!");
                Console.WriteLine("Apabila layanan telah di hentikan, jalankan BPIWABK.Console.exe untuk kembali memulai layanan.");
                Console.WriteLine("Tekan tombol Enter untuk menghentikan layanan.");
                Console.ReadLine();
                Console.WriteLine("Menghetikan...");
                ChannelServices.UnregisterChannel(channel);
                Console.WriteLine("Layanan telah dihentikan.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Kesalahan terjadi: " + e.Message);
                Console.WriteLine("Tekan tombol Enter untuk menutup.");
                Console.ReadLine();
            }
        }
    }
}
