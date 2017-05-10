namespace YADC_MHGen_
{
    partial class DmgCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RawField = new System.Windows.Forms.TextBox();
            this.ErrorPreventer = new System.Windows.Forms.ErrorProvider(this.components);
            this.RawLabel = new System.Windows.Forms.Label();
            this.MVField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.WeaponLabel = new System.Windows.Forms.Label();
            this.TypeSelect = new System.Windows.Forms.ComboBox();
            this.WeaponSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WeapRawField = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.WeapAffinityField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SharpnessField = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AffinityField = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SharpnessLabel = new System.Windows.Forms.Label();
            this.SharpField = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.EleField = new System.Windows.Forms.TextBox();
            this.HiddenField = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.HitzoneField = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.EleZoneField = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.OutLabel = new System.Windows.Forms.Label();
            this.QuestField = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.FinalVal = new System.Windows.Forms.Label();
            this.CalcButt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPreventer)).BeginInit();
            this.SuspendLayout();
            // 
            // RawField
            // 
            this.RawField.Location = new System.Drawing.Point(12, 314);
            this.RawField.MaxLength = 3;
            this.RawField.Name = "RawField";
            this.RawField.Size = new System.Drawing.Size(96, 20);
            this.RawField.TabIndex = 0;
            this.RawField.Text = "0";
            this.RawField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RawField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.RawField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // ErrorPreventer
            // 
            this.ErrorPreventer.ContainerControl = this;
            // 
            // RawLabel
            // 
            this.RawLabel.AutoSize = true;
            this.RawLabel.Location = new System.Drawing.Point(9, 298);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(99, 13);
            this.RawLabel.TabIndex = 1;
            this.RawLabel.Text = "Raw Attack Power:";
            // 
            // MVField
            // 
            this.MVField.Location = new System.Drawing.Point(114, 314);
            this.MVField.Name = "MVField";
            this.MVField.Size = new System.Drawing.Size(96, 20);
            this.MVField.TabIndex = 2;
            this.MVField.Text = "0";
            this.MVField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MVField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.MVField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Motion Value:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Damage Output:";
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(13, 13);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(78, 13);
            this.TypeLabel.TabIndex = 6;
            this.TypeLabel.Text = "Weapon Type:";
            // 
            // WeaponLabel
            // 
            this.WeaponLabel.AutoSize = true;
            this.WeaponLabel.Location = new System.Drawing.Point(13, 45);
            this.WeaponLabel.Name = "WeaponLabel";
            this.WeaponLabel.Size = new System.Drawing.Size(51, 13);
            this.WeaponLabel.TabIndex = 7;
            this.WeaponLabel.Text = "Weapon:";
            // 
            // TypeSelect
            // 
            this.TypeSelect.FormattingEnabled = true;
            this.TypeSelect.Location = new System.Drawing.Point(111, 11);
            this.TypeSelect.Name = "TypeSelect";
            this.TypeSelect.Size = new System.Drawing.Size(121, 21);
            this.TypeSelect.TabIndex = 8;
            // 
            // WeaponSelect
            // 
            this.WeaponSelect.FormattingEnabled = true;
            this.WeaponSelect.Location = new System.Drawing.Point(111, 42);
            this.WeaponSelect.Name = "WeaponSelect";
            this.WeaponSelect.Size = new System.Drawing.Size(121, 21);
            this.WeaponSelect.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Weapon Raw:";
            // 
            // WeapRawField
            // 
            this.WeapRawField.Location = new System.Drawing.Point(111, 73);
            this.WeapRawField.Name = "WeapRawField";
            this.WeapRawField.Size = new System.Drawing.Size(121, 20);
            this.WeapRawField.TabIndex = 11;
            this.WeapRawField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Raw Affinity:";
            // 
            // WeapAffinityField
            // 
            this.WeapAffinityField.Location = new System.Drawing.Point(111, 105);
            this.WeapAffinityField.Name = "WeapAffinityField";
            this.WeapAffinityField.Size = new System.Drawing.Size(121, 20);
            this.WeapAffinityField.TabIndex = 13;
            this.WeapAffinityField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Sharpness:";
            // 
            // SharpnessField
            // 
            this.SharpnessField.FormattingEnabled = true;
            this.SharpnessField.Location = new System.Drawing.Point(111, 142);
            this.SharpnessField.Name = "SharpnessField";
            this.SharpnessField.Size = new System.Drawing.Size(121, 21);
            this.SharpnessField.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(238, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Affinity:";
            // 
            // AffinityField
            // 
            this.AffinityField.Location = new System.Drawing.Point(216, 314);
            this.AffinityField.Name = "AffinityField";
            this.AffinityField.Size = new System.Drawing.Size(96, 20);
            this.AffinityField.TabIndex = 18;
            this.AffinityField.Text = "0";
            this.AffinityField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AffinityField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.AffinityField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(318, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "%";
            // 
            // SharpnessLabel
            // 
            this.SharpnessLabel.AutoSize = true;
            this.SharpnessLabel.Location = new System.Drawing.Point(111, 342);
            this.SharpnessLabel.Name = "SharpnessLabel";
            this.SharpnessLabel.Size = new System.Drawing.Size(84, 13);
            this.SharpnessLabel.TabIndex = 20;
            this.SharpnessLabel.Text = "Sharpness Mod:";
            // 
            // SharpField
            // 
            this.SharpField.Location = new System.Drawing.Point(114, 358);
            this.SharpField.Name = "SharpField";
            this.SharpField.Size = new System.Drawing.Size(96, 20);
            this.SharpField.TabIndex = 21;
            this.SharpField.Text = "1.0";
            this.SharpField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SharpField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.SharpField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 342);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Ele. Attack Power:";
            // 
            // EleField
            // 
            this.EleField.Location = new System.Drawing.Point(12, 358);
            this.EleField.Name = "EleField";
            this.EleField.Size = new System.Drawing.Size(96, 20);
            this.EleField.TabIndex = 23;
            this.EleField.Text = "0";
            this.EleField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EleField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.EleField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // HiddenField
            // 
            this.HiddenField.Location = new System.Drawing.Point(216, 358);
            this.HiddenField.Name = "HiddenField";
            this.HiddenField.Size = new System.Drawing.Size(96, 20);
            this.HiddenField.TabIndex = 24;
            this.HiddenField.Text = "1.0";
            this.HiddenField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HiddenField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.HiddenField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(213, 342);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Hidden Mods: (Total)";
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(453, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(2, 372);
            this.label11.TabIndex = 26;
            // 
            // HitzoneField
            // 
            this.HitzoneField.Location = new System.Drawing.Point(471, 318);
            this.HitzoneField.Name = "HitzoneField";
            this.HitzoneField.Size = new System.Drawing.Size(96, 20);
            this.HitzoneField.TabIndex = 27;
            this.HitzoneField.Text = "0";
            this.HitzoneField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HitzoneField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.HitzoneField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(468, 302);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Hitzone Value:";
            // 
            // EleZoneField
            // 
            this.EleZoneField.Location = new System.Drawing.Point(471, 358);
            this.EleZoneField.Name = "EleZoneField";
            this.EleZoneField.Size = new System.Drawing.Size(96, 20);
            this.EleZoneField.TabIndex = 29;
            this.EleZoneField.Text = "0";
            this.EleZoneField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EleZoneField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.EleZoneField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(471, 341);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Ele. Hitzone Value:";
            // 
            // OutLabel
            // 
            this.OutLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OutLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OutLabel.Location = new System.Drawing.Point(337, 357);
            this.OutLabel.Name = "OutLabel";
            this.OutLabel.Size = new System.Drawing.Size(96, 20);
            this.OutLabel.TabIndex = 31;
            this.OutLabel.Text = "0";
            this.OutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // QuestField
            // 
            this.QuestField.Location = new System.Drawing.Point(573, 358);
            this.QuestField.Name = "QuestField";
            this.QuestField.Size = new System.Drawing.Size(96, 20);
            this.QuestField.TabIndex = 32;
            this.QuestField.Text = "0";
            this.QuestField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QuestField.Validating += new System.ComponentModel.CancelEventHandler(this.GenericField_Validating);
            this.QuestField.Validated += new System.EventHandler(this.GenericField_Validated);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(570, 342);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "Quest Mod:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(688, 341);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Final Damage:";
            // 
            // FinalVal
            // 
            this.FinalVal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FinalVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FinalVal.Location = new System.Drawing.Point(691, 357);
            this.FinalVal.Name = "FinalVal";
            this.FinalVal.Size = new System.Drawing.Size(96, 20);
            this.FinalVal.TabIndex = 35;
            this.FinalVal.Text = "0";
            this.FinalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CalcButt
            // 
            this.CalcButt.Location = new System.Drawing.Point(337, 314);
            this.CalcButt.Name = "CalcButt";
            this.CalcButt.Size = new System.Drawing.Size(96, 23);
            this.CalcButt.TabIndex = 36;
            this.CalcButt.Text = "Calculate Output";
            this.CalcButt.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(691, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "Calculate All";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DmgCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 390);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CalcButt);
            this.Controls.Add(this.FinalVal);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.QuestField);
            this.Controls.Add(this.OutLabel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.EleZoneField);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.HitzoneField);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.HiddenField);
            this.Controls.Add(this.EleField);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SharpField);
            this.Controls.Add(this.SharpnessLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AffinityField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SharpnessField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WeapAffinityField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.WeapRawField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WeaponSelect);
            this.Controls.Add(this.TypeSelect);
            this.Controls.Add(this.WeaponLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MVField);
            this.Controls.Add(this.RawLabel);
            this.Controls.Add(this.RawField);
            this.Name = "DmgCalculator";
            this.Text = "Yet Another MH Damage Calculator (MHGen) (Beta vers.)";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPreventer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RawField;
        private System.Windows.Forms.ErrorProvider ErrorPreventer;
        private System.Windows.Forms.Label RawLabel;
        private System.Windows.Forms.TextBox MVField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WeapAffinityField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox WeapRawField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox WeaponSelect;
        private System.Windows.Forms.ComboBox TypeSelect;
        private System.Windows.Forms.Label WeaponLabel;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox SharpnessField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox AffinityField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SharpField;
        private System.Windows.Forms.Label SharpnessLabel;
        private System.Windows.Forms.TextBox EleField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox HiddenField;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox HitzoneField;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label OutLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox EleZoneField;
        private System.Windows.Forms.TextBox QuestField;
        private System.Windows.Forms.Label FinalVal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button CalcButt;
    }
}

