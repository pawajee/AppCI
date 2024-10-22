
namespace QRCodeGen
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsLoaded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnFetchFileList = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lblCurrentFileName = new System.Windows.Forms.Label();
            this.txtChunkSize = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(61, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(813, 20);
            this.txtFileName.TabIndex = 0;
            this.txtFileName.Text = "C:\\temp\\SpecialNeeds\\BAA\\compressed\\BAA.z01";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(90, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 560);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(707, 38);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileName,
            this.IsLoaded});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(707, 67);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(359, 699);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // FileName
            // 
            this.FileName.Text = "File";
            this.FileName.Width = 290;
            // 
            // IsLoaded
            // 
            this.IsLoaded.Text = "Is Loaded";
            this.IsLoaded.Width = 70;
            // 
            // btnFetchFileList
            // 
            this.btnFetchFileList.Location = new System.Drawing.Point(90, 38);
            this.btnFetchFileList.Name = "btnFetchFileList";
            this.btnFetchFileList.Size = new System.Drawing.Size(92, 23);
            this.btnFetchFileList.TabIndex = 4;
            this.btnFetchFileList.Text = "Fetch File List";
            this.btnFetchFileList.UseVisualStyleBackColor = true;
            this.btnFetchFileList.Click += new System.EventHandler(this.btnFetchFileList_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(788, 38);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(247, 39);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(46, 20);
            this.txtInterval.TabIndex = 6;
            this.txtInterval.Text = "200";
            // 
            // lblCurrentFileName
            // 
            this.lblCurrentFileName.AutoSize = true;
            this.lblCurrentFileName.Location = new System.Drawing.Point(299, 43);
            this.lblCurrentFileName.Name = "lblCurrentFileName";
            this.lblCurrentFileName.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentFileName.TabIndex = 7;
            this.lblCurrentFileName.Text = "label1";
            // 
            // txtChunkSize
            // 
            this.txtChunkSize.Location = new System.Drawing.Point(197, 39);
            this.txtChunkSize.Name = "txtChunkSize";
            this.txtChunkSize.Size = new System.Drawing.Size(44, 20);
            this.txtChunkSize.TabIndex = 8;
            this.txtChunkSize.Text = "2800";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 778);
            this.Controls.Add(this.txtChunkSize);
            this.Controls.Add(this.lblCurrentFileName);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnFetchFileList);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFileName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnFetchFileList;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader IsLoaded;
        private System.Windows.Forms.Label lblCurrentFileName;
        private System.Windows.Forms.TextBox txtChunkSize;
    }
}

