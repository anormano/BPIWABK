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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [NavigationItem(true, GroupName = "Menu Utama")]
    //[Appearance("DataAwal", TargetItems = "NamaLengkapAwal,NomorIndukAwal,StatusKepegawaianAwal,AgamaAwal,StatusPernikahanAwal,JenisKelaminAwal,GolonganDarahAwal,NomorKTPAwal,NomorNPWPAwal, TempatLahirAwal, TanggalLahirAwal,EmailAwal,HandphoneAwal,TelponAwal,AlamatAwal,PropinsiAwal,KabupatenAwal,KecamatanAwal,AlamatTinggalAwal,PropinsiTinggalAwal,KabupatenTinggalAwal,PropinsiTinggalAwal,KecamatanTinggalAwal,FotoAwal,KTPAwal,NPWPAwal", BackColor = "Gray", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PerubahanDataPegawai : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PerubahanDataPegawai(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Pemilik = Session.GetObjectByKey<Pegawai>(SecuritySystem.CurrentUserId);
        }
        public object getData(string PropertyName)
        {
            Pegawai pegawai = Session.GetObjectByKey<Pegawai>(Pemilik.Oid);
            return pegawai.GetMemberValue(PropertyName);
        }

        string nomorInduk;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ModelDefault("NullText", "N/A")]
        [VisibleInListView(false)]
        public string NomorInduk
        {
            get => nomorInduk;
            set => SetPropertyValue(nameof(NomorInduk), ref nomorInduk, value);
        }


        [VisibleInListView(false)]
        public string NomorIndukAwal
        {
            get => (string)getData(nameof(NomorInduk));
        }

        DateTime tanggalKirim;
        public DateTime TanggalKirim
        {
            get => tanggalKirim;
            set => SetPropertyValue(nameof(TanggalKirim), ref tanggalKirim, value);
        }

        MediaDataObject _foto;
        [VisibleInListView(false)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200, DetailViewImageEditorFixedWidth = 200, ListViewImageEditorCustomHeight = 100)]
        public MediaDataObject Foto
        {
            get => _foto;
            set => SetPropertyValue(nameof(Foto), ref _foto, value);
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200, DetailViewImageEditorFixedWidth = 200, ListViewImageEditorCustomHeight = 100)]
        [VisibleInListView(false)]
        public MediaDataObject FotoAwal
        {
            get => (MediaDataObject)getData(nameof(Foto));
        }

        string namaLengkap;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInDetailView(true)]
        [RuleRequiredField]
        public string NamaLengkap
        {
            get => namaLengkap;
            set => SetPropertyValue(nameof(NamaLengkap), ref namaLengkap, value);
        }

        [VisibleInListView(false)]
        public string NamaLengkapAwal
        {
            get => (string)getData(nameof(NamaLengkap));
        }

        StatusKepegawaian statusKepegawaian;
        [VisibleInListView(false)]
        public StatusKepegawaian StatusKepegawaian
        {
            get => statusKepegawaian;
            set => SetPropertyValue(nameof(StatusKepegawaian), ref statusKepegawaian, value);
        }

        [VisibleInListView(false)]
        public StatusKepegawaian StatusKepegawaianAwal
        {
            get => (StatusKepegawaian)getData(nameof(StatusKepegawaian));
        }

        string tempatLahir;
        [VisibleInListView(false)]
        public string TempatLahir
        {
            get => tempatLahir;
            set => SetPropertyValue(nameof(TempatLahir), ref tempatLahir, value);
        }
        [VisibleInListView(false)]
        public string TempatLahirAwal
        {
            get => (string)getData(nameof(TempatLahir));
        }

        DateTime tanggalLahir;
        [VisibleInListView(false)]
        public DateTime TanggalLahir
        {
            get => tanggalLahir;
            set => SetPropertyValue(nameof(TanggalLahir), ref tanggalLahir, value);
        }

        [VisibleInListView(false)]
        public DateTime? TanggalLahirAwal
        {
            get => (DateTime?)getData(nameof(TanggalLahir));
        }

        string email;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }

        [VisibleInListView(false)]
        public string EmailAwal
        {
            get => (string)getData(nameof(Email));
        }

        string handphone;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Handphone
        {
            get => handphone;
            set => SetPropertyValue(nameof(Handphone), ref handphone, value);
        }

        [VisibleInListView(false)]
        public string HandphoneAwal
        {
            get => (string)getData(nameof(Handphone));
        }

        string telepon;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Telepon
        {
            get => telepon;
            set => SetPropertyValue(nameof(Telepon), ref telepon, value);
        }

        [VisibleInListView(false)]
        public string TelponAwal
        {
            get => (string)getData(nameof(Telepon));
        }

        string alamat;
        [Size(200)]
        [VisibleInListView(false)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "3")]
        public string Alamat
        {
            get => alamat;
            set => SetPropertyValue(nameof(Alamat), ref alamat, value);
        }

        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "3")]
        [VisibleInListView(false)]
        public string AlamatAwal
        {
            get => (string)getData(nameof(Alamat));
        }

        Propinsi propinsi;
        [ImmediatePostData]
        [VisibleInListView(false)]
        public Propinsi Propinsi
        {
            get => propinsi;
            set => SetPropertyValue(nameof(Propinsi), ref propinsi, value);
        }

        [VisibleInListView(false)]
        public Propinsi PropinsiAwal
        {
            get => (Propinsi)getData(nameof(Propinsi));
        }

        Kabupaten kabupaten;
        [ImmediatePostData]
        [VisibleInListView(false)]
        [DataSourceProperty("Propinsi.Kabupaten")]
        public Kabupaten Kabupaten
        {
            get => kabupaten;
            set => SetPropertyValue(nameof(Kabupaten), ref kabupaten, value);
        }

        [VisibleInListView(false)]
        public Kabupaten KabupatenAwal
        {
            get => (Kabupaten)getData(nameof(Kabupaten));
        }

        Kecamatan kecamatan;
        [VisibleInListView(false)]
        [DataSourceProperty("Kabupaten.Kecamatan")]
        public Kecamatan Kecamatan
        {
            get => kecamatan;
            set => SetPropertyValue(nameof(Kecamatan), ref kecamatan, value);
        }

        [VisibleInListView(false)]
        public Kecamatan KecamatanAwal
        {
            get => (Kecamatan)getData(nameof(Kecamatan));
        }

        string alamatTinggal;
        [Size(200)]
        [VisibleInListView(false)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "3")]
        public string AlamatTinggal
        {
            get => alamatTinggal;
            set => SetPropertyValue(nameof(AlamatTinggal), ref alamatTinggal, value);
        }

        [ModelDefault("RowCount", "3")]
        [VisibleInListView(false)]
        public string AlamatTinggalAwal
        {
            get => (string)getData(nameof(AlamatTinggal));
        }

        Propinsi propinsiTinggal;
        [VisibleInListView(false)]
        [ImmediatePostData]
        public Propinsi PropinsiTinggal
        {
            get => propinsiTinggal;
            set => SetPropertyValue(nameof(PropinsiTinggal), ref propinsiTinggal, value);
        }

        [VisibleInListView(false)]
        public Propinsi PropinsiTinggalAwal
        {
            get => (Propinsi)getData(nameof(PropinsiTinggal));
        }

        Kabupaten kabupatenTinggal;
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PropinsiTinggal.Kabupaten")]
        public Kabupaten KabupatenTinggal
        {
            get => kabupatenTinggal;
            set => SetPropertyValue(nameof(KabupatenTinggal), ref kabupatenTinggal, value);
        }

        [VisibleInListView(false)]
        public Kabupaten KabupatenTinggalAwal
        {
            get => (Kabupaten)getData(nameof(KabupatenTinggal));
        }

        Kecamatan kecamatanTinggal;
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("KabupatenTinggal.Kecamatan")]
        public Kecamatan KecamatanTinggal
        {
            get => kecamatanTinggal;
            set => SetPropertyValue(nameof(KecamatanTinggal), ref kecamatanTinggal, value);
        }

        [VisibleInListView(false)]
        public Kecamatan KecamatanTinggalAwal
        {
            get => (Kecamatan)getData(nameof(KecamatanTinggal));
        }

        StatusPernikahan statusPernikahan;
        [VisibleInListView(false)]
        public StatusPernikahan StatusPernikahan
        {
            get => statusPernikahan;
            set => SetPropertyValue(nameof(StatusPernikahan), ref statusPernikahan, value);
        }
        [VisibleInListView(false)]
        public StatusPernikahan StatusPernikahanAwal
        {
            get => (StatusPernikahan)getData(nameof(StatusPernikahan));
        }

        JenisKelamin jenisKelamin;
        [VisibleInListView(false)]
        public JenisKelamin JenisKelamin
        {
            get => jenisKelamin;
            set => SetPropertyValue(nameof(JenisKelamin), ref jenisKelamin, value);
        }

        [VisibleInListView(false)]
        public JenisKelamin JenisKelaminAwal
        {
            get => (JenisKelamin)getData(nameof(JenisKelamin));
        }

        Agama agama;
        [VisibleInListView(false)]
        public Agama Agama
        {
            get => agama;
            set => SetPropertyValue(nameof(Agama), ref agama, value);
        }

        [VisibleInListView(false)]
        public Agama AgamaAwal
        {
            get => (Agama)getData(nameof(Agama));
        }

        GolonganDarah golonganDarah;
        [VisibleInListView(false)]
        public GolonganDarah GolonganDarah
        {
            get => golonganDarah;
            set => SetPropertyValue(nameof(GolonganDarah), ref golonganDarah, value);
        }

        [VisibleInListView(false)]
        public GolonganDarah GolonganDarahAwal
        {
            get => (GolonganDarah)getData(nameof(GolonganDarah));
        }

        string nomorKTP;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NomorKTP
        {
            get => nomorKTP;
            set => SetPropertyValue(nameof(NomorKTP), ref nomorKTP, value);
        }

        [VisibleInListView(false)]
        public string NomorKTPAwal
        {
            get => (string)getData(nameof(NomorKTP));
        }

        string nomorNPWP;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NomorNPWP
        {
            get => nomorNPWP;
            set => SetPropertyValue(nameof(NomorNPWP), ref nomorNPWP, value);
        }

        [VisibleInListView(false)]
        public string NomorNPWPAwal
        {
            get => (string)getData(nameof(NomorNPWP));
        }

        Pegawai pemilik;
        [VisibleInListView(false)]
        public Pegawai Pemilik
        {
            get => pemilik;
            set => SetPropertyValue(nameof(Pemilik), ref pemilik, value);
        }

        bool verifikasiSatu;
        [ModelDefault("Caption", "Verifikasi 1")]
        public bool VerifikasiSatu
        {
            get => verifikasiSatu;
            set => SetPropertyValue(nameof(VerifikasiSatu), ref verifikasiSatu, value);
        }

        DateTime tanggalVerifikasiSatu;
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tanggal Verifikasi 1")]
        public DateTime TanggalVerifikasiSatu
        {
            get => tanggalVerifikasiSatu;
            set => SetPropertyValue(nameof(TanggalVerifikasiSatu), ref tanggalVerifikasiSatu, value);
        }

        bool verifikasiDua;
        [ModelDefault("Caption", "Verifikasi 2")]
        public bool VerifikasiDua
        {
            get => verifikasiDua;
            set => SetPropertyValue(nameof(VerifikasiDua), ref verifikasiDua, value);
        }

        DateTime tanggalVerifikasiDua;
        [ModelDefault("Caption", "Tanggal Verifikasi 2")]
        [VisibleInListView(false)]
        public DateTime TanggalVerifikasiDua
        {
            get => tanggalVerifikasiDua;
            set => SetPropertyValue(nameof(TanggalVerifikasiDua), ref tanggalVerifikasiDua, value);
        }

        bool verifikasiTiga;
        [ModelDefault("Caption", "Verifikasi 3")]
        public bool VerifikasiTiga
        {
            get => verifikasiTiga;
            set => SetPropertyValue(nameof(VerifikasiTiga), ref verifikasiTiga, value);
        }

        DateTime tanggalVerifikasiTiga;
        [ModelDefault("Caption", "Tanggal Verifikasi 3")]
        [VisibleInListView(false)]
        public DateTime TanggalVerifikasiTiga
        {
            get => tanggalVerifikasiTiga;
            set => SetPropertyValue(nameof(TanggalVerifikasiTiga), ref tanggalVerifikasiTiga, value);
        }

        MediaDataObject kTP;
        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, ListViewImageEditorCustomHeight = 100, DetailViewImageEditorFixedHeight = 100, DetailViewImageEditorFixedWidth = 200)]
        public MediaDataObject KTP
        {
            get => kTP;
            set => SetPropertyValue(nameof(KTP), ref kTP, value);
        }

        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, ListViewImageEditorCustomHeight = 100, DetailViewImageEditorFixedHeight = 100, DetailViewImageEditorFixedWidth = 200)]
        [VisibleInListView(false)]
        public MediaDataObject KTPAwal
        {
            get => (MediaDataObject)getData(nameof(KTP));
        }

        MediaDataObject nPWP;
        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, ListViewImageEditorCustomHeight = 100, DetailViewImageEditorFixedHeight = 100, DetailViewImageEditorFixedWidth = 200)]
        public MediaDataObject NPWP
        {
            get => nPWP;
            set => SetPropertyValue(nameof(NPWP), ref nPWP, value);
        }

        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, ListViewImageEditorCustomHeight = 100, DetailViewImageEditorFixedHeight = 100, DetailViewImageEditorFixedWidth = 200)]
        public MediaDataObject NPWPAwal
        {
            get => (MediaDataObject)getData(nameof(NPWP));
        }
    }
}