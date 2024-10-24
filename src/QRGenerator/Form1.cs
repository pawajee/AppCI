using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using System.IO;
namespace QRCodeGen
{
    public partial class Form1 : Form
    {
        OpenFileDialog fileDialog;
        MyUserSettings mus = new MyUserSettings();
        int chunkSize=2800;
        int QrCodeWidth = 550;
        int QrCodeHeight = 550;
        private Timer showTimer= new Timer();
        private int nextQrCodeInterval = 500;
        Dictionary<string, Bitmap[]> imgData = new Dictionary<string, Bitmap[]>();
        string currentFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // GenerateQrCodes(txtFileName.Text);

            this.Show();
            listView1.View = View.Details;
        //    ShowQrCode(GenerateQrCodes(txtFileName.Text));
            txtInterval.Text = mus.txtInterval;
            txtFileName.Text = mus.txtFileName;
            txtChunkSize.Text = mus.txtChunkSize;
        }
        //async void LoadNextFiles(string fileName)
        //{
        //    var imgDataTask = new Dictionary<string, Task<Tuple<(Bitmap[], string)>>>();
        //    var arrTasks = new List<Task<Tuple<(Bitmap[], string)>>>();

        //    foreach (ListViewItem itemRow in this.listView1.Items)
        //    {

        //        //for (int i = 0; i < itemRow.SubItems.Count; i++)
        //        //{
        //        //return itemRow.SubItems[i])
        //        var filePath = itemRow.SubItems[0].Text;
        //        var qrCodeTask = GenerateQrCodes(filePath);
        //        arrTasks.Add(qrCodeTask);
        //        imgDataTask.Add(Path.GetFileName(filePath), qrCodeTask);
        //        // }
        //    }
        //    while (arrTasks.Any())
        //    {
        //        Task<Tuple<(Bitmap[], string)>> finishedTask = await Task.WhenAny(arrTasks);
        //        arrTasks.Remove(finishedTask);
        //        var res = await finishedTask;
        //        var image = res.Item1.Item1;
        //        var fileName = res.Item1.Item2;
        //        imgData.Add(fileName, image);
        //        setFileLoadStatus(fileName);
        //        //  total += await finishedTask;

        //    }
        //    //   await Task.WhenAll(arrTasks);

        //}
        async void LoadFiles()
        {
            var imgDataTask = new Dictionary<string, Task<Tuple<(Bitmap[], string)>>>();
            var arrTasks = new List<Task<Tuple<(Bitmap[],string)>>>();
            
            foreach (ListViewItem itemRow in this.listView1.Items)
            {

                //for (int i = 0; i < itemRow.SubItems.Count; i++)
                //{
                    //return itemRow.SubItems[i])
                  var filePath=  itemRow.SubItems[0].Text;
                    var qrCodeTask = GenerateQrCodes(filePath);
                    arrTasks.Add(qrCodeTask);
                    imgDataTask.Add(Path.GetFileName(filePath), qrCodeTask);
               // }
            }
            while (arrTasks.Any())
            {
                Task<Tuple<(Bitmap[], string)>> finishedTask = await Task.WhenAny(arrTasks);
                arrTasks.Remove(finishedTask);
                var res = await finishedTask;
                var image = res.Item1.Item1;
                var fileName = res.Item1.Item2;
                imgData.Add(fileName, image);
                setFileLoadStatus(fileName);
              //  total += await finishedTask;

            }
         //   await Task.WhenAll(arrTasks);

        }
        void setFileLoadStatus(string fileName) {
            foreach (ListViewItem itemRow in this.listView1.Items)
            {

                var filePath = itemRow.SubItems[0].Text;
                if (Path.GetFileName(filePath) == fileName)
                {
                    itemRow.SubItems[1].Text = "Loaded";
                  //  itemRow.SubItems[i]
                }
            }
        }
        private void ShowQrCode(Bitmap[] bitmaps)
        {
            var NoOfQRCodes = bitmaps.Length;
            var counter=0;
            showTimer.Tick += (s,e)=> {
                pictureBox1.Image=   bitmaps[counter++];
                if (counter == NoOfQRCodes) {
                    counter = 0;
                }
            };
            showTimer.Interval =nextQrCodeInterval;
            showTimer.Start();
        }

