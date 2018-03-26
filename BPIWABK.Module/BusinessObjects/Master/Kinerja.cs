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
using BPIWABK.Module.BusinessObjects.Administrative;
using BPIWABK.Module.BusinessObjects.Reference;

namespace BPIWABK.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("PeriodePenilaian")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Kinerja : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kinerja(Session session)
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
        [Association("Pegawai-PenilaianKinerja")]
        public Pegawai Pegawai
        {
            get => pegawai;
            set => SetPropertyValue(nameof(Pegawai), ref pegawai, value);
        }

        DateTime periodePenilaianMulai;
        [RuleRequiredField]
        public DateTime PeriodePenilaianMulai
        {
            get => periodePenilaianMulai;
            set => SetPropertyValue(nameof(PeriodePenilaianMulai), ref periodePenilaianMulai, value);
        }

        DateTime periodePenilaianAKhir;
        [RuleRequiredField]
        public DateTime PeriodePenilaianAKhir
        {
            get => periodePenilaianAKhir;
            set => SetPropertyValue(nameof(PeriodePenilaianAKhir), ref periodePenilaianAKhir, value);
        }

        PenilaianKinerja pemahaman;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Pemahaman terhadap pekerjaan")]
        [ImmediatePostData]
        public PenilaianKinerja Pemahaman
        {
            get => pemahaman;
            set => SetPropertyValue(nameof(Pemahaman), ref pemahaman, value);
        }

        PenilaianKinerja ketepatan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Ketepatan waktu dalam menyelesaikan tugas")]
        [ImmediatePostData]
        public PenilaianKinerja Ketepatan
        {
            get => ketepatan;
            set => SetPropertyValue(nameof(Ketepatan), ref ketepatan, value);
        }

        PenilaianKinerja kesesuaian;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Kesesuaian hasil kerja dengan yang diharapkan")]
        [ImmediatePostData]
        public PenilaianKinerja Kesesuaian
        {
            get => kesesuaian;
            set => SetPropertyValue(nameof(Kesesuaian), ref kesesuaian, value);
        }

        PenilaianKinerja kerapihan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Kerapihan pengadministrasian pekerjaan")]
        [ImmediatePostData]
        public PenilaianKinerja Kerapihan
        {
            get => kerapihan;
            set => SetPropertyValue(nameof(Kerapihan), ref kerapihan, value);
        }

        PenilaianKinerja inisiatif;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Inisiatif")]
        [ImmediatePostData]
        public PenilaianKinerja Inisiatif
        {
            get => inisiatif;
            set => SetPropertyValue(nameof(Inisiatif), ref inisiatif, value);
        }

        PenilaianKinerja kerjasama;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Kerjasama")]
        [ImmediatePostData]
        public PenilaianKinerja Kerjasama
        {
            get => kerjasama;
            set => SetPropertyValue(nameof(Kerjasama), ref kerjasama, value);
        }

        PenilaianKinerja kesopanan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Kesopanan dan keluwesan komunikasi")]
        [ImmediatePostData]
        public PenilaianKinerja Kesopanan
        {
            get => kesopanan;
            set => SetPropertyValue(nameof(Kesopanan), ref kesopanan, value);
        }

        PenilaianKinerja ketanggapan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Ketanggapan dan ketangkasan dalam melayani")]
        [ImmediatePostData]
        public PenilaianKinerja Ketanggapan
        {
            get => ketanggapan;
            set => SetPropertyValue(nameof(Ketanggapan), ref ketanggapan, value);
        }

        PenilaianKinerja perilaku;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "perilaku")]
        [ImmediatePostData]
        public PenilaianKinerja Perilaku
        {
            get => perilaku;
            set => SetPropertyValue(nameof(Perilaku), ref perilaku, value);
        }

        PenilaianKinerja kedisiplinan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Kedisiplinan")]
        [ImmediatePostData]
        public PenilaianKinerja Kedisiplinan
        {
            get => kedisiplinan;
            set => SetPropertyValue(nameof(Kedisiplinan), ref kedisiplinan, value);
        }

        PenilaianKinerja tanggungJawab;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tanggung Jawab")]
        [ImmediatePostData]
        public PenilaianKinerja TanggungJawab
        {
            get => tanggungJawab;
            set => SetPropertyValue(nameof(TanggungJawab), ref tanggungJawab, value);
        }

        PenilaianKinerja loyalitas;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Loyalitas")]
        [ImmediatePostData]
        public PenilaianKinerja Loyalitas
        {
            get => loyalitas;
            set => SetPropertyValue(nameof(Loyalitas), ref loyalitas, value);
        }

        PenilaianKinerja ketaatan;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Ketaatan terhadap instruksi kerja atasan")]
        [ImmediatePostData]
        public PenilaianKinerja Ketaatan
        {
            get => ketaatan;
            set => SetPropertyValue(nameof(Ketaatan), ref ketaatan, value);
        }

        [PersistentAlias("Pemahaman+Ketepatan+Kesesuaian+Kerapihan+Inisiatif+Kerjasama+Kesopanan+Ketanggapan+Perilaku+Kedisiplinan+TanggungJawab+Loyalitas+Ketaatan")]
        public int TotalNilai
        {
            get
            {
                return (int)EvaluateAlias(nameof(TotalNilai));
            }
        }

        string predikat;
        public string Predikat
        {
            get
            {
                predikat = string.Empty;

                if (TotalNilai >= 13 && TotalNilai <= 25)
                    predikat = "Buruk";
                else if (TotalNilai >= 25 && TotalNilai <= 33)
                    predikat = "Cukup";
                else if (TotalNilai >= 34 && TotalNilai <= 44)
                    predikat = "Baik";
                else if (TotalNilai >= 45 && TotalNilai <= 52)
                    predikat ="Amat Baik";

                return predikat;
            }
        }

        string rekomendasi;
        [Size(SizeAttribute.Unlimited)]
        public string Rekomendasi
        {
            get => rekomendasi;
            set => SetPropertyValue(nameof(Rekomendasi), ref rekomendasi, value);
        }

        string catatanPrestasi;
        [Size(SizeAttribute.Unlimited)]
        public string CatatanPrestasi
        {
            get => catatanPrestasi;
            set => SetPropertyValue(nameof(CatatanPrestasi), ref catatanPrestasi, value);
        }

        string catatanPerilaku;
        [Size(SizeAttribute.Unlimited)]
        public string CatatanPerilaku
        {
            get => catatanPerilaku;
            set => SetPropertyValue(nameof(CatatanPerilaku), ref catatanPerilaku, value);
        }
    }
}