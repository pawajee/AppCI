using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.
using OpenCvSharp;
namespace TxQR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ParseVideo();
        }
        void ParseVideo() {
    

            var videoFile = @"C:\temp\capture\barcode.webm";
            System.IO.Directory.CreateDirectory("frames");

            var capture = new VideoCapture(videoFile);
            var window = new Window("El Bruno - OpenCVSharp Save Video Frame by Frame");
            var image = new Mat();
            Debug.WriteLine($@"framerate: {capture.Fps}, ");
            var i = 0;
            while (capture.IsOpened())
            {
                
                capture.Read(image);
                if (image.Empty())
                    break;

                i++;
                var imgNumber = i.ToString().PadLeft(8, '0');

                var frameImageFileName = $@"frames\image{imgNumber}.png";
                Cv2.ImWrite(frameImageFileName, image);
              
                window.ShowImage(image);
                if (Cv2.WaitKey(1) == 113) // Q
                    break;
            }

            Console.WriteLine("Complete !");
        }
    }
}
/////////////////////////designer////////////////////////

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
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

