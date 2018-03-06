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
    [DefaultProperty("Nama")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [RuleCriteria("SD or SMP or SMA or D1 or D2 or D3 or D4 or S1 or S2 or S3", InvertResult = false, ResultType = ValidationResultType.Error, UsedProperties = "SD, SMP, SMA, D1, D2, D3, D4, S1, S2, S3", CustomMessageTemplate = "Setidaknya satu jenjang pendidikan harus dipilih")]
    public class LembagaPendidikan : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public LembagaPendidikan(Session session)
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
        string nama;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [RuleUniqueValue]
        public string Nama
        {
            get => nama;
            set => SetPropertyValue(nameof(Nama), ref nama, value);
        }


        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Jenjang
        {
            get
            {
                string listJenjang = null;
                if (SD) listJenjang += "SD, ";
                if (SMP) listJenjang += "SMP, ";
                if (SMA) listJenjang += "SMA, ";
                if (D1) listJenjang += "D1, ";
                if (D2) listJenjang += "D2, ";
                if (D3) listJenjang += "D3, ";
                if (D4) listJenjang += "D4, ";
                if (S1) listJenjang += "S1, ";
                if (S2) listJenjang += "S2, ";
                if (S3) listJenjang += "S3, ";

                return listJenjang.Substring(0, listJenjang.Length - 2);
            }
        }

        bool sD;
        public bool SD
        {
            get => sD;
            set => SetPropertyValue(nameof(SD), ref sD, value);
        }

        bool sMP;
        public bool SMP
        {
            get => sMP;
            set => SetPropertyValue(nameof(SMP), ref sMP, value);
        }

        bool sMA;
        public bool SMA
        {
            get => sMA;
            set => SetPropertyValue(nameof(SMA), ref sMA, value);
        }

        bool d1;
        public bool D1
        {
            get => d1;
            set => SetPropertyValue(nameof(D1), ref d1, value);
        }

        bool d2;
        public bool D2
        {
            get => d2;
            set => SetPropertyValue(nameof(D2), ref d2, value);
        }

        bool d3;
        public bool D3
        {
            get => d3;
            set => SetPropertyValue(nameof(D3), ref d3, value);
        }

        bool d4;
        public bool D4
        {
            get => d4;
            set => SetPropertyValue(nameof(D4), ref d4, value);
        }

        bool s1;
        public bool S1
        {
            get => s1;
            set => SetPropertyValue(nameof(S1), ref s1, value);
        }

        bool s2;
        public bool S2
        {
            get => s2;
            set => SetPropertyValue(nameof(S2), ref s2, value);
        }

        bool s3;
        public bool S3
        {
            get => s3;
            set => SetPropertyValue(nameof(S3), ref s3, value);
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