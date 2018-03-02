using System;
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
using DevExpress.ExpressApp.Editors;

namespace BPIWABK.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    [ImageName("BO_Task")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly,true, NewItemRowPosition.Top)]
    [NavigationItem(false)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Kegiatan : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kegiatan(Session session)
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
        [Key(true)]
        [RuleRequiredField]
        public int Oid
        {
            get
            {
                return oid;
            }
            set
            {
                SetPropertyValue("Oid", ref oid, value);
            }
        }

        int urutan;
        [RuleValueComparison(ValueComparisonType.GreaterThan,0)]
        public int Urutan
        {
            get
            {
                return urutan;
            }
            set
            {
                SetPropertyValue("Urutan", ref urutan, value);
            }
        }

        SOP sOP;
        [Association("SOP-Kegiatan")]
        public SOP SOP
        {
            get
            {
                return sOP;
            }
            set
            {
                SetPropertyValue("SOP", ref sOP, value);
            }
        }

        InputKegiatan input;
        public InputKegiatan Input
        {
            get
            {
                return input;
            }
            set
            {
                SetPropertyValue("Input", ref input, value);
            }
        }

        OutputKegiatan output;
        public OutputKegiatan Output
        {
            get
            {
                return output;
            }
            set
            {
                SetPropertyValue("Output", ref output, value);
            }
        }


        string deskripsiKegiatan;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string DeskripsiKegiatan
        {
            get
            {
                return deskripsiKegiatan;
            }
            set
            {
                SetPropertyValue("DeskripsiKegiatan", ref deskripsiKegiatan, value);
            }
        }

        UnitKerja pelaksanaKerja;
        public UnitKerja PelaksanaKerja
        {
            get
            {
                return pelaksanaKerja;
            }
            set
            {
                SetPropertyValue("PelaksanaKerja", ref pelaksanaKerja, value);
            }
        }

        int waktu;
        public int Waktu
        {
            get
            {
                return waktu;
            }
            set
            {
                SetPropertyValue("Waktu", ref waktu, value);
            }
        }

        public string waktuKegiatan;
        public string WaktuKegiatan
        {
            get
            {
                switch (SatuanWaktu)
                {
                    case SatuanWaktu.Hari:
                        waktuKegiatan = string.Format("{0} Hari Kerja", Waktu);
                        break;
                    case SatuanWaktu.Jam:
                        waktuKegiatan = string.Format("{0} Jam", Waktu);
                        break;
                    case SatuanWaktu.Menit:
                        waktuKegiatan = string.Format("{0} Menit", Waktu);
                        break;
                    default:
                        waktuKegiatan = string.Empty;
                        break;
                }

                return waktuKegiatan;
            }
        }

        int waktuMenit;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int WaktuMenit
        {
            get
            {
                switch (SatuanWaktu)
                {
                    case SatuanWaktu.Hari:
                        waktuMenit = Waktu * 1440;
                        break;
                    case SatuanWaktu.Jam:
                        waktuMenit = Waktu * 60;
                        break;
                    case SatuanWaktu.Menit:
                        waktuMenit = Waktu * 1;
                        break;
                    default:
                        return 0;
                }

                return waktuMenit;
            }
        }

        SatuanWaktu satuanWaktu;
        public SatuanWaktu SatuanWaktu
        {
            get => satuanWaktu;
            set => SetPropertyValue(nameof(SatuanWaktu), ref satuanWaktu, value);
        }


        string keterangan;
        [Size(SizeAttribute.Unlimited)]
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
    }
}