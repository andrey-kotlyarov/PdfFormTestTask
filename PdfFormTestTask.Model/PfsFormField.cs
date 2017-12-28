using Aspose.Pdf.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PdfFormTestTask.Model
{
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

        public PfsFormField() { }



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

        public void AddRadioOption(RadioButtonOptionField radioButtonOptionField)
        {

            Options.Add(new SelectListItem()
            {
                Text = radioButtonOptionField.FullName,
                Value = null
            });
        }
    }
}
