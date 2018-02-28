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

namespace BPIWABK.Module.BusinessObjects.Reference
{
    [DefaultClassOptions]
    [DefaultProperty("Pengguna")]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PenggunaSOP : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PenggunaSOP(Session session)
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

        string pengguna;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Pengguna
        {
            get
            {

                pengguna = null;
                if (KementerianPUPR)
                    pengguna += "Kementerian PUPR, ";
                if (InternalUnit)
                    pengguna += "Internal, ";
                if (Publik)
                    pengguna += "Publik, ";

                if (!string.IsNullOrEmpty(pengguna))
                    pengguna = pengguna.Substring(0, pengguna.LastIndexOf(", "));

                return pengguna;
            }
        }

        bool kementerianPUPR;
        [ImmediatePostData]
        public bool KementerianPUPR
        {
            get => kementerianPUPR;
            set => SetPropertyValue(nameof(KementerianPUPR), ref kementerianPUPR, value);
        }

        bool internalUnit;
        [ModelDefault("Caption", "Internal")]
        [ImmediatePostData]
        public bool InternalUnit
        {
            get => internalUnit;
            set => SetPropertyValue(nameof(InternalUnit), ref internalUnit, value);
        }

        bool publik;
        [ImmediatePostData]
        public bool Publik
        {
            get => publik;
            set => SetPropertyValue(nameof(Publik), ref publik, value);
        }
    }
}