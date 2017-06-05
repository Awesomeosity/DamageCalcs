using System;
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
            public double attack;
            public string secType;
            public string sharpness;
            public double affinity;
            public string elementalType;
            public double elementalDamage;
            public double secDamage;
            public string sharpness1;
            public string sharpness2;

            /// <summary>
            /// Secondary constructor used while reading the database.
            /// </summary>
            /// <param name="_level">Incoming level.</param>
            /// <param name="_attack">Attack power of the weapon at the enclosing level.</param>
            /// <param name="_secType">Secondary typing of the weapon.</param>
            /// <param name="_sharpness">Maximum sharpness of the weapon at the enclosing level.</param>
            /// <param name="_affinity">Amount of affinity at the level.</param>
            /// <param name="_elementalType">The secondary damage type of the weapon, element or status.</param>
            /// <param name="_elementalDamage">The amount of elemental damage done.</param>
            /// <param name="_secDamage">The amount of elemental damage done.</param>
            /// <param name="_sharpness1">The maximum level of sharpness with Sharpness +1</param>
            /// <param name="_sharpness2">The maximum level of sharpness with Sharpness +2</param>
            public stats(int _level, double _attack, string _secType, string _sharpness, double _affinity, string _elementalType, double _elementalDamage, double _secDamage, string _sharpness1, string _sharpness2)
            {
                level = _level;
                attack = _attack;
                secType = _secType;
                sharpness = _sharpness;
                affinity = _affinity;
                elementalType = _elementalType;
                elementalDamage = _elementalDamage;
                secDamage = _secDamage;
                sharpness1 = _sharpness1;
                sharpness2 = _sharpness2;
            }
        }

        /// <summary>
        /// Stores the stats of a move of a weapon type.
        /// </summary>
        public struct moveStat
        {
            public string name;
            public int id;

            public string onlyFor;
            public string damageType;
            public double totalValue;
            public double perHitValue;
            public int hitCount;
            public double sharpnessMod;
            public double KODamage;
            public double ExhDamage;

            public bool mindsEye;
            public bool draw;
            public bool aerial;

            /// <summary>
            /// Ctor for moveStat struct.
            /// </summary>
            /// <param name="_name">Custom name. Doesn't really matter</param>
            /// <param name="_id">Corresponds to how many button presses you need to get to the move.</param>
            /// <param name="only">Restricts this move to a certain subtype of weapon (Wide Full Burst GL for example)</param>
            /// <param name="_damageType">Cut, Impact, Shot, or Fixed.</param>
            /// <param name="_motionValue">Total MV of the move.</param>
            /// <param name="_perHitValue">Average MV of the move.</param>
            /// <param name="_hitCount">Number of hits in the move.</param>
            /// <param name="_sharpnessMod">The sharpness modifier added when executing the move.</param>
            /// <param name="_KODamage">KO Damage dealt.</param>
            /// <param name="_ExhDamage">Exhaust damage dealt.</param>
            /// <param name="_mindsEye">Whether this move has natural Mind's Eye or not.</param>
            /// <param name="_draw">Is this move a draw attack?</param>
            /// <param name="_aerial">Is this move an aerial attack?</param>
            public moveStat(string _name, int _id, string only, string _damageType, double _motionValue, double _perHitValue, int _hitCount, double _sharpnessMod, double _KODamage, double _ExhDamage, bool _mindsEye, bool _draw, bool _aerial)
            {
                name = _name;
                id = _id;
                onlyFor = only;
                damageType = _damageType;
                totalValue = _motionValue;
                perHitValue = _perHitValue;
                hitCount = _hitCount;
                sharpnessMod = _sharpnessMod;
                KODamage = _KODamage;
                ExhDamage = _ExhDamage;
                mindsEye = _mindsEye;
                draw = _draw;
                aerial = _aerial;
            }
        }

        /// <summary>
        /// TODO
        /// Stores the stats of a single hitzone.
        /// </summary>
        public struct monsterStat
        {

        }

        /// <summary>
        /// Stores the relevant variables from the database portion of the application to
        /// import to the calculator portion.
        /// No ctor. Will be filled in when the UpdateButt is clicked.
        /// </summary>
        public struct importedStats
        {
            public string sharpness;
            public string altDamageType;
            public double affinity;
            public double totalAttackPower;
            public double eleAttackPower;
            public double motionValue;
            public double rawSharpMod;
            public double eleSharpMod;
            public double hiddenMod;
            public double KOPower;
            public double exhaustPower;
            public string eleCrit;
            public double hitzone;
            public double eleHitzone;
            public double questMod;
            public double KOHitzone;
            public double exhaustHitzone;
            public bool criticalBoost;
            public bool mindsEye;

            public double rawMod; //Stores the multiplier of the raw damage.
            public double eleMod; //Stores the elemental multiplier. Has a cap of 1.2x, surpassed when used Demon Riot on an Element Phial SA.
            public double expMod; //Stores the explosive multiplier. Has a cap of 1.3x, 1.4x when considering Impact Phial CB.
            public double staMod; //Stores the status multiplier. Has a cap of 1.25x, surpassed when using Demon Riot on a Status Phial SA.
            public bool CB; //Shows whether or not the explosive multiplier should be increased because Impact Phials are being used. 
            public bool DemonRiot; //Shows whether or not Demon Riot is being used.
        }

        Dictionary<string, Tuple<double, double>> sharpnessValues = new Dictionary<string, Tuple<double, double>>(); //Stores translation of sharpness to sharpness modifiers
        Dictionary<string, string> str2image = new Dictionary<string, string>(); //Stores the paths to the image files.
        Dictionary<string, double> monsterStatus = new Dictionary<string, double>(); //Stores conversion of string to multipliers, used for the monster's status.
        Dictionary<string, List<string>> type2Weapons = new Dictionary<string, List<string>>(); //Stores weapons under weapon types.
        Dictionary<string, List<moveStat>> type2Moves = new Dictionary<string, List<moveStat>>(); //Stores conversion of weapon types to moves.
        Dictionary<string, string> names2FinalNames = new Dictionary<string, string>(); //Stores mapping of names to final names.
        Dictionary<string, string> finalNames2Names = new Dictionary<string, string>(); //Stores mapping of final names to names.
        Dictionary<string, List<stats>> names2Stats = new Dictionary<string, List<stats>>(); //This will store a mapping of names to a list of stats by levels.
        Dictionary<string, bool> armorModifiers = new Dictionary<string, bool>(); //Stores conversion of strings to modifiers.
        Dictionary<string, bool> kitchenItemModifiers = new Dictionary<string, bool>(); //Stores conversion of strings to kitchen modifiers.
        Dictionary<string, bool> weaponModifiers = new Dictionary<string, bool>(); //Stores conversion of strings to weapon-specific modifiers.
        Dictionary<string, bool> otherModifiers = new Dictionary<string, bool>(); //Will store other things.

        importedStats weaponAndMods = new importedStats(); //Will be used later. Required to be global for the modifier methods.

        /// <summary>
        /// Called when initializing the form.
        /// </summary>
        public DmgCalculator()
        {
            InitializeComponent(); //Required.
            FillOut(); //Fills out dictionaries.
            readFiles(); //Read the xml files and fills out the database.
            paraBoost.Checked = false; //Force checkboxes to be unchecked on initialization.
            moveMinds.Checked = false;
            paraFixed.Checked = false;
            paraMinds.Checked = false;
            paraSharpness.SelectedIndex = 0; //Force comboBoxes to be set to a certain position.
            paraAltType.SelectedIndex = 0;
            paraEleCrit.SelectedIndex = 0;
            paraMonStat.SelectedIndex = 0;
            paraSecEle.SelectedIndex = 0;
            AverageSel.Select(); //Force selection of a radio button.
            calcEleBox.Image = null; //Force clear of picture boxes.
            calcFinalEleBox.Image = null;
            weapAltBox.Image = null;
            calcSecBox.Image = null;
            calcFinalSecBox.Image = null;
            paraEleHitzone.ReadOnly = true; //Force readonly to be true on elemental boxes (Assume just raw damage on startup)
            paraSecPower.ReadOnly = true;
            paraSecHitzone.ReadOnly = true;
            calcEleOut.BackColor = SystemColors.Control; //These are labels, which means we need to change the background color instead.
            calcEle.BackColor = SystemColors.Control;
            calcSecOut.BackColor = SystemColors.Control;
            calcFinalSec.BackColor = SystemColors.Control;
            calcKOBox.Load("./Images/KO.png"); //Load the images from the folder.
            calcExhBox.Load("./Images/Exhaust.png");
            moveKOBox.Load("./Images/KO.png");
            moveExhBox.Load("./Images/Exhaust.png");
            weapSecBox.Image = null;
            weapOverride.Checked = false;
            weapOverride.Enabled = false;
            paraSecEle.Enabled = false;
        }

        //EVENT FUNCTIONS
        /// <summary>
        /// Checks if whatever's put into the field can be converted into a double.
        /// If no, then throws an error.
        /// If yes, then allows the user to continue.
        /// </summary>
        /// <param name="sender">Any field in which the user can input numbers.</param>
        /// <param name="e"></param>
        private void GenericField_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!calcFieldValidation(((TextBox)sender).Text, out errorMsg))
            {
                e.Cancel = true;
                ((TextBox)sender).Select(0, ((TextBox)sender).Text.Length);

                this.ErrorPreventer.SetError((TextBox)sender, errorMsg);
            }
        }

        /// <summary>
        /// Resets the ErrorPreventer if the input is correct.
        /// </summary>
        /// <param name="sender">Any field in which the user can input numbers.</param>
        /// <param name="e"></param>
        private void GenericField_Validated(object sender, System.EventArgs e)
        {
            ErrorPreventer.SetError((TextBox)sender, "");
        }

        /*CalcButt Functions*/
        /// <summary>
        /// Executed when the calculate output button is clicked.
        /// </summary>
        /// <param name="sender">Should only be the 'Calculate Output' button.</param>
        /// <param name="e"></param>
        private void CalcButt_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double, double> rawEleOut = calculateDamage(); //Helper function.

            calcRawOut.Text = rawEleOut.Item1.ToString(); //Used the Tuple output from the function to fill in the labels.
            calcEleOut.Text = rawEleOut.Item2.ToString();
            calcSecOut.Text = rawEleOut.Item3.ToString();
        }

        /*CalcAll Functions*/
        /// <summary>
        /// Executed when the 'Calculate All' button is clicked.
        /// </summary>
        /// <param name="sender">Should only be the 'Calculate All' button.</param>
        /// <param name="e"></param>
        private void CalcAll_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double, double> rawEleTuple = calculateDamage(); //Use helper function.
            Tuple<double, double, double, double, double, string, double> finalTuple = calculateMoreDamage(rawEleTuple.Item1, rawEleTuple.Item2, rawEleTuple.Item3); //Another one.

            calcRawOut.Text = rawEleTuple.Item1.ToString(); //Do as the CalcButt function does
            calcEleOut.Text = rawEleTuple.Item2.ToString();
            calcSecOut.Text = rawEleTuple.Item3.ToString();

            calcFinalRaw.Text = finalTuple.Item2.ToString(); //But with use of the outputted tuple from the moreDamage function.
            calcEle.Text = finalTuple.Item3.ToString();

            calcKO.Text = finalTuple.Item4.ToString();
            calcExh.Text = finalTuple.Item5.ToString();

            calcFinalSec.Text = finalTuple.Item7.ToString();

            calcFinal.Text = finalTuple.Item1.ToString();

            calcBounce.Text = finalTuple.Item6;
        }

        /// <summary>
        /// Changes several control elements on the form when the alternate damage form is changed.
        /// Changes the picture boxes and relevant fields to be not read-only if an element is selected.
        /// </summary>
        /// <param name="sender">Should only be the AltDamageField text box.</param>
        /// <param name="e"></param>
        private void AltDamageField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string element = (string)((ComboBox)sender).SelectedItem;
            if (element != "(None)") //If there is an element
            {
                string path = str2image[element];
                calcEleBox.Load(path);
                calcFinalEleBox.Load(path);
                paraEle.ReadOnly = false;
                calcEleOut.BackColor = SystemColors.ControlLightLight;
                calcEle.BackColor = SystemColors.ControlLightLight;

                if (element == "Poison" | element == "Para" | element == "Sleep" | element == "Blast") //If the element is a status
                {
                    paraEleHitzone.ReadOnly = true;
                    paraEleHitzone.Text = 0.ToString();
                }
                else
                {
                    paraEleHitzone.ReadOnly = false;
                }

                paraSecEle.Enabled = true;
            }

            else //If there isn't an element
            {
                calcEleBox.Image = null;
                calcFinalEleBox.Image = null;
                calcEleOut.BackColor = System.Drawing.SystemColors.Control;
                calcEle.BackColor = System.Drawing.SystemColors.Control;
                paraEle.ReadOnly = true;
                paraEleHitzone.ReadOnly = true;
                paraEle.Text = 0.ToString();
                paraEleHitzone.Text = 0.ToString();
                paraSecEle.Enabled = false;
                paraSecEle.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Like the above, but for a DB's secondary element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string element = (string)((ComboBox)sender).SelectedItem;
            if (element != "(None)") //If there is an element
            {
                string path = str2image[element];
                calcSecBox.Load(path);
                calcFinalSecBox.Load(path);
                paraSecPower.ReadOnly = false;
                calcSecOut.BackColor = SystemColors.ControlLightLight;
                calcFinalSec.BackColor = SystemColors.ControlLightLight;

                if (element == "Poison" | element == "Para" | element == "Sleep" | element == "Blast") //If the element is a status
                {
                    paraSecHitzone.ReadOnly = true;
                    paraSecHitzone.Text = 0.ToString();
                }
                else
                {
                    paraSecHitzone.ReadOnly = false;
                }
            }

            else //If there isn't an element
            {
                calcSecBox.Image = null;
                calcFinalSecBox.Image = null;
                calcSecOut.BackColor = System.Drawing.SystemColors.Control;
                calcFinalSec.BackColor = System.Drawing.SystemColors.Control;
                paraSecPower.ReadOnly = true;
                paraSecHitzone.ReadOnly = true;
                paraSecPower.Text = 0.ToString();
                paraSecHitzone.Text = 0.ToString();
            }
        }

        /// <summary>
        /// Here, I want to set the weapon field's collection to a specific one, based on what was selected here.
        /// </summary>
        /// <param name="sender">Should only be the TypeField comboBox.</param>
        /// <param name="e"></param>
        private void TypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            weapName.Items.Clear();
            foreach (string weapons in type2Weapons[(string)((ComboBox)sender).SelectedItem])
            {
                weapName.Items.Add(weapons); //Fill in weapon names
            }

            weapFinal.Items.Clear();
            foreach (string names in weapName.Items)
            {
                weapFinal.Items.Add(names2FinalNames[names]); //Fill in weapon names at final forms.
            }

            fillMoves((string)((ComboBox)sender).SelectedItem, "None");

            
        }

        

        /// <summary>
        /// Adds levels of the weapon selected to the level selection box.
        /// </summary>
        /// <param name="sender">Should always be the WeaponField comboBox</param>
        /// <param name="e"></param>
        private void WeaponField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string weaponName = (string)((ComboBox)sender).SelectedItem;
            if ((string)weapFinal.SelectedItem != names2FinalNames[weaponName])
            {
                weapFinal.SelectedItem = names2FinalNames[weaponName];
            }

            weapLevel.Items.Clear();
            foreach (stats levels in names2Stats[weaponName])
            {
                weapLevel.Items.Add(levels.level);
            }
        }

        /// <summary>
        /// This should only tell the weapon field to change its index, so that its event triggers.
        /// </summary>
        /// <param name="sender">Only WeaponFinalField</param>
        /// <param name="e"></param>
        private void WeaponFinalField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string weaponFinalName = (string)((ComboBox)sender).SelectedItem;
            if ((string)weapName.SelectedItem != finalNames2Names[weaponFinalName])
            {
                weapName.SelectedItem = finalNames2Names[weaponFinalName];
            }
        }

        /// <summary>
        /// Controls the sharpness box in the hunter parameters. Should automatically change the eleSharp and rawSharp boxes.
        /// Note to self: If this event fires and forces those boxes to have sharpness mods equivalent to the sharpness,
        /// and not considers other sharpness modifiers, look here first.
        /// </summary>
        /// <param name="sender">sharpnessBox Only.</param>
        /// <param name="e"></param>
        private void sharpnessBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sharpness = (string)((ComboBox)sender).SelectedItem;
            paraRawSharp.Text = sharpnessValues[sharpness].Item1.ToString();
            paraEleSharp.Text = sharpnessValues[sharpness].Item2.ToString();
        }

        /// <summary>
        /// After a level is chosen, updates the Weapon Base Stats groupbox controls to have correct values.
        /// </summary>
        /// <param name="sender">Only LevelField.</param>
        /// <param name="e"></param>
        private void LevelField_SelectedIndexChanged(object sender, EventArgs e)
        {
            int weaponLevel = (int)((ComboBox)sender).SelectedItem;
            foreach (stats statistics in names2Stats[weapName.Text])
            {
                if (statistics.level == weaponLevel)
                {
                    weapAttack.Text = statistics.attack.ToString();
                    weapAltPower.Text = statistics.elementalDamage.ToString();
                    weapSecPower.Text = statistics.secDamage.ToString();
                    weapAffinity.Text = statistics.affinity.ToString();
                    weapSharpness.Text = statistics.sharpness;
                    weapOne.Text = statistics.sharpness1;
                    weapTwo.Text = statistics.sharpness2;

                    weapAlt.SelectedItem = statistics.elementalType;
                    weapSecType.SelectedItem = statistics.secType;
                }
            }
        }

        /// <summary>
        /// Changes the elemental picture box in weaponBaseStats.
        /// </summary>
        /// <param name="sender">Only Weapon AltField.</param>
        /// <param name="e"></param>
        private void WeaponAltField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)((ComboBox)sender).SelectedItem != "(None)")
            {
                string path = str2image[(string)((ComboBox)sender).SelectedItem];
                weapAltBox.Load(path);
            }
            else
            {
                weapAltBox.Image = null;
            }
        }

        /// <summary>
        /// Disables some controls or reenables them, depending on the status of the check box.
        /// This check box controls if the damage is fixed-type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (paraFixed.Checked)
            {
                paraSharpness.SelectedIndex = 0;
                paraSharpness.Enabled = false;
                paraEleCrit.SelectedIndex = 0;
                paraEleCrit.Enabled = false;
                paraAffinity.Text = 0.ToString();
                paraAffinity.ReadOnly = true;
                paraRawSharp.Text = 1.0.ToString();
                paraRawSharp.ReadOnly = true;
                paraEleSharp.Text = 1.0.ToString();
                paraEleSharp.ReadOnly = true;
                paraRaw.Text = 100.ToString();
                paraRaw.ReadOnly = true;
                paraRawHitzone.Text = 0.ToString();
                paraRawHitzone.ReadOnly = true;
            }
            else
            {
                paraSharpness.Enabled = true;
                paraEleCrit.Enabled = true;
                paraAffinity.ReadOnly = false;
                paraRawSharp.ReadOnly = false;
                paraEleSharp.ReadOnly = false;
                paraRaw.ReadOnly = false;
                paraRawHitzone.ReadOnly = false;
            }
        }

        private void NameSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)((ComboBox)sender).SelectedItem;
            if ((string)MotionSort.SelectedItem != name)
            {
                MotionSort.SelectedItem = name;
            }

            if ((string)ComboSort.SelectedItem != name)
            {
                ComboSort.SelectedItem = name;
            }

            foreach (moveStat move in type2Moves[(string)weapType.SelectedItem])
            {
                if (move.name == name)
                {
                    moveTotal.Text = move.totalValue.ToString();
                    moveSharp.Text = move.sharpnessMod.ToString();
                    moveAvg.Text = move.perHitValue.ToString();
                    moveHitCount.Text = move.hitCount.ToString();
                    moveKO.Text = move.KODamage.ToString();
                    moveExh.Text = move.ExhDamage.ToString();
                    moveMinds.Checked = move.mindsEye;
                    moveDamType.SelectedItem = move.damageType;
                    moveDraw.Checked = move.draw;
                    moveAerial.Checked = move.aerial;
                }
            }
        }

        private void MotionSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)((ComboBox)sender).SelectedItem;
            if ((string)MotionSort.SelectedItem != name)
            {
                ComboSort.SelectedItem = name;
            }

            if ((string)ComboSort.SelectedItem != name)
            {
                NameSort.SelectedItem = name;
            }
        }

        private void ComboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)((ComboBox)sender).SelectedItem;
            if ((string)MotionSort.SelectedItem != name)
            {
                MotionSort.SelectedItem = name;
            }

            if ((string)NameSort.SelectedItem != name)
            {
                NameSort.SelectedItem = name;
            }
        }

        /// <summary>
        /// Updates the Total MV Text Field when text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MVField_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                string temp = (double.Parse(((TextBox)sender).Text) * double.Parse(paraHitCount.Text)).ToString();
                if (paraTotal.Text != temp)
                {
                    paraTotal.Text = temp;
                }
            }
        }

        /// <summary>
        /// Like the above, but for the reverse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                string temp = (double.Parse(((TextBox)sender).Text) / double.Parse(paraHitCount.Text)).ToString();
                if (paraMV.Text != temp)
                {
                    paraMV.Text = temp;
                }
            }
        }

        /// <summary>
        /// Like the above. But uses the average motion values to make a new total value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                paraTotal.Text = (double.Parse(paraMV.Text) * double.Parse(((TextBox)sender).Text)).ToString();
            }
        }

        /// <summary>
        /// Updates the Avg MV Text Field when text is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveAvg_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                string temp = (double.Parse(((TextBox)sender).Text) * double.Parse(moveHitCount.Text)).ToString();
                if (moveTotal.Text != temp)
                {
                    moveTotal.Text = temp;
                }
            }
        }

        /// <summary>
        /// Like the above, but for the reverse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveTotal_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                string temp = (double.Parse(((TextBox)sender).Text) / double.Parse(moveHitCount.Text)).ToString();
                if (moveAvg.Text != temp)
                {
                    moveAvg.Text = temp;
                }
            }
        }

        /// <summary>
        /// Like the above. But uses the average motion values to make a new total value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveHitCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                moveTotal.Text = (double.Parse(moveAvg.Text) * double.Parse(((TextBox)sender).Text)).ToString();
            }
        }

        private void weapOverride_CheckedChanged(object sender, EventArgs e)
        {
            fillMoves(weapType.SelectedText, weapSecType.SelectedText);
        }

        private void weapSecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            if(index == 0)
            {
                weapSecBox.Image = null;
                weapOverride.Checked = false;
                weapOverride.Enabled = false;
            }
            else if (index == 10 || index == 11 || index == 12 || index == 44)
            {
                weapSecBox.Image = null;
            }

            else if (index == 1 || index == 18 || index == 19 || index == 20 || index == 21)
            {
                weapSecBox.Load(str2image["Fire"]);
            }

            else if (index == 2 || index == 22 || index == 23 || index == 24 || index == 25)
            {
                weapSecBox.Load(str2image["Water"]);
            }

            else if (index == 3 || index == 30 || index == 31 || index == 32 || index == 33)
            {
                weapSecBox.Load(str2image["Ice"]);
            }

            else if (index == 4 || index == 26 || index == 27 || index == 28 || index == 29)
            {
                weapSecBox.Load(str2image["Thunder"]);
            }

            else if (index == 5 || index == 13 || index == 34 || index == 35)
            {
                weapSecBox.Load(str2image["Dragon"]);
            }

            else if (index == 6 || index == 14 || index == 36 || index == 37 || index == 45)
            {
                weapSecBox.Load(str2image["Poison"]);
            }

            else if (index == 7 || index == 15 || index == 38 || index == 39 || index == 46)
            {
                weapSecBox.Load(str2image["Para"]);
            }

            else if (index == 8 || index == 40 || index == 41 || index == 47)
            {
                weapSecBox.Load(str2image["Sleep"]);
            }

            else if (index == 9 || index == 42 || index == 43 || index == 49)
            {
                weapSecBox.Load(str2image["Blast"]);
            }

            else if (index == 4 || index == 26 || index == 27 || index == 28 || index == 29)
            {
                weapSecBox.Load(str2image["Thunder"]);
            }

            else if (index == 16 || index == 48)
            {
                weapSecBox.Load("./Images/KO.png");
            }

            else if (index == 17)
            {
                weapSecBox.Image = weapAltBox.Image;
            }

            if (index != 0)
            {
                weapOverride.Enabled = true;
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
            if (!double.TryParse(input, out result))
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
        /// <returns>A Tuple storing the Raw and Elemental damage outputs.</returns>
        private Tuple<double, double, double> calculateDamage()
        {
            double total = double.Parse(paraRaw.Text);
            double motion = double.Parse(paraTotal.Text) * 0.01;
            double affinity = double.Parse(paraAffinity.Text) * 0.01;
            double element = double.Parse(paraEle.Text);
            double DBElement = double.Parse(paraSecPower.Text);

            double rawSharp = double.Parse(paraRawSharp.Text);
            double eleSharp = double.Parse(paraEleSharp.Text);

            double rawTotal = 0;
            double eleTotal = 0;
            double DBTotal = 0;

            double subAffinity = affinity; //Temp affinity used in calculations
            double critBoost = .25;
            double eleCrit = 0;
            double statusCrit = 0;

            if (paraFixed.Checked) //If fixed damage is in play
            {
                return new Tuple<double, double, double>(total, element, DBElement);
            }

            else //If it is in play
            {
                if (NeutralSel.Checked)
                {
                    subAffinity = 0;
                }
                else if (PositiveSel.Checked)
                {
                    subAffinity = 100;
                }
                else if (NegativeSel.Checked)
                {
                    subAffinity = -100;
                }

                if (AverageSel.Checked || PositiveSel.Checked)
                {
                    if (paraBoost.Checked)
                    {
                        critBoost = .40;
                    }

                    if (paraStatusCrit.Checked)
                    {
                        statusCrit = .2;
                    }

                    if (paraEleCrit.SelectedIndex == 1)
                    {
                        eleCrit = 0.2;
                    }
                    else if (paraEleCrit.SelectedIndex == 2)
                    {
                        eleCrit = 0.3;
                    }
                    else if (paraEleCrit.SelectedIndex == 3)
                    {
                        eleCrit = 0.35;
                    }
                    else if (paraEleCrit.SelectedIndex == 4)
                    {
                        eleCrit = 0.25;
                    }
                }

                rawTotal = total * (1 + subAffinity * critBoost) * rawSharp * motion;

                string ele = (string)paraAltType.SelectedItem;
                string secEle = (string)paraSecEle.SelectedItem;
                if (ele != "Poison" && ele != "Para" && ele != "Sleep" && ele != "Blast")
                {
                    eleTotal = element * eleSharp * (1 + subAffinity * eleCrit);
                }
                else if (ele != "Blast")
                {
                    eleTotal = element * eleSharp * (1 + subAffinity * statusCrit);
                }
                else
                {
                    eleTotal = element * eleSharp;
                }

                if (secEle != "Poison" && secEle != "Para" && secEle != "Sleep" && secEle != "Blast")
                {
                    DBTotal = DBElement * eleSharp * (1 + subAffinity * eleCrit);
                }
                else if (secEle != "Blast")
                {
                    DBTotal = DBElement * eleSharp * (1 + subAffinity * statusCrit);
                }
                else
                {
                    DBTotal = DBElement * eleSharp;
                }
            }

            return new Tuple<double, double, double>(rawTotal, eleTotal, DBTotal);
        }

        /// <summary>
        /// This function calculates the damage with hitzones.
        /// </summary>
        /// <param name="rawDamage">Raw damage.</param>
        /// <param name="elementalDamage">Elemental damage.</param>
        /// <returns>A Tuple storing the total, raw, element, DB's second element, KO, exhaust, and bounce status of the attack after calculations.</returns>
        private Tuple<double, double, double, double, double, string, double> calculateMoreDamage(double rawDamage, double elementalDamage, double DBElement)
        {
            double rawZone = double.Parse(paraRawHitzone.Text) * 0.01;
            double eleZone = double.Parse(paraEleHitzone.Text) * 0.01;
            double KODam = double.Parse(paraKO.Text);
            double ExhDam = double.Parse(paraExh.Text);
            double KOZone = double.Parse(paraKOZone.Text) * 0.01;
            double ExhaustZone = double.Parse(paraExhZone.Text) * 0.01;
            double questMod = double.Parse(paraQuest.Text);

            rawDamage *= monsterStatus[(string)paraMonStat.SelectedItem];
            double totaldamage = rawDamage;
            double KODamage = KODam * KOZone;
            double ExhDamage = ExhDam * ExhaustZone;
            string BounceBool = "No";

            if (!paraFixed.Checked)
            {
                rawDamage = rawDamage * rawZone * questMod;

                if ((rawZone * double.Parse(paraRawSharp.Text)) > 0.25 || paraMinds.Checked)
                {
                    BounceBool = "No";
                }
                else
                {
                    BounceBool = "Yes";
                }
            }
            else
            {
                rawDamage *= questMod;
            }

            string element = (string)paraAltType.SelectedItem;
            if (element != "Poison" && element != "Para" && element != "Sleep" && element != "Blast")
            {
                elementalDamage = elementalDamage * eleZone * questMod;
                totaldamage = rawDamage + elementalDamage;
            }

            if (paraSecEle.Text != "(None)") //For DB's Second Element
            {
                string altElement = (string)paraSecEle.SelectedItem;
                if (altElement != "Poison" && altElement != "Para" && altElement != "Sleep" && altElement != "Blast")
                {
                    DBElement = DBElement * eleZone * questMod;
                    totaldamage += DBElement;
                }
            }

            totaldamage = Math.Floor(totaldamage);

            return new Tuple<double, double, double, double, double, string, double>(totaldamage, rawDamage, elementalDamage, KODamage, ExhDamage, BounceBool, DBElement);
        }

        private void fillMoves(string selectedItem, string v)
        {
            NameSort.Items.Clear();
            MotionSort.Items.Clear();
            ComboSort.Items.Clear();

            List<moveStat> tempListMotion = new List<moveStat>();
            List<moveStat> tempListCombo = new List<moveStat>();

            foreach (moveStat moves in type2Moves[selectedItem]) //Fills out the moves for the move search box.
            {
                if (moves.onlyFor == v)
                {
                    NameSort.Items.Add(moves.name);
                    tempListMotion.Add(moves);
                    tempListCombo.Add(moves);
                }
            }

            quickSortVar1(tempListMotion, 1, tempListMotion.Count - 1); //Usage of quickSort.
            quickSortVar2(tempListCombo, 1, tempListCombo.Count - 1);

            foreach (moveStat moves in tempListMotion)
            {
                MotionSort.Items.Add(moves.name);
            }

            foreach (moveStat moves in tempListCombo)
            {
                ComboSort.Items.Add(moves.name);
            }
        }

        /// <summary>
        /// Fills the global Dictionaries with data.
        /// </summary>
        private void FillOut()
        {
            sharpnessValues.Add("(No Sharpness)", new Tuple<double, double>(1.00, 1.00));
            sharpnessValues.Add("White", new Tuple<double, double>(1.32, 1.12));
            sharpnessValues.Add("Blue", new Tuple<double, double>(1.20, 1.06));
            sharpnessValues.Add("Green", new Tuple<double, double>(1.05, 1.0));
            sharpnessValues.Add("Yellow", new Tuple<double, double>(1.00, 0.75));
            sharpnessValues.Add("Orange", new Tuple<double, double>(0.75, 0.50));
            sharpnessValues.Add("Red", new Tuple<double, double>(0.50, 0.25));

            str2image.Add("(None)", "No Image");
            str2image.Add("Fire", "./Images/Fire.png");
            str2image.Add("Water", "./Images/Water.png");
            str2image.Add("Thunder", "./Images/Thunder.png");
            str2image.Add("Ice", "./Images/Ice.png");
            str2image.Add("Dragon", "./Images/Dragon.png");
            str2image.Add("Poison", "./Images/Poison.png");
            str2image.Add("Sleep", "./Images/Sleep.png");
            str2image.Add("Para", "./Images/Para.png");
            str2image.Add("Blast", "./Images/Blast.png");

            monsterStatus.Add("Normal", 1);
            monsterStatus.Add("Pitfall Trapped", 1.1);
            monsterStatus.Add("Sleeping (Bomb)", 3);
            monsterStatus.Add("Sleeping (Else)", 2);
            monsterStatus.Add("Paralyzed", 1.1);

            //Armor skills section
#if false
            armorModifiers.Add("Art. Novice (Fixed Weapons)",   Artillery(1));
            armorModifiers.Add("Art. Novice (Explosive Ammo)",  Artillery(2));
            armorModifiers.Add("Art. Novice (Impact CB)",       Artillery(3));
            armorModifiers.Add("Art. Novice (GL)",              Artillery(4));
            armorModifiers.Add("Art. Expert (Fixed Weapons)",   Artillery(5));
            armorModifiers.Add("Art. Expert (Explosive Ammo)",  Artillery(6));
            armorModifiers.Add("Art. Expert (Impact CB)",       Artillery(7));
            armorModifiers.Add("Art. Expert (GL)",              Artillery(8));
            armorModifiers.Add("Attack Up (S)",                 Attack(1));
            armorModifiers.Add("Attack Up (M)",                 Attack(2));
            armorModifiers.Add("Attack Up (L)",                 Attack(3));
            armorModifiers.Add("Attack Down (S)",               Attack(4));
            armorModifiers.Add("Attack Down (M)",               Attack(5));
            armorModifiers.Add("Attack Down (L)",               Attack(6));

            armorModifiers.Add("Bludgeoner (Green)",            Blunt(1));
            armorModifiers.Add("Bludgeoner (Yellow)",           Blunt(2));
            armorModifiers.Add("Bludgeoner (Orange/Red)",       Blunt(3));
            armorModifiers.Add("Bombardier (Blast)",            BombBoost(1));
            armorModifiers.Add("Bombardier (Bomb)",             BombBoost(2));

            armorModifiers.Add("Repeat Offender (1 Hit)",       ChainCrit(1));
            armorModifiers.Add("Repeat Offender (>5 Hits)",     ChainCrit(2));
            armorModifiers.Add("Trump Card (Lion's Maw)",       Chance(1));
            armorModifiers.Add("Trump Card (Dragon's Breath)",  Chance(2));
            armorModifiers.Add("Trump Card (Demon Riot 'Pwr')", Chance(3));
            armorModifiers.Add("Trump Card (Demon Riot 'Sta')", Chance(4));
            armorModifiers.Add("Trump Card (Demon Riot 'Ele')", Chance(5));
            armorModifiers.Add("Trump Card (Demon Riot 'Dra')", Chance(6));
            armorModifiers.Add("Trump Card (Other HAs)",        Chance(7));
            armorModifiers.Add("Polar Hunter (Cool Drink)",     ColdBlooded(1));
            armorModifiers.Add("Polar Hunter (Cold Areas)",     ColdBlooded(2));
            armorModifiers.Add("Polar Hunter (Both Effects)",   ColdBlooded(3));
            armorModifiers.Add("Resuscitate",                   Crisis());
            armorModifiers.Add("Critical Draw",                 CritDraw());
            armorModifiers.Add("Elemental Crit (GS)",           CritElement(1));
            armorModifiers.Add("Elemental Crit (LBG/HBG)",      CritElement(2));
            armorModifiers.Add("Elemental Crit (SnS/DB/Bow)",   CritElement(3));
            armorModifiers.Add("Elemental Crit (Other)",        CritElement(4));
            armorModifiers.Add("Status Crit",                   CritStatus();
            armorModifiers.Add("Critical Boost",                CriticalUp());

            armorModifiers.Add("P. D. Fencer (1st Cart)",       DFencing(1));
            armorModifiers.Add("P. D. Fencer (2nd Cart)",       DFencing(2));
            armorModifiers.Add("Deadeye Soul",                  Deadeye());
            armorModifiers.Add("Dragon Atk +1",                 DragonAtk(1));
            armorModifiers.Add("Dragon Atk +2",                 DragonAtk(2));
            armorModifiers.Add("Dragon Atk Down",               DragonAtk(3));
            armorModifiers.Add("Dreadking Soul",                Dreadking());
            armorModifiers.Add("Dreadqueen Soul",               Dreadqueen());
            armorModifiers.Add("Drilltusk Soul",                Drilltusk());

            armorModifiers.Add("Element Atk Up",                Elemental());
            armorModifiers.Add("Critical Eye +1",               Expert(1));
            armorModifiers.Add("Critical Eye +2",               Expert(2));
            armorModifiers.Add("Critical Eye +3",               Expert(3));
            armorModifiers.Add("Critical Eye -1",               Expert(4));
            armorModifiers.Add("Critical Eye -2",               Expert(5));
            armorModifiers.Add("Critical Eye -3",               Expert(6));

            armorModifiers.Add("Mind's Eye",                    Fencing());
            armorModifiers.Add("Fire Atk +1",                   FireAtk(1));
            armorModifiers.Add("Fire Atk +2",                   FireAtk(2));
            armorModifiers.Add("Fire Atk Down",                 FireAtk(3));
            armorModifiers.Add("Antivirus",                     FrenzyRes());
            armorModifiers.Add("Resentment",                    Furor());

            armorModifiers.Add("Latent Power +1",               GlovesOff(1));
            armorModifiers.Add("Latent Power +2",               GlovesOff(2));
            
            armorModifiers.Add("Sharpness +1",                  Handicraft(1));
            armorModifiers.Add("Sharpness +2",                  Handicraft(2));
            armorModifiers.Add("TrueShot Up",                   Haphazard());
            armorModifiers.Add("Heavy/Heavy Up",                HeavyUp());
            armorModifiers.Add("Hellblade Soul",                Hellblade());
            armorModifiers.Add("Tropic Hunter (Hot Drink)",     HotBlooded(1));
            armorModifiers.Add("Tropic Hunter (Hot Area)",      HotBlooded(2));
            armorModifiers.Add("Tropic Hunter (Both Effects)",  HotBlooded(3));

            armorModifiers.Add("Ice Atk +1",                    IceAtk(1));
            armorModifiers.Add("Ice Atk +2",                    IceAtk(2));
            armorModifiers.Add("Ice Atk Down",                  IceAtk(3));

            armorModifiers.Add("KO King",                       KO());

            armorModifiers.Add("Normal/Rapid Up",               NormalUp());

            armorModifiers.Add("Pellet/Spread Up (Pellet S)",   PelletUp(1));
            armorModifiers.Add("Pellet/Spread Up (Spread)",     PelletUp(2));
            armorModifiers.Add("Pierce/Pierce Up",              PierceUp());
            armorModifiers.Add("Adrenaline +2",                 Potential(2));
            armorModifiers.Add("Worrywart",                     Potential(3));
            armorModifiers.Add("Punishing Draw (Cut)",          PunishDraw(1));
            armorModifiers.Add("Punishing Draw (Impact)",       PunishDraw(2));

            armorModifiers.Add("Bonus Shot",                    RapidFire());
            armorModifiers.Add("Redhelm Soul",                  Redhelm());

            armorModifiers.Add("Silverwind Soul",               Silverwind());
            armorModifiers.Add("Challenger +1",                 Spirit(1));
            armorModifiers.Add("Challenger +2",                 Spirit(2));
            armorModifiers.Add("Stamina Thief",                 StamDrain());
            armorModifiers.Add("Status Atk +1",                 Status(1));
            armorModifiers.Add("Status Atk +2",                 Status(2));
            armorModifiers.Add("Status Atk Down",               Status(3));
            armorModifiers.Add("Fortify (1st Cart)",            Survivor(1));
            armorModifiers.Add("Fortify (2nd Cart)",            Survivor(2));

            armorModifiers.Add("Weakness Exploit",              Tenderizer());
            armorModifiers.Add("Thunder Atk +1",                ThunderAtk(1));
            armorModifiers.Add("Thunder Atk +2",                ThunderAtk(2));
            armorModifiers.Add("Thunder Atk Down",              ThunderAtk(3));
            armorModifiers.Add("Thunderlord Soul",              Thunderlord());

            armorModifiers.Add("Peak Performance",              Unscathed());

            armorModifiers.Add("Airborne",                      Vault());

            armorModifiers.Add("Water Atk +1",                  WaterAtk(1));
            armorModifiers.Add("Water Atk +2",                  WaterAtk(2));
            armorModifiers.Add("Water Atk Down",                WaterAtk(3));
#endif
            //Item/Kitchen Modifiers.
#if true
            kitchenItemModifiers.Add("F.Bombardier (Fixed Weaps.)", FBombardier(1));
            kitchenItemModifiers.Add("F.Bombardier (Explosive S)", FBombardier(2));
            kitchenItemModifiers.Add("F.Bombardier (Impact CB)", FBombardier(3));
            kitchenItemModifiers.Add("F.Bombardier (GL)", FBombardier(4));
            kitchenItemModifiers.Add("F.Booster", FBooster());
            kitchenItemModifiers.Add("F.Bulldozer", FBulldozer());
            kitchenItemModifiers.Add("F.Heroics", FHeroics());
            kitchenItemModifiers.Add("F.Pyro", FPyro());
            //kitchenItemModifiers.Add("F.Rider",                     FRider()); //Removed because not considering Mount damage.
            kitchenItemModifiers.Add("F.Sharpshooter", FSharpshooter());
            kitchenItemModifiers.Add("F.Slugger", FSlugger());
            kitchenItemModifiers.Add("F.Specialist", FSpecialist());
            kitchenItemModifiers.Add("F.Temper", FTemper());
            kitchenItemModifiers.Add("Cool Cat", CoolCat());

            kitchenItemModifiers.Add("Powercharm", Powercharm());
            kitchenItemModifiers.Add("Power Talon", PowerTalon());
            kitchenItemModifiers.Add("Demon Drug", DemonDrug(1));
            kitchenItemModifiers.Add("Mega Demon Drug", DemonDrug(2));
            kitchenItemModifiers.Add("Attack Up (S) Meal", AUMeal(1));
            kitchenItemModifiers.Add("Attack Up (M) Meal", AUMeal(2));
            kitchenItemModifiers.Add("Attack Up (L) Meal", AUMeal(3));
            kitchenItemModifiers.Add("Might Seed", MightSeed(1));
            kitchenItemModifiers.Add("Might Pill", MightSeed(2));
            kitchenItemModifiers.Add("Nitroshroom (Mushromancer)", Nitroshroom());
            kitchenItemModifiers.Add("Demon Horn", Demon(1));
            kitchenItemModifiers.Add("Demon S", Demon(2));
            kitchenItemModifiers.Add("Demon Affinity S", Demon(3));
#endif
            //Weapon Mods
#if false
            weaponModifiers.Add("Low Sharpness Modifier (0.6x)",        LSM(1));
            weaponModifiers.Add("Low Sharpness Modifier (0.7x)",        LSM(2));
            weaponModifiers.Add("GS - Center of Blade",                 GS(1));
            weaponModifiers.Add("GS - Lion's Maw I",                    GS(2));
            weaponModifiers.Add("GS - Lion's Maw II",                   GS(3));
            weaponModifiers.Add("GS - Lion's Maw III",                  GS(4));
            weaponModifiers.Add("LS - Center of Blade",                 LS(1));
            weaponModifiers.Add("LS - Spirit Gauge ON",                 LS(2));
            weaponModifiers.Add("LS - Spirit Gauge (White)",            LS(3));
            weaponModifiers.Add("LS - Spirit Gauge (Yellow)",           LS(4));
            weaponModifiers.Add("LS - Spirit Gauge (Red)",              LS(5));
            weaponModifiers.Add("SnS - Sword Sharpness",                SnS(1));
            weaponModifiers.Add("SnS - Affinity Oil",                   SnS(2));
            weaponModifiers.Add("SnS - Stamina Oil",                    SnS(3));
            weaponModifiers.Add("SnS - Mind's Eye Oil",                 SnS(4));
            weaponModifiers.Add("DB - Element Modifier (0.7x)",         DB());
            weaponModifiers.Add("HH - Attack Up (S) Song",              HH(1));
            weaponModifiers.Add("HH - Attack Up (S) Encore",            HH(2));
            weaponModifiers.Add("HH - Attack Up (L) Song",              HH(3));
            weaponModifiers.Add("HH - Attack Up (L) Encore",            HH(4));
            weaponModifiers.Add("HH - Elem. Attack Boost Song",         HH(5));
            weaponModifiers.Add("HH - Elem. Attack Boost Encore",       HH(6));
            weaponModifiers.Add("HH - Abnormal Boost Song",             HH(7));
            weaponModifiers.Add("HH - Abnormal Boost Encore",           HH(8));
            weaponModifiers.Add("HH - Affinity Up Song",                HH(9));
            weaponModifiers.Add("HH - Affinity Up Encore",              HH(10));
            weaponModifiers.Add("Lance - Enraged Guard (Yellow)",       Lance(1));
            weaponModifiers.Add("Lance - Enraged Guard (Orange)",       Lance(2));
            weaponModifiers.Add("Lance - Enraged Guard (Red)",          Lance(3));
            weaponModifiers.Add("Lance - Impact/Cut Hitzone",           Lance(4));
            weaponModifiers.Add("GL - Dragon Breath",                   GL(1));
            weaponModifiers.Add("GL - Impact/Cut Hitzone",              GL(2));
            weaponModifiers.Add("SA - Sword Mode",                      SA(1));
            weaponModifiers.Add("SA - Energy Charge",                   SA(2));
            weaponModifiers.Add("SA - Demon Riot 'Pwr'",                SA(3));
            weaponModifiers.Add("SA - Demon Riot 'Ele'",                SA(4));
            weaponModifiers.Add("SA - Demon Riot 'Drg'",                SA(5));
            weaponModifiers.Add("SA - Demon Riot 'Sta'",                SA(6));
            weaponModifiers.Add("CB - Red Shield",                      CB(1));
            weaponModifiers.Add("IG - Red (Balanced)",                  IG(1));
            weaponModifiers.Add("IG - Red/White",                       IG(2));
            weaponModifiers.Add("IG - Triple Up",                       IG(3));
            weaponModifiers.Add("Gunner - Normal Distance (1x)",        Gunner(1));
            weaponModifiers.Add("Gunner - Critical Distance (1.5x)",    Gunner(2));
            weaponModifiers.Add("Gunner - Long Range (0.8x)",           Gunner(3));
            weaponModifiers.Add("Gunner - Ex. Long Range (0.5x)",       Gunner(4));
            weaponModifiers.Add("LBG - Raw Multiplier (1.3x)",          LBG(1));
            weaponModifiers.Add("HBG - Raw Multiplier (1.5x)",          HBG(1));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (Non-Status)",     Bow(1));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (+Poison)",        Bow(2));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (+Para/Sleep)",    Bow(3));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (Non-Status)",     Bow(4));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (+Poison)",        Bow(5));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (+Para/Sleep)",    Bow(6));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (Non-Status)",     Bow(7));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (+Poison)",        Bow(8));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (+Para/Sleep)",    Bow(9));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (Non-Status)",     Bow(10));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (+Poison)",        Bow(11));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (+Para/Sleep)",    Bow(12));
            weaponModifiers.Add("Bow - Power C. Lvl. 1",                Bow(13));
            weaponModifiers.Add("Bow - Power C. Lvl. 2",                Bow(14));
            weaponModifiers.Add("Bow - Ele. C. Lvl. 1",                 Bow(15));
            weaponModifiers.Add("Bow - Ele. C. Lvl. 2",                 Bow(16));
            weaponModifiers.Add("Bow - Para. C.",                       Bow(17));
            weaponModifiers.Add("Bow - Poison C.",                      Bow(18));
            weaponModifiers.Add("Bow - Sleep C.",                       Bow(19));
            weaponModifiers.Add("Bow - Blast C.",                       Bow(20));
            weaponModifiers.Add("Bow - Exh. C.",                        Bow(21));
            weaponModifiers.Add("Bow - Coating Boost",                  Bow(22));
