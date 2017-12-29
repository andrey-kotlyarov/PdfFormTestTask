using Aspose.Pdf.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PdfFormTestTask.Model
{
    /// <summary>
    /// Model of Field ofForm 
    /// </summary>
    public class PfsFormField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
        public bool Required { get; set; }
        public int MaxLen { get; set; }
        public string FieldType { get; set; }
        public bool ReadOnly { get; set; }
        public IList<SelectListItem> Options { get; set; }

        /// <summary>
        /// Default CTOR for Newtonsoft.Json
        /// </summary>
        public PfsFormField() { }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="field">Aspose.Pdf.Forms.Field</param>
        public PfsFormField(Field field)
        {
            Options = new List<SelectListItem>();

            Name = field.FullName;
            Value = field.Value;
            Required = field.Required;
            ReadOnly = field.ReadOnly;

            if (field is TextBoxField)
            {
                TextBoxField tb = field as TextBoxField;

                FieldType = "TextBox";
                MaxLen = tb.MaxLen;

            }
            else if (field is ComboBoxField)
            {
                FieldType = "ListBox";
                ComboBoxField cb = field as ComboBoxField;
                foreach (Option option in cb.Options)
                {
                    Options.Add(new SelectListItem()
                    {
                        Selected = option.Selected,
                        Text = option.Name,
                        Value = option.Value
                    });
                }
            }
            else if (field is CheckboxField)
            {
                FieldType = "CheckBox";
                Checked = (field as CheckboxField).Checked;
            }
            else if (field is RadioButtonField)
            {
                FieldType = "RadioButton";
                foreach (Option option in (field as RadioButtonField).Options)
                {
                    Options.Add(new SelectListItem()
                    {
                        Selected = option.Selected,
                        Text = option.Name,
                        Value = option.Name
                    });
                }

            }

        }
    }
}
