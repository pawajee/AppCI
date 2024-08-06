
namespace BASE64
{
    partial class frmBase64Conversion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtConvert = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.txtReverse = new System.Windows.Forms.TextBox();
            this.btnShowFileDialogFromBase64 = new System.Windows.Forms.Button();
            this.btnShowFileDialogToBase64 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtConvert
            // 
            this.txtConvert.Location = new System.Drawing.Point(99, 29);
            this.txtConvert.Name = "txtConvert";
            this.txtConvert.Size = new System.Drawing.Size(551, 23);
            this.txtConvert.TabIndex = 0;
            this.txtConvert.TextChanged += new System.EventHandler(this.txtConvert_TextChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(701, 28);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(106, 23);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "To Base64";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(701, 70);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(106, 23);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "From Base 64";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // txtReverse
            // 
            this.txtReverse.Location = new System.Drawing.Point(99, 71);
            this.txtReverse.Name = "txtReverse";
            this.txtReverse.Size = new System.Drawing.Size(551, 23);
            this.txtReverse.TabIndex = 2;
            this.txtReverse.TextChanged += new System.EventHandler(this.txtReverse_TextChanged);
            // 
            // btnShowFileDialogFromBase64
            // 
            this.btnShowFileDialogFromBase64.Location = new System.Drawing.Point(661, 70);
            this.btnShowFileDialogFromBase64.Name = "btnShowFileDialogFromBase64";
            this.btnShowFileDialogFromBase64.Size = new System.Drawing.Size(36, 23);
            this.btnShowFileDialogFromBase64.TabIndex = 4;
            this.btnShowFileDialogFromBase64.Text = "...";
            this.btnShowFileDialogFromBase64.UseVisualStyleBackColor = true;
            this.btnShowFileDialogFromBase64.Click += new System.EventHandler(this.btnShowFileDialogFromBase64_Click);
            // 
            // btnShowFileDialogToBase64
            // 
            this.btnShowFileDialogToBase64.Location = new System.Drawing.Point(661, 29);
            this.btnShowFileDialogToBase64.Name = "btnShowFileDialogToBase64";
            this.btnShowFileDialogToBase64.Size = new System.Drawing.Size(36, 23);
            this.btnShowFileDialogToBase64.TabIndex = 5;
            this.btnShowFileDialogToBase64.Text = "...";
            this.btnShowFileDialogToBase64.UseVisualStyleBackColor = true;
            this.btnShowFileDialogToBase64.Click += new System.EventHandler(this.btnShowFileDialogToBase64_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "File To Base64";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "File To Reverse";
            // 
            // frmBase64Conversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 125);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowFileDialogToBase64);
            this.Controls.Add(this.btnShowFileDialogFromBase64);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.txtReverse);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtConvert);
            this.Name = "frmBase64Conversion";
            this.Text = "Base64 Conversion/Reversal Tool";
            this.Load += new System.EventHandler(this.frmBase64Conversion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConvert;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.TextBox txtReverse;
        private System.Windows.Forms.Button btnShowFileDialogFromBase64;
        private System.Windows.Forms.Button btnShowFileDialogToBase64;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