#endif
            //Other modifiers
#if false
            otherModifiers.Add("Frenzy", functionName());
#endif
        }

        /// <summary>
        /// Reads the files from the appropriate folders.
        /// </summary>
        private void readFiles()
        {
            //Read weapon database
            readWeapons();

            //Read motion value database
            readMotion();

            //Read 
        }

        /// <summary>
        /// Reads from the MotionValues folder and retrives motion values and other stats for each move of each type of weapon.
        /// </summary>
        private void readMotion()
        {
            string[] files = System.IO.Directory.GetFiles("./MotionValues/", "*.xml", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string type = file.Remove(file.Length - 4);
                type = type.Remove(0, 18);
                if (type.Contains('_'))
                {
                    type.Replace('_', ' ');
                }

                List<moveStat> moves = new List<moveStat>();
                type2Moves.Add(type, moves);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                using (XmlReader reader = XmlReader.Create(file, settings))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.Name == "move" && reader.NodeType != XmlNodeType.EndElement)
                        {
                            int id = int.Parse(reader.GetAttribute("id"));
                            reader.Read(); //name tag
                            reader.Read(); //name string
                            string name = reader.Value;

                            reader.Read(); //end name
                            reader.Read(); //onlyfor tag
                            reader.Read(); //onlyfor string
                            string only = reader.Value;

                            reader.Read(); //end onlyfor
                            reader.Read(); //type tag
                            reader.Read(); //type string
                            string damageType = reader.Value;

                            reader.Read(); //end type
                            reader.Read(); //mv tag
                            reader.Read(); //mv int
                            double motionValue = double.Parse(reader.Value);

                            reader.Read(); //end mv
                            reader.Read(); //perHit tag
                            reader.Read(); //perHit value
                            double perHit = double.Parse(reader.Value);

                            reader.Read(); //end perHit
                            reader.Read(); //hitCount tag
                            reader.Read(); //hitCount value
                            int hitCount = int.Parse(reader.Value);

                            reader.Read(); //end hitcount
                            reader.Read(); //sharpness tag
                            reader.Read(); //sharpness double
                            double sharpnessMod = double.Parse(reader.Value);

                            reader.Read(); //end sharpness
                            reader.Read(); //KO tag
                            reader.Read(); //KO int
                            int KODamage = int.Parse(reader.Value);

                            reader.Read(); //end KO
                            reader.Read(); //exhaust tag
                            reader.Read(); //exhaust string
                            int exhaustDamage = int.Parse(reader.Value);

                            reader.Read(); //end exhaust
                            reader.Read(); //minds tag
                            reader.Read(); //minds value
                            string minds = reader.Value;
                            bool mindsEye = false;
                            if (minds == "Yes")
                            {
                                mindsEye = true;
                            }

                            reader.Read(); //end minds
                            reader.Read(); //draw tag
                            reader.Read(); //draw value
                            string draw = reader.Value;
                            bool drawAttack = false;
                            if (draw == "Yes")
                            {
                                drawAttack = true;
                            }

                            reader.Read();
                            reader.Read();
                            reader.Read();
                            string aerial = reader.Value;
                            bool aerialAttack = false;
                            if (aerial == "Yes")
                            {
                                aerialAttack = true;
                            }
                            moves.Add(new moveStat(name, id, only, damageType, motionValue, perHit, hitCount, sharpnessMod, KODamage, exhaustDamage, mindsEye, drawAttack, aerialAttack));

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reads the files included in the Weapons folder and generates weapon type names.
        /// Additionally, also reads inside the files and creates stats based on the weapons within.
        /// TODO: Add secondary things (Phials, 2nd Element of DBs)
        /// </summary>
        private void readWeapons()
        {
            string[] files = System.IO.Directory.GetFiles("./Weapons/", "*.xml", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string type = file.Remove(file.Length - 4); //Strip trailing '.xml'
                type = type.Remove(0, 13); //Strip preceeding './Weapons/' and order numbers
                if (type.Contains('_'))
                {
                    type.Replace('_', ' ');
                }

                weapType.Items.Add(type);

                List<string> weapons = new List<string>();
                type2Weapons.Add(type, weapons); //Mapping of weapon types to weapons


                //Now we read the files
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                using (XmlReader reader = XmlReader.Create(file, settings))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.Name == "weapon" && reader.NodeType != XmlNodeType.EndElement)
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

                            while (reader.NodeType != XmlNodeType.EndElement && reader.Name != "weapon")
                            {
                                int level = int.Parse(reader.GetAttribute("number")); //Get attribute of level number.
                                reader.Read(); //attack tag
                                reader.Read(); //attack int

                                double attack = double.Parse(reader.Value);
                                reader.Read(); //end attack
                                reader.Read(); //second type tag
                                reader.Read(); //second type string

                                string secType = reader.Value;
                                reader.Read(); //end second type
                                reader.Read(); //sharpness tag
                                reader.Read(); //sharpness string

                                string sharpness = reader.Value;
                                reader.Read(); //end sharpness string
                                reader.Read(); //affinity tag
                                reader.Read(); //affinity int

                                string affinity = reader.Value; //Extract affinity
                                double aff = double.Parse(affinity.Remove(affinity.Length - 1)); //Remove percentage sign
                                reader.Read(); //end affinity tag
                                reader.Read(); //eleType tag
                                reader.Read(); //eleType string

                                string eleType = reader.Value;
                                reader.Read(); //eleType end
                                reader.Read(); //eleDamage tag
                                reader.Read(); //eleDamage int

                                double eleDamage = double.Parse(reader.Value);
                                reader.Read(); //end eleDamage
                                reader.Read(); //secDamage tag
                                reader.Read(); //secDamage double

                                double secDamage = double.Parse(reader.Value);
                                reader.Read(); //end secDamage
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

                                statistics.Add(new stats(level, attack, secType, sharpness, aff, eleType, eleDamage, secDamage, sharpness1, sharpness2));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of QuickSort. Used for sorting by MotionValue.
        /// </summary>
        /// <param name="statList"></param>
        /// <param name="loIndex"></param>
        /// <param name="hiIndex"></param>
        private void quickSortVar1(List<moveStat> statList, int loIndex, int hiIndex)
        {
            if (loIndex < hiIndex)
            {
                int p = partitionVar1(statList, loIndex, hiIndex);
                quickSortVar1(statList, loIndex, p - 1);
                quickSortVar1(statList, loIndex, p - 1);
            }
        }

        private int partitionVar1(List<moveStat> statList, int loIndex, int hiIndex)
        {
            double pivot = statList[hiIndex].totalValue;
            int i = loIndex - 1;

            for (int j = loIndex; j != hiIndex; j++)
            {
                if (statList[j].totalValue <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        moveStat tempStat = statList[i];
                        statList[i] = statList[j];
                        statList[j] = tempStat;
                    }
                }
            }

            moveStat temp = statList[i + 1];
            statList[i + 1] = statList[hiIndex];
            statList[hiIndex] = temp;

            return i + 1;
        }

        /// <summary>
        /// Implementation of QuickSort, used for sorting by ID.
        /// </summary>
        /// <param name="statList"></param>
        /// <param name="loIndex"></param>
        /// <param name="hiIndex"></param>
        private void quickSortVar2(List<moveStat> statList, int loIndex, int hiIndex)
        {
            if (loIndex < hiIndex)
            {
                int p = partitionVar2(statList, loIndex, hiIndex);
                quickSortVar2(statList, loIndex, p - 1);
                quickSortVar2(statList, loIndex, p - 1);
            }
        }

        private int partitionVar2(List<moveStat> statList, int loIndex, int hiIndex)
        {
            int pivot = statList[hiIndex].id;
            int i = loIndex - 1;

            for (int j = loIndex; j != hiIndex; j++)
            {
                if (statList[j].id <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        moveStat tempStat = statList[i];
                        statList[i] = statList[j];
                        statList[j] = tempStat;
                    }
                }
            }

            moveStat temp = statList[i + 1];
            statList[i + 1] = statList[hiIndex];
            statList[hiIndex] = temp;

            return i + 1;
        }



#if true
        //Beginning of Armor Skill methods.
        private bool Artillery(int skillVal)
        {
            if (skillVal == 1) //Art. Novice (Fixed Weapons)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.1;
            }
            else if (skillVal == 2) //Art. Novice (Explosive Ammo)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.15;
            }
            else if (skillVal == 3) //Art. Novice (Impact CB)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.30;
                weaponAndMods.CB = true;
            }
            else if (skillVal == 4) //Art. Novice (GL)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 5) //Art. Expert (Fixed Weapons)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.2;
            }
            else if (skillVal == 6) //Art. Expert (Explosive Ammo)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.3;
            }
            else if (skillVal == 7) //Art. Expert (Impact CB)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.35;
                weaponAndMods.CB = true;
            }
            else if (skillVal == 8) //Art. Expert (GL)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.2;
            }
            else
            {
                return false; //Failsafe.
            }
            return true;
        }

        private bool Attack(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 20;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.totalAttackPower -= 5;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.totalAttackPower -= 10;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.totalAttackPower -= 15;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Blunt(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 15;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 25;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 30;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool BombBoost(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.2;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.expMod = weaponAndMods.expMod * 1.3;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool ChainCrit(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.affinity += 25;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 30;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Chance(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2 * 1.15;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2 * 1.2;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2 * 1.15;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.eleAttackPower = weaponAndMods.eleAttackPower * 1.2 * 1.15;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.eleAttackPower = weaponAndMods.eleAttackPower * 1.2 * 1.15;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.eleAttackPower = weaponAndMods.eleAttackPower * 1.2 * 1.15;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 7)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool ColdBlooded(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 20;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Crisis()
        {
            weaponAndMods.totalAttackPower += 20;
            return true;
        }

        private bool CritDraw()
        {
            weaponAndMods.affinity = 100;
            return true;
        }

        private bool CritElement(int skillVal)
        {
            paraEleCrit.SelectedIndex = skillVal;
            return true;
        }

        private bool CriticalUp()
        {
            paraBoost.Checked = true;
            return true;
        }

        private bool DFencing(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Deadeye()
        {
            weaponAndMods.totalAttackPower += 20;
            weaponAndMods.affinity += 15;
            return true;
        }

        private bool DragonAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 4;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.04;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 6;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Dreadking()
        {
            weaponAndMods.totalAttackPower += 20;
            return true;
        }

        private bool Dreadqueen()
        {
            weaponAndMods.eleAttackPower += 1;
            weaponAndMods.staMod = weaponAndMods.staMod * 1.2;
            return true;
        }

        private bool Drilltusk()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.3;
            return true;
        }

        private bool Elemental()
        {
            weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            return true;
        }

        private bool Expert(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.affinity += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 20;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.affinity += 30;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.affinity -= 5;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.affinity -= 10;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.affinity -= 15;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Fencing()
        {
            moveMinds.Checked = true;
            return true;
        }

        private bool FireAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 4;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.04;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 6;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool FrenzyRes()
        {
            weaponAndMods.affinity += 15;
            return true;
        }

        private bool Furor()
        {
            weaponAndMods.totalAttackPower += 20;
            return true;
        }

        private bool GlovesOff(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.affinity += 30;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 50;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Handicraft(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.sharpness = (string)weapOne.SelectedItem;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.sharpness = (string)weapTwo.SelectedItem;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Haphazard()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2;
            return true;
        }

        private bool HeavyUp()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            return true;
        }

        private bool Hellblade()
        {
            weaponAndMods.sharpness = (string)weapTwo.SelectedItem;
            return true;
        }

        private bool HotBlooded(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 20;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool IceAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 4;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.04;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 6;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool KO()
        {
            if (!modList.Items.ContainsKey("F.Slugger"))
            {
                weaponAndMods.KOPower = weaponAndMods.KOPower * 1.2;
            }
            return true;
        }

        private bool NormalUp()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            return true;
        }

        private bool PelletUp(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.3;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool PierceUp()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            return true;
        }

        private bool Potential(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.3;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 0.7;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool PunishDraw(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 5;
                weaponAndMods.KOPower += 30;
                weaponAndMods.exhaustPower += 20;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 5;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Redhelm()
        {
            weaponAndMods.totalAttackPower += 20;
            return true;
        }

        private bool Silverwind()
        {
            weaponAndMods.affinity += 30;
            return true;
        }

        private bool Spirit(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 10;
                weaponAndMods.affinity += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 20;
                weaponAndMods.affinity += 15;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool StamDrain()
        {
            weaponAndMods.exhaustPower = weaponAndMods.exhaustPower * 1.2;
            return true;
        }

        private bool Status(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 1;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 1;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.2;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.9;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Survivor(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Tenderizer()
        {
            weaponAndMods.affinity += 50;
            return true;
        }

        private bool ThunderAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 4;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 6;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.2;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Thunderlord()
        {
            weaponAndMods.affinity += 50;
            return true;
        }

        private bool Unscathed()
        {
            weaponAndMods.totalAttackPower += 20;
            return true;
        }

        private bool Vault()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            return true;
        }

        private bool WaterAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleAttackPower += 4;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleAttackPower += 6;
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.2;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
            }
            else
            {
                return false;
            }
            return true;
        }
#endif
        //Item/Kitchen Modifier Methods
#if true
        private bool FBombardier(int skillVal)
        {
            if (skillVal == 1) //Art. Novice (Fixed Weapons)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            }
            else if (skillVal == 2) //Art. Novice (Explosive Ammo)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.15;
            }
            else if (skillVal == 3) //Art. Novice (Impact CB)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.15;
                weaponAndMods.CB = true;
            }
            else if (skillVal == 4) //Art. Novice (GL)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool FBooster()
        {
            weaponAndMods.totalAttackPower += 3;
            return true;
        }

        private bool FBulldozer()
        {
            if (weapSharpness.Text != "(No Sharpness)")
            {
                weaponAndMods.rawSharpMod *= 1.05;
                weaponAndMods.eleSharpMod *= 1.05;
            }
            return true;
        }

        private bool FHeroics()
        {
            weaponAndMods.rawMod *= 1.35;
            return true;
        }

        private bool FPyro()
        {
            weaponAndMods.staMod *= 1.1;
            return true;
        }

        //private bool FRider()
        //{

        //}

        private bool FSharpshooter()
        {
            weaponAndMods.rawMod *= 1.1;
            return true;
        }

        private bool FSlugger()
        {
            if (!modList.Items.ContainsKey("KO King"))
            {
                weaponAndMods.KOPower *= 1.1;
            }
            return true;
        }

        private bool FSpecialist()
        {
            weaponAndMods.staMod *= 1.125;
            return true;
        }

        private bool FTemper()
        {
            weaponAndMods.rawMod *= 1.05;
            return true;
        }

        private bool CoolCat()
        {
            weaponAndMods.totalAttackPower += 15;
            return true;
        }

        private bool Powercharm()
        {
            weaponAndMods.totalAttackPower += 6;
            return true;
        }

        private bool PowerTalon()
        {
            weaponAndMods.totalAttackPower += 9;
            return true;
        }

        private bool DemonDrug(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 7;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool AUMeal(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 3;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 5;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 7;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool MightSeed(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 25;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Nitroshroom()
        {
            weaponAndMods.totalAttackPower += 10;
            return true;
        }

        private bool Demon(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.totalAttackPower += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower += 10;
                if (weapSharpness.Text != "(No Sharpness)")
                {
                    weaponAndMods.rawSharpMod *= 1.1;
                }
            }
            else if (skillVal == 3)
            {
                weaponAndMods.totalAttackPower += 7;
                if (weapSharpness.Text != "(No Sharpness)")
                {
                    weaponAndMods.rawSharpMod *= 1.1;
                }
                weaponAndMods.affinity += 10;
            }
            else
            {
                return false;
            }
            return true;
        }
#endif
        //Weapon Mods
#if true
        private bool LSM(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 0.6;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 0.7;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool GS(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.05;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.1;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 1.33;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool LS(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.05;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.13;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.05;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 1.1;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool SnS(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawSharpMod *= 1.06;
                weaponAndMods.eleSharpMod *= 1.06;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 1.3;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.KOPower += 8;
                weaponAndMods.exhaustPower += 10;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.mindsEye = true;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool DB()
        {
            weaponAndMods.eleMod *= 0.7;
            return true;
        }

        private bool HH(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawSharpMod *= 1.06;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 1.3;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.KOPower += 8;
                weaponAndMods.exhaustPower += 10;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.mindsEye = true;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.affinity += 1.3;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.KOPower += 8;
                weaponAndMods.exhaustPower += 10;
            }
            else if (skillVal == 7)
            {
                weaponAndMods.mindsEye = true;
            }
            else if (skillVal == 8)
            {
                weaponAndMods.mindsEye = true;
            }
            else
            {
                return false;
            }
            return true;
        }

        

        

        //private bool functionName()
        //{

        //}
#endif
    }
}
