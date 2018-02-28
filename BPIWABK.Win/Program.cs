using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security.ClientServer.Remoting;

namespace BPIWABK.Win
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
         if (Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
         {
            Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
         }
         Tracing.Initialize();
         BPIWABKWindowsFormsApplication winApplication = new BPIWABKWindowsFormsApplication();
         // Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
         //winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
         SecurityAdapterHelper.Enable();
         string connectionString = "tcp://localhost:8082/DataServer";
#if DEBUG
         connectionString = "tcp://127.0.0.1:8082/DataServer";
#endif
         try
         {
            Hashtable t = new Hashtable();
            t.Add("secure", true);
            t.Add("tokenImpersonationLevel", "impersonation");
            //t.Add("username", "ConsoleUser");
            //t.Add("password", "password");
            //if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Username"]) == false)
            //{
            //    t.Add("username", ConfigurationManager.AppSettings["Username"]);
            //}
            //if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Password"]) == false)
            //{
            //    t.Add("password", ConfigurationManager.AppSettings["Password"]);
            //}
            TcpChannel channel = new TcpChannel(t, null, null);
            ChannelServices.RegisterChannel(channel, true);
            IDataServer clientDataServer = (IDataServer)Activator.GetObject(typeof(RemoteSecuredDataServer), connectionString);
            ServerSecurityClient securityClient = new ServerSecurityClient(clientDataServer, new ClientInfoFactory());
            securityClient.SupportNavigationPermissionsForTypes = false;
            securityClient.IsSupportChangePassword = true;
            winApplication.Security = securityClient;
            winApplication.CreateCustomObjectSpaceProvider += delegate (object sender, CreateCustomObjectSpaceProviderEventArgs e)
               {
                  e.ObjectSpaceProvider = new DataServerObjectSpaceProvider(clientDataServer, securityClient);
               };
            winApplication.Setup();
            winApplication.Start();
         }
         catch (Exception e)
         {
            winApplication.HandleException(e);
         }
      }
   }
}
