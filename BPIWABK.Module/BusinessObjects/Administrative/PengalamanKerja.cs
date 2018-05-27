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

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [ImageName("BO_Customer")]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PengalamanKerja : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PengalamanKerja(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            BulanMulai = NamaBulan.Januari;
            BulanSelesai = NamaBulan.Januari;
            TahunMulai = DateTime.Now.Year;
            TahunSelesai = DateTime.Now.Year;
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

        Pegawai pegawai;
        [Association("Pegawai-RiwayatPengalamanKerja")]
        public Pegawai Pegawai
        {
            get => pegawai;
            set => SetPropertyValue(nameof(Pegawai), ref pegawai, value);
        }
        string namaPerusahaan;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string NamaPerusahaan
        {
            get => namaPerusahaan;
            set => SetPropertyValue(nameof(NamaPerusahaan), ref namaPerusahaan, value);
        }

        string posisi;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Posisi
        {
            get => posisi;
            set => SetPropertyValue(nameof(Posisi), ref posisi, value);
        }

        NamaBulan bulanMulai;
        [RuleRequiredField]
        public NamaBulan BulanMulai
        {
            get => bulanMulai;
            set => SetPropertyValue(nameof(BulanMulai), ref bulanMulai, value);
        }

        int tahunMulai;
        [RuleRequiredField]
        [ModelDefault("EditMask", "D")]
        [ModelDefault("DisplayFormat", "D")]
        public int TahunMulai
        {
            get => tahunMulai;
            set => SetPropertyValue(nameof(TahunMulai), ref tahunMulai, value);
        }

        NamaBulan bulanSelesai;
        public NamaBulan BulanSelesai
        {
            get => bulanSelesai;
            set => SetPropertyValue(nameof(BulanSelesai), ref bulanSelesai, value);
        }

        int tahunSelesai;
        [ModelDefault("EditMask", "D")]
        [ModelDefault("DisplayFormat", "D")]
        public int TahunSelesai
        {
            get => tahunSelesai;
            set => SetPropertyValue(nameof(TahunSelesai), ref tahunSelesai, value);
        }

        MediaDataObject referensi;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit,
            ListViewImageEditorCustomHeight = 100,
            DetailViewImageEditorFixedHeight = 100,
            DetailViewImageEditorFixedWidth = 150)]
        public MediaDataObject Referensi
        {
            get => referensi;
            set => SetPropertyValue(nameof(Referensi), ref referensi, value);
        }
    }
}