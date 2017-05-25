﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace YADC_MHGen_
{
    public partial class DmgCalculator : Form
    {
        /// <summary>
        /// Stores the stats of a single level of a single weapon.
        /// </summary>
        public struct stats
        {
            public int level;
            public int attack;
            public string sharpness;
            public int affinity;
            public string elementalType;
            public int elementalDamage;
            public string sharpness1;
            public string sharpness2;

            public stats(int _level, int _attack, string _sharpness, int _affinity, string _elementalType, int _elementalDamage, string _sharpness1, string _sharpness2)
            {
                level = _level;
                attack = _attack;
                sharpness = _sharpness;
                affinity = _affinity;
                elementalType = _elementalType;
                elementalDamage = _elementalDamage;
                sharpness1 = _sharpness1;
                sharpness2 = _sharpness2;
            }
        }

        /// <summary>
        /// Stores the stats of a move of a weapon type.
        /// </summary>
        public struct moveStat
        {
            string name;
            int motionValue;
            double sharpnessMod;
            int KODamage;
            int ExhDamage;

            public moveStat(string _name, int _motionValue, double _sharpnessMod, int _KODamage, int _ExhDamage)
            {
                name = _name;
                motionValue = _motionValue;
                sharpnessMod = _sharpnessMod;
                KODamage = _KODamage;
                ExhDamage = _ExhDamage;
            }
        }

        Dictionary<string, Tuple<double, double>> sharpnessValues = new Dictionary<string, Tuple<double, double>>(); //Stores translation of sharpness to sharpness modifiers
        Dictionary<string, string> str2image = new Dictionary<string, string>(); //Stores the paths to the image files.
        Dictionary<string, List<string>> type2Weapons = new Dictionary<string, List<string>>(); //Stores weapons under weapon types.
        Dictionary<string, List<moveStat>> type2Moves = new Dictionary<string, List<moveStat>>(); //Stores conversion of weapon types to moves.
        Dictionary<string, string> names2FinalNames = new Dictionary<string, string>(); //Stores mapping of names to final names.
        Dictionary<string, string> finalNames2Names = new Dictionary<string, string>(); //Stores mapping of final names to names.
        Dictionary<string, List<stats>> names2Stats = new Dictionary<string, List<stats>>(); //God forgive me. This will store a mapping of names to a list of stats by levels.

        public DmgCalculator()
        {
            InitializeComponent();
            FillOut();
            readFiles();
            this.sharpnessBox.SelectedIndex = 0;
            this.AltDamageField.SelectedIndex = 0;
            AverageSel.Select();
            ElementBox.Image = null;
            FinalEleBox.Image = null;
            EleLabelBox.Image = null;
            EleZoneField.ReadOnly = true;
            EleOut.BackColor = SystemColors.Control;
            FinalEleField.BackColor = SystemColors.Control;
            KOBox.Load("./Images/KO.png");
            ExhaustBox.Load("./Images/Exhaust.png");
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
            Tuple<double, double, double, double, string> finalTuple = calculateMoreDamage(rawEleTuple.Item1, rawEleTuple.Item2);

            RawOut.Text = rawEleTuple.Item1.ToString();
            EleOut.Text = rawEleTuple.Item2.ToString();

            FinalRawField.Text = finalTuple.Item1.ToString();
            FinalEleField.Text = finalTuple.Item2.ToString();

            KOOut.Text = finalTuple.Item3.ToString();
            ExhaustOut.Text = finalTuple.Item4.ToString();

            FinalField.Text = Math.Floor(finalTuple.Item1 + finalTuple.Item2).ToString();

            BounceLabel.Text = finalTuple.Item5;
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

        /// <summary>
        /// Here, I want to set the weapon field's collection to a specific one, based on what was selected here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeaponField.Items.Clear();
            foreach (string weapons in type2Weapons[(string)((ComboBox)sender).SelectedItem])
            {
                WeaponField.Items.Add(weapons);
            }

            WeaponFinalField.Items.Clear();
            foreach (string names in WeaponField.Items)
            {
                WeaponFinalField.Items.Add(names2FinalNames[names]);
            }

            //TODO: Add Movelist to movelist box here.
        }

        private void WeaponField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string weaponName = (string)((ComboBox)sender).SelectedItem;
            if((string)WeaponFinalField.SelectedItem != names2FinalNames[weaponName])
            {
                WeaponFinalField.SelectedItem = names2FinalNames[weaponName];
            }

            LevelField.Items.Clear();
            foreach (stats levels in names2Stats[weaponName])
            {
                LevelField.Items.Add(levels.level);
            }
        }

        private void WeaponFinalField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string weaponFinalName = (string)((ComboBox)sender).SelectedItem;
            if((string)WeaponField.SelectedItem != finalNames2Names[weaponFinalName])
            {
                WeaponField.SelectedItem = finalNames2Names[weaponFinalName];
            }
        }

        private void sharpnessBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sharpness = (string)((ComboBox)sender).SelectedItem;
            RawSharpField.Text = sharpnessValues[sharpness].Item1.ToString();
            EleSharpField.Text = sharpnessValues[sharpness].Item2.ToString();
        }

        private void LevelField_SelectedIndexChanged(object sender, EventArgs e)
        {
            int weaponLevel = (int)((ComboBox)sender).SelectedItem;
            foreach(stats statistics in names2Stats[WeaponField.Text])
            {
                if(statistics.level == weaponLevel)
                {
                    AttackLabel.Text = statistics.attack.ToString();
                    EleTypeLabel.Text = statistics.elementalDamage.ToString();
                    AffinityLabel.Text = statistics.affinity.ToString();
                    SharpnessLabel.Text = statistics.sharpness;
                    OneLabel.Text = statistics.sharpness1;
                    TwoLabel.Text = statistics.sharpness2;

                    WeaponAltField.SelectedItem = statistics.elementalType;
                }
            }
        }

        private void WeaponAltField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)((ComboBox)sender).SelectedItem != "(None)")
            {
                string path = str2image[(string)((ComboBox)sender).SelectedItem];
                EleLabelBox.Load(path);
            }
            else
            {
                EleLabelBox.Image = null;
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

            double rawSharp = double.Parse(RawSharpField.Text);
            double eleSharp = double.Parse(EleSharpField.Text);

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
        private Tuple<double, double, double, double, string> calculateMoreDamage(double item1, double item2)
        {
            double rawZone = double.Parse(HitzoneField.Text) * 0.01;
            double eleZone = double.Parse(EleZoneField.Text) * 0.01;
            double KODam = double.Parse(KOField.Text);
            double ExhDam = double.Parse(ExhaustField.Text);
            double KOZone = double.Parse(KOZoneField.Text) * 0.01;
            double ExhaustZone = double.Parse(ExhaustZoneField.Text) * 0.01;
            double questMod = double.Parse(QuestField.Text);

            item1 = item1 * rawZone * questMod;
            double item3 = KODam * KOZone;
            double item4 = ExhDam * ExhaustZone;

            string element = (string)AltDamageField.SelectedItem;
            if (element != "Poison" && element != "Para" && element != "Sleep" && element != "Blast")
            {
                item2 = item2 * eleZone * questMod;
            }

            string item5 = "No";
            if((rawZone * double.Parse(RawSharpField.Text)) > 0.25 )
            {
                item5 = "No";
            }
            else
            {
                item5 = "Yes";
            }

            return new Tuple<double, double, double, double, string>(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// Updates the calculation fields. Should be called when a change is made.
        /// </summary>
        private void UpdateCalcFields()
        {
            throw new NotImplementedException();
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

        private void readFiles()
        {
            //Read weapon database
            readWeapons();

            //Read motion value database
            //readMotion();

            //Read 
        }

        private void readMotion()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the files included in the Weapons folder and generates weapon type names.
        /// Additionally, also reads inside the files and creates stats based on the weapons within.
        /// </summary>
        private void readWeapons()
        {
            string[] files = System.IO.Directory.GetFiles("./Weapons/", "*.xml", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string type = file.Remove(file.Length - 4); //Strip trailing '.xml'
                type = type.Remove(0, 13); //Strip preceeding './Weapons/' and order numbers
                if(type.Contains('_'))
                {
                    type.Replace('_', ' ');
                }

                TypeField.Items.Add(type);

                List<string> weapons = new List<string>();
                type2Weapons.Add(type, weapons); //Mapping of weapon types to weapons


                //Now we read the files
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                using (XmlReader reader = XmlReader.Create(file,settings))
                {
                    reader.MoveToContent();
                    while(reader.Read())
                    {
                        if(reader.Name == "weapon")
                        {
                            reader.Read(); //Name tag
                            reader.Read(); //Name string
                            string name = reader.Value;
                            weapons.Add(name);
                            List<stats> statistics = new List<stats>();
                            names2Stats.Add(name, statistics);

                            reader.Read(); //end Name
                            reader.Read(); //final name tag
                            reader.Read(); //final name string
                            string finalName = reader.Value;

                            names2FinalNames.Add(name, finalName);
                            finalNames2Names.Add(finalName, name);

                            reader.Read(); //end final name
                            reader.Read(); //level

                            while(reader.NodeType != XmlNodeType.EndElement && reader.Name != "weapon")
                            {
                                int level = int.Parse(reader.GetAttribute("number")); //Get attribute of level number.
                                reader.Read(); //attack tag
                                reader.Read(); //attack int

                                int attack = int.Parse(reader.Value);
                                reader.Read(); //end attack
                                reader.Read(); //sharpness tag
                                reader.Read(); //sharpness string

                                string sharpness = reader.Value;
                                reader.Read(); //end sharpness string
                                reader.Read(); //affinity tag
                                reader.Read(); //affinity int

                                string affinity = reader.Value; //Extract affinity
                                int aff = int.Parse(affinity.Remove(affinity.Length - 1)); //Remove percentage sign
                                reader.Read(); //end affinity tag
                                reader.Read(); //eleType tag
                                reader.Read(); //eleType string

                                string eleType = reader.Value;
                                reader.Read(); //eleType end
                                reader.Read(); //eleDamage tag
                                reader.Read(); //eleDamage int

                                int eleDamage = int.Parse(reader.Value);

                                reader.Read(); //end eleDamage
                                reader.Read(); //sharpness1 tag
                                reader.Read(); //sharpness1 string

                                string sharpness1 = reader.Value;
                                reader.Read(); //end sharpness1
                                reader.Read(); //sharpness2 tag
                                reader.Read(); //sharpness2 string

                                string sharpness2 = reader.Value;
                                reader.Read(); //end sharpness2
                                reader.Read(); //end level
                                reader.Read(); //new level

                                statistics.Add(new stats(level, attack, sharpness, aff, eleType, eleDamage, sharpness1, sharpness2));
                            }
                        }
                    }
                }
            }
        }

        
    }
}
