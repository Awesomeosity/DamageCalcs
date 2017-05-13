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

        public Dictionary<string, Tuple<double, double>> sharpnessValues = new Dictionary<string, Tuple<double, double>>(); //Stores translation of sharpness to sharpness modifiers
        public Dictionary<string, string> str2image = new Dictionary<string, string>(); //Stores the paths to the image files.


        public DmgCalculator()
        {
            InitializeComponent();
            this.sharpnessBox.SelectedIndex = 0;
            this.AltDamageField.SelectedIndex = 0;
            AverageSel.Select();
            ElementBox.Image = null;
            EleOut.BackColor = System.Drawing.SystemColors.Control;
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
        private void CalcButt_Click(object sender, System.EventArgs e)
        {
            double total     = double.Parse(RawField.Text);
            double motion    = double.Parse(MVField.Text) * 0.01;
            double affinity  = double.Parse(AffinityField.Text) * 0.01;
            double element   = double.Parse(EleField.Text);
            double hidden    = double.Parse(HiddenField.Text);

            double rawSharp = sharpnessValues[sharpnessBox.Text].Item1;
            double eleSharp = sharpnessValues[sharpnessBox.Text].Item2;

            double rawTotal = 0;
            double eleTotal = 0;

            if(AverageSel.Checked)
            {
                rawTotal = total * (1 + affinity * 0.25) * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if(PositiveSel.Checked)
            {
                rawTotal = total * 1.25 * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if(NegativeSel.Checked)
            {
                rawTotal = total * 0.75 * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if(NeutralSel.Checked)
            {
                rawTotal = total * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else
            {
                RawOut.Text = "Error"; //Should never, ever get here.
                EleOut.Text = "Error";
            }

            RawOut.Text = rawTotal.ToString();
            EleOut.Text = eleTotal.ToString();
        }

        /*CalcAll Functions*/
        private void CalcAll_Click(object sender, System.EventArgs e)
        {
            double total    = double.Parse(RawField.Text);
            double motion   = double.Parse(MVField.Text) * 0.01;
            double affinity = double.Parse(AffinityField.Text) * 0.01;
            double element  = double.Parse(EleField.Text);
            double hidden   = double.Parse(HiddenField.Text);
            double rawZone  = double.Parse(HitzoneField.Text);
            double eleZone  = double.Parse(EleZoneField.Text);
            double questMod = double.Parse(QuestField.Text);

            double rawSharp = sharpnessValues[sharpnessBox.Text].Item1;
            double eleSharp = sharpnessValues[sharpnessBox.Text].Item2;

            double rawFinal = 0;
            double eleFinal = 0;

            //if(AverageSel)
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

        private void FillOut() //Fills out the Dictionaries with data.
        {
            sharpnessValues.Add("(No Sharpness)",   new Tuple<double, double>(1.00, 1.00));
            sharpnessValues.Add("White",            new Tuple<double, double>(1.32, 1.12));
            sharpnessValues.Add("Blue",             new Tuple<double, double>(1.20, 1.06));
            sharpnessValues.Add("Green",            new Tuple<double, double>(1.05, 1.0));
            sharpnessValues.Add("Yellow",           new Tuple<double, double>(1.00, 0.75));
            sharpnessValues.Add("Orange",           new Tuple<double, double>(0.75, 0.50));
            sharpnessValues.Add("Red",              new Tuple<double, double>(0.50, 0.25));

            str2image.Add("(None)",     "No Image");
            str2image.Add("Fire",       "./Images/Fire.png");
            str2image.Add("Water",      "./Images/Water.png");
            str2image.Add("Thunder",    "./Images/Thunder.png");
            str2image.Add("Ice",        "./Images/Ice.png");
            str2image.Add("Dragon",     "./Images/Dragon.png");
            str2image.Add("Poison",     "./Images/Poison.png");
            str2image.Add("Sleep",      "./Images/Sleep.png");
            str2image.Add("Para",       "./Images/Para.png");
            str2image.Add("Blast",      "./Images/Blast.png");
        }

        private void AltDamageField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string element = (string)((ComboBox)sender).SelectedItem;
            if(element != "(None)")
            {
                string path = str2image[element];
                ElementBox.Load(path);
                EleOut.BackColor = SystemColors.ControlLightLight;
            }
            else
            {
                ElementBox.Image = null;
                EleOut.BackColor = System.Drawing.SystemColors.Control;
            }
        }
    }
}
