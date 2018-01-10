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
    /// Model of Field of Form 
    /// </summary>
    public class PfsFormField
    {
        /// <summary>
        /// Unique identifier of field/
        /// It takes from PDF doc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the field.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// CheckBox state.
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Is field required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Max length of text field
        /// </summary>
        public int MaxLen { get; set; }

        /// <summary>
        /// Field Type
        /// </summary>
        public FieldType FieldType { get; set; }
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

                FieldType = FieldType.TextBox;
                MaxLen = tb.MaxLen;

            }
            else if (field is ComboBoxField)
            {
                FieldType = FieldType.ListBox;
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
                FieldType = FieldType.CheckBox;
                Checked = (field as CheckboxField).Checked;
            }
            else if (field is RadioButtonField)
            {
                FieldType = FieldType.RadioButton;
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
