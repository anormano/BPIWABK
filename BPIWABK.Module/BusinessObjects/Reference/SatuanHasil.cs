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
using DevExpress.ExpressApp.Editors;

namespace BPIWABK.Module.BusinessObjects.Reference
{
    [DefaultClassOptions]
    [NavigationItem("Referensi")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Nama")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SatuanHasil : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SatuanHasil(Session session)
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

        int oid;
        [RuleUniqueValue]
        [Key(true)]
        public int Oid
        {
            get => oid;
            set => SetPropertyValue(nameof(Oid), ref oid, value);
        }

        string nama;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Nama
        {
            get => nama;
            set => SetPropertyValue(nameof(Nama), ref nama, value);
        }

        string keterangan;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Keterangan
        {
            get => keterangan;
            set => SetPropertyValue(nameof(Keterangan), ref keterangan, value);
        }
    }
}