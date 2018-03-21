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
using DevExpress.Persistent.Base.General;
using BPIWABK.Module.BusinessObjects.Administrative;
using BPIWABK.Module.BusinessObjects.Master;

namespace BPIWABK.Module.BusinessObjects.Reference
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Nama")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class UnitKerja : XPLiteObject, ITreeNode
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UnitKerja(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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

        UnitKerja induk;
        [Association]
        [VisibleInListView(false)]
        public UnitKerja Induk
        {
            get => induk;
            set => SetPropertyValue(nameof(Induk), ref induk, value);
        }

        Pegawai pejabat;
        public Pegawai Pejabat
        {
            get => pejabat;
            set => SetPropertyValue(nameof(Pejabat), ref pejabat, value);
        }

        [PersistentAlias("UraianTugas.Sum([WaktuMenit])")]
        public int JumlahWaktuKerja
        {
            get
            {
                int jumlahWaktuKerja = 0;
                if (UraianTugas.Count > 0)
                    jumlahWaktuKerja = (int)EvaluateAlias(nameof(JumlahWaktuKerja));
                return jumlahWaktuKerja;
            }
        }

        [PersistentAlias("SOPTerkait.Count()")]
        [ModelDefault("Caption", "Jumlah SOP Terkait")]
        public int JumlahSOPTerkait
        {
            get
            {
                return (int)EvaluateAlias(nameof(JumlahSOPTerkait));
            }
        }

        [ModelDefault("Caption", "SOP Terkait")]
        public XPCollection<SOP> SOPTerkait
        {
            get => new XPCollection<SOP>(Session, CriteriaOperator.Parse("[Kegiatan][[PelaksanaKegiatan.Kode] = ?]", Kode));
        }

        public XPCollection<Kegiatan> UraianTugas
        {
            get => new XPCollection<Kegiatan>(Session, CriteriaOperator.Parse("[PelaksanaKegiatan.Kode] = ?", Kode));
        }

        [Association]
        public XPCollection<UnitKerja> SubUnit
        {
            get
            {
                return GetCollection<UnitKerja>(nameof(SubUnit));
            }
        }

        IBindingList ITreeNode.Children
        {
            get => SubUnit;
        }

        ITreeNode ITreeNode.Parent
        {
            get => Induk;
        }

        string ITreeNode.Name
        {
            get => Nama;
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
}