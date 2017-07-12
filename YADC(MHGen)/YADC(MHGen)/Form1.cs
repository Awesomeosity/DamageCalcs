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

            public string damageType;
            public double totalValue;
            public double perHitValue;
            public int hitCount;
            public double sharpnessMod;
            public double eleMod;
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
            public moveStat(string _name, int _id, string _damageType, double _motionValue, double _perHitValue, int _hitCount, double _sharpnessMod, double _eleMod, double _KODamage, double _ExhDamage, bool _mindsEye, bool _draw, bool _aerial)
            {
                name = _name;
                id = _id;
                damageType = _damageType;
                totalValue = _motionValue;
                perHitValue = _perHitValue;
                hitCount = _hitCount;
                sharpnessMod = _sharpnessMod;
                eleMod = _eleMod;
                KODamage = _KODamage;
                ExhDamage = _ExhDamage;
                mindsEye = _mindsEye;
                draw = _draw;
                aerial = _aerial;
            }
        }

        public struct hitzoneStats
        {
            public string name;
            public double cutZone;
            public double impactZone;
            public double shotZone;
            public double fireZone;
            public double waterZone;
            public double thunderZone;
            public double iceZone;
            public double dragonZone;
            public double KOZone;
            public double exhaustZone;

            /// <summary>
            /// Constructor for a single hitzone's stats
            /// </summary>
            /// <param name="_name">Name of the hitzone.</param>
            /// <param name="_cutZone">Cutting zone value.</param>
            /// <param name="_impactZone">Impact zone value.</param>
            /// <param name="_shotZone">Shot zone value.</param>
            /// <param name="_fireZone">Fire Zone value.</param>
            /// <param name="_waterZone">Water zone value.</param>
            /// <param name="_thunderZone">Thunder zone value.</param>
            /// <param name="_iceZone">Ice zone value.</param>
            /// <param name="_dragonZone">Dragon zone value.</param>
            /// <param name="_KOZone">KO zone value.</param>
            /// <param name="_exhaustZone">Exhaust zone value.</param>
            public hitzoneStats(string _name, double _cutZone, double _impactZone, double _shotZone, double _fireZone, double _waterZone, double _thunderZone, double _iceZone, double _dragonZone, double _KOZone, double _exhaustZone)
            {
                name = _name;
                cutZone = _cutZone;
                impactZone = _impactZone;
                shotZone = _shotZone;
                fireZone = _fireZone;
                waterZone = _waterZone;
                thunderZone = _thunderZone;
                iceZone = _iceZone;
                dragonZone = _dragonZone;
                KOZone = _KOZone;
                exhaustZone = _exhaustZone;
            }
        }

        public struct questStat
        {
            public string name;
            public double questMod;
            public double exhaustMod;

            /// <summary>
            /// Quest statistics
            /// </summary>
            /// <param name="_name">Name of the quest</param>
            /// <param name="_questMod">Quest modifier (Defense modifier)</param>
            /// <param name="_exhaustMod">Exhaust modifier</param>
            public questStat(string _name, double _questMod, double _exhaustMod)
            {
                name = _name;
                questMod = _questMod;
                exhaustMod = _exhaustMod;
            }
        }

        public struct monsterStatusThresholds
        {
            public double poisonInit;
            public double poisonInc;
            public double poisonMax;

            public double sleepInit;
            public double sleepInc;
            public double sleepMax;

            public double paraInit;
            public double paraInc;
            public double paraMax;

            public double KOInit;
            public double KOInc;
            public double KOMax;

            public double exhaustInit;
            public double exhaustInc;
            public double exhaustMax;

            public double blastInit;
            public double blastInc;
            public double blastMax;
        }
        /// <summary>
        /// TODO
        /// Stores the stats of a single hitzone.
        /// </summary>
        public class monsterStat
        {
            public string name;
            public List<hitzoneStats> hitzones;
            public List<questStat> quests;
            public monsterStatusThresholds status;

            public monsterStat(string _name)
            {
                name = _name;
                hitzones = new List<hitzoneStats>();
                quests = new List<questStat>();
                status = new monsterStatusThresholds();
            }
        }

        /// <summary>
        /// Stores the relevant variables from the database portion of the application to
        /// import to the calculator portion.
        /// No ctor. Will be filled in when the UpdateButt is clicked.
        /// </summary>
        public struct importedStats
        {
            public string sharpness; //Current Sharpness
            public string altDamageType;
            public double totalAttackPower;
            public double eleAttackPower;
            public double affinity;
            public double rawSharpMod;
            public double eleSharpMod;
            public double avgMV;
            public int hitCount;
            public double totalMV;
            public double KOPower;
            public double exhaustPower;
            public bool criticalBoost;
            public bool mindsEye;
            public string damageType;
            public string eleCrit;
            public bool statusCrit;

            public string secElement;
            public double secPower;

            public double hitzone;
            public double eleHitzone;
            public double secHitzone;
            public double questMod;
            public double KOHitzone;
            public double exhaustHitzone;
            public double exhaustMod;

            public double rawMod; //Stores the multiplier of the raw damage.
            public double eleMod; //Stores the elemental multiplier. Has a cap of 1.2x, surpassed when used Demon Riot on an Element Phial SA.
            public double expMod; //Stores the explosive multiplier. Has a cap of 1.3x, 1.4x when considering Impact Phial CB.
            public double staMod; //Stores the status multiplier. Has a cap of 1.25x, surpassed when using Demon Riot on a Status Phial SA.
            public bool CB; //Shows whether or not the explosive multiplier should be increased because Impact Phials are being used. 
            public bool DemonRiot; //Shows whether or not Demon Riot is being used.

            public int addRaw; //Stores the additive portion of raw
            public int addElement; //Stores the additive portion of element after Atk +1 or +2
        }

        Dictionary<string, Tuple<double, double>> sharpnessValues = new Dictionary<string, Tuple<double, double>>(); //Stores translation of sharpness to sharpness modifiers
        Dictionary<string, string> str2image = new Dictionary<string, string>(); //Stores the paths to the image files.
        Dictionary<string, double> monsterStatus = new Dictionary<string, double>(); //Stores conversion of string to multipliers, used for the monster's status.
        Dictionary<string, List<string>> type2Weapons = new Dictionary<string, List<string>>(); //Stores weapons under weapon types.
        Dictionary<string, List<moveStat>> type2Moves = new Dictionary<string, List<moveStat>>(); //Stores conversion of weapon types to moves.
        Dictionary<string, string> names2FinalNames = new Dictionary<string, string>(); //Stores mapping of names to final names.
        Dictionary<string, string> finalNames2Names = new Dictionary<string, string>(); //Stores mapping of final names to names.
        Dictionary<string, List<stats>> names2Stats = new Dictionary<string, List<stats>>(); //This will store a mapping of names to a list of stats by levels.
        Dictionary<string, Func<int, bool>> armorModifiers = new Dictionary<string, Func<int, bool>>(); //Stores conversion of strings to modifiers.
        Dictionary<string, Func<int, bool>> kitchenItemModifiers = new Dictionary<string, Func<int, bool>>(); //Stores conversion of strings to kitchen modifiers.
        Dictionary<string, Func<int, bool>> weaponModifiers = new Dictionary<string, Func<int, bool>>(); //Stores conversion of strings to weapon-specific modifiers.
        Dictionary<string, Func<int, bool>> otherModifiers = new Dictionary<string, Func<int, bool>>(); //Will store other things.
        Dictionary<string, monsterStat> monsterStats = new Dictionary<string, monsterStat>();

        importedStats weaponAndMods = new importedStats(); //Will be used later. Required to be global for the modifier methods.

        string secondType; //Stores the second type of the weapon.

        /// <summary>
        /// Called when initializing the form.
        /// </summary>
        public DmgCalculator()
        {
            InitializeComponent(); //Required.
            Application.EnableVisualStyles();
            FillOut(); //Fills out dictionaries.
            readFiles(); //Read the xml files and fills out the database.
            prep(); //Prepares the forms.
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

        private void AffinityField_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!calcFieldValidation(((TextBox)sender).Text, out errorMsg))
            {
                e.Cancel = true;
                ((TextBox)sender).Select(0, ((TextBox)sender).Text.Length);

                this.ErrorPreventer.SetError((TextBox)sender, errorMsg);
            }
            else if (double.Parse(((TextBox)sender).Text) > 100)
            {
                ((TextBox)sender).Text = 100.ToString();
            }
            else if (double.Parse(((TextBox)sender).Text) < -100)
            {
                ((TextBox)sender).Text = "-100";
            }
        }

        /*CalcButt Functions*/
        /// <summary>
        /// Executed when the calculate output button is clicked.
        /// </summary>
        /// <param name="sender">Should only be the 'Calculate Output' button.</param>
        /// <param name="e"></param>
        private void CalcButt_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double, double, double> rawEleOut = calculateDamage(); //Helper function.

            calcRawWeap.Text = rawEleOut.Item1.ToString();
            calcRawOut.Text = rawEleOut.Item2.ToString(); //Used the Tuple output from the function to fill in the labels.
            calcEleOut.Text = rawEleOut.Item3.ToString();
            calcSecOut.Text = rawEleOut.Item4.ToString();
        }

        /*CalcAll Functions*/
        /// <summary>
        /// Executed when the 'Calculate All' button is clicked.
        /// </summary>
        /// <param name="sender">Should only be the 'Calculate All' button.</param>
        /// <param name="e"></param>
        private void CalcAll_Click(object sender, System.EventArgs e)
        {
            Tuple<double, double, double, double> rawEleTuple = calculateDamage(); //Use helper function.
            Tuple<double, double, double, double, double, string, double> finalTuple = calculateMoreDamage(rawEleTuple.Item2, rawEleTuple.Item3, rawEleTuple.Item4); //Another one.

            calcRawWeap.Text = rawEleTuple.Item1.ToString();
            calcRawOut.Text = rawEleTuple.Item2.ToString(); //Do as the CalcButt function does
            calcEleOut.Text = rawEleTuple.Item3.ToString();
            calcSecOut.Text = rawEleTuple.Item4.ToString();

            calcFinalRaw.Text = finalTuple.Item2.ToString(); //But with use of the outputted tuple from the moreDamage function.
            calcEle.Text = finalTuple.Item3.ToString();

            calcKO.Text = finalTuple.Item4.ToString();
            calcExh.Text = finalTuple.Item5.ToString();

            calcFinalSec.Text = finalTuple.Item7.ToString();

            calcFinal.Text = finalTuple.Item1.ToString();

            calcBounce.Text = finalTuple.Item6;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (staSecEle.SelectedIndex == 0)
            {
                string status = "";

                Tuple<int, int, int> statusTuple = calculateSta(false);
                if (statusTuple.Item1 != 0 && statusTuple.Item2 != 0 && statusTuple.Item3 != 0)
                {
                    string[] formatArray = new string[] { statusTuple.Item1.ToString(), staType.Text, statusTuple.Item2.ToString(), statusTuple.Item3.ToString() };
                    status += String.Format("It will take {0} hits to inflict {1} status on this monster at the initial threshold, {2} more hits per tolerance level, and {3} hits at maximum tolerance. \n", formatArray);
                }

                if (staSecEle.SelectedIndex != 0)
                {
                    Tuple<int, int, int> DBTuple = calculateSta(true);
                    string[] formatDB = new string[] { DBTuple.Item1.ToString(), staSecEle.Text, DBTuple.Item2.ToString(), DBTuple.Item3.ToString() };
                    status += String.Format("It will take {0} hits to inflict {1} status on this monster at the initial threshold, {2} more hits per tolerance level, and {3} hits at maximum tolerance. \n", formatDB);
                }

                Tuple<int, int, int, int, int, int> KOTuple = calculateKO();
                if (KOTuple.Item1 != 0 && KOTuple.Item2 != 0 && KOTuple.Item3 != 0)
                {
                    string[] formatKO = new string[] { KOTuple.Item1.ToString(), KOTuple.Item2.ToString(), KOTuple.Item3.ToString() };
                    status += String.Format("It will take {0} hits to inflict KO status on this monster at the initial threshold, {1} more hits per tolerance level, and {2} hits at maximum tolerance. \n", formatKO);
                }

                if (KOTuple.Item4 != 0 && KOTuple.Item5 != 0 && KOTuple.Item6 != 0)
                {
                    string[] formatExh = new string[] { KOTuple.Item4.ToString(), KOTuple.Item5.ToString(), KOTuple.Item6.ToString() };
                    status += String.Format("It will take {0} hits to inflict Exhaust damage on this monster at the initial threshold, {1} more hits per tolerance level, and {2} hits at maximum tolerance. \n", formatExh);
                }

                if (status == "")
                {
                    status = "It is impossible to deal any sort of status damage with the current parameters.";
                }

                staText.Text = status;
            }
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

            fillMoves((string)((ComboBox)sender).SelectedItem);

            if (((ComboBox)sender).Text == "Light Bowgun" || ((ComboBox)sender).Text == "Heavy Bowgun")
            {
                eleShotType.Enabled = true;
            }
            else
            {
                eleShotType.SelectedIndex = 0;
                eleShotType.Enabled = false;
            }
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
            if (weapOverride.Checked)
            {
                fillMoves((string)weapType.SelectedItem);
            }
            else
            {
                fillMoves((string)((ComboBox)sender).SelectedItem);
            }
        }

        private void weapSecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            if (index == 0)
            {
                secondType = "None";
                weapSecBox.Image = null;
                weapOverride.Checked = false;
                weapOverride.Enabled = false;
            }
            //else if ()
            //{
            //    weapSecBox.Image = null;
            //}

            else if (index == 1)
            {
                secondType = "Fire";
                weapSecBox.Load(str2image["Fire"]);
            }

            else if (index == 2)
            {
                secondType = "Water";
                weapSecBox.Load(str2image["Water"]);
            }

            else if (index == 3)
            {
                secondType = "Ice";
                weapSecBox.Load(str2image["Ice"]);
            }

            else if (index == 4)
            {
                secondType = "Thunder";
                weapSecBox.Load(str2image["Thunder"]);
            }

            else if (index == 5)
            {
                secondType = "Dragon";
                weapSecBox.Load(str2image["Dragon"]);
            }

            else if (index == 6 || index == 11)
            {
                secondType = "Poison";
                weapSecBox.Load(str2image["Poison"]);
            }

            else if (index == 7 || index == 12)
            {
                secondType = "Para";
                weapSecBox.Load(str2image["Para"]);
            }

            else if (index == 8)
            {
                secondType = "Sleep";
                weapSecBox.Load(str2image["Sleep"]);
            }

            else if (index == 9)
            {
                secondType = "Blast";
                weapSecBox.Load(str2image["Blast"]);
            }

            else if (index == 13)
            {
                weapOverride.Checked = false;
                weapOverride.Enabled = false;
                weapSecBox.Load("./Images/KO.png");
            }

            if (index != 0)
            {
                weapOverride.Enabled = true;
            }
        }

        private void ArmorButt_Click(object sender, EventArgs e)
        {
            bool alreadyThere = false;
            foreach (ListViewItem item in modList.Items)
            {
                if (modArmor.Text == item.Text)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                ListViewItem item = new ListViewItem(modArmor.Text);
                item.Group = modList.Groups[0];
                modList.Items.Add(item);
            }
        }

        private void KitchenButt_Click(object sender, EventArgs e)
        {
            bool alreadyThere = false;
            foreach (ListViewItem item in modList.Items)
            {
                if (modKitchen.Text == item.Text)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                ListViewItem item = new ListViewItem(modKitchen.Text);
                item.Group = modList.Groups[1];
                modList.Items.Add(item);
            }
        }

        private void WeaponButt_Click(object sender, EventArgs e)
        {
            bool alreadyThere = false;
            foreach (ListViewItem item in modList.Items)
            {
                if (modWeapon.Text == item.Text)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                ListViewItem item = new ListViewItem(modWeapon.Text);
                item.Group = modList.Groups[2];
                modList.Items.Add(item);
            }
        }

        private void OtherButt_Click(object sender, EventArgs e)
        {
            bool alreadyThere = false;
            foreach (ListViewItem item in modList.Items)
            {
                if (modOther.Text == item.Text)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
            {
                ListViewItem item = new ListViewItem(modOther.Text);
                item.Group = modList.Groups[3];
                modList.Items.Add(item);
            }
        }

        private void RemoveButt_Click(object sender, EventArgs e)
        {
            if (modList.SelectedItems.Count != 0)
            {
                foreach (ListViewItem item in modList.SelectedItems)
                {
                    modList.Items.Remove(item);
                }
            }
        }

        private void RemoveAllButt_Click(object sender, EventArgs e)
        {
            if (modList.Items.Count != 0)
            {
                foreach (ListViewItem item in modList.Items)
                {
                    modList.Items.Remove(item);
                }
            }
        }

        /// <summary>
        /// Using the importedStats struct, fill in the calculation fields with all information from the database section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paraUpdate_Click(object sender, EventArgs e)
        {
            importSetUp();
            importModifiers();
            export();
        }

        private void staImport_Click(object sender, EventArgs e)
        {
            importSetUp();
            importModifiers();
            statusExport();
        }

        private void monName_SelectedIndexChanged(object sender, EventArgs e)
        {
            monHitzone.Items.Clear();

            foreach (hitzoneStats zones in monsterStats[(string)(((ComboBox)sender).SelectedItem)].hitzones)
            {
                monHitzone.Items.Add(zones.name);
            }

            monQuest.Items.Clear();

            foreach (questStat quests in monsterStats[(string)(((ComboBox)sender).SelectedItem)].quests)
            {
                monQuest.Items.Add(quests.name);
            }
        }

        private void monHitzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoneName = (string)((ComboBox)sender).SelectedItem;
            foreach (hitzoneStats hitzone in monsterStats[(string)monName.SelectedItem].hitzones)
            {
                if (hitzone.name == zoneName)
                {
                    monCut.Text = hitzone.cutZone.ToString();
                    monImpact.Text = hitzone.impactZone.ToString();
                    monShot.Text = hitzone.shotZone.ToString();
                    monKO.Text = hitzone.KOZone.ToString();
                    monExh.Text = hitzone.exhaustZone.ToString();
                    monFire.Text = hitzone.fireZone.ToString();
                    monWater.Text = hitzone.waterZone.ToString();
                    monThunder.Text = hitzone.thunderZone.ToString();
                    monIce.Text = hitzone.iceZone.ToString();
                    monDragon.Text = hitzone.dragonZone.ToString();
                }
            }
        }

        private void monQuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            string questName = (string)((ComboBox)sender).SelectedItem;
            foreach (questStat quest in monsterStats[(string)monName.SelectedItem].quests)
            {
                if (quest.name == questName)
                {
                    monQuestMod.Text = quest.questMod.ToString();
                    monExhField.Text = quest.exhaustMod.ToString();
                }
            }
        }

        private void staSecEle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (staSecEle.SelectedIndex != 0)
            {
                staSecPower.Enabled = true;
            }
            else
            {
                staSecPower.Text = 0.ToString();
                staSecPower.Enabled = false;
            }
        }

        private void staCritCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (staCritCheck.Checked == true)
            {
                staAffinity.Enabled = true;
            }
            else
            {
                staAffinity.Text = 0.ToString();
                staAffinity.Enabled = false;
            }
        }

        /*Functions*/
        /// <summary>
        /// Help function which sets up the form appropriately.
        /// </summary>
        private void prep()
        {
            monFireBox.Load(str2image["Fire"]);
            monWaterBox.Load(str2image["Water"]);
            monThunderBox.Load(str2image["Thunder"]);
            monIceBox.Load(str2image["Ice"]);
            monDragonBox.Load(str2image["Dragon"]);
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

            weapAlt.SelectedIndex = 0;
            weapSharpness.SelectedIndex = 0;
            weapSecType.SelectedIndex = 0;
            weapOne.SelectedIndex = 0;
            weapTwo.SelectedIndex = 0;

            staType.SelectedIndex = 0;
            staSecEle.SelectedIndex = 0;
            staPoiBox.Load("./Images/Poison.png");
            staSleepBox.Load("./Images/Sleep.png");
            staParaBox.Load("./Images/Para.png");
            staKOBox.Load("./Images/KO.png");
            staExhBox.Load("./Images/Exhaust.png");
            staBlastBox.Load("./Images/Blast.png");

            eleShotType.SelectedIndex = 0;
        }

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
        private Tuple<double, double, double, double> calculateDamage()
        {
            double total = double.Parse(paraRaw.Text);
            double hitCount = double.Parse(paraHitCount.Text);
            double motion = double.Parse(paraMV.Text) * 0.01 * hitCount;
            double affinity = double.Parse(paraAffinity.Text) * 0.01;
            double element = 0;
            double DBElement = 0;
            if (paraSecPower.Text != "0")
            {
                element = double.Parse(paraEle.Text) * double.Parse(paraHitCount.Text) / 2;
                DBElement = double.Parse(paraSecPower.Text) * double.Parse(paraHitCount.Text) / 2;
            }
            else
            {
                element = double.Parse(paraEle.Text) * double.Parse(paraHitCount.Text);
            }

            double rawSharp = double.Parse(paraRawSharp.Text);
            double eleSharp = double.Parse(paraEleSharp.Text);

            double rawWeap = 0;
            double rawTotal = 0;
            double eleTotal = 0;
            double DBTotal = 0;

            double subAffinity = affinity; //Temp affinity used in calculations
            double critBoost = .25;
            double eleCrit = 0;
            double statusCrit = 0;

            if (paraFixed.Checked) //If fixed damage is in play
            {
                return new Tuple<double, double, double, double>(motion / 0.01, motion / 0.01, element, DBElement);
            }

            else //If it is in play
            {
                if (NeutralSel.Checked)
                {
                    subAffinity = 0;
                }
                else if (PositiveSel.Checked)
                {
                    subAffinity = 1;
                }
                else if (NegativeSel.Checked)
                {
                    subAffinity = -1;
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

                rawWeap = total * (1 + subAffinity * critBoost) * rawSharp;
                rawTotal = rawWeap * motion;

                string ele = (string)paraAltType.SelectedItem;
                string secEle = (string)paraSecEle.SelectedItem;
                if (isElement(ele))
                {
                    eleTotal = element * eleSharp * (1 + subAffinity * eleCrit);
                }
                else if (isStatus(ele))
                {
                    eleTotal = element * eleSharp * (1 + subAffinity * statusCrit);
                }
                else
                {
                    eleTotal = element * eleSharp;
                }

                if (isElement(secEle))
                {
                    DBTotal = DBElement * eleSharp * (1 + subAffinity * eleCrit);
                }
                else if (isStatus(secEle))
                {
                    DBTotal = DBElement * eleSharp * (1 + subAffinity * statusCrit);
                }
                else
                {
                    DBTotal = DBElement * eleSharp; //For Blast
                }
            }

            return new Tuple<double, double, double, double>(rawWeap, rawTotal, eleTotal, DBTotal);
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
            double KODam = double.Parse(paraKO.Text) * double.Parse(paraHitCount.Text);
            double ExhDam = double.Parse(paraExh.Text) * double.Parse(paraHitCount.Text);
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

            totaldamage = rawDamage;

            string element = (string)paraAltType.SelectedItem;
            if (isElement(element))
            {
                elementalDamage = elementalDamage * eleZone * questMod;
                totaldamage += elementalDamage;
            }

            if (paraSecEle.Text != "(None)") //For DB's Second Element
            {
                string altElement = (string)paraSecEle.SelectedItem;
                if (isElement(altElement))
                {
                    DBElement = DBElement * eleZone * questMod;
                    totaldamage += DBElement;
                }
            }

            totaldamage = Math.Floor(totaldamage);

            return new Tuple<double, double, double, double, double, string, double>(totaldamage, rawDamage, elementalDamage, KODamage, ExhDamage, BounceBool, DBElement);
        }

        private Tuple<int, int, int, int, int, int> calculateKO()
        {
            double KO = double.Parse(staKOPow.Text);
            double Exhaust = double.Parse(staExhaust.Text);

            KO *= double.Parse(staKOZone.Text) / 100;
            Exhaust *= double.Parse(staExhaustZone.Text) / 100;

            double KOInit = double.Parse(staKOInit.Text);
            double KOInc = double.Parse(staKOInc.Text);
            double KOMax = double.Parse(staKOMax.Text);

            double ExhInit = double.Parse(staExhaustInit.Text);
            double ExhInc = double.Parse(staExhInc.Text);
            double ExhMax = double.Parse(staExhMax.Text);

            int KOHitsInit = 0;
            int KOHitsInc = 0;
            int KOHitsMax = 0;

            int ExhHitsInit = 0;
            int ExhHitsInc = 0;
            int ExhHitsMax = 0;

            if (KO != 0)
            {
                KOHitsInit = (int)Math.Ceiling(KOInit / KO);
                KOHitsInc = (int)Math.Ceiling(KOInc / KO);
                KOHitsMax = (int)Math.Ceiling(KOMax / KO);
            }

            if (Exhaust != 0)
            {
                ExhHitsInit = (int)Math.Ceiling(ExhInit / Exhaust);
                ExhHitsInc = (int)Math.Ceiling(ExhInc / Exhaust);
                ExhHitsMax = (int)Math.Ceiling(ExhMax / Exhaust);
            }

            return new Tuple<int, int, int, int, int, int>(KOHitsInit, KOHitsInc, KOHitsMax, ExhHitsInit, ExhHitsInc, ExhHitsMax);
        }

        private Tuple<int, int, int> calculateSta(bool usingDB)
        {
            double power = 0;
            double init = 0;
            double inc = 0;
            double max = 0;

            if (!usingDB)
            {
                power = double.Parse(staPower.Text);

                if (staType.SelectedIndex == 0)
                {
                    init = double.Parse(staPoiInit.Text);
                    inc = double.Parse(staPoiInc.Text);
                    max = double.Parse(staPoiMax.Text);
                }
                else if (staType.SelectedIndex == 1)
                {
                    init = double.Parse(staSleepInit.Text);
                    inc = double.Parse(staSleepInc.Text);
                    max = double.Parse(staSleepMax.Text);
                }
                else if (staType.SelectedIndex == 2)
                {
                    init = double.Parse(staParaInit.Text);
                    inc = double.Parse(staParaInc.Text);
                    max = double.Parse(staParaMax.Text);
                }
                else if (staType.SelectedIndex == 3)
                {
                    init = double.Parse(staBlastInit.Text);
                    inc = double.Parse(staBlastInc.Text);
                    max = double.Parse(staBlastMax.Text);
                }
            }
            else
            {
                power = double.Parse(staSecPower.Text);

                if (staSecEle.SelectedIndex == 0)
                {
                    init = double.Parse(staPoiInit.Text);
                    inc = double.Parse(staPoiInc.Text);
                    max = double.Parse(staPoiMax.Text);
                }
                else if (staSecEle.SelectedIndex == 1)
                {
                    init = double.Parse(staSleepInit.Text);
                    inc = double.Parse(staSleepInc.Text);
                    max = double.Parse(staSleepMax.Text);
                }
                else if (staSecEle.SelectedIndex == 2)
                {
                    init = double.Parse(staParaInit.Text);
                    inc = double.Parse(staParaInc.Text);
                    max = double.Parse(staParaMax.Text);
                }
                else if (staSecEle.SelectedIndex == 3)
                {
                    init = double.Parse(staBlastInit.Text);
                    inc = double.Parse(staBlastInc.Text);
                    max = double.Parse(staBlastMax.Text);
                }
            }

            power *= double.Parse(staEleSharp.Text);

            if (staCritCheck.Checked)
            {
                double affinity = double.Parse(staAffinity.Text);
                power *= (1 + affinity * 1.2);
            }

            if (power == 0)
            {
                return new Tuple<int, int, int>(0, 0, 0);
            }

            int statusInit = (int)Math.Ceiling(init / power);
            int statusInc = (int)Math.Ceiling(inc / power);
            int statusMax = (int)Math.Ceiling(max / power);

            return new Tuple<int, int, int>(statusInit, statusInc, statusMax);
        }

        private void fillMoves(string selectedItem)
        {
            NameSort.Items.Clear();
            MotionSort.Items.Clear();
            ComboSort.Items.Clear();

            List<moveStat> tempListMotion = new List<moveStat>();
            List<moveStat> tempListCombo = new List<moveStat>();

            foreach (moveStat moves in type2Moves[selectedItem]) //Fills out the moves for the move search box.
            {
                NameSort.Items.Add(moves.name);
                tempListMotion.Add(moves);
                tempListCombo.Add(moves);
            }

            insertSort1(tempListMotion); //Usage of quickSort.
            insertSort2(tempListCombo);

            foreach (moveStat moves in tempListMotion)
            {
                MotionSort.Items.Add(moves.name);
            }

            foreach (moveStat moves in tempListCombo)
            {
                ComboSort.Items.Add(moves.name);
            }
        }

        private bool isElement(string element)
        {
            if (element == "Fire" || element == "Water" || element == "Thunder" || element == "Ice" || element == "Dragon")
            {
                return true;
            }
            return false;
        }

        private bool isStatus(string element)
        {
            if (element == "Poison" || element == "Para" || element == "Sleep")
            {
                return true;
            }
            return false;
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
#if true
            armorModifiers.Add("Art. Novice (Fixed Weapons)", x => Artillery(1));
            armorModifiers.Add("Art. Novice (Explosive Ammo)", x => Artillery(2));
            armorModifiers.Add("Art. Novice (Impact CB)", x => Artillery(3));
            armorModifiers.Add("Art. Novice (GL)", x => Artillery(4));
            armorModifiers.Add("Art. Expert (Fixed Weapons)", x => Artillery(5));
            armorModifiers.Add("Art. Expert (Explosive Ammo)", x => Artillery(6));
            armorModifiers.Add("Art. Expert (Impact CB)", x => Artillery(7));
            armorModifiers.Add("Art. Expert (GL)", x => Artillery(8));
            armorModifiers.Add("Attack Up (S)", x => Attack(1));
            armorModifiers.Add("Attack Up (M)", x => Attack(2));
            armorModifiers.Add("Attack Up (L)", x => Attack(3));
            armorModifiers.Add("Attack Down (S)", x => Attack(4));
            armorModifiers.Add("Attack Down (M)", x => Attack(5));
            armorModifiers.Add("Attack Down (L)", x => Attack(6));

            armorModifiers.Add("Bludgeoner", x => Blunt());
            armorModifiers.Add("Bombardier (Blast)", x => BombBoost(1));
            armorModifiers.Add("Bombardier (Bomb)", x => BombBoost(2));

            armorModifiers.Add("Repeat Offender (1 Hit)", x => ChainCrit(1));
            armorModifiers.Add("Repeat Offender (>5 Hits)", x => ChainCrit(2));
            armorModifiers.Add("Trump Card (Lion's Maw)", x => Chance(1));
            armorModifiers.Add("Trump Card (Dragon's Breath)", x => Chance(2));
            armorModifiers.Add("Trump Card (Demon Riot 'Pwr')", x => Chance(3));
            armorModifiers.Add("Trump Card (Demon Riot 'Sta')", x => Chance(4));
            armorModifiers.Add("Trump Card (Demon Riot 'Ele')", x => Chance(5));
            armorModifiers.Add("Trump Card (Demon Riot 'Dra')", x => Chance(6));
            armorModifiers.Add("Trump Card (Other HAs)", x => Chance(7));
            armorModifiers.Add("Polar Hunter (Cool Drink)", x => ColdBlooded(1));
            armorModifiers.Add("Polar Hunter (Cold Areas)", x => ColdBlooded(2));
            armorModifiers.Add("Polar Hunter (Both Effects)", x => ColdBlooded(3));
            armorModifiers.Add("Resuscitate", x => Crisis());
            armorModifiers.Add("Critical Draw", x => CritDraw());
            armorModifiers.Add("Elemental Crit (GS)", x => CritElement(1));
            armorModifiers.Add("Elemental Crit (LBG/HBG)", x => CritElement(2));
            armorModifiers.Add("Elemental Crit (SnS/DB/Bow)", x => CritElement(3));
            armorModifiers.Add("Elemental Crit (Other)", x => CritElement(4));
            armorModifiers.Add("Status Crit", x => CritStatus());
            armorModifiers.Add("Critical Boost", x => CriticalUp());

            armorModifiers.Add("P. D. Fencer (1st Cart)", x => DFencing(1));
            armorModifiers.Add("P. D. Fencer (2nd Cart)", x => DFencing(2));
            armorModifiers.Add("Deadeye Soul", x => Deadeye());
            armorModifiers.Add("Dragon Atk +1", x => DragonAtk(1));
            armorModifiers.Add("Dragon Atk +2", x => DragonAtk(2));
            armorModifiers.Add("Dragon Atk Down", x => DragonAtk(3));
            armorModifiers.Add("Dreadking Soul", x => Dreadking());
            armorModifiers.Add("Dreadqueen Soul", x => Dreadqueen());
            armorModifiers.Add("Drilltusk Soul", x => Drilltusk());

            armorModifiers.Add("Element Atk Up", x => Elemental());
            armorModifiers.Add("Critical Eye +1", x => Expert(1));
            armorModifiers.Add("Critical Eye +2", x => Expert(2));
            armorModifiers.Add("Critical Eye +3", x => Expert(3));
            armorModifiers.Add("Critical Eye -1", x => Expert(4));
            armorModifiers.Add("Critical Eye -2", x => Expert(5));
            armorModifiers.Add("Critical Eye -3", x => Expert(6));

            armorModifiers.Add("Mind's Eye", x => Fencing());
            armorModifiers.Add("Fire Atk +1", x => FireAtk(1));
            armorModifiers.Add("Fire Atk +2", x => FireAtk(2));
            armorModifiers.Add("Fire Atk Down", x => FireAtk(3));
            armorModifiers.Add("Antivirus", x => FrenzyRes());
            armorModifiers.Add("Resentment", x => Furor());

            armorModifiers.Add("Latent Power +1", x => GlovesOff(1));
            armorModifiers.Add("Latent Power +2", x => GlovesOff(2));

            armorModifiers.Add("Sharpness +1", x => Handicraft(1));
            armorModifiers.Add("Sharpness +2", x => Handicraft(2));
            armorModifiers.Add("TrueShot Up", x => Haphazard());
            armorModifiers.Add("Heavy/Heavy Up", x => HeavyUp());
            armorModifiers.Add("Hellblade Soul", x => Hellblade());
            armorModifiers.Add("Tropic Hunter ", x => HotBlooded());

            armorModifiers.Add("Ice Atk +1", x => IceAtk(1));
            armorModifiers.Add("Ice Atk +2", x => IceAtk(2));
            armorModifiers.Add("Ice Atk Down", x => IceAtk(3));

            armorModifiers.Add("KO King", x => KO());

            armorModifiers.Add("Normal/Rapid Up", x => NormalUp());

            armorModifiers.Add("Pellet/Spread Up (Pellet S)", x => PelletUp(1));
            armorModifiers.Add("Pellet/Spread Up (Spread)", x => PelletUp(2));
            armorModifiers.Add("Pierce/Pierce Up", x => PierceUp());
            armorModifiers.Add("Adrenaline +2", x => Potential(2));
            armorModifiers.Add("Worrywart", x => Potential(3));
            armorModifiers.Add("Punishing Draw (Cut)", x => PunishDraw(1));
            armorModifiers.Add("Punishing Draw (Impact)", x => PunishDraw(2));

            armorModifiers.Add("Bonus Shot", x => RapidFire());
            armorModifiers.Add("Redhelm Soul", x => Redhelm());

            armorModifiers.Add("Silverwind Soul", x => Silverwind());
            armorModifiers.Add("Challenger +1", x => Spirit(1));
            armorModifiers.Add("Challenger +2", x => Spirit(2));
            armorModifiers.Add("Stamina Thief", x => StamDrain());
            armorModifiers.Add("Status Atk +1", x => Status(1));
            armorModifiers.Add("Status Atk +2", x => Status(2));
            armorModifiers.Add("Status Atk Down", x => Status(3));
            armorModifiers.Add("Fortify (1st Cart)", x => Survivor(1));
            armorModifiers.Add("Fortify (2nd Cart)", x => Survivor(2));

            armorModifiers.Add("Weakness Exploit", x => Tenderizer());
            armorModifiers.Add("Thunder Atk +1", x => ThunderAtk(1));
            armorModifiers.Add("Thunder Atk +2", x => ThunderAtk(2));
            armorModifiers.Add("Thunder Atk Down", x => ThunderAtk(3));
            armorModifiers.Add("Thunderlord Soul", x => Thunderlord());

            armorModifiers.Add("Peak Performance", x => Unscathed());

            armorModifiers.Add("Airborne", x => Vault());

            armorModifiers.Add("Water Atk +1", x => WaterAtk(1));
            armorModifiers.Add("Water Atk +2", x => WaterAtk(2));
            armorModifiers.Add("Water Atk Down", x => WaterAtk(3));
#endif

            foreach (KeyValuePair<string, Func<int, bool>> pair in armorModifiers)
            {
                modArmor.Items.Add(pair.Key);
            }

            //Item/Kitchen Modifiers.
#if true
            kitchenItemModifiers.Add("F.Bombardier (Fixed Weaps.)", x => FBombardier(1));
            kitchenItemModifiers.Add("F.Bombardier (Explosive S)", x => FBombardier(2));
            kitchenItemModifiers.Add("F.Bombardier (Impact CB)", x => FBombardier(3));
            kitchenItemModifiers.Add("F.Bombardier (GL)", x => FBombardier(4));
            kitchenItemModifiers.Add("F.Booster", x => FBooster());
            kitchenItemModifiers.Add("F.Bulldozer", x => FBulldozer());
            kitchenItemModifiers.Add("F.Heroics", x => FHeroics());
            kitchenItemModifiers.Add("F.Pyro (Blast)", x => FPyro());
            //kitchenItemModifiers.Add("F.Rider",                     x => FRider()); //Removed because not considering Mount damage.
            kitchenItemModifiers.Add("F.Sharpshooter", x => FSharpshooter());
            kitchenItemModifiers.Add("F.Slugger", x => FSlugger());
            kitchenItemModifiers.Add("F.Specialist", x => FSpecialist());
            kitchenItemModifiers.Add("F.Temper", x => FTemper());
            kitchenItemModifiers.Add("Cool Cat", x => CoolCat());

            kitchenItemModifiers.Add("Powercharm", x => Powercharm());
            kitchenItemModifiers.Add("Power Talon", x => PowerTalon());
            kitchenItemModifiers.Add("Demon Drug", x => DemonDrug(1));
            kitchenItemModifiers.Add("Mega Demon Drug", x => DemonDrug(2));
            kitchenItemModifiers.Add("Attack Up (S) Meal", x => AUMeal(1));
            kitchenItemModifiers.Add("Attack Up (M) Meal", x => AUMeal(2));
            kitchenItemModifiers.Add("Attack Up (L) Meal", x => AUMeal(3));
            kitchenItemModifiers.Add("Might Seed", x => MightSeed(1));
            kitchenItemModifiers.Add("Might Pill", x => MightSeed(2));
            kitchenItemModifiers.Add("Nitroshroom (Mushromancer)", x => Nitroshroom());
            kitchenItemModifiers.Add("Demon Horn", x => Demon(1));
            kitchenItemModifiers.Add("Demon S", x => Demon(2));
            kitchenItemModifiers.Add("Demon Affinity S", x => Demon(3));
#endif
            foreach (KeyValuePair<string, Func<int, bool>> pair in kitchenItemModifiers)
            {
                modKitchen.Items.Add(pair.Key);
            }

            //Weapon Mods
#if true
            weaponModifiers.Add("LSM (Hit Early)", x => LSM(1));
            weaponModifiers.Add("LSM (Hit Late)", x => LSM(2));
            weaponModifiers.Add("GS - Center of Blade", x => GS(1));
            weaponModifiers.Add("GS - Lion's Maw I", x => GS(2));
            weaponModifiers.Add("GS - Lion's Maw II", x => GS(3));
            weaponModifiers.Add("GS - Lion's Maw III", x => GS(4));
            weaponModifiers.Add("LS - Center of Blade", x => LS(1));
            weaponModifiers.Add("LS - Spirit Gauge ON", x => LS(2));
            weaponModifiers.Add("LS - Spirit Gauge (White)", x => LS(3));
            weaponModifiers.Add("LS - Spirit Gauge (Yellow)", x => LS(4));
            weaponModifiers.Add("LS - Spirit Gauge (Red)", x => LS(5));
            weaponModifiers.Add("SnS - Affinity Oil", x => SnS(2));
            weaponModifiers.Add("SnS - Stamina Oil", x => SnS(3));
            weaponModifiers.Add("SnS - Mind's Eye Oil", x => SnS(4));
            weaponModifiers.Add("HH - Attack Up (S) Song", x => HH(1));
            weaponModifiers.Add("HH - Attack Up (S) Encore", x => HH(2));
            weaponModifiers.Add("HH - Attack Up (L) Song", x => HH(3));
            weaponModifiers.Add("HH - Attack Up (L) Encore", x => HH(4));
            weaponModifiers.Add("HH - Elem. Attack Boost Song", x => HH(5));
            weaponModifiers.Add("HH - Elem. Attack Boost Encore", x => HH(6));
            weaponModifiers.Add("HH - Abnormal Boost Song", x => HH(7));
            weaponModifiers.Add("HH - Abnormal Boost Encore", x => HH(8));
            weaponModifiers.Add("HH - Affinity Up Song", x => HH(9));
            weaponModifiers.Add("HH - Affinity Up Encore", x => HH(10));
            weaponModifiers.Add("HH - Self-Improvement Encore", x => HH(11));
            weaponModifiers.Add("Lance - Enraged Guard (Yellow)", x => Lance(1));
            weaponModifiers.Add("Lance - Enraged Guard (Orange)", x => Lance(2));
            weaponModifiers.Add("Lance - Enraged Guard (Red)", x => Lance(3));
            weaponModifiers.Add("Lance - Impact/Cut Hitzone", x => Lance(4));
            //weaponModifiers.Add("GL - Dragon Breath", x => GL(1));
            weaponModifiers.Add("GL - Orange Heat", x => GL(2));
            weaponModifiers.Add("GL - Red Heat", x => GL(3));
            weaponModifiers.Add("SA - Power Phial", x => SA(1));
            weaponModifiers.Add("SA - Element Phial", x => SA(2));
            weaponModifiers.Add("SA - Energy Charge II", x => SA(3));
            weaponModifiers.Add("SA - Energy Charge III", x => SA(4));
            weaponModifiers.Add("SA - Demon Riot I 'Pwr'", x => SA(5));
            weaponModifiers.Add("SA - Demon Riot II 'Pwr'", x => SA(6));
            weaponModifiers.Add("SA - Demon Riot III 'Pwr'", x => SA(7));
            weaponModifiers.Add("SA - Demon Riot I 'Ele'", x => SA(8));
            weaponModifiers.Add("SA - Demon Riot II 'Ele'", x => SA(9));
            weaponModifiers.Add("SA - Demon Riot III 'Ele'", x => SA(10));
            weaponModifiers.Add("SA - Demon Riot I 'Sta'", x => SA(11));
            weaponModifiers.Add("SA - Demon Riot II 'Sta'", x => SA(12));
            weaponModifiers.Add("SA - Demon Riot III 'Sta'", x => SA(13));
            weaponModifiers.Add("CB - Red Shield (Other Styles)", x => CB(1));
            weaponModifiers.Add("CB - Red Shield (Striker)", x => CB(2));
            weaponModifiers.Add("IG - Red (Balanced)", x => IG(1));
            weaponModifiers.Add("IG - White (Balanced)", x => IG(2));
            weaponModifiers.Add("IG - Red/White", x => IG(3));
            weaponModifiers.Add("IG - Triple Up", x => IG(4));
            weaponModifiers.Add("Gunner - Normal Distance (1x)", x => Gunner(1));
            weaponModifiers.Add("Gunner - Critical Distance (1.5x)", x => Gunner(2));
            weaponModifiers.Add("Gunner - Long Range (0.8x)", x => Gunner(3));
            weaponModifiers.Add("Gunner - Ex. Long Range (0.5x)", x => Gunner(4));
            weaponModifiers.Add("LBG - Raw Multiplier (1.3x)", x => LBG(1));
            weaponModifiers.Add("LBG - Long Barrel (1.05x)", x => LBG(2));
            weaponModifiers.Add("LBG - Power Reload", x => LBG(3));
            weaponModifiers.Add("HBG - Raw Multiplier (1.48x)", x => HBG(1));
            weaponModifiers.Add("HBG - Power Barrel (1.05x)", x => HBG(2));
            weaponModifiers.Add("HBG - Power Reload", x => HBG(3));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (Non-Status)", x => Bow(1));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (+Poison)", x => Bow(2));
            weaponModifiers.Add("Bow - Charge Lvl. 1 (+Para/Sleep)", x => Bow(3));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (Non-Status)", x => Bow(4));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (+Poison)", x => Bow(5));
            weaponModifiers.Add("Bow - Charge Lvl. 2 (+Para/Sleep)", x => Bow(6));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (Non-Status)", x => Bow(7));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (+Poison)", x => Bow(8));
            weaponModifiers.Add("Bow - Charge Lvl. 3 (+Para/Sleep)", x => Bow(9));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (Non-Status)", x => Bow(10));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (+Poison)", x => Bow(11));
            weaponModifiers.Add("Bow - Charge Lvl. 4 (+Para/Sleep)", x => Bow(12));
            weaponModifiers.Add("Bow - Power C. Lvl. 1", x => Bow(13));
            weaponModifiers.Add("Bow - Power C. Lvl. 2", x => Bow(14));
            weaponModifiers.Add("Bow - Ele. C. Lvl. 1", x => Bow(15));
            weaponModifiers.Add("Bow - Ele. C. Lvl. 2", x => Bow(16));
            weaponModifiers.Add("Bow - Coating Boost 'Pwr'", x => Bow(17));
            weaponModifiers.Add("Bow - Coating Boost 'Ele'", x => Bow(18));
            weaponModifiers.Add("Bow - Coating Boost 'C.Range'", x => Bow(19));
            weaponModifiers.Add("Bow - Coating Boost 'Sta'", x => Bow(20));
