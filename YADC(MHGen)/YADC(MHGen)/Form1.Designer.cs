namespace YADC_MHGen_
{
    partial class Form1
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
            this.OutputField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.WeaponLabel = new System.Windows.Forms.Label();
            this.TypeSelect = new System.Windows.Forms.ComboBox();
            this.WeaponSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPreventer)).BeginInit();
            this.SuspendLayout();
            // 
            // RawField
            // 
            this.RawField.Location = new System.Drawing.Point(12, 358);
            this.RawField.MaxLength = 3;
            this.RawField.Name = "RawField";
            this.RawField.Size = new System.Drawing.Size(100, 20);
            this.RawField.TabIndex = 0;
            this.RawField.Text = "0";
            this.RawField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RawField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RawField_KeyPress);
            this.RawField.Validating += new System.ComponentModel.CancelEventHandler(this.RawField_Validating);
            this.RawField.Validated += new System.EventHandler(this.RawField_Validated);
            // 
            // ErrorPreventer
            // 
            this.ErrorPreventer.ContainerControl = this;
            // 
            // RawLabel
            // 
            this.RawLabel.AutoSize = true;
            this.RawLabel.Location = new System.Drawing.Point(16, 342);
            this.RawLabel.Name = "RawLabel";
            this.RawLabel.Size = new System.Drawing.Size(99, 13);
            this.RawLabel.TabIndex = 1;
            this.RawLabel.Text = "Raw Attack Power:";
            // 
            // MVField
            // 
            this.MVField.Location = new System.Drawing.Point(143, 358);
            this.MVField.Name = "MVField";
            this.MVField.Size = new System.Drawing.Size(100, 20);
            this.MVField.TabIndex = 2;
            this.MVField.Text = "0";
            this.MVField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MVField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MVField_KeyPress);
            this.MVField.Validating += new System.ComponentModel.CancelEventHandler(this.MotionField_Validating);
            this.MVField.Validated += new System.EventHandler(this.RawField_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Motion Value:";
            // 
            // OutputField
            // 
            this.OutputField.Location = new System.Drawing.Point(632, 358);
            this.OutputField.Name = "OutputField";
            this.OutputField.Size = new System.Drawing.Size(100, 20);
            this.OutputField.TabIndex = 4;
            this.OutputField.Text = "0";
            this.OutputField.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(638, 342);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 11;
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(111, 105);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 13;
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(111, 142);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 15;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 390);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WeaponSelect);
            this.Controls.Add(this.TypeSelect);
            this.Controls.Add(this.WeaponLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OutputField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MVField);
            this.Controls.Add(this.RawLabel);
            this.Controls.Add(this.RawField);
            this.Name = "Form1";
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
        private System.Windows.Forms.TextBox OutputField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox WeaponSelect;
        private System.Windows.Forms.ComboBox TypeSelect;
        private System.Windows.Forms.Label WeaponLabel;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
    }
}

