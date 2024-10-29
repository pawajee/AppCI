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
using System.Threading;

namespace QRCodeGen
{
    public partial class Form1 : Form
    {
        OpenFileDialog fileDialog;
        MyUserSettings mus = new MyUserSettings();
        int chunkSize=2800;
        int QrCodeWidth = 550;
        int QrCodeHeight = 550;
      //  private Timer showTimer= new Timer();
        private int nextQrCodeInterval = 500;
        Dictionary<string, Bitmap[]> imgData = new Dictionary<string, Bitmap[]>();
        string currentFile;
        bool stopGeneratingQrCode = true;
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

            txtMissingCodeFilesPath.Text = mus.txtMissingCodeFilesPath;
            txtMissingCodesListFile.Text = mus.txtMissingCodesListFile;
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
        //async void LoadFiles()
        //{
        //    var imgDataTask = new Dictionary<string, Task<Tuple<(Bitmap[], string)>>>();
        //    var arrTasks = new List<Task<Tuple<(Bitmap[],string)>>>();
            
        //    foreach (ListViewItem itemRow in this.listView1.Items)
        //    {

        //        //for (int i = 0; i < itemRow.SubItems.Count; i++)
        //        //{
        //            //return itemRow.SubItems[i])
        //          var filePath=  itemRow.SubItems[0].Text;
        //            var qrCodeTask = GenerateQrCodes(filePath);
        //            arrTasks.Add(qrCodeTask);
        //            imgDataTask.Add(Path.GetFileName(filePath), qrCodeTask);
        //       // }
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
        //      //  total += await finishedTask;

        //    }
        // //   await Task.WhenAll(arrTasks);

        //}
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
        //private void ShowQrCode(Bitmap[] bitmaps)
        //{
        //    var NoOfQRCodes = bitmaps.Length;
        //    var counter=0;
        //    showTimer.Tick += (s,e)=> {
        //        pictureBox1.Image=   bitmaps[counter++];
        //        if (counter == NoOfQRCodes) {
        //            counter = 0;
        //        }
        //    };
        //    showTimer.Interval =nextQrCodeInterval;
        //    showTimer.Start();
        //}