#endif
            foreach (KeyValuePair<string, Func<int, bool>> pair in weaponModifiers)
            {
                modWeapon.Items.Add(pair.Key);
            }

            //Other modifiers
#if true
            otherModifiers.Add("Frenzy", x => Frenzy());
#endif

            foreach (KeyValuePair<string, Func<int, bool>> pair in otherModifiers)
            {
                modOther.Items.Add(pair.Key);
            }
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

            //Read monster database
            readMonsters();
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
                    type = type.Replace('_', ' ');
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
                            reader.Read(); //eleMod tag
                            reader.Read(); //eleMod value
                            double eleMod = double.Parse(reader.Value);

                            reader.Read(); //end eleMod
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
                            moves.Add(new moveStat(name, id, damageType, motionValue, perHit, hitCount, sharpnessMod, eleMod, KODamage, exhaustDamage, mindsEye, drawAttack, aerialAttack));

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
                    type = type.Replace('_', ' ');
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

                                double aff = double.Parse(reader.Value);
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

        private void readMonsters()
        {
            string[] files = System.IO.Directory.GetFiles("./Monsters/", "*.xml", System.IO.SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string monsterFile = file.Remove(file.Length - 4); //Strip trailing '.xml'
                monsterFile = monsterFile.Remove(0, 14);
                if (monsterFile.Contains('_'))
                {
                    monsterFile = monsterFile.Replace('_', ' ');
                }

                monName.Items.Add(monsterFile);

                monsterStat thisStat = new monsterStat(monsterFile);
                monsterStats.Add(monsterFile, thisStat);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                using (XmlReader reader = XmlReader.Create(file, settings))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.Name == "hitzone" && reader.NodeType != XmlNodeType.EndElement)
                        {
                            string name = reader.GetAttribute("name");
                            reader.Read(); //cut tag
                            reader.Read(); //cut double

                            double cut = double.Parse(reader.Value);
                            reader.Read(); //end cut
                            reader.Read(); //impact tag
                            reader.Read(); //impact double

                            double impact = double.Parse(reader.Value);
                            reader.Read(); //end impact
                            reader.Read(); //shot tag
                            reader.Read(); //shot double

                            double shot = double.Parse(reader.Value);
                            reader.Read(); //end shot
                            reader.Read(); //KO tag
                            reader.Read(); //KO double

                            double KO = double.Parse(reader.Value);
                            reader.Read(); //end KO
                            reader.Read(); //exhaust tag
                            reader.Read(); //exhaust double

                            double exhaust = double.Parse(reader.Value);
                            reader.Read(); //end exhaust
                            reader.Read(); //fire tag
                            reader.Read(); //fire double

                            double fire = double.Parse(reader.Value);
                            reader.Read(); //end fire
                            reader.Read(); //water tag
                            reader.Read(); //water double

                            double water = double.Parse(reader.Value);
                            reader.Read(); //end water
                            reader.Read(); //thunder tag
                            reader.Read(); //thunder double

                            double thunder = double.Parse(reader.Value);
                            reader.Read(); //end thunder
                            reader.Read(); //ice tag
                            reader.Read(); //ice double

                            double ice = double.Parse(reader.Value);
                            reader.Read(); //end ice
                            reader.Read(); //dragon tag
                            reader.Read(); //dragon double

                            double dragon = double.Parse(reader.Value);
                            reader.Read(); //end dragon
                            reader.Read(); //end hitzone

                            hitzoneStats hitzone = new hitzoneStats(name, cut, impact, shot, fire, water, thunder, ice, dragon, KO, exhaust);
                            thisStat.hitzones.Add(hitzone);
                        }

                        else if (reader.Name == "quest" && reader.NodeType != XmlNodeType.EndElement)
                        {
                            reader.Read(); //name tag
                            reader.Read(); //name string

                            string name = reader.Value;
                            reader.Read(); //end name
                            reader.Read(); //quest tag
                            reader.Read(); //quest double

                            double questMod = double.Parse(reader.Value);
                            reader.Read(); //end quest
                            reader.Read(); //exhaust tag
                            reader.Read(); //exhaust double

                            double exhaustMod = double.Parse(reader.Value);
                            reader.Read(); //end exhaust
                            reader.Read(); //end quest

                            questStat status = new questStat();
                            status.name = name;
                            status.questMod = questMod;
                            status.exhaustMod = exhaustMod;

                            thisStat.quests.Add(status);
                        }

                        else if (reader.Name == "status" && reader.NodeType != XmlNodeType.EndElement)
                        {
                            monsterStatusThresholds thresh = new monsterStatusThresholds();

                            reader.Read(); //poison tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double poisonInit = double.Parse(reader.Value);
                            thresh.poisonInit = poisonInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double poisonInc = double.Parse(reader.Value);
                            thresh.poisonInc = poisonInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double poisonMax = double.Parse(reader.Value);
                            thresh.poisonMax = poisonMax;
                            reader.Read(); //end max
                            reader.Read(); //end poison

                            reader.Read(); //sleep tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double sleepInit = double.Parse(reader.Value);
                            thresh.sleepInit = sleepInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double sleepInc = double.Parse(reader.Value);
                            thresh.sleepInc = sleepInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double sleepMax = double.Parse(reader.Value);
                            thresh.sleepMax = sleepMax;
                            reader.Read(); //end max
                            reader.Read(); //end sleep

                            reader.Read(); //para tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double paraInit = double.Parse(reader.Value);
                            thresh.paraInit = paraInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double paraInc = double.Parse(reader.Value);
                            thresh.paraInc = paraInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double paraMax = double.Parse(reader.Value);
                            thresh.paraMax = paraMax;
                            reader.Read(); //end max
                            reader.Read(); //end para

                            reader.Read(); //KO tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double KOInit = double.Parse(reader.Value);
                            thresh.KOInit = KOInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double KOInc = double.Parse(reader.Value);
                            thresh.KOInc = KOInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double KOMax = double.Parse(reader.Value);
                            thresh.KOMax = KOMax;
                            reader.Read(); //end max
                            reader.Read(); //end KO

                            reader.Read(); //exhaust tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double exhaustInit = double.Parse(reader.Value);
                            thresh.exhaustInit = exhaustInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double exhaustInc = double.Parse(reader.Value);
                            thresh.exhaustInc = exhaustInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double exhaustMax = double.Parse(reader.Value);
                            thresh.exhaustMax = exhaustMax;
                            reader.Read(); //end max
                            reader.Read(); //end exhaust

                            reader.Read(); //blast tag
                            reader.Read(); //init
                            reader.Read(); //init double

                            double blastInit = double.Parse(reader.Value);
                            thresh.blastInit = blastInit;
                            reader.Read(); //end init
                            reader.Read(); //increase tag
                            reader.Read(); //increase double

                            double blastInc = double.Parse(reader.Value);
                            thresh.blastInc = blastInc;
                            reader.Read(); //end increase
                            reader.Read(); //max tag
                            reader.Read(); //max double

                            double blastMax = double.Parse(reader.Value);
                            thresh.blastMax = blastMax;
                            reader.Read(); //end max
                            reader.Read(); //end blast

                            thisStat.status = thresh;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of Insert Sort, used for sorting by ID.
        /// Adapted from the pseudocode on Wikipedia.
        /// </summary>
        /// <param name="statList"></param>
        private void insertSort1(List<moveStat> statList)
        {
            for (int i = 1; i != statList.Count; i++)
            {
                double x = statList[i].totalValue;
                moveStat x1 = statList[i];

                int j = i - 1;
                while (j >= 0 && statList[j].totalValue > x)
                {
                    statList[j + 1] = statList[j];
                    j = j - 1;
                }

                statList[j + 1] = x1;
            }
        }

        /// <summary>
        /// Implementation of Insert Sort, used for sorting by ID.
        /// Adapted from the pseudocode on Wikipedia.
        /// </summary>
        /// <param name="statList"></param>
        private void insertSort2(List<moveStat> statList)
        {
            for (int i = 1; i != statList.Count; i++)
            {
                int x = statList[i].id;
                moveStat x1 = statList[i];

                int j = i - 1;
                while (j >= 0 && statList[j].id > x)
                {
                    statList[j + 1] = statList[j];
                    j = j - 1;
                }

                statList[j + 1] = x1;
            }
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
                weaponAndMods.addRaw += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.addRaw += 20;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.addRaw -= 5;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.addRaw -= 10;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.addRaw -= 15;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Blunt()
        {
            if (weaponAndMods.sharpness == "Green")
            {
                weaponAndMods.addRaw += 15;
            }
            else if (weaponAndMods.sharpness == "Yellow")
            {
                weaponAndMods.addRaw += 25;
            }
            else if (weaponAndMods.sharpness == "Orange" || weaponAndMods.sharpness == "Red")
            {
                weaponAndMods.addRaw += 30;
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
                if (weaponAndMods.altDamageType == "Blast" || weaponAndMods.secElement == "Blast")
                {
                    weaponAndMods.staMod = weaponAndMods.staMod * 1.2;
                }
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
                weaponAndMods.addRaw += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.addRaw += 20;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Crisis()
        {
            weaponAndMods.addRaw += 20;
            return true;
        }

        private bool CritDraw()
        {
            if (moveDraw.Checked)
            {
                weaponAndMods.affinity += 100;
            }
            return true;
        }

        private bool CritElement(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.eleCrit = "GS";
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleCrit = "LBG/HBG";
            }
            else if (skillVal == 3)
            {
                weaponAndMods.eleCrit = "SnS/DB/Bow";
            }
            else if (skillVal == 4)
            {
                weaponAndMods.eleCrit = "Other";
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool CritStatus()
        {
            weaponAndMods.statusCrit = true;
            return true;
        }

        private bool CriticalUp()
        {
            weaponAndMods.criticalBoost = true;
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
            weaponAndMods.addRaw += 20;
            weaponAndMods.affinity += 15;
            return true;
        }

        private bool DragonAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                if (weaponAndMods.altDamageType == "Dragon" || weaponAndMods.secElement == "Dragon")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.05;
                    weaponAndMods.addElement += 4;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.altDamageType == "Dragon" || weaponAndMods.secElement == "Dragon")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
                    weaponAndMods.addElement += 6;
                }
            }
            else if (skillVal == 3)
            {
                if (weaponAndMods.altDamageType == "Dragon" || weaponAndMods.secElement == "Dragon")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Dreadking()
        {
            weaponAndMods.addRaw += 20;
            return true;
        }

        private bool Dreadqueen()
        {
            if (isStatus(weaponAndMods.altDamageType) || isStatus(weaponAndMods.secElement))
            {
                weaponAndMods.staMod = weaponAndMods.staMod * 1.2;
                weaponAndMods.addElement += 1;
            }
            return true;
        }

        private bool Drilltusk()
        {
            weaponAndMods.rawMod = weaponAndMods.rawMod * 1.3;
            return true;
        }

        private bool Elemental()
        {
            if (isElement(weaponAndMods.altDamageType) || isElement(weaponAndMods.secElement))
            {
                weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
            }
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
                if (weaponAndMods.altDamageType == "Fire" || weaponAndMods.secElement == "Fire")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.05;
                    weaponAndMods.addElement += 4;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.altDamageType == "Fire" || weaponAndMods.secElement == "Fire")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
                    weaponAndMods.addElement += 6;
                }
            }
            else if (skillVal == 3)
            {
                if (weaponAndMods.altDamageType == "Fire" || weaponAndMods.secElement == "Fire")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
                }
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
            weaponAndMods.addRaw += 20;
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

        private bool HotBlooded()
        {
            weaponAndMods.addRaw += 15;
            return true;
        }

        private bool IceAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                if (weaponAndMods.altDamageType == "Ice" || weaponAndMods.secElement == "Ice")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.05;
                    weaponAndMods.addElement += 4;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.altDamageType == "Ice" || weaponAndMods.secElement == "Ice")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
                    weaponAndMods.addElement += 6;
                }
            }
            else if (skillVal == 3)
            {
                if (weaponAndMods.altDamageType == "Ice" || weaponAndMods.secElement == "Ice")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
                }
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
                if (moveDraw.Checked)
                {
                    weaponAndMods.addRaw += 5;
                    weaponAndMods.KOPower += 30;
                    weaponAndMods.exhaustPower += 20;
                }
            }
            else if (skillVal == 2)
            {
                if (moveDraw.Checked)
                {
                    weaponAndMods.addRaw += 5;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool RapidFire()
        {
            weaponAndMods.hitCount++;
            weaponAndMods.totalMV = weaponAndMods.hitCount * weaponAndMods.avgMV;
            return true;
        }

        private bool Redhelm()
        {
            weaponAndMods.addRaw += 20;
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
                weaponAndMods.addRaw += 10;
                weaponAndMods.affinity += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 20;
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
                if (isStatus(weaponAndMods.altDamageType) || isStatus(weaponAndMods.secElement))
                {
                    weaponAndMods.staMod = weaponAndMods.staMod * 1.1;
                    weaponAndMods.addElement += 1;
                }
            }
            else if (skillVal == 2)
            {
                if (isStatus(weaponAndMods.altDamageType) || isStatus(weaponAndMods.secElement))
                {
                    weaponAndMods.staMod = weaponAndMods.staMod * 1.2;
                    weaponAndMods.addElement += 1;
                }
            }
            else if (skillVal == 3)
            {
                if (isStatus(weaponAndMods.altDamageType) || isStatus(weaponAndMods.secElement))
                {
                    weaponAndMods.staMod = weaponAndMods.staMod * 0.9;
                }
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
                if (weaponAndMods.altDamageType == "Thunder" || weaponAndMods.secElement == "Thunder")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.05;
                    weaponAndMods.addElement += 4;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.altDamageType == "Thunder" || weaponAndMods.secElement == "Thunder")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
                    weaponAndMods.addElement += 6;
                }
            }
            else if (skillVal == 3)
            {
                if (weaponAndMods.altDamageType == "Thunder" || weaponAndMods.secElement == "Thunder")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
                }
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
            weaponAndMods.addRaw += 20;
            return true;
        }

        private bool Vault()
        {
            if (moveAerial.Checked)
            {
                weaponAndMods.rawMod = weaponAndMods.rawMod * 1.1;
            }
            return true;
        }

        private bool WaterAtk(int skillVal)
        {
            if (skillVal == 1)
            {
                if (weaponAndMods.altDamageType == "Water" || weaponAndMods.secElement == "Water")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.05;
                    weaponAndMods.addElement += 4;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.altDamageType == "Water" || weaponAndMods.secElement == "Water")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 1.1;
                    weaponAndMods.addElement += 6;
                }
            }
            else if (skillVal == 3)
            {
                if (weaponAndMods.altDamageType == "Water" || weaponAndMods.secElement == "Water")
                {
                    weaponAndMods.eleMod = weaponAndMods.eleMod * 0.75;
                }
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
            weaponAndMods.addRaw += 3;
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
            if (weaponAndMods.altDamageType == "Blast" || weaponAndMods.secElement == "Blast")
            {
                weaponAndMods.staMod *= 1.1;
            }

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
            if (isStatus(weaponAndMods.altDamageType) || isStatus(weaponAndMods.secElement))
            {
                weaponAndMods.staMod *= 1.125;
            }
            return true;
        }

        private bool FTemper()
        {
            weaponAndMods.rawMod *= 1.05;
            return true;
        }

        private bool CoolCat()
        {
            weaponAndMods.addRaw += 15;
            return true;
        }

        private bool Powercharm()
        {
            weaponAndMods.addRaw += 6;
            return true;
        }

        private bool PowerTalon()
        {
            weaponAndMods.addRaw += 9;
            return true;
        }

        private bool DemonDrug(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.addRaw += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 7;
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
                weaponAndMods.addRaw += 3;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 5;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.addRaw += 7;
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
                weaponAndMods.addRaw += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 25;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Nitroshroom()
        {
            weaponAndMods.addRaw += 10;
            return true;
        }

        private bool Demon(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.addRaw += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.addRaw += 10;
                if (weapSharpness.Text != "(No Sharpness)")
                {
                    weaponAndMods.rawSharpMod *= 1.1;
                }
            }
            else if (skillVal == 3)
            {
                weaponAndMods.addRaw += 7;
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
                if (weaponAndMods.sharpness == "Yellow" || weaponAndMods.sharpness == "Orange" || weaponAndMods.sharpness == "Red")
                {
                    weaponAndMods.rawMod *= 0.6;
                }
            }
            else if (skillVal == 2)
            {
                if (weaponAndMods.sharpness == "Yellow" || weaponAndMods.sharpness == "Orange" || weaponAndMods.sharpness == "Red")
                {
                    weaponAndMods.rawMod *= 0.7;
                }
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

        private bool HH(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.15;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 1.20;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.eleMod *= 1.08;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.eleMod *= 1.10;
            }
            else if (skillVal == 7)
            {
                weaponAndMods.staMod *= 1.1;
            }
            else if (skillVal == 8)
            {
                weaponAndMods.staMod *= 1.15;
            }
            else if (skillVal == 9)
            {
                weaponAndMods.affinity += 15;
            }
            else if (skillVal == 10)
            {
                weaponAndMods.affinity += 20;
            }
            else if (skillVal == 11)
            {
                weaponAndMods.mindsEye = true;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Lance(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.3;
            }
            else if (skillVal == 4)
            {
                if (double.Parse(monImpact.Text) * 0.72 > double.Parse(monCut.Text))
                {
                    weaponAndMods.hitzone = double.Parse(monImpact.Text) * 0.72;
                }
            }
            else if (skillVal == 5)
            {
                weaponAndMods.eleMod *= 0.5;
                weaponAndMods.staMod *= 0.5;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool GL(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.avgMV += 10;
                weaponAndMods.secPower += 10;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.15;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool SA(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.eleMod *= 1.25;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.affinity += 10;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.affinity += 30;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.rawMod *= 1.05;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.rawMod *= 1.10;
            }
            else if (skillVal == 7)
            {
                weaponAndMods.rawMod *= 1.20;
            }
            else if (skillVal == 8)
            {
                weaponAndMods.eleMod *= 1.05;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 9)
            {
                weaponAndMods.eleMod *= 1.10;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 10)
            {
                weaponAndMods.eleMod *= 1.20;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 11)
            {
                weaponAndMods.staMod *= 1.05;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 12)
            {
                weaponAndMods.staMod *= 1.10;
                weaponAndMods.DemonRiot = true;
            }
            else if (skillVal == 13)
            {
                weaponAndMods.staMod *= 1.20;
                weaponAndMods.DemonRiot = true;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool CB(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.15;
                weaponAndMods.KOPower *= 1.35;
                weaponAndMods.exhaustPower *= 1.35;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.2;
                weaponAndMods.KOPower *= 1.35;
                weaponAndMods.exhaustPower *= 1.35;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool IG(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.addRaw += 5;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.affinity += 10;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.15;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 1.2;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Gunner(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 1.5;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 0.8;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 0.5;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool LBG(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.3;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower *= 1.05;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.06;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool HBG(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 1.48;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.totalAttackPower *= 1.05;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 1.06;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool Bow(int skillVal)
        {
            if (skillVal == 1)
            {
                weaponAndMods.rawMod *= 0.4;
                weaponAndMods.eleMod *= 0.7;
            }
            else if (skillVal == 2)
            {
                weaponAndMods.rawMod *= 0.4;
                weaponAndMods.staMod *= 0.5;
            }
            else if (skillVal == 3)
            {
                weaponAndMods.rawMod *= 0.4;
                weaponAndMods.staMod *= 0.5;
            }
            else if (skillVal == 4)
            {
                weaponAndMods.rawMod *= 1.0;
                weaponAndMods.eleMod *= 0.85;
            }
            else if (skillVal == 5)
            {
                weaponAndMods.rawMod *= 1.0;
                weaponAndMods.staMod *= 1.0;
            }
            else if (skillVal == 6)
            {
                weaponAndMods.rawMod *= 1.0;
                weaponAndMods.staMod *= 1.0;
            }
            else if (skillVal == 7)
            {
                weaponAndMods.rawMod *= 1.5;
                weaponAndMods.eleMod *= 1.0;
            }
            else if (skillVal == 8)
            {
                weaponAndMods.rawMod *= 1.5;
                weaponAndMods.staMod *= 1.5;
            }
            else if (skillVal == 9)
            {
                weaponAndMods.rawMod *= 0.4;
                weaponAndMods.staMod *= 0.7;
            }
            else if (skillVal == 10)
            {
                weaponAndMods.rawMod *= 1.7;
                weaponAndMods.eleMod *= 1.125;
            }
            else if (skillVal == 11)
            {
                weaponAndMods.rawMod *= 1.7;
                weaponAndMods.staMod *= 1.5;
            }
            else if (skillVal == 12)
            {
                weaponAndMods.rawMod *= 1.7;
                weaponAndMods.staMod *= 1.3;
            }
            else if (skillVal == 13)
            {
                weaponAndMods.rawMod *= 1.35;
            }
            else if (skillVal == 14)
            {
                weaponAndMods.rawMod *= 1.5;
            }
            else if (skillVal == 15)
            {
                weaponAndMods.eleMod *= 1.35;
            }
            else if (skillVal == 16)
            {
                weaponAndMods.eleMod *= 1.50;
            }
            else if (skillVal == 17)
            {
                weaponAndMods.rawMod *= 1.20;
            }
            else if (skillVal == 18)
            {
                weaponAndMods.eleMod *= 1.20;
            }
            else if (skillVal == 19)
            {
                weaponAndMods.rawMod *= 1.20;
            }
            else if (skillVal == 20)
            {
                weaponAndMods.staMod *= 1.20;
            }
            else
            {
                return false;
            }
            return true;
        }
#endif
        //Other modifiers
#if true
        private bool Frenzy()
        {
            weaponAndMods.affinity += 15;
            return true;
        }



        private void importSetUp()
        {
            weaponAndMods = new importedStats();
            weaponAndMods.sharpness = (string)weapSharpness.SelectedItem;
            weaponAndMods.totalAttackPower = double.Parse(weapAttack.Text);
            weaponAndMods.affinity = double.Parse(weapAffinity.Text);
            weaponAndMods.rawSharpMod = sharpnessValues[weaponAndMods.sharpness].Item1;
            weaponAndMods.eleSharpMod = sharpnessValues[weaponAndMods.sharpness].Item2;

            if (weapOverride.Checked)
            {
                weaponAndMods.altDamageType = secondType;
                weaponAndMods.eleAttackPower = double.Parse(weapSecPower.Text);
                weaponAndMods.secElement = "None";
                weaponAndMods.secPower = 0;
            }
            else
            {
                weaponAndMods.altDamageType = weapAlt.Text;
                weaponAndMods.eleAttackPower = double.Parse(weapAltPower.Text);
                weaponAndMods.secElement = secondType;
                weaponAndMods.secPower = double.Parse(weapSecPower.Text);
            }

            weaponAndMods.avgMV = double.Parse(moveAvg.Text);
            weaponAndMods.hitCount = int.Parse(moveHitCount.Text);
            weaponAndMods.totalMV = double.Parse(moveTotal.Text);
            weaponAndMods.KOPower = double.Parse(moveKO.Text);
            weaponAndMods.exhaustPower = double.Parse(moveExh.Text);
            weaponAndMods.criticalBoost = false;
            weaponAndMods.mindsEye = moveMinds.Checked;
            weaponAndMods.damageType = moveDamType.Text;
            weaponAndMods.eleCrit = "None";
            weaponAndMods.statusCrit = false;

            weaponAndMods.rawSharpMod *= double.Parse(moveSharp.Text);
            weaponAndMods.eleSharpMod *= double.Parse(moveEleMod.Text);

            if (weaponAndMods.damageType == "Cut")
            {
                weaponAndMods.hitzone = double.Parse(monCut.Text);
            }
            else if (weaponAndMods.damageType == "Impact")
            {
                weaponAndMods.hitzone = double.Parse(monImpact.Text);
            }
            else if (weaponAndMods.damageType == "Shot")
            {
                weaponAndMods.hitzone = double.Parse(monShot.Text);
            }
            else if (weaponAndMods.damageType == "Fixed")
            {
                weaponAndMods.hitzone = 0;
            }

            if (weaponAndMods.altDamageType == "Fire")
            {
                weaponAndMods.eleHitzone = double.Parse(monFire.Text);
            }
            else if (weaponAndMods.altDamageType == "Water")
            {
                weaponAndMods.eleHitzone = double.Parse(monWater.Text);
            }
            else if (weaponAndMods.altDamageType == "Thunder")
            {
                weaponAndMods.eleHitzone = double.Parse(monThunder.Text);
            }
            else if (weaponAndMods.altDamageType == "Ice")
            {
                weaponAndMods.eleHitzone = double.Parse(monIce.Text);
            }
            else if (weaponAndMods.altDamageType == "Dragon")
            {
                weaponAndMods.eleHitzone = double.Parse(monDragon.Text);
            }

            if (weaponAndMods.secElement == "Fire")
            {
                weaponAndMods.secHitzone = double.Parse(monFire.Text);
            }
            else if (weaponAndMods.secElement == "Water")
            {
                weaponAndMods.secHitzone = double.Parse(monWater.Text);
            }
            else if (weaponAndMods.secElement == "Thunder")
            {
                weaponAndMods.secHitzone = double.Parse(monThunder.Text);
            }
            else if (weaponAndMods.secElement == "Ice")
            {
                weaponAndMods.secHitzone = double.Parse(monIce.Text);
            }
            else if (weaponAndMods.secElement == "Dragon")
            {
                weaponAndMods.secHitzone = double.Parse(monDragon.Text);
            }

            weaponAndMods.questMod = double.Parse(monQuestMod.Text);
            weaponAndMods.exhaustMod = double.Parse(monExhField.Text);
            weaponAndMods.KOHitzone = double.Parse(monKO.Text);
            weaponAndMods.exhaustHitzone = double.Parse(monExh.Text);

            weaponAndMods.rawMod = 1;
            weaponAndMods.eleMod = 1;
            weaponAndMods.expMod = 1;
            weaponAndMods.staMod = 1;
            weaponAndMods.CB = false;
            weaponAndMods.DemonRiot = false;
            weaponAndMods.addRaw = 0;
            weaponAndMods.addElement = 0;
        }

        private void importModifiers()
        {
            foreach (ListViewItem item in modList.Groups[0].Items)
            {
                armorModifiers[item.Text](0);
            }
            foreach (ListViewItem item in modList.Groups[1].Items)
            {
                kitchenItemModifiers[item.Text](0);
            }
            foreach (ListViewItem item in modList.Groups[2].Items)
            {
                weaponModifiers[item.Text](0);
            }
            foreach (ListViewItem item in modList.Groups[3].Items)
            {
                otherModifiers[item.Text](0);
            }

            if (weaponAndMods.damageType == "Fixed")
            {
                if (weaponAndMods.expMod > 1.3 && !weaponAndMods.CB)
                {
                    weaponAndMods.expMod = 1.3;
                }
                weaponAndMods.totalAttackPower = 100;
                weaponAndMods.totalAttackPower *= weaponAndMods.expMod;
            }
            else
            {
                weaponAndMods.totalAttackPower += weaponAndMods.addRaw;
                weaponAndMods.totalAttackPower *= weaponAndMods.rawMod;
            }

            if (isElement(weaponAndMods.altDamageType))
            {
                if (weaponAndMods.eleMod > 1.2 && !weaponAndMods.DemonRiot)
                {
                    weaponAndMods.eleMod = 1.2;
                }
                weaponAndMods.eleAttackPower *= weaponAndMods.eleMod;
                weaponAndMods.eleAttackPower += weaponAndMods.addElement;
            }
            else if (isStatus(weaponAndMods.altDamageType) || weaponAndMods.altDamageType == "Blast")
            {
                if (weaponAndMods.staMod > 1.25 && !weaponAndMods.DemonRiot)
                {
                    weaponAndMods.staMod = 1.25;
                }
                weaponAndMods.eleAttackPower *= weaponAndMods.staMod;
                weaponAndMods.eleAttackPower += weaponAndMods.addElement;
            }

            if (weaponAndMods.affinity > 100)
            {
                weaponAndMods.affinity = 100;
            }

            else if (weaponAndMods.affinity < -100)
            {
                weaponAndMods.affinity = -100;
            }
        }

        private void export()
        {
            paraSharpness.SelectedItem = weaponAndMods.sharpness;
            paraRaw.Text = weaponAndMods.totalAttackPower.ToString();
            paraRawSharp.Text = weaponAndMods.rawSharpMod.ToString();
            paraKO.Text = weaponAndMods.KOPower.ToString();
            paraAltType.SelectedItem = weaponAndMods.altDamageType;
            paraEle.Text = weaponAndMods.eleAttackPower.ToString();
            paraEleSharp.Text = weaponAndMods.eleSharpMod.ToString();
            paraExh.Text = weaponAndMods.exhaustPower.ToString();
            paraMV.Text = weaponAndMods.avgMV.ToString();
            paraHitCount.Text = weaponAndMods.hitCount.ToString();
            paraTotal.Text = weaponAndMods.totalMV.ToString();
            paraEleCrit.SelectedItem = weaponAndMods.eleCrit;
            paraAffinity.Text = weaponAndMods.affinity.ToString();
            paraSecEle.SelectedItem = weaponAndMods.secElement;
            paraSecPower.Text = weaponAndMods.secPower.ToString();
            paraFixed.Checked = (weaponAndMods.damageType == "Fixed");
            paraBoost.Checked = weaponAndMods.criticalBoost;
            paraMinds.Checked = weaponAndMods.mindsEye;
            paraStatusCrit.Checked = weaponAndMods.statusCrit;

            paraRawHitzone.Text = weaponAndMods.hitzone.ToString();
            paraEleHitzone.Text = weaponAndMods.eleHitzone.ToString();
            paraSecHitzone.Text = weaponAndMods.secHitzone.ToString();
            paraKOZone.Text = weaponAndMods.KOHitzone.ToString();
            paraExhZone.Text = weaponAndMods.exhaustHitzone.ToString();
            paraExhMod.Text = weaponAndMods.exhaustMod.ToString();
            paraQuest.Text = weaponAndMods.questMod.ToString();
        }

        private void statusExport()
        {
            if (isStatus(weaponAndMods.altDamageType) || weaponAndMods.altDamageType == "Blast")
            {
                staType.SelectedItem = weaponAndMods.altDamageType;
                staPower.Text = weaponAndMods.eleAttackPower.ToString();
            }
            else
            {
                staType.SelectedIndex = 0;
                staPower.Text = 0.ToString();
            }

            if (isStatus(weaponAndMods.secElement) || weaponAndMods.secElement == "Blast")
            {
                staSecEle.SelectedItem = weaponAndMods.secElement;
                staSecPower.Text = weaponAndMods.secPower.ToString();
            }
            else
            {
                staSecEle.SelectedIndex = 0;
                staSecPower.Text = 0.ToString();
            }

            staEleSharp.Text = weaponAndMods.eleSharpMod.ToString();
            staKOPow.Text = weaponAndMods.KOPower.ToString();
            staExhaust.Text = weaponAndMods.exhaustPower.ToString();

            if (weaponAndMods.statusCrit == true)
            {
                staCritCheck.Checked = true;
                staAffinity.Text = weaponAndMods.affinity.ToString();
            }
            else
            {
                staCritCheck.Checked = false;
                staAffinity.Text = 0.ToString();
            }

            staKOZone.Text = weaponAndMods.KOHitzone.ToString();
            staExhaustZone.Text = weaponAndMods.exhaustHitzone.ToString();
            staExhMod.Text = weaponAndMods.exhaustMod.ToString();

            monsterStatusThresholds thresholds = monsterStats[monName.Text].status;
            staPoiInit.Text = thresholds.poisonInit.ToString();
            staPoiInc.Text = thresholds.poisonInc.ToString();
            staPoiMax.Text = thresholds.poisonMax.ToString();
            staSleepInit.Text = thresholds.sleepInit.ToString();
            staSleepInc.Text = thresholds.sleepInc.ToString();
            staSleepMax.Text = thresholds.sleepMax.ToString();
            staParaInit.Text = thresholds.paraInit.ToString();
            staParaInc.Text = thresholds.paraInc.ToString();
            staParaMax.Text = thresholds.paraMax.ToString();
            staKOInit.Text = thresholds.KOInit.ToString();
            staKOInc.Text = thresholds.KOInc.ToString();
            staKOMax.Text = thresholds.KOMax.ToString();
            staExhaustInit.Text = thresholds.exhaustInit.ToString();
            staExhInc.Text = thresholds.exhaustInc.ToString();
            staExhMax.Text = thresholds.exhaustMax.ToString();
            staBlastInit.Text = thresholds.blastInit.ToString();
            staBlastInc.Text = thresholds.blastInc.ToString();
            staBlastMax.Text = thresholds.blastMax.ToString();
        }

        private void eleShotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setElementShot(); //Sets the Selected Index of the alt type of the weapon.
            findPower(); //Sets the elemental power of the weapon.
            findRFMod(); //Sets the element, raw, hit count, and element/raw mods.
        }

        private void findRFMod()
        {
            int index = eleShotType.SelectedIndex;
            if(index == 1 || index == 6 || index == 15 || index == 18 || index == 27 || index == 30 || index == 37 || index == 40) //Single non-RF shots from Element
            {
                moveTotal.Text = "7";
                moveSharp.Text = "1.0";
                moveAvg.Text = "7";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if(index == 2 || index == 7 || index == 16 || index == 19 || index == 28 || index == 38) //RF Shots x3 (0.7x mod)
            {
                moveTotal.Text = "21";
                moveSharp.Text = "0.7";
                moveAvg.Text = "7";
                moveHitCount.Text = "3";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 3 || index == 8 || index == 17 || index == 20 || index == 29 || index == 39) //RF Shots x3 (0.7x mod) One Hit
            {
                moveTotal.Text = "7";
                moveSharp.Text = "0.7";
                moveAvg.Text = "7";
                moveHitCount.Text = "1";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if(index == 4) //RF Shots x4 (0.6x Mod)
            {
                moveTotal.Text = "28";
                moveSharp.Text = "0.6";
                moveAvg.Text = "7";
                moveHitCount.Text = "4";
                moveEleMod.Text = "0.6";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 5) //RF Shots x4 (0.6x Mod) One Hit
            {
                moveTotal.Text = "7";
                moveSharp.Text = "0.6";
                moveAvg.Text = "7";
                moveHitCount.Text = "1";
                moveEleMod.Text = "0.6";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 9 || index == 21 || index == 31 || index == 41) //Single non-RF shots from Element Pierce 1
            {
                moveTotal.Text = "6";
                moveSharp.Text = "1.0";
                moveAvg.Text = "2";
                moveHitCount.Text = "3";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 10 || index == 22 || index == 32 || index == 42) //Single non-RF shots from Element Pierce 1, One Hit
            {
                moveTotal.Text = "2";
                moveSharp.Text = "1.0";
                moveAvg.Text = "2";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 43) //RF Pierce Element Shots x3 (0.7x mod)
            {
                moveTotal.Text = "18";
                moveSharp.Text = "0.7";
                moveAvg.Text = "2";
                moveHitCount.Text = "9";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 44) //RF Pierce Element Shots x3 (0.7x mod) One Hit
            {
                moveTotal.Text = "2";
                moveSharp.Text = "0.7";
                moveAvg.Text = "2";
                moveHitCount.Text = "1";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 11 || index == 23 || index == 33 || index == 45) //Single non-RF shots from Element Pierce 2
            {
                moveTotal.Text = "15";
                moveSharp.Text = "1.0";
                moveAvg.Text = "3";
                moveHitCount.Text = "5";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 12 || index == 24 || index == 34 || index == 46) //Single non-RF shots from Element Pierce 2, One Hit
            {
                moveTotal.Text = "3";
                moveSharp.Text = "1.0";
                moveAvg.Text = "3";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 13 || index == 25 || index == 35 || index == 47) //RF Pierce Element 2 Shots x3 (0.7x mod)
            {
                moveTotal.Text = "45";
                moveSharp.Text = "0.7";
                moveAvg.Text = "3";
                moveHitCount.Text = "15";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 14 || index == 26 || index == 36 || index == 48) //RF Pierce Element 2 Shots x3 (0.7x mod) One Hit
            {
                moveTotal.Text = "3";
                moveSharp.Text = "0.7";
                moveAvg.Text = "3";
                moveHitCount.Text = "1";
                moveEleMod.Text = "0.7";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if(index == 49 || index == 53) //Single shot Dragon
            {
                moveTotal.Text = "5";
                moveSharp.Text = "1.0";
                moveAvg.Text = "1";
                moveHitCount.Text = "5";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 50 || index == 54) //Single shot Dragon One Hit
            {
                moveTotal.Text = "1";
                moveSharp.Text = "1.0";
                moveAvg.Text = "1";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 51 || index == 55) //RF Shots x2 Dragon (0.6x Mod)
            {
                moveTotal.Text = "10";
                moveSharp.Text = "0.6";
                moveAvg.Text = "1";
                moveHitCount.Text = "10";
                moveEleMod.Text = "0.6";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 52 || index == 56) //RF Shots x2 Dragon (0.6x Mod) One Hit
            {
                moveTotal.Text = "1";
                moveSharp.Text = "0.6";
                moveAvg.Text = "1";
                moveHitCount.Text = "1";
                moveEleMod.Text = "0.6";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 57 || index == 59 || index == 61 || index == 63) //Status Shots Lvl 1
            {
                moveTotal.Text = "10";
                moveSharp.Text = "1.0";
                moveAvg.Text = "10";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else if (index == 58 || index == 60 || index == 62 || index == 64) //Status Shots Lvl. 2
            {
                moveTotal.Text = "15";
                moveSharp.Text = "1.0";
                moveAvg.Text = "15";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
            else
            {
                moveTotal.Text = "0";
                moveSharp.Text = "1.0";
                moveAvg.Text = "0";
                moveHitCount.Text = "1";
                moveEleMod.Text = "1.0";
                moveKO.Text = "0";
                moveExh.Text = "0";
            }
        }

        private void findPower()
        {
            int index = eleShotType.SelectedIndex;
            if (index == 1 || index == 2 || index == 3 || index == 4 || index == 5 ||
                index == 15 || index == 16 || index == 17 ||
                index == 27 || index == 28 || index == 29 ||
                index == 37 || index == 38 || index == 39) //All Level 1 Element Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.45).ToString();
            }
            else if (index == 6 || index == 7 || index == 8 || index == 18 || index == 19 || index == 20 || index == 30 || index == 40) //All Level 2 Element Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.58).ToString();
            }
            else if (index == 9 || index == 10 || index == 21 || index == 22 || index == 31 ||
                index == 32 || index == 41 || index == 42 || index == 43 || index == 44) //All Level 1 Piercing Element Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.20).ToString();
            }
            else if (index == 11 || index == 12 || index == 13 || index == 14 || index == 23 || index == 24 || index == 25 || index == 26 ||
                index == 33 || index == 34 || index == 35 || index == 36 || index == 45 || index == 46 || index == 47 || index == 48) //All Level 2 Piercing Element Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.23).ToString();
            }
            else if (index == 49 || index == 50 || index == 51 || index == 52) //All Dragon Level 1 Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.40).ToString();
            }
            else if (index == 53 || index == 54 || index == 55 || index == 56) //All Dragon Level 2 Shots
            {
                weapAltPower.Text = (double.Parse(weapAttack.Text) * 0.48).ToString();
            }
            else if (index == 57 || index == 59 || index == 61 || index == 63) //Status Shots Lvl. 1
            {
                weapAltPower.Text = "25";
            }
            else if (index == 58 || index == 60 || index == 62 || index == 64) //Status Shots Lvl. 2
            {
                weapAltPower.Text = "50";
            }
            else
            {
                weapAltPower.Text = "0";
            }
        }

        /// <summary>
        /// Sets various stats based on the Elemental shot selected.
        /// </summary>
        private void setElementShot()
        {
            if (findElement() == "Fire")
            {
                weapAlt.SelectedIndex = 1;
                moveMinds.Checked = true;
            }
            else if(findElement() == "Water")
            {
                weapAlt.SelectedIndex = 2;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Thunder")
            {
                weapAlt.SelectedIndex = 3;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Ice")
            {
                weapAlt.SelectedIndex = 4;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Dragon")
            {
                weapAlt.SelectedIndex = 5;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Poison")
            {
                weapAlt.SelectedIndex = 6;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Para")
            {
                weapAlt.SelectedIndex = 7;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Sleep")
            {
                weapAlt.SelectedIndex = 8;
                moveMinds.Checked = true;
            }
            else if (findElement() == "Blast")
            {
                weapAlt.SelectedIndex = 9;
                moveMinds.Checked = true;
            }
            else
            {
                weapAlt.SelectedIndex = 0;
                moveMinds.Checked = false;
            }
        }

        private string findElement()
        {
            int index = eleShotType.SelectedIndex;
            if(index == 1 || index == 2 || index == 3 || index == 4 ||
                index == 5 || index == 6 || index == 7 || index == 8 ||
                index == 9 || index == 10 || index == 11 || index == 12 ||
                index == 13 || index == 14)
            {
                return "Fire";
            }
            else if(index == 15 || index == 16 || index == 17 || index == 18 ||
                index == 19 || index == 20 || index == 21 || index == 22 ||
                index == 23 || index == 24 || index == 25 || index == 26)
            {
                return "Water";
            }
            else if (index == 27 || index == 28 || index == 29 || index == 30 ||
                index == 31 || index == 32 || index == 33 || index == 34 ||
                index == 35 || index == 36)
            {
                return "Thunder";
            }
            else if (index == 37 || index == 38 || index == 39 || index == 40 ||
                index == 41 || index == 42 || index == 43 || index == 44 ||
                index == 45 || index == 46 || index == 47 || index == 48)
            {
                return "Ice";
            }
            else if (index == 49 || index == 50 || index == 51 || index == 52 ||
                index == 53 || index == 54 || index == 55 || index == 56)
            {
                return "Dragon";
            }
            else if (index == 57 || index == 58)
            {
                return "Poison";
            }
            else if (index == 59 || index == 60)
            {
                return "Sleep";
            }
            else if (index == 61 || index == 62)
            {
                return "Para";
            }
            else if (index == 63 || index == 64)
            {
                return "Blast";
            }
            else if (index == 0)
            {
                return "(None)";
            }
            else
            {
                return "Error";
            }
        }

#endif
    }
}
