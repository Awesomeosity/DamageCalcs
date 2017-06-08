namespace YADC_MHGen_
{
    partial class Form2
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label72 = new System.Windows.Forms.Label();
            this.monQuest = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.monHitzone = new System.Windows.Forms.ComboBox();
            this.label78 = new System.Windows.Forms.Label();
            this.monName = new System.Windows.Forms.ComboBox();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label72);
            this.groupBox10.Controls.Add(this.monQuest);
            this.groupBox10.Controls.Add(this.label77);
            this.groupBox10.Controls.Add(this.monHitzone);
            this.groupBox10.Controls.Add(this.label78);
            this.groupBox10.Controls.Add(this.monName);
            this.groupBox10.Location = new System.Drawing.Point(12, 12);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(222, 97);
            this.groupBox10.TabIndex = 136;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Monster Search";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(5, 75);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(69, 13);
            this.label72.TabIndex = 134;
            this.label72.Text = "Quest Name:";
            // 
            // monQuest
            // 
            this.monQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monQuest.FormattingEnabled = true;
            this.monQuest.Location = new System.Drawing.Point(88, 69);
            this.monQuest.Name = "monQuest";
            this.monQuest.Size = new System.Drawing.Size(129, 21);
            this.monQuest.TabIndex = 133;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(5, 48);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(77, 13);
            this.label77.TabIndex = 3;
            this.label77.Text = "Hitzone Name:";
            // 
            // monHitzone
            // 
            this.monHitzone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monHitzone.FormattingEnabled = true;
            this.monHitzone.Location = new System.Drawing.Point(88, 42);
            this.monHitzone.Name = "monHitzone";
            this.monHitzone.Size = new System.Drawing.Size(129, 21);
            this.monHitzone.TabIndex = 2;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(5, 20);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(79, 13);
            this.label78.TabIndex = 1;
            this.label78.Text = "Monster Name:";
            // 
            // monName
            // 
            this.monName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monName.FormattingEnabled = true;
            this.monName.Location = new System.Drawing.Point(88, 14);
            this.monName.Name = "monName";
            this.monName.Size = new System.Drawing.Size(129, 21);
            this.monName.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 390);
            this.Controls.Add(this.groupBox10);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.ComboBox monQuest;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.ComboBox monHitzone;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox monName;
    }
}