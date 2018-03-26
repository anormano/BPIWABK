using BPIWABK.Module.BusinessObjects.Master;
using BPIWABK.Module.BusinessObjects.Reference;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace BPIWABK.Module.BusinessObjects.Administrative
{
    [DefaultClassOptions]
    [CurrentUserDisplayImage("Foto")]
    [ImageName("BO_Contact")]
    [DefaultProperty("NamaLengkap")]
    [Appearance("HideRoles", TargetItems = "Roles", Visibility = ViewItemVisibility.Hide, Criteria = "Not Roles[IsAdministrative = True]")]
    public class Pegawai : PermissionPolicyUser
    {
        public Pegawai(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TanggalPendaftaranSistem = DateTime.Now;
        }
        
        private string nomorInduk;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ModelDefault("NullText", "N/A")]
        [VisibleInListView(false)]
        public string NomorInduk
        {
            get => nomorInduk;
            set => SetPropertyValue(nameof(NomorInduk), ref nomorInduk, value);
        }

        private MediaDataObject _foto;

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200, ListViewImageEditorCustomHeight = 100)]
        public MediaDataObject Foto
        {
            get => _foto;
            set => SetPropertyValue(nameof(Foto), ref _foto, value);
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

        StatusKepegawaian statusKepegawaian;
        [VisibleInListView(false)]
        public StatusKepegawaian StatusKepegawaian
        {
            get => statusKepegawaian;
            set => SetPropertyValue(nameof(StatusKepegawaian), ref statusKepegawaian, value);
        }

        UnitKerja unitKerja;
        public UnitKerja UnitKerja
        {
            get => unitKerja;
            set => SetPropertyValue(nameof(UnitKerja), ref unitKerja, value);
        }

        Jabatan jabatan;
        public Jabatan Jabatan
        {
            get => jabatan;
            set => SetPropertyValue(nameof(Jabatan), ref jabatan, value);
        }

        string tempatLahir;
        [VisibleInListView(false)]
        public string TempatLahir
        {
            get => tempatLahir;
            set => SetPropertyValue(nameof(TempatLahir), ref tempatLahir, value);
        }

        DateTime tanggalLahir;
        [VisibleInListView(false)]
        public DateTime TanggalLahir
        {
            get => tanggalLahir;
            set => SetPropertyValue(nameof(TanggalLahir), ref tanggalLahir, value);
        }


        [VisibleInListView(false), VisibleInDetailView(false)]
        public int Umur
        {
            get
            {
                return CalculateAge(TanggalLahir) ?? 0;
            }
        }

        public int? CalculateAge(DateTime StartDate)
        {
            var today = DateTime.Today;
            var age = today.Year - StartDate.Year;
            if (StartDate > today.AddYears(-age)) age--;
            if (age > 100)
            {
                return null;
            }
            else
            {
                return age;
            }
        }

        string email;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }

        string handphone;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Handphone
        {
            get => handphone;
            set => SetPropertyValue(nameof(Handphone), ref handphone, value);
        }

        string telepon;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Telepon
        {
            get => telepon;
            set => SetPropertyValue(nameof(Telepon), ref telepon, value);
        }

        string alamat;
        [Size(200)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "3")]
        [VisibleInListView(false)]
        public string Alamat
        {
            get => alamat;
            set => SetPropertyValue(nameof(Alamat), ref alamat, value);
        }


        Propinsi propinsi;
        [ImmediatePostData]
        [VisibleInListView(false)]
        public Propinsi Propinsi
        {
            get => propinsi;
            set => SetPropertyValue(nameof(Propinsi), ref propinsi, value);
        }

        Kabupaten kabupaten;
        [ImmediatePostData]
        [DataSourceProperty("Propinsi.Kabupaten")]
        [VisibleInListView(false)]
        public Kabupaten Kabupaten
        {
            get => kabupaten;
            set => SetPropertyValue(nameof(Kabupaten), ref kabupaten, value);
        }

        Kecamatan kecamatan;
        [DataSourceProperty("Kabupaten.Kecamatan")]
        [VisibleInListView(false)]
        public Kecamatan Kecamatan
        {
            get => kecamatan;
            set => SetPropertyValue(nameof(Kecamatan), ref kecamatan, value);
        }

        string alamatTinggal;
        [Size(200)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [VisibleInListView(false)]
        [ModelDefault("RowCount", "3")]
        public string AlamatTinggal
        {
            get => alamatTinggal;
            set => SetPropertyValue(nameof(AlamatTinggal), ref alamatTinggal, value);
        }

        Propinsi propinsiTinggal;
        [ImmediatePostData]
        [VisibleInListView(false)]
        public Propinsi PropinsiTinggal
        {
            get => propinsiTinggal;
            set => SetPropertyValue(nameof(PropinsiTinggal), ref propinsiTinggal, value);
        }

        Kabupaten kabupatenTinggal;
        [ImmediatePostData]
        [DataSourceProperty("PropinsiTinggal.Kabupaten")]
        [VisibleInListView(false)]
        public Kabupaten KabupatenTinggal
        {
            get => kabupatenTinggal;
            set => SetPropertyValue(nameof(KabupatenTinggal), ref kabupatenTinggal, value);
        }

        Kecamatan kecamatanTinggal;
        [ImmediatePostData]
        [DataSourceProperty("KabupatenTinggal.Kecamatan")]
        [VisibleInListView(false)]
        public Kecamatan KecamatanTinggal
        {
            get => kecamatanTinggal;
            set => SetPropertyValue(nameof(KecamatanTinggal), ref kecamatanTinggal, value);
        }
        
        StatusPernikahan statusPernikahan;
        [VisibleInListView(false)]
        public StatusPernikahan StatusPernikahan
        {
            get => statusPernikahan;
            set => SetPropertyValue(nameof(StatusPernikahan), ref statusPernikahan, value);
        }

        JenisKelamin jenisKelamin;
        [VisibleInListView(false)]
        public JenisKelamin JenisKelamin
        {
            get => jenisKelamin;
            set => SetPropertyValue(nameof(JenisKelamin), ref jenisKelamin, value);
        }

        Agama agama;
        [VisibleInListView(false)]
        public Agama Agama
        {
            get => agama;
            set => SetPropertyValue(nameof(Agama), ref agama, value);
        }

        GolonganDarah golonganDarah;
        [VisibleInListView(false)]
        public GolonganDarah GolonganDarah
        {
            get => golonganDarah;
            set => SetPropertyValue(nameof(GolonganDarah), ref golonganDarah, value);
        }

        string nomorKTP;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string NomorKTP
        {
            get => nomorKTP;
            set => SetPropertyValue(nameof(NomorKTP), ref nomorKTP, value);
        }

        string nomorNPWP;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string NomorNPWP
        {
            get => nomorNPWP;
            set => SetPropertyValue(nameof(NomorNPWP), ref nomorNPWP, value);
        }

        string nomorRekening;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string NomorRekening
        {
            get => nomorRekening;
            set => SetPropertyValue(nameof(NomorRekening), ref nomorRekening, value);
        }

        Bank bank;
        [VisibleInListView(false)]
        public Bank Bank
        {
            get => bank;
            set => SetPropertyValue(nameof(Bank), ref bank, value);
        }

        DateTime tanggalPendaftaranSistem;
        [VisibleInListView(false)]
        public DateTime TanggalPendaftaranSistem
        {
            get => tanggalPendaftaranSistem;
            set => SetPropertyValue(nameof(TanggalPendaftaranSistem), ref tanggalPendaftaranSistem, value);
        }

        [VisibleInListView(false)]
        public JenjangPendidikan JenjangPendidikan
        {
            get
            {
                JenjangPendidikan jenjangPendidikan = JenjangPendidikan.Kosong;
                if (RiwayatPendidikanFormal.Count > 0)
                {
                    XPCollection<PendidikanFormal> xpc = new XPCollection<PendidikanFormal>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("TahunLulus", DevExpress.Xpo.DB.SortingDirection.Descending)) { TopReturnedObjects = 1 };
                    PendidikanFormal obj = xpc.Count > 0 ? xpc[0] : null;
                    jenjangPendidikan = obj.JenjangPendidikan;
                }

                return jenjangPendidikan;
            }
        }

        [VisibleInListView(false)]
        public int? TahunLulus
        {
            get
            {
                int? tahunLulus = null;
                if (RiwayatPendidikanFormal.Count > 0)
                {
                    XPCollection<PendidikanFormal> xpc = new XPCollection<PendidikanFormal>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("TahunLulus", DevExpress.Xpo.DB.SortingDirection.Descending)) { TopReturnedObjects = 1 };
                    PendidikanFormal obj = xpc.Count > 0 ? xpc[0] : null;
                    tahunLulus = obj.TahunLulus;
                }

                return tahunLulus;
            }
        }

        [ModelDefault("Caption", "No SK Pengangkatan Awal")]
        [VisibleInListView(false)]
        public string SKPengangkatanPertama
        {
            get
            {
                string noSK = null;
                if (SKPengangkatan.Count > 0)
                {
                    XPCollection<SKPengangkatan> xpc = new XPCollection<SKPengangkatan>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("Tahun", DevExpress.Xpo.DB.SortingDirection.Descending)) { TopReturnedObjects = 1 };
                    SKPengangkatan obj = xpc.Count > 0 ? xpc[0] : null;
                    noSK = obj.NOSK;
                }
                return noSK;
            }
        }

        [ModelDefault("Caption", "Tahun Awal")]
        [VisibleInListView(false)]
        //[ModelDefault("DisplayFormat", "{O:#}")]
        public int? TahunSKPengangkatanPertama
        {
            get
            {
                int? tahunSK = null;
                if (SKPengangkatan.Count > 0)
                {
                    XPCollection<SKPengangkatan> xpc = new XPCollection<SKPengangkatan>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("Tahun", DevExpress.Xpo.DB.SortingDirection.Descending)) { TopReturnedObjects = 1 };
                    SKPengangkatan obj = xpc.Count > 0 ? xpc[0] : null;
                    tahunSK = obj.Tahun;
                }
                return tahunSK;
            }
        }

        [ModelDefault("Caption", "No SK Pengangkatan Terkini")]
        [VisibleInListView(false)]
        public string SKPengangkatanTerakhir
        {
            get
            {
                string noSK = null;
                if (SKPengangkatan.Count > 0)
                {
                    XPCollection<SKPengangkatan> xpc = new XPCollection<SKPengangkatan>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("Tahun", DevExpress.Xpo.DB.SortingDirection.Ascending)) { TopReturnedObjects = 1 };
                    SKPengangkatan obj = xpc.Count > 0 ? xpc[0] : null;
                    noSK = obj.NOSK;
                }
                return noSK;
            }
        }

        [ModelDefault("Caption", "Tahun Terkini")]
        [VisibleInListView(false)]
        public int? TahunSKPengangkatanTerakhir
        {
            get
            {
                int? tahunSK = null;
                if (SKPengangkatan.Count > 0)
                {
                    XPCollection<SKPengangkatan> xpc = new XPCollection<SKPengangkatan>(Session, CriteriaOperator.Parse("Pegawai.Oid = ?", this.Oid), new SortProperty("Tahun", DevExpress.Xpo.DB.SortingDirection.Ascending)) { TopReturnedObjects = 1 };
                    SKPengangkatan obj = xpc.Count > 0 ? xpc[0] : null;
                    tahunSK = obj.Tahun;
                }
                return tahunSK;
            }
        }

        MediaDataObject kTP;
        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, DetailViewImageEditorFixedHeight = 150)]
        public MediaDataObject KTP
        {
            get => kTP;
            set => SetPropertyValue(nameof(KTP), ref kTP, value);
        }

        MediaDataObject nPWP;
        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, DetailViewImageEditorFixedHeight = 150)]
        public MediaDataObject NPWP
        {
            get => nPWP;
            set => SetPropertyValue(nameof(NPWP), ref nPWP, value);
        }

        MediaDataObject rekeningKoran;
        [VisibleInListView(false)]
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, DetailViewImageEditorFixedHeight = 150)]
        public MediaDataObject RekeningKoran
        {
            get => rekeningKoran;
            set => SetPropertyValue(nameof(RekeningKoran), ref rekeningKoran, value);
        }

        [Association("Pegawai-PenilaianKinerja"), Aggregated]
        public XPCollection<Kinerja> PenilaianKinerja
        {
            get
            {
                return GetCollection<Kinerja>(nameof(PenilaianKinerja));
            }
        }

        [Association("Pegawai-SK"), DevExpress.Xpo.Aggregated]
        [ModelDefault("Caption", "Riwayat SK Pengangkatan")]
        [VisibleInListView(false)]
        public XPCollection<SKPengangkatan> SKPengangkatan
        {
            get => GetCollection<SKPengangkatan>(nameof(SKPengangkatan));
        }

        [Association("Pegawai-RiwayatPendidikanFormal"), DevExpress.Xpo.Aggregated]
        public XPCollection<PendidikanFormal> RiwayatPendidikanFormal
        {
            get =>GetCollection<PendidikanFormal>(nameof(RiwayatPendidikanFormal));
        }

        [Association("Pegawai-RiwayatPengalamanKerja"), DevExpress.Xpo.Aggregated]
        public XPCollection<PengalamanKerja> RiwayatPengalamanKerja
        {
            get => GetCollection<PengalamanKerja>(nameof(RiwayatPengalamanKerja));
        }

        [Association("Pegawai-RiwayatPengalamanOrganisasi"), DevExpress.Xpo.Aggregated]
        public XPCollection<PengalamanOrganisasi> RiwayatPengalamanOrganisasi
        {
            get => GetCollection<PengalamanOrganisasi>(nameof(RiwayatPengalamanOrganisasi));
        }

        [Association("Pegawai-Seminar"), DevExpress.Xpo.Aggregated]
        [ModelDefault("Caption", "Riwayat Seminar/Training/Workshop")]
        public XPCollection<Seminar> SeminarSeminar
        {
            get => GetCollection<Seminar>(nameof(SeminarSeminar));
        }
    }
}