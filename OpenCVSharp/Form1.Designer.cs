
namespace TxQR
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
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtRectangle = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtStartProcessing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(61, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(665, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(728, 10);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtRectangle
            // 
            this.txtRectangle.Location = new System.Drawing.Point(61, 64);
            this.txtRectangle.Name = "txtRectangle";
            this.txtRectangle.Size = new System.Drawing.Size(108, 20);
            this.txtRectangle.TabIndex = 2;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(61, 38);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(665, 20);
            this.txtDestination.TabIndex = 3;
            // 
            // txtStartProcessing
            // 
            this.txtStartProcessing.Location = new System.Drawing.Point(175, 64);
            this.txtStartProcessing.Name = "txtStartProcessing";
            this.txtStartProcessing.Size = new System.Drawing.Size(139, 23);
            this.txtStartProcessing.TabIndex = 4;
            this.txtStartProcessing.Text = "Start";
            this.txtStartProcessing.UseVisualStyleBackColor = true;
            this.txtStartProcessing.Click += new System.EventHandler(this.txtStartProcessing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 113);
            this.Controls.Add(this.txtStartProcessing);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtRectangle);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFileName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtRectangle;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button txtStartProcessing;
    }
}