        async Task<Tuple<(Bitmap[], string)>>  GenerateQrCodes(string filePath)
        {
            string fileBase64;
            int fileLength;
            chunkSize = 2800;
            int.TryParse(txtChunkSize.Text,out chunkSize);
            return await Task.Run(() =>
             {
                 if (File.Exists(filePath))
                 {
                     fileBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
                     fileLength = fileBase64.Length;
                     var noOfChunks = Math.Floor((decimal)(fileLength / chunkSize));

                     var chunks = Enumerable.Range(0, fileBase64.Length / chunkSize)
                                .Select(x => fileBase64.Substring(x * chunkSize, chunkSize));

                    //for (var i = 0; i < noOfChunks; i++) { 

                    //}
                    var arrQrCodes = new Bitmap[(int)noOfChunks + 1];
                     int i = 0;
                     var checkSum = getCheckSum(fileBase64);
                     foreach (var chunk in chunks)
                     {
                         if (i == 0)
                         {
                             var data = i.ToString() + '/' + ((int)noOfChunks - 1) + '/' + Path.GetFileName(filePath) + '/' + checkSum + '$';
                             arrQrCodes[i++] = GenerateQRCode(data, QrCodeWidth, QrCodeHeight, Path.GetFileName(filePath));
                         }
                         else
                         {
                             var chunkPart = i.ToString() + '/' + Path.GetFileName(filePath) + '/' + getCheckSum(chunk) + '$' + chunk;
                             arrQrCodes[i++] = GenerateQRCode(chunkPart, QrCodeWidth, QrCodeHeight, Path.GetFileName(filePath));
                         }
                        // Application.DoEvents();
                    }
                     var tuple = new Tuple<(Bitmap[], string)>((arrQrCodes, Path.GetFileName(filePath)));
                    // var tsk = new Task<Tuple<(Bitmap[], string)>>();
                     return new Tuple<(Bitmap[], string)>((arrQrCodes, Path.GetFileName(filePath))); 
                 }
                 return null;
             });

        }

        private object getCheckSum(string fileBase64)
        {
            int hash = 0;
            if (fileBase64.Length == 0)
            {
                return hash;
            }
            for (int i = 0; i < fileBase64.Length; i++)
            {
                int chr = (int)fileBase64[i];
                hash += chr % 123;
            }
            return hash;
        }

        private Bitmap GenerateQRCode(string inputText, int width, int height,string fileName)
        {
            Bitmap qrImage = null;
            try
            {
                //   qrImage
               
                var barcodeWriter = new ZXing.BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        PureBarcode = true,
                       // Margin=5,
                       // NoPadding=true
                    }
                };

                qrImage = barcodeWriter.Write(inputText);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Exception occurred while processing the file {fileName}");
            }
            return qrImage;
        }

        private void btnFetchFileList_Click(object sender, EventArgs e)
        {
            btnFetchFileList.Enabled = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFileName.Text))
                {
                    string fpath = Path.GetDirectoryName(txtFileName.Text), fName = Path.GetFileName(txtFileName.Text);

                    string[] files = Directory.GetFiles(fpath, fName, SearchOption.TopDirectoryOnly);
                    listView1.Items.Clear();
                    foreach (var cfile in files)
                    {
                        //byte[] bytes = File.ReadAllBytes(Path.Combine(fpath, cfile));
                        //string file = "!@#" + Convert.ToBase64String(bytes);
                        //File.WriteAllText(Path.Combine(fpath, cfile) + ".base64.txt", file);
                        string[] row = { cfile, "Not Loaded" };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                    }

                }
                loadFiles();
                //foreach (ListViewItem itemRow in this.listView1.Items)
                //{

                //    var filePath = itemRow.SubItems[0].Text;
                //    var qrCodeTask = GenerateQrCodes(filePath);
                //    arrTasks.Add(qrCodeTask);
                //    imgDataTask.Add(Path.GetFileName(filePath), qrCodeTask);
                //    // }
                //}
            }
            catch (Exception ex)
            { 
            
            }

        }
        async void  loadFiles() {
            Bitmap[] imag1;
            Bitmap[] imag2;
            Bitmap[] imag3;
            List<Task<Tuple<(Bitmap[], string)>>> arrTasks = new List<Task<Tuple<(Bitmap[], string)>>>();
            for (var i = 0; i < listView1.Items.Count; i++)
            {


                var filePath = listView1.Items[i].SubItems[0].Text;
                if (i == 0)
                {
                    arrTasks.Add( GenerateQrCodes(filePath));
                }


                if ((i + 1) < listView1.Items.Count)
                {
                    filePath = listView1.Items[i+1].SubItems[0].Text;
                    arrTasks.Add( GenerateQrCodes(filePath));
                }
                //if ((i + 2) < listView1.Items.Count)
                //{
                //    arrTasks[2] = GenerateQrCodes(filePath);
                //}
                var res=await arrTasks[0];
                //var res = await finishedTask;
                var bitmaps = res.Item1.Item1;
                var fileName = res.Item1.Item2;
             //   imgData.Add(fileName, image);
                setFileLoadStatus(fileName);
                lblCurrentFileName.Text = fileName;
                for (var iLoop = 0; iLoop < bitmaps.Length; iLoop++)
                {
                    var intvl = txtInterval.Text;
                    if (string.IsNullOrWhiteSpace(intvl)) { intvl = "0.1"; };

                    await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(intvl)));
                    pictureBox1.Image = bitmaps[iLoop];
                   // bitmaps[iLoop].Dispose();
                }
                //arrTasks[0] = arrTasks[1];

                arrTasks.Remove(arrTasks[0]);
                //var NoOfQRCodes = bitmaps.Length;
                //var counter = 0;
                //showTimer.Tick += (s, e) => {
                //    pictureBox1.Image = bitmaps[counter++];
                //    if (counter == NoOfQRCodes)
                //    {
                //        counter = 0;
                //    }
                //};
            }
            btnFetchFileList.Enabled = true;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           mus.txtInterval = txtInterval.Text;
           mus.txtFileName = txtFileName.Text;
            mus.txtChunkSize = txtChunkSize.Text;
            mus.Save();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }
    }
}
