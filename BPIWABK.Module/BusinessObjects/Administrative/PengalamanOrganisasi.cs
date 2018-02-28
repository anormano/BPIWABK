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
    [NavigationItem(false)]
    [CreatableItem(false)]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Bottom)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PengalamanOrganisasi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PengalamanOrganisasi(Session session)
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

        Pegawai pegawai;
        [Association("Pegawai-RiwayatPengalamanOrganisasi")]
        public Pegawai Pegawai
        {
            get => pegawai;
            set => SetPropertyValue(nameof(Pegawai), ref pegawai, value);
        }

        string namaOrganisasi;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string NamaOrganisasi
        {
            get => namaOrganisasi;
            set => SetPropertyValue(nameof(NamaOrganisasi), ref namaOrganisasi, value);
        }

        string posisi;
        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Posisi
        {
            get => posisi;
            set => SetPropertyValue(nameof(Posisi), ref posisi, value);
        }

        NamaBulan bulanMulai;
        public NamaBulan BulanMulai
        {
            get => bulanMulai;
            set => SetPropertyValue(nameof(BulanMulai), ref bulanMulai, value);
        }

        int tahunMulai;
        public int TahunMulai
        {
            get => tahunMulai;
            set => SetPropertyValue(nameof(TahunMulai), ref tahunMulai, value);
        }

        NamaBulan bulanAkhir;
        public NamaBulan BulanAkhir
        {
            get => bulanAkhir;
            set => SetPropertyValue(nameof(BulanAkhir), ref bulanAkhir, value);
        }

        int tahunSelesai;
        public int TahunSelesai
        {
            get => tahunSelesai;
            set => SetPropertyValue(nameof(TahunSelesai), ref tahunSelesai, value);
        }
    }
}