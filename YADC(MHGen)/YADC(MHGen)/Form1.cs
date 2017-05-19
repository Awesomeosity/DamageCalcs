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
            FinalEleBox.Image = null;
            EleZoneField.ReadOnly = true;
            EleOut.BackColor = SystemColors.Control;
            FinalEleField.BackColor = SystemColors.Control;
            KOBox.Load("./Images/KO.png");
            ExhaustBox.Load("./Images/Exhaust.png");
            FillOut();
        }

        //EVENT FUNCTIONS
        /// <summary>
        /// Checks if whatever's put into the field can be converted into a double.
        /// If no, then throws an error.
        /// If yes, then allows the user to continue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenericField_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!calcFieldValidation(((TextBox)sender).Text, out errorMsg))
            {
                e.Cancel = true;
                ((TextBox)sender).Select(0, ((TextBox)sender).Text.Length);

                this.ErrorPreventer.SetError((TextBox)sender, errorMsg);
            }
        }

        /// <summary>
        /// Resets the ErrorPreventer if the input is correct.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenericField_Validated(object sender, System.EventArgs e)
        {
            ErrorPreventer.SetError((TextBox)sender, "");
        }
        

        /*CalcButt Functions*/
        private void CalcButt_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double> rawEleOut = calculateDamage();

            RawOut.Text = rawEleOut.Item1.ToString();
            EleOut.Text = rawEleOut.Item2.ToString();
        }

        /*CalcAll Functions*/
        private void CalcAll_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double> rawEleTuple = calculateDamage();
            Tuple<double, double> finalTuple = calculateMoreDamage(rawEleTuple.Item1, rawEleTuple.Item2);

            RawOut.Text = rawEleTuple.Item1.ToString();
            EleOut.Text = rawEleTuple.Item2.ToString();

            FinalRawField.Text = finalTuple.Item1.ToString();
            FinalEleField.Text = finalTuple.Item2.ToString();

            FinalField.Text = Math.Floor(finalTuple.Item1 + finalTuple.Item2).ToString();

            
        }

        private void AltDamageField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string element = (string)((ComboBox)sender).SelectedItem;
            if (element != "(None)")
            {
                string path = str2image[element];
                ElementBox.Load(path);
                FinalEleBox.Load(path);
                EleField.ReadOnly = false;
                EleOut.BackColor = SystemColors.ControlLightLight;
                FinalEleField.BackColor = SystemColors.ControlLightLight;

                if (element == "Poison" | element == "Para" | element == "Sleep" | element == "Blast")
                {
                    EleZoneField.ReadOnly = true;
                }
                else
                {
                    EleZoneField.ReadOnly = false;
                }
            }

            else
            {
                ElementBox.Image = null;
                FinalEleBox.Image = null;
                EleOut.BackColor = System.Drawing.SystemColors.Control;
                FinalEleField.BackColor = System.Drawing.SystemColors.Control;
                EleField.ReadOnly = true;
                EleZoneField.ReadOnly = true;
                EleField.Text = 0.ToString();
                EleZoneField.Text = 0.ToString();
            }
        }


        /*Functions*/
        /// <summary>
        /// Validates whatever's put into the field to doubles.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This function calculates damage before considering the monster parameters.
        /// </summary>
        /// <returns></returns>
        private Tuple<double, double> calculateDamage()
        {
            double total = double.Parse(RawField.Text);
            double motion = double.Parse(MVField.Text) * 0.01;
            double affinity = double.Parse(AffinityField.Text) * 0.01;
            double element = double.Parse(EleField.Text);
            double hidden = double.Parse(HiddenField.Text);

            double rawSharp = sharpnessValues[sharpnessBox.Text].Item1;
            double eleSharp = sharpnessValues[sharpnessBox.Text].Item2;

            double rawTotal = 0;
            double eleTotal = 0;

            if (AverageSel.Checked)
            {
                rawTotal = total * (1 + affinity * 0.25) * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if (PositiveSel.Checked)
            {
                rawTotal = total * 1.25 * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if (NegativeSel.Checked)
            {
                rawTotal = total * 0.75 * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            else if (NeutralSel.Checked)
            {
                rawTotal = total * rawSharp * hidden * motion;
                eleTotal = element * eleSharp * hidden;
            }

            return new Tuple<double, double>(rawTotal, eleTotal);
        }

        /// <summary>
        /// This function calculates the damage with hitzones.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        private Tuple<double, double> calculateMoreDamage(double item1, double item2)
        {
            double rawZone = double.Parse(HitzoneField.Text) * 0.01;
            double eleZone = double.Parse(EleZoneField.Text) * 0.01;
            double questMod = double.Parse(QuestField.Text);

            item1 = item1 * rawZone * questMod;

            string element = (string)AltDamageField.SelectedItem;
            if (element != "Poison" & element != "Para" & element != "Sleep" & element != "Blast")
            {
                item2 = item2 * eleZone * questMod;
            }

            return new Tuple<double, double>(item1, item2);
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
    }
}