        async Task<Tuple<(Bitmap[], string)>>  GenerateQrCodes(string filePath, CancellationToken ct)
        {
            string fileBase64;
            int fileLength;
            chunkSize = 2770;
            int.TryParse(txtChunkSize.Text,out chunkSize);
            return await Task.Run(() =>
             {
                 if (File.Exists(filePath) && Path.GetExtension( filePath).ToLower() !=".base64")
                 {
                     fileBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
                    //File.WriteAllText(filePath + ".base64",fileBase64);
                     fileLength = fileBase64.Length;
                     var noOfChunks =(int) Math.Floor((decimal)(fileLength / chunkSize))+1;

                     var chunks = Enumerable.Range(0, noOfChunks)
                                .Select((x) =>
                                {
                                    var res = x != (noOfChunks-1) ? fileBase64.Substring(x * chunkSize, chunkSize) :
                                                    fileBase64.Substring(x * chunkSize);
                                        return res;
                                });

                     //StringBuilder sb = new StringBuilder();
                     //foreach (var chunk in chunks)
                     //{
                     //    sb.Append(chunk);
                     //}
                     var arrQrCodes = new Bitmap[(int)noOfChunks + 1];
                     int i = 0;
                     var checkSum = getCheckSum(fileBase64);
                     foreach (var chunk in chunks)
                     {
                         if (i == 0)
                         {
                             var data = i.ToString() + '/' + ((int)noOfChunks ) + '/' + Path.GetFileName(filePath) + '/' + checkSum + '$';
                             arrQrCodes[i++] = GenerateQRCode(data, QrCodeWidth, QrCodeHeight, Path.GetFileName(filePath));
                         }
                        var chunkPart = i.ToString() + '/' + Path.GetFileName(filePath) + '/' + getCheckSum(chunk) + '$' + chunk;
                        arrQrCodes[i++] = GenerateQRCode(chunkPart, QrCodeWidth, QrCodeHeight, Path.GetFileName(filePath));

                         if (ct.IsCancellationRequested)
                         {
                             // another thread decided to cancel
                             //  Console.WriteLine("task canceled");
                             return null;
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
            stopGeneratingQrCode = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFileName.Text))
                {
                    string fpath = Path.GetDirectoryName(txtFileName.Text), fName = Path.GetFileName(txtFileName.Text);

                    string[] files = Directory.GetFiles(fpath, fName, SearchOption.TopDirectoryOnly);
                    listView1.Items.Clear();
                    foreach (var cfile in files)
                    {
                        if(Path.GetExtension(cfile).ToLower() == ".base64"){
                            continue;

                        }
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
                MessageBox.Show($@"Exception occurred:
{ex.Message}
-------------------------------------
{ex.StackTrace}
");
            }

        }
        async void  loadFiles() {
            //Bitmap[] imag1;
            //Bitmap[] imag2;
            //Bitmap[] imag3;
            List<Task<Tuple<(Bitmap[], string)>>> arrTasks = new List<Task<Tuple<(Bitmap[], string)>>>();
            List<CancellationTokenSource> arrCancel = new List<CancellationTokenSource>();
            for (var i = 0; i < listView1.Items.Count; i++)
            {
               

                var filePath = listView1.Items[i].SubItems[0].Text; // get current item
                if (i == 0)
                {
                    var tokenSource = new CancellationTokenSource();
                    CancellationToken ct = tokenSource.Token;
                    var task = GenerateQrCodes(filePath,ct);
                    if (task != null)
                    {

                        arrTasks.Add(task);
                        arrCancel.Add(tokenSource);
                    }
                }


                if ((i + 1) < listView1.Items.Count) //get next item to avoid waiting
                {
                    filePath = listView1.Items[i+1].SubItems[0].Text;
                    var tokenSource = new CancellationTokenSource();
                    CancellationToken ct = tokenSource.Token;
                    var task = GenerateQrCodes(filePath,ct);
                    if (task != null)
                    {
                        arrTasks.Add(task);
                        arrCancel.Add(tokenSource);
                    }
                }
                if (stopGeneratingQrCode)
                {
                    cancelTasks(arrTasks, arrCancel);
                    return;
                }
                //if ((i + 2) < listView1.Items.Count)
                //{
                //    arrTasks[2] = GenerateQrCodes(filePath);
                //}
                var res=await arrTasks[0];
                arrTasks.Remove(arrTasks[0]); // remove the task once it's completed
                //var res = await finishedTask;
                var bitmaps = res.Item1.Item1;
                var fileName = res.Item1.Item2;
             //   imgData.Add(fileName, image);
                setFileLoadStatus(fileName);
                lblCurrentFileName.Text = fileName;
                var intvl = txtInterval.Text;
                if (string.IsNullOrWhiteSpace(intvl)) { intvl = "0.2"; };
                for (var iLoop = 0; iLoop < bitmaps.Length; iLoop++)
                {



                    
                    pictureBox1.Image = bitmaps[iLoop];
                    pictureBox1.Refresh();
                    await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(intvl)));
                    if (stopGeneratingQrCode) {
                        cancelTasks(arrTasks, arrCancel);
                        return;
                    }
                    lblQrCodeNo.Text = iLoop.ToString() +" / "+ (bitmaps.Length-1);
                   // bitmaps[iLoop].Dispose();
                }
                await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(intvl)));
                //arrTasks[0] = arrTasks[1];


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
            clearPictureBox();
            btnFetchFileList.Enabled = true;
        }

        private void cancelTasks(List<Task<Tuple<(Bitmap[], string)>>> arrTasks, List<CancellationTokenSource> arrCancel)
        {

            if (arrTasks.Count > 1)
            {
                arrCancel[0].Cancel();
                arrCancel[1].Cancel();
            }
            else if (arrTasks.Count > 0)
            {
                arrCancel[0].Cancel();
            }


        }

        private void btnRestart_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mus.txtInterval = txtInterval.Text;
            mus.txtFileName = txtFileName.Text;
            mus.txtChunkSize = txtChunkSize.Text;
            mus.txtMissingCodeFilesPath = txtMissingCodeFilesPath.Text;
            mus.txtMissingCodesListFile = txtMissingCodesListFile.Text;
            mus.Save();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

        private void btnStartMissingCodeGen_Click(object sender, EventArgs e)
        {
            LoadMissingChunkFiles();
        }
        async void LoadMissingChunkFiles()
        {
            var fileName = txtMissingCodesListFile.Text.Trim();

            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                var missingFiles = File.ReadAllLines(fileName);

                foreach (var file in missingFiles)
                {
                    if (!string.IsNullOrWhiteSpace(file))
                    {
                        var arrMissingInfo = file.Split(':');
                        if (file.Length > 1)
                        {
                            var arrMissingChunks = arrMissingInfo[1].Split(',');
                            var missingFileName = arrMissingInfo[0].Trim();
                            lblCurrentFileName.Text = missingFileName;
                          //  Application.DoEvents();
                            var qrCodes = FileMissingChunks(missingFileName, arrMissingChunks);
                            var intvl = txtInterval.Text;
                            if (qrCodes != null)
                            {
                                if (string.IsNullOrWhiteSpace(intvl)) { intvl = "0.2"; };
                                var iLoop = 0;
                                foreach (var qrCode in qrCodes)
                                {
                                    await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(intvl)));
                                    lblQrCodeNo.Text = arrMissingChunks[iLoop].ToString();
                                    pictureBox1.Image = qrCode;
                                    iLoop++;
                                }
                            }
                        }
                    }

                }

                clearPictureBox();

            }
        }
        List<Bitmap> FileMissingChunks(string filePath, string[] arrMissingChunks)
        {
            string fileBase64;
            int fileLength;
            chunkSize = 2770;
            int.TryParse(txtChunkSize.Text.Trim(), out chunkSize);

            filePath = Path.Combine(txtMissingCodeFilesPath.Text.Trim(), filePath);
            if (File.Exists(filePath))
            {
                fileBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
                fileLength = fileBase64.Length;
                var noOfChunks = (int)Math.Floor((decimal)(fileLength / chunkSize)) + 1;

                var chunks = Enumerable.Range(0, noOfChunks)
                           .Select((x) =>
                           {
                               var res = x != (noOfChunks - 1) ? fileBase64.Substring(x * chunkSize, chunkSize) :
                                               fileBase64.Substring(x * chunkSize);
                               return res;
                           }).ToList();

                //for (var i = 0; i < noOfChunks; i++) { 

                //}
                //var arrQrCodes = new Bitmap[(int)noOfChunks + 1];
                List<Bitmap> arrQrCodes = new List<Bitmap>();
                int i = 0;
                var checkSum = getCheckSum(fileBase64);
                foreach (var strChunkNo in arrMissingChunks)
                {
                    int chunkNo = -1;
                    if (int.TryParse(strChunkNo.Trim(), out chunkNo))
                    {
                        var chunk = chunks[chunkNo-1];
                        var chunkPart = chunkNo.ToString() + '/' + Path.GetFileName(filePath) + '/' + getCheckSum(chunk) + '$' + chunk;
                        arrQrCodes.Add( GenerateQRCode(chunkPart, QrCodeWidth, QrCodeHeight, Path.GetFileName(filePath)));
                    }
                    // }

                    Application.DoEvents();
                }

                return arrQrCodes;
            }
            return null;

        }
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            stopGeneratingQrCode = true;
            btnFetchFileList.Enabled = true;
            clearPictureBox();
        }
        void clearPictureBox() {
            if (pictureBox1.Image == null) {
                return;
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
            pictureBox1.Update();
            pictureBox1.Refresh();
            Application.DoEvents();
            pictureBox1.Update();
            pictureBox1.Refresh();
        }
        private void txtMissingCodesListFile_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtMissingCodesListFile.Text.Trim())) {
                txtMissingCodeFilesPath.Text = Path.GetDirectoryName( txtMissingCodesListFile.Text.Trim());
            }
        }
    }
}
