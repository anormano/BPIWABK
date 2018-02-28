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

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Contract")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SKPengangkatan : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SKPengangkatan(Session session)
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

        string nOSK;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ModelDefault("Caption", "No. SK")]
        [RuleRequiredField]
        public string NOSK
        {
            get => nOSK;
            set => SetPropertyValue(nameof(NOSK), ref nOSK, value);
        }
        Pegawai pegawai;
        [Association("Pegawai-SK")]
        public Pegawai Pegawai
        {
            get => pegawai;
            set => SetPropertyValue(nameof(Pegawai), ref pegawai, value);
        }

        int tahun;
        [RuleRequiredField]
        public int Tahun
        {
            get => tahun;
            set => SetPropertyValue(nameof(Tahun), ref tahun, value);
        }

        MediaDataObject suratSK;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorCustomHeight = 100,
            DetailViewImageEditorFixedHeight = 100,
            DetailViewImageEditorFixedWidth = 150)]
        public MediaDataObject SuratSK
        {
            get => suratSK;
            set => SetPropertyValue(nameof(SuratSK), ref suratSK, value);
        }
    }
}