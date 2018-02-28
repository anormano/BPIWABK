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
    //[ImageName("BO_Contact")]
    [DefaultProperty("Nama")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Kabupaten : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kabupaten(Session session)
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
        int code;
        [Key(AutoGenerate = true)]
        public int Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }

        string nama;
        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Nama
        {
            get => nama;
            set => SetPropertyValue(nameof(Nama), ref nama, value);
        }

        Propinsi propinsi;
        [Association("Propinsi-Kabupaten")]
        [RuleRequiredField]
        public Propinsi Propinsi
        {
            get => propinsi;
            set => SetPropertyValue(nameof(Propinsi), ref propinsi, value);
        }

        [Association("Kabupaten-Kecamatan")]
        public XPCollection<Kecamatan> Kecamatan
        {
            get => GetCollection<Kecamatan>(nameof(Kecamatan));
        }
    }
}