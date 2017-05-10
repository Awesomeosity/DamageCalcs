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

        public Dictionary<string, Tuple<double, double>> sharpnessValues = new Dictionary<string, Tuple<double, double>>();

        public DmgCalculator()
        {
            InitializeComponent();
            this.sharpnessBox.SelectedIndex = 0;
            radioButton1.Select();
            FillOut();
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
        

        /*CalcButt Functions*/
        private void CalcButt_Click(object sender, System.EventArgs e) //TO BE MODIFIED
        { //REASON: NEED TO CALCULATE AFFINITY HITS (NEGATIVE, POSITIVE, NEUTRAL), AS WELL AS ON AVERAGE
            double total     = double.Parse(RawField.Text);
            double motion    = double.Parse(MVField.Text) * 0.01;
            double affinity  = double.Parse(AffinityField.Text) * 0.01;
            double element   = double.Parse(EleField.Text);
            double hidden    = double.Parse(HiddenField.Text);

            double rawSharp = sharpnessValues[sharpnessBox.Text].Item1;
            double eleSharp = sharpnessValues[sharpnessBox.Text].Item2;

            double rawTotal = total * (1 + affinity * 0.25) * rawSharp * hidden * motion;
            double eleTotal = element * eleSharp * hidden;

            RawOut.Text = rawTotal.ToString();
            EleOut.Text = eleTotal.ToString();
        }


        /*Functions*/
        private void damageCalc(string RawDamage, string MotionValue) //TO BE MODIFIED
        {
            float raw = float.Parse(RawDamage);
            float MV = float.Parse(MotionValue);
            RawOut.Text = (raw * MV).ToString();
        }

        public bool calcFieldValidation(string input, out string ErrorMessage)
        {
            double result;
            if(!double.TryParse(input, out result))
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

        private void FillOut()
        {
            sharpnessValues.Add("(No Sharpness)",   new Tuple<double, double>(1.00, 1.00));
            sharpnessValues.Add("White",            new Tuple<double, double>(1.32, 1.12));
            sharpnessValues.Add("Blue",             new Tuple<double, double>(1.20, 1.06));
            sharpnessValues.Add("Green",            new Tuple<double, double>(1.05, 1.0));
            sharpnessValues.Add("Yellow",           new Tuple<double, double>(1.00, 0.75));
            sharpnessValues.Add("Orange",           new Tuple<double, double>(0.75, 0.50));
            sharpnessValues.Add("Red",              new Tuple<double, double>(0.50, 0.25));
        }
        
    }
}
