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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RawField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if(!calcFieldValidation(RawField.Text, out errorMsg))
            {
                e.Cancel = true;
                RawField.Select(0, RawField.Text.Length);

                this.ErrorPreventer.SetError(RawField, errorMsg);
            }
        }

        private void RawField_Validated(object sender, System.EventArgs e)
        {
            ErrorPreventer.SetError(RawField, "");
        }

        private void MotionField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if (!calcFieldValidation(MVField.Text, out errorMsg))
            {
                e.Cancel = true;
                MVField.Select(0, MVField.Text.Length);

                this.ErrorPreventer.SetError(MVField, errorMsg);
            }
        }

        private void MVField_Validated(object sender, System.EventArgs e)
        {
            ErrorPreventer.SetError(MVField, "");
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
