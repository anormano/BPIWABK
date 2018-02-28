using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [ImageName("BO_Role")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Peran : PermissionPolicyRole
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Peran(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        public bool CanExport
        {
            get { return GetPropertyValue<bool>("CanExport"); }
            set { SetPropertyValue<bool>("CanExport", value); }
        }
    }

    public class ExportPermission : IOperationPermission
    {
        public string Operation
        {
            get { return "Export"; }
        }
    }

    [Serializable]
    public class ExportPermissionRequest : IPermissionRequest
    {
        public object GetHashObject()
        {
            return this.GetType().FullName;
        }
    }

    public class ExportPermissionRequestProcessor :
    PermissionRequestProcessorBase<ExportPermissionRequest>
    {
        private IPermissionDictionary permissions;
        public ExportPermissionRequestProcessor(IPermissionDictionary permissions)
        {
            this.permissions = permissions;
        }
        public override bool IsGranted(ExportPermissionRequest permissionRequest)
        {
            return (permissions.FindFirst<ExportPermission>() != null);
        }
    }

    public class SecuredExportController : ViewController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ExportController controller = Frame.GetController<ExportController>();
            if (controller != null)
            {
                controller.ExportAction.Executing += ExportAction_Executing;
                if (SecuritySystem.Instance is IRequestSecurity)
                {
                    controller.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ExportPermissionRequest()));
                }
            }
        }
        void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SecuritySystem.Demand(new ExportPermissionRequest());
        }
    }
}