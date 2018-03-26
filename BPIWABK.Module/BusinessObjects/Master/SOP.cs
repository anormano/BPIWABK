using BPIWABK.Module.BusinessObjects.Administrative;
using BPIWABK.Module.BusinessObjects.Reference;
using DevExpress.Diagram.Core;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

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

        [Action(Caption = "Buat Diagram", ConfirmationMessage = "Gambar diagram yang telah ada akan di ganti, lanjutkan?", ImageName = "Attention", AutoCommit = true, TargetObjectsCriteria = "Kegiatan.Count() > 0")]
        public void ActionMethod()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            MediaDataObject mediaDataObject = new MediaDataObject(Session);
            mediaDataObject.MediaData = GenerateDiagram();
            DiagramSOP = mediaDataObject;
        }

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
     
        MediaDataObject diagramSOP;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public MediaDataObject DiagramSOP
        {
            get => diagramSOP;
            set => SetPropertyValue(nameof(DiagramSOP), ref diagramSOP, value);
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
                }
                else
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

        public byte[] GenerateDiagram()
        {
            List<DiagramKegiatan> listDiagram = new List<DiagramKegiatan>();
            List<DiagramLane> listLane = new List<DiagramLane>();
            DiagramControl diagram = new DiagramControl();
            DiagramShape ds = new DiagramShape();

            int height = 50;
            int width = 100;

            float x = 200;
            float y = 200;

            //create lane
            foreach (Kegiatan kegiatan in Kegiatan)
            {
                DiagramLane diagramLane = new DiagramLane();

                if (listLane.Count > 0)
                {
                    bool isFound = false;
                    foreach (DiagramLane lane in listLane)
                    {
                        if (lane.UnitKerja == kegiatan.PelaksanaKegiatan) isFound = true;
                    }
                    if (!isFound)
                    {
                        diagramLane.Kode = kegiatan.PelaksanaKegiatan.Kode;
                        diagramLane.UnitKerja = kegiatan.PelaksanaKegiatan;
                        diagramLane.LaneX = x;
                        x += 201;
                        listLane.Add(diagramLane);
                    }
                }
                else
                {
                    diagramLane.Kode = kegiatan.PelaksanaKegiatan.Kode;
                    diagramLane.LaneX = x;
                    diagramLane.UnitKerja = kegiatan.PelaksanaKegiatan;
                    x += 201;
                    listLane.Add(diagramLane);
                }
            }

            foreach (DiagramLane lane in listLane)
            {
                DiagramShape shape = new DiagramShape();
                shape.Appearance.BorderColor = System.Drawing.Color.Black;
                shape.Appearance.BackColor = System.Drawing.Color.White;
                shape.Appearance.ForeColor = System.Drawing.Color.Black;
                shape.Content = lane.UnitKerja.Nama;
                shape.Position = new DevExpress.Utils.PointFloat(lane.LaneX, y);
                shape.Height = 100;
                shape.Width = 200;
                diagram.Items.Add(shape);
            }

            y += 101;

            foreach (Kegiatan kegiatan in Kegiatan)
            {
                foreach (DiagramLane lane in listLane)
                {
                    if (lane.Kode == kegiatan.PelaksanaKegiatan.Kode)
                    {
                        x = lane.LaneX;
                    }

                    DiagramShape gridShape = new DiagramShape();
                    gridShape.Shape = BasicShapes.Rectangle;
                    gridShape.Appearance.BackColor = System.Drawing.Color.Transparent;
                    gridShape.Appearance.BorderColor = System.Drawing.Color.Black;
                    gridShape.Position = new DevExpress.Utils.PointFloat(lane.LaneX, y);
                    gridShape.Height = 100;
                    gridShape.Width = 200;
                    diagram.Items.Add(gridShape);
                }

                DiagramKegiatan diagramItem = new DiagramKegiatan();
                diagramItem.Urutan = kegiatan.Urutan;
                diagramItem.Deskripsi = kegiatan.DeskripsiKegiatan;
                diagramItem.DiagramItem = new DiagramShape();
                diagramItem.JenisAlur = kegiatan.AlurKegiatan.JenisAlur;
                diagram.Items.Add(diagramItem.DiagramItem);
                diagramItem.DiagramItem.Tag = kegiatan.PelaksanaKegiatan.Kode;


                switch (diagramItem.JenisAlur)
                {
                    case JenisAlur.Mulai:
                        diagramItem.DiagramItem.Shape = BasicShapes.RoundedRectangle;
                        diagramItem.BranchA = kegiatan.Urutan + 1;
                        break;
                    case JenisAlur.Lanjut:
                        diagramItem.DiagramItem.Shape = BasicShapes.Rectangle;
                        diagramItem.BranchA = kegiatan.Urutan + 1;
                        break;
                    case JenisAlur.YaTidak:
                        diagramItem.DiagramItem.Shape = BasicShapes.Diamond;
                        diagramItem.BranchA = kegiatan.AlurKegiatan.JikaYa;
                        diagramItem.BranchB = kegiatan.AlurKegiatan.JikaTidak;
                        break;
                    case JenisAlur.Selesai:
                        diagramItem.DiagramItem.Shape = BasicShapes.RoundedRectangle;
                        break;
                    default:
                        diagramItem.DiagramItem.Shape = BasicShapes.Rectangle;
                        break;
                }

                diagramItem.DiagramItem.Width = width;
                diagramItem.DiagramItem.Height = height;
                diagramItem.DiagramItem.Position = new DevExpress.Utils.PointFloat(x + 50, y + 20);
                diagramItem.DiagramItem.Content = diagramItem.Deskripsi;
                listDiagram.Add(diagramItem);
                y += 101;
            }

            foreach (DiagramKegiatan diagramItem in listDiagram)
            {
                DiagramConnector connectorA = new DiagramConnector();
                DiagramConnector connectorB = new DiagramConnector();
                connectorA.Appearance.BorderColor = System.Drawing.Color.Black;
                connectorA.Appearance.ForeColor = System.Drawing.Color.Black;

                connectorB.Appearance.BorderColor = System.Drawing.Color.Black;
                connectorB.Appearance.ForeColor = System.Drawing.Color.Black;
                DiagramKegiatan nextItemA;
                DiagramKegiatan nextItemB;
                switch (diagramItem.JenisAlur)
                {
                    case JenisAlur.Mulai:
                        nextItemA = listDiagram[diagramItem.BranchA - 1];
                        connectorA.BeginItem = diagramItem.DiagramItem;
                        connectorA.EndItem = nextItemA.DiagramItem;
                        diagram.Items.Add(connectorA);
                        break;
                    case JenisAlur.Lanjut:
                        nextItemA = listDiagram[diagramItem.BranchA - 1];
                        connectorA.BeginItem = diagramItem.DiagramItem;
                        connectorA.EndItem = nextItemA.DiagramItem;
                        diagram.Items.Add(connectorA);
                        break;
                    case JenisAlur.YaTidak:
                        nextItemA = listDiagram[diagramItem.BranchA - 1];
                        nextItemB = listDiagram[diagramItem.BranchB - 1];
                        connectorA.BeginItem = diagramItem.DiagramItem;
                        connectorA.EndItem = nextItemA.DiagramItem;
                        connectorA.Content = "Ya";
                        connectorB.BeginItem = diagramItem.DiagramItem;
                        connectorB.EndItem = nextItemB.DiagramItem;
                        connectorB.Content = "Tidak";
                        diagram.Items.Add(connectorA);
                        diagram.Items.Add(connectorB);
                        break;
                }
            }

            MemoryStream stream = new MemoryStream();
            diagram.ExportDiagram(stream, DiagramExportFormat.PNG);
            stream.Position = 0;
            return stream.ToArray();
        }

        struct DiagramKegiatan
        {
            public int Urutan;
            public string Deskripsi;
            public DiagramShape DiagramItem;
            public JenisAlur JenisAlur;
            public int BranchA;
            public int BranchB;
        }

        struct DiagramLane
        {
            public string Kode;
            public float LaneX;
            public UnitKerja UnitKerja;
        }
    }
}