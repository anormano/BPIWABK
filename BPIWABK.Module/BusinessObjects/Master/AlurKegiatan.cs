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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace BPIWABK.Module.BusinessObjects.Master
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Alur")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("Alur", Enabled = false, Criteria = "[JenisAlur] <> 'YaTidak'", TargetItems = "JikaYa,JikaTidak")]
    public class AlurKegiatan : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public AlurKegiatan(Session session)
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

        JenisAlur jenisAlur;
        public JenisAlur JenisAlur
        {
            get => jenisAlur;
            set => SetPropertyValue(nameof(JenisAlur), ref jenisAlur, value);
        }

        int jikaYa;
        public int JikaYa
        {
            get => jikaYa;
            set => SetPropertyValue(nameof(JikaYa), ref jikaYa, value);
        }

        int jikaTidak;
        public int JikaTidak
        {
            get => jikaTidak;
            set => SetPropertyValue(nameof(JikaTidak), ref jikaTidak, value);
        }

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Alur
        {
            get
            {
                string teksAlur = string.Empty;
                switch (JenisAlur)
                {
                    case JenisAlur.Mulai:
                        teksAlur = "Mulai kegiatan";
                        break;
                    case JenisAlur.Lanjut:
                        teksAlur = "Lanjut kelangkah berikutnya";
                        break;
                    case JenisAlur.YaTidak:
                        teksAlur = string.Format("Jika YA menuju ke kegiatan {0}, jika tidak menuju ke kegiatan {1}", JikaYa, JikaTidak);
                        break;
                    case JenisAlur.Selesai:
                        teksAlur = "Kegiatan selesai";
                        break;
                }
                return teksAlur;
            }
        }
    }
}