﻿using System;
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
            //if (SOP != null && SOP.PemilikSOP != null)
            //{
            //    UnitKerja satuanTugas = Session.GetObjectByKey<UnitKerja>(SOP.PemilikSOP.Kode);
            //}
            AlurKegiatan alurKegiatan = new AlurKegiatan(this.Session);
            alurKegiatan.JenisAlur = JenisAlur.Lanjut;
            AlurKegiatan = alurKegiatan;
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
            get => oid;
            set => SetPropertyValue(nameof(Oid), ref oid, value);
        }

        int urutan;
        [RuleValueComparison(ValueComparisonType.GreaterThan,0)]
        public int Urutan
        {
            get => urutan;
            set => SetPropertyValue(nameof(Urutan), ref urutan, value);
        }

        SOP sOP;
        [Association("SOP-Kegiatan")]
        public SOP SOP
        {
            get => sOP;
            set
            {
                SetPropertyValue(nameof(SOP), ref sOP, value);
                if (!IsLoading && this.Session.IsNewObject(this))
                {
                    PelaksanaKegiatan = Session.GetObjectByKey<UnitKerja>(value.PemilikSOP.Kode);
                    Urutan = value.Kegiatan.Count;
                }
            }
        }

        public int JumlahOutput
        {
            get
            {
                int jumlahOutput = 0;
                if (Output != null) {
                    string outputString = Output;
                    string[] outputData = outputString.Split(';');
                    jumlahOutput = outputData.Length;
                }
                return jumlahOutput;
            }
        }

        String input;
        [Size(SizeAttribute.Unlimited)]
        public String Input
        {
            get => input;
            set => SetPropertyValue(nameof(Input), ref input, value);
        }

        String output;
        [Size(SizeAttribute.Unlimited)]
        public String Output
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

        UnitKerja pelaksanaKegiatan;
        public UnitKerja PelaksanaKegiatan
        {
            get
            {
                return pelaksanaKegiatan;
            }
            set
            {
                SetPropertyValue("PelaksanaKegiatan", ref pelaksanaKegiatan, value);
            }
        }

        int waktu;
        [VisibleInListView(false)]
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

        string waktuKegiatan;
        [VisibleInDetailView(false)]
        public string WaktuKegiatan
        {
            get
            {
                switch (SatuanWaktu)
                {
                    case SatuanWaktu.Minggu:
                        waktuKegiatan = string.Format("{0} Minggu", Waktu);
                        break;
                    case SatuanWaktu.Bulan:
                        waktuKegiatan = string.Format("{0} Bulan", Waktu);
                        break;
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
                    case SatuanWaktu.Minggu:
                        waktuMenit = Waktu * 10080;
                        break;
                    case SatuanWaktu.Bulan:
                        waktuMenit = Waktu * 43800;
                        break;
                    default:
                        return 0;
                }

                return waktuMenit;
            }
        }

        SatuanWaktu satuanWaktu;
        [VisibleInListView(false)]
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

        AlurKegiatan alurKegiatan;
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), VisibleInListView(true)]
        public AlurKegiatan AlurKegiatan
        {
            get => alurKegiatan;
            set => SetPropertyValue(nameof(AlurKegiatan), ref alurKegiatan, value);
        }
    }
}