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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(632, 358);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 390);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
    }
}

