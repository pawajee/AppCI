using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace BASE64
{
    public partial class frmBase64Conversion : Form
    {
        OpenFileDialog fileDialog;
        MyUserSettings mus;
        public frmBase64Conversion()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace( txtConvert.Text ))
            {
                string fpath = Path.GetDirectoryName(txtConvert.Text), fName = Path.GetFileName(txtConvert.Text);

                string[] files = Directory.GetFiles(fpath, fName, SearchOption.TopDirectoryOnly);
                foreach (var cfile in files)
                {
                    byte[] bytes = File.ReadAllBytes(Path.Combine(fpath,cfile));
                    string file = Convert.ToBase64String(bytes);
                    File.WriteAllText(Path.Combine(fpath,cfile) + ".base64.txt", file);
                }

            }
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            string fpath = txtReverse.Text;

            var file = File.ReadAllText(fpath);
            var bytes = Convert.FromBase64String(file);
            fpath = fpath.Replace(".base64","").Replace(".txt", "");
            File.WriteAllBytes(fpath + ".rev", bytes);
        }

        

        private void btnShowFileDialogToBase64_Click(object sender, EventArgs e)
        {
            txtConvert.Text = GetFile() ?? txtConvert.Text;


          //  fileDialog.Filter = "Database files (*.mdb, *.accdb)|*.mdb;*.accdb";
          //fileDialog.FilterIndex = 0;

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

        private void frmBase64Conversion_Load(object sender, EventArgs e)
        {
            fileDialog = new OpenFileDialog();
            MyUserSettings mus = new MyUserSettings();
          //  fileDialog.
           // fileDialog.InitialDirectory = Environment.CurrentDirectory;
            txtConvert_TextChanged(null, null);
            txtReverse_TextChanged(null, null);
            txtConvert.Text = mus.txtConvert;
            txtReverse.Text = mus.txtReverse;
        }

        private void btnShowFileDialogFromBase64_Click(object sender, EventArgs e)
        {
            txtConvert.Text = GetFile() ?? txtConvert.Text;
        }

        private void txtConvert_TextChanged(object sender, EventArgs e)
        {
            btnConvert.Enabled = txtConvert.Text.Trim() != "";// && File.Exists(txtConvert.Text);
        }

        private void txtReverse_TextChanged(object sender, EventArgs e)
        {
            //if (btnReverse.Text.Trim() == "") return;
            btnReverse.Enabled = txtReverse.Text.Trim() != "" && File.Exists(btnReverse.Text);
        }
    }

}
