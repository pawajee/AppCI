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
using System.IO;
//using System.
using OpenCvSharp;
namespace TxQR
{
    public partial class Form1 : Form
    {
        OpenFileDialog fileDialog=new OpenFileDialog();
        MyUserSettings mus= new MyUserSettings();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFileName.Text = mus.txtFileName;
            txtRectangle.Text = mus.txtRectangle;
            txtDestination.Text = mus.txtDestination;
        }
        void ParseVideo(string videoFile) {
    

           // var videoFile = @"C:\temp\capture\barcode.webm";
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
                
                var frameImageFileName = $@"{txtDestination.Text.Trim()}\image{imgNumber}.png";
                //18,173: 377,534
                //;
                Directory.CreateDirectory(txtDestination.Text);
                if (!string.IsNullOrWhiteSpace(txtRectangle.Text.Trim()))
                {
                    var arrRec=txtRectangle.Text.Trim().Split(',');
                    Rect rect;
                    if (getRect(arrRec, out rect))
                    {
                        image = image[rect];
                    }
                }
                Cv2.ImWrite(frameImageFileName, image);
               
                window.ShowImage(image);
                if (Cv2.WaitKey(1) == 113) // Q
                    break;
            }

            Console.WriteLine("Complete !");
        }
        bool getRect(string[] arrRect,out Rect rect) {
            int L,T,R,B ;
            if (
                int.TryParse(arrRect[0].Trim(), out L) &&
                int.TryParse(arrRect[1].Trim(), out T) &&
                int.TryParse(arrRect[2].Trim(), out R) &&
                int.TryParse(arrRect[3].Trim(), out B)
               )
            {
                rect = Rect.FromLTRB(L, T, R, B);
                return true;
            }
            else
            {
                rect=Rect.FromLTRB(0,0,0,0);
                return false;
            }
        }
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            txtFileName.Text = GetFile() ?? txtFileName.Text;
        }
        private string GetFile()
        {
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return fileDialog.FileName;
        }
        private void txtStartProcessing_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text.Trim()!="") {
                ParseVideo(txtFileName.Text.Trim());
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mus.txtFileName= txtFileName.Text;
            mus.txtRectangle= txtRectangle.Text;
             mus.txtDestination = txtDestination.Text;
            mus.Save();
        }
    }
}
