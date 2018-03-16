using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using BPIWABK.Module.BusinessObjects.Reference;
using BPIWABK.Module.BusinessObjects.Administrative;

namespace BPIWABK.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    [ImageName("BO_StateMachine")]
    [DefaultProperty("NamaSOP")]
    [FileAttachment("BerkasSOP")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem("Menu Utama")]
    public class SOP : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SOP(Session session)
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
        protected override void OnSaved()
        {
            base.OnSaved();
        }
        string nomorSOP;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue]
        [RuleRequiredField]
        [Key(false)]
        public string NomorSOP
        {
            get => nomorSOP;
            set => SetPropertyValue(nameof(NomorSOP), ref nomorSOP, value);
        }
        string namaSOP;
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "1")]
        [VisibleInListView(true)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [RuleRequiredField]
        public string NamaSOP
        {
            get => namaSOP;
            set => SetPropertyValue(nameof(NamaSOP), ref namaSOP, value);
        }

        SatuanHasil satuanHasil;
        [VisibleInListView(false)]
        public SatuanHasil SatuanHasil
        {
            get => satuanHasil;
            set => SetPropertyValue(nameof(SatuanHasil), ref satuanHasil, value);
        }

        TugasFungsi tugasFungsi;
        [Association("TugasFungsi-SOP")]
        [RuleRequiredField]
        [VisibleInListView(false)]
        public TugasFungsi TugasFungsi
        {
            get
            {
                return tugasFungsi;
            }
            set
            {
                SetPropertyValue("TugasFungsi", ref tugasFungsi, value);
            }
        }

        DateTime tanggalPembuatan;
        [VisibleInListView(false)]
        public DateTime TanggalPembuatan
        {
            get
            {
                return tanggalPembuatan;
            }
            set
            {
                SetPropertyValue("TanggalPembuatan", ref tanggalPembuatan, value);
            }
        }

        DateTime tanggalRevisi;
        [VisibleInListView(false)]
        public DateTime TanggalRevisi
        {
            get
            {
                return tanggalRevisi;
            }
            set
            {
                SetPropertyValue("TanggalRevisi", ref tanggalRevisi, value);
            }
        }

        DateTime tanggalBerlaku;
        [VisibleInListView(false)]
        public DateTime TanggalBerlaku
        {
            get
            {
                return tanggalBerlaku;
            }
            set
            {
                SetPropertyValue("TanggalBerlaku", ref tanggalBerlaku, value);
            }
        }

        PenggunaSOP pengguna;
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), VisibleInListView(false)]
        public PenggunaSOP Pengguna
        {
            get
            {
                return pengguna;
            }
            set
            {
                SetPropertyValue("Pengguna", ref pengguna, value);
            }
        }

        UnitKerja pemilikSOP;
        [RuleRequiredField]
        public UnitKerja PemilikSOP
        {
            get => pemilikSOP;
            set => SetPropertyValue(nameof(PemilikSOP), ref pemilikSOP, value);
        }

        [Association("SOP-Kegiatan"), Aggregated]
        public XPCollection<Kegiatan> Kegiatan
        {
            get
            {
                return GetCollection<Kegiatan>("Kegiatan");
            }
        }

        string keterangan;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                SetPropertyValue("Keterangan", ref keterangan, value);
            }
        }

        [Association("SOP-Peralatan")]
        public XPCollection<Alat> Peralatan
        {
            get
            {
                return GetCollection<Alat>("Peralatan");
            }
        }

        //[Association("SOP-KorelasiJabatan")]
        //public XPCollection<SatuanTugas> KorelasiJabatan
        //{
        //    get
        //    {
        //        return GetCollection<SatuanTugas>("KorelasiJabatan");
        //    }
        //}

        [VisibleInListView(false)]
        [PersistentAlias("Kegiatan.Sum(JumlahOutput)")]
        public int JumlahOutput
        {
            get
            {
                if (Kegiatan.Count > 0)
                {
                    return (int)EvaluateAlias(nameof(JumlahOutput));
                } else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("Kegiatan.Sum(WaktuMenit)")]
        public int? TotalWaktu
        {
            get
            {
                return (int?)EvaluateAlias("TotalWaktu");
            }
        }

        [PersistentAlias("Kegiatan.Count()")]
        [VisibleInListView(false)]
        public int JumlahKegiatan
        {
            get
            {
                return Convert.ToInt16(EvaluateAlias("JumlahKegiatan"));
            }
        }

        [VisibleInListView(false)]
        public int WaktuKerjaEfektif
        {
            get
            {
                Setelan setelan = Session.FindObject<Setelan>(null);
                return setelan.WaktuKerjaEfektif;
            }
        }

        FileData berkasSOP;
        [VisibleInListView(false)]
        public FileData BerkasSOP
        {
            get
            {
                return berkasSOP;
            }
            set
            {
                SetPropertyValue("BerkasSOP", ref berkasSOP, value);
            }
        }

        [Association("SOP-DokumenTerkait"), Aggregated]
        public XPCollection<DokumenTerkait> DokumenTerkait
        {
            get
            {
                return GetCollection<DokumenTerkait>(nameof(DokumenTerkait));
            }
        }
    }
}