using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
//using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Editors
{
    [PropertyEditor(typeof(Enum), false)]
    public class RadioButtonListEnumPropertyEditor : ASPxPropertyEditor
    {
        private EnumDescriptor enumDescriptor;
        private Dictionary<ASPxRadioButton, object> controlsHash = new Dictionary<ASPxRadioButton, object>();
        private Dictionary<string, object> controlsHash2 = new Dictionary<string, object>();
        private ASPxRadioButtonList rbl;

        public RadioButtonListEnumPropertyEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        {
            this.enumDescriptor = new EnumDescriptor(MemberInfo.MemberType);
        }

        protected override WebControl CreateEditModeControlCore()
        {
            Panel placeHolder = new Panel();
            controlsHash.Clear();

            rbl = new ASPxRadioButtonList();
            rbl.ID = "radiobuttonlist";
            rbl.ValueType = enumDescriptor.EnumType;
            foreach (object enumValue in enumDescriptor.Values)
            {
                rbl.Items.Add(enumDescriptor.GetCaption(enumValue), enumValue);
                controlsHash2.Add(enumDescriptor.GetCaption(enumValue), enumValue);
                rbl.SelectedIndexChanged += rbl_SelectedIndexChanged;
            }

            #region hide DevExpress Code Original
            //foreach (object enumValue in enumDescriptor.Values)
            //{

            //    ASPxRadioButton radioButton = new ASPxRadioButton();
            //    radioButton.ID = "radioButton_" + enumValue.ToString();
            //    controlsHash.Add(radioButton, enumValue);
            //    radioButton.Text = enumDescriptor.GetCaption(enumValue);
            //    radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //    radioButton.GroupName = propertyName;

            //    placeHolder.Controls.Add(radioButton);
            //}
            #endregion

            placeHolder.Controls.Add(rbl);
            rbl.RepeatDirection = RepeatDirection.Horizontal;

            return placeHolder;
        }


        private void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditValueChangedHandler(sender, e);
            WriteValue();
        }

        #region hide DevExpress Code Original

        //void radioButton_CheckedChanged(object sender, EventArgs e)
        //{
        //    EditValueChangedHandler(sender, e);
        //}

        #endregion

        protected override string GetPropertyDisplayValue()
        {
            return enumDescriptor.GetCaption(PropertyValue);
        }

        protected override void ReadEditModeValueCore()
        {
            object value = PropertyValue;
            if (value != null)
            {
                foreach(ListEditItem item in rbl.Items) {
                    if(item.Text == enumDescriptor.GetCaption(PropertyValue)) {
                        rbl.SelectedItem = item;
                    }
                }
            }
        }

        #region hide DevExpress Code Original

        //protected override void ReadEditModeValueCore()
        //{
        //    object value = PropertyValue;
        //    if (value != null)
        //    {
        //        foreach (ASPxRadioButton radioButton in Editor.Controls)
        //        {
        //            radioButton.Checked = value.Equals(controlsHash[radioButton]);
        //        }
        //    }
        //}

        //protected override object GetControlValueCore()
        //{
        //    object result = null;
        //    foreach (ASPxRadioButton radioButton in Editor.Controls)
        //    {
        //        if (radioButton.Checked)
        //        {
        //            result = controlsHash[radioButton];
        //            break;
        //        }
        //    }
        //    return result;
        //}

        #endregion

        protected override object GetControlValueCore()
        {
            object result = null;
            foreach (ASPxRadioButtonList radioButton in Editor.Controls)
            {
                if (radioButton.SelectedItem.Selected)
                {
                    result = radioButton.SelectedItem.Value;
                    break;
                }
            }
            return result;
        }


        #region Hide DevExpress Code Original

        //public override void BreakLinksToControl(bool unwireEventsOnly)
        //{
        //    if (Editor != null)
        //    {
        //        foreach (ASpx radioButton in Editor.Controls)
        //        {
        //            radioButton.CheckedChanged -= new EventHandler(rbl_SelectedIndexChanged);
        //        }
        //        if (!unwireEventsOnly)
        //        {
        //            controlsHash.Clear();
        //        }
        //    }
        //    base.BreakLinksToControl(unwireEventsOnly);
        //}

        #endregion

        // get error when set the value of radiobutton
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (Editor != null)
            {
                foreach (ASPxRadioButtonList radioButton in Editor.Controls)
                {
                    //radioButton.CheckedChanged -= new EventHandler(rbl_SelectedIndexChanged);
                }
                if (!unwireEventsOnly)
                {
                    rbl = null;
                    controlsHash2.Clear();
                }
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }

        #region hide DevExpress Code Original

        //public override void BreakLinksToControl(bool unwireEventsOnly)
        //{
        //    if (Editor != null)
        //    {
        //        foreach (ASPxRadioButton radioButton in Editor.Controls)
        //        {
        //            radioButton.CheckedChanged -= new EventHandler(radioButton_CheckedChanged);
        //        }
        //        if (!unwireEventsOnly)
        //        {
        //            controlsHash.Clear();
        //        }
        //    }
        //    base.BreakLinksToControl(unwireEventsOnly);
        //}

        #endregion

        protected override void SetImmediatePostDataScript(string script)
        {
            foreach (ASPxRadioButton radioButton in controlsHash.Keys)
            {
                radioButton.ClientSideEvents.CheckedChanged = script;
            }
        }

        public new Panel Editor
        {
            get { return (Panel)base.Editor; }
        }
    }
}
