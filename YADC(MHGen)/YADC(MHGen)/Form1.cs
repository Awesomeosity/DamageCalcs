using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YADC_MHGen_
{
    public partial class DmgCalculator : Form
    {
        public DmgCalculator()
        {
            InitializeComponent();
        }

        /*Generic Field Validation*/
        private void GenericField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if(!calcFieldValidation(((TextBox)sender).Text, out errorMsg))
            {
                e.Cancel = true;
                ((TextBox)sender).Select(0, ((TextBox)sender).Text.Length);

                this.ErrorPreventer.SetError((TextBox)sender, errorMsg);
            }
        }

        private void GenericField_Validated(object sender, System.EventArgs e)
        {
            ErrorPreventer.SetError((TextBox)sender, "");
        }
        


        /*Functions*/
        private void damageCalc(string RawDamage, string MotionValue) //TO BE MODIFIED
        {
            float raw = float.Parse(RawDamage);
            float MV = float.Parse(MotionValue);
            OutLabel.Text = (raw * MV).ToString();
        }

        public bool calcFieldValidation(string input, out string ErrorMessage)
        {
            float result;
            if(!float.TryParse(input, out result))
            {
                ErrorMessage = "Enter in a valid number.";
                return false;
            }
            else
            {
                ErrorMessage = "";
                return true;
            }
        }
    }
}
