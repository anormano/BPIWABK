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
using BPIWABK.Module.BusinessObjects.Reference;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [Appearance("Disabled", TargetItems = "TanggalPendaftaranSistem", Enabled = false)]
    [ImageName("BO_Education")]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]

    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PendidikanFormal : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PendidikanFormal(Session session)
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

        int tahunLulus;
        [RuleRequiredField]
        public int TahunLulus
        {
            get => tahunLulus;
            set => SetPropertyValue(nameof(TahunLulus), ref tahunLulus, value);
        }

        Pegawai pegawai;
        [Association("Pegawai-RiwayatPendidikanFormal")]
        public Pegawai Pegawai
        {
            get => pegawai;
            set => SetPropertyValue(nameof(Pegawai), ref pegawai, value);
        }

        string namaLembagaPendidikan;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string NamaLembagaPendidikan
        {
            get => namaLembagaPendidikan;
            set => SetPropertyValue(nameof(NamaLembagaPendidikan), ref namaLembagaPendidikan, value);
        }

        JenjangPendidikan jenjangPendidikan;
        [RuleRequiredField]
        public JenjangPendidikan JenjangPendidikan
        {
            get => jenjangPendidikan;
            set => SetPropertyValue(nameof(JenjangPendidikan), ref jenjangPendidikan, value);
            
        }

        Jurusan jurusan;
        [RuleRequiredField]
        public Jurusan Jurusan
        {
            get => jurusan;
            set => SetPropertyValue(nameof(Jurusan), ref jurusan, value);
        }

        double iPK;
        [ModelDefault("Caption","IPK/NEM")]
        public double IPK
        {
            get => iPK;
            set => SetPropertyValue(nameof(IPK), ref iPK, value);
        }

        MediaDataObject ijazah;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorCustomHeight = 100,
            DetailViewImageEditorFixedHeight = 100,
            DetailViewImageEditorFixedWidth = 150)]
        public MediaDataObject Ijazah
        {
            get => ijazah;
            set => SetPropertyValue(nameof(Ijazah), ref ijazah, value);
        }

        MediaDataObject transkrip;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorCustomHeight = 100,
            DetailViewImageEditorFixedHeight = 100,
            DetailViewImageEditorFixedWidth = 150)]
        public MediaDataObject Transkrip
        {
            get => transkrip;
            set => SetPropertyValue(nameof(Transkrip), ref transkrip, value);
        }
    }
}