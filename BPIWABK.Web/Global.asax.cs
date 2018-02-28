using BPIWABK.Module.BusinessObjects.Administrative;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security.ClientServer.Remoting;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using DevExpress.Web;
using DevExpress.XtraReports.Security;
using System;
using System.Collections;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace BPIWABK.Web
{
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
            SecurityAdapterHelper.Enable();
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
            //SecurityAdapterHelper.Enable();
            //ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
            //ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            //WebApplication.EnableMultipleBrowserTabsSupport = true;
            //string connectionString = "tcp://localhost:8082/DataServer";
            //Hashtable t = new Hashtable();
            //t.Add("secure", true);
            //t.Add("tokenImpersonationLevel", "impersonation");
            ////t.Add("username", "BPIWABK\\ConsoleUser");
            ////t.Add("password", "password");
            //TcpChannel channel = new TcpChannel(t, null, null);
            //ChannelServices.RegisterChannel(channel, true);
            //this.Application["DataServer"] = Activator.GetObject(typeof(RemoteSecuredDataServer), connectionString);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Tracing.Initialize();
            WebApplication.SetInstance(Session, new BPIWABKAspNetApplication());
            WebApplication.Instance.MaxLogonAttemptCount = 5;
            WebApplication.Instance.Settings.LogonTemplateContentPath = "CustomLogonTemplateContent.ascx";
            WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath = "CustomDefaultVerticalTemplateContent.ascx";
            DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            WebApplication.Instance.SwitchToNewStyle();
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
            //Tracing.Initialize();
            //WebApplication.SetInstance(Session, new BPIWABKAspNetApplication());
            //IDataServer clientDataServer = (IDataServer)this.Application["DataServer"];
            //ServerSecurityClient securityClient = new ServerSecurityClient(clientDataServer, new ClientInfoFactory());

            //securityClient.SupportNavigationPermissionsForTypes = false;
            //securityClient.IsSupportChangePassword = true;
            //MyDetailsController.CanGenerateMyDetailsNavigationItem = false;
            //WebApplication.Instance.Security = securityClient;
            //WebApplication.Instance.CreateCustomObjectSpaceProvider +=
            //    delegate (object _sender, CreateCustomObjectSpaceProviderEventArgs args)
            //    {
            //        args.ObjectSpaceProvider = new DataServerObjectSpaceProvider(
            //            clientDataServer, securityClient);
            //    };
            ////DevExpress.ExpressApp.Web.Templates.ActionContainers.NavigationActionContainer.ShowImages = true;
            ////DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            //WcfDataServerHelper.AddKnownType(typeof(ExportPermissionRequest));
            //WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath = "CustomDefaultVerticalTemplateContent.ascx";
            //WebApplication.Instance.Settings.LogonTemplateContentPath = "CustomLogonTemplateContent.ascx";
            //WebApplication.Instance.SwitchToNewStyle();
            //WebApplication.Instance.Setup();
            //WebApplication.Instance.Start();
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            ErrorHandling.Instance.ProcessApplicationError();
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }

        protected void Application_End(Object sender, EventArgs e)
        {
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion Web Form Designer generated code
    }
}