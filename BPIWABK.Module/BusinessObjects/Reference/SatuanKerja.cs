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
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Editors;
using BPIWABK.Module.BusinessObjects.Master;
using BPIWABK.Module.BusinessObjects.Administrative;

namespace BPIWABK.Module.BusinessObjects.Reference
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Nama")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public abstract class SatuanKerja : XPLiteObject, ITreeNode
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SatuanKerja(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected abstract ITreeNode Induk
        {
            get;
        }

        protected abstract IBindingList Satuan
        {
            get;
        }

        string kode;
        [Size(20)]
        [RuleRequiredField]
        [RuleUniqueValue]
        [Key]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Kode
        {
            get => kode;
            set => SetPropertyValue(nameof(Kode), ref kode, value);
        }

        string nama;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Nama
        {
            get => nama;
            set => SetPropertyValue(nameof(Nama), ref nama, value);
        }

        Pegawai pejabat;
        public Pegawai Pejabat
        {
            get => pejabat;
            set => SetPropertyValue(nameof(Pejabat), ref pejabat, value);
        }

        string deskripsi;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Deskripsi
        {
            get => deskripsi;
            set => SetPropertyValue(nameof(Deskripsi), ref deskripsi, value);
        }

        public XPCollection<SOP> SOP
        {
            get => new XPCollection<SOP>(Session, CriteriaOperator.Parse("[Kegiatan][[PelaksanaKegiatan.Kode] = ?]", Kode));
        }


        public XPCollection<Kegiatan> Kegiatan
        {
            get => new XPCollection<Kegiatan>(Session, CriteriaOperator.Parse("PelaksanaKegiatan.Kode=?", Kode));

        }


        #region ITreeNode
        IBindingList ITreeNode.Children => Satuan;
        ITreeNode ITreeNode.Parent => Induk;
        string ITreeNode.Name => Nama;
        #endregion
    }

    public class Kementerian : SatuanKerja
    {
        public Kementerian(Session session) : base(session) { }
        public Kementerian(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }
        protected override ITreeNode Induk => null;
        protected override IBindingList Satuan => EselonI;
        [Association("Kementerian-EselonI"), Aggregated]
        public XPCollection<EselonI> EselonI => GetCollection<EselonI>(nameof(EselonI));
    }

    public class EselonI : SatuanKerja
    {
        public EselonI(Session session) : base(session) { }
        public EselonI(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }
        protected override ITreeNode Induk => kementerian;
        protected override IBindingList Satuan => EselonII;
        Kementerian kementerian;
        [Association("Kementerian-EselonI")]
        public Kementerian Kementerian
        {
            get => kementerian;
            set => SetPropertyValue(nameof(Kementerian), ref kementerian, value);
        }
        [Association("EselonI-EselonII"), Aggregated]
        public XPCollection<EselonII> EselonII => GetCollection<EselonII>(nameof(EselonII));
    }

    public class EselonII : SatuanKerja
    {
        EselonI eselonI;
        protected override ITreeNode Induk => eselonI;
        protected override IBindingList Satuan => EselonIII;
        public EselonII(Session session) : base(session) { }
        public EselonII(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }
        [Association("EselonI-EselonII")]
        public EselonI EselonI
        {
            get => eselonI;
            set => SetPropertyValue(nameof(EselonI), ref eselonI, value);
        }
        [Association("EselonII-EselonIII"), Aggregated]
        public XPCollection<EselonIII> EselonIII => GetCollection<EselonIII>(nameof(EselonIII));
    }

    public class EselonIII : SatuanKerja
    {
        EselonII eselonII;
        protected override ITreeNode Induk => eselonII;
        protected override IBindingList Satuan => EselonIV;
        public EselonIII(Session session) : base(session) { }
        public EselonIII(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }
        [Association("EselonII-EselonIII")]
        public EselonII EselonII
        {
            get => eselonII;
            set => SetPropertyValue(nameof(EselonII), ref eselonII, value);
        }
        [Association("EselonIII-EselonIV"), Aggregated]
        public XPCollection<EselonIV> EselonIV => GetCollection<EselonIV>(nameof(EselonIV));
    }

    public class EselonIV : SatuanKerja
    {
        EselonIII eselonIII;
        protected override ITreeNode Induk => eselonIII;
        protected override IBindingList Satuan => StafPelaksana;
        public EselonIV(Session session) : base(session) { }
        public EselonIV(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }

        [Association("EselonIII-EselonIV")]
        public EselonIII EselonIII
        {
            get => eselonIII;
            set => SetPropertyValue(nameof(EselonIII), ref eselonIII, value);
        }

        [Association("EselonIV-EselonV"), Aggregated]
        public XPCollection<StafPelaksana> StafPelaksana => GetCollection<StafPelaksana>(nameof(StafPelaksana));
    }

    public class StafPelaksana : SatuanKerja
    {
        EselonIV eselonIV;
        protected override ITreeNode Induk => eselonIV;
        protected override IBindingList Satuan => new BindingList<Object>();
        public StafPelaksana(Session session) : base(session) { }
        public StafPelaksana(Session session, string nama) : base(session)
        {
            this.Nama = nama;
        }

        [Association("EselonIV-EselonV")]
        public EselonIV EselonIV
        {
            get => eselonIV;
            set => SetPropertyValue(nameof(EselonIV), ref eselonIV, value);
        }
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
}
