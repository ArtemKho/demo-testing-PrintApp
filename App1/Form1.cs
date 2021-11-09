using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace App1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string FilePath;
            string OnlyFileName;
            string CopyFile;
            string FileLocation;
            string FullCopyFileName;
            string FileExtension;
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.InitialDirectory = "C:\\Users\\Admin\\Desktop";
                OFD.Filter = "pdf files (*.pdf)|*.pdf|docx files (*.docx)|*.docx|doc files (*.doc)|*.doc|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                OFD.RestoreDirectory = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    FilePath = OFD.FileName;
                    OnlyFileName = System.IO.Path.GetFileName(FilePath);
                    if (OnlyFileName.Substring(0, 3) == "XX ")
                    {
                        FullCopyFileName = FilePath;
                    }
                    else
                    {
                        CopyFile = "XX " + OnlyFileName;
                        FileLocation = System.IO.Path.GetDirectoryName(FilePath);
                        FullCopyFileName = FileLocation + "\\" + CopyFile;
                        System.IO.File.Copy(FilePath, FullCopyFileName, false);
                    }
                    
                    Form2 newform2 = new Form2(FullCopyFileName);
                    newform2.Show();
                }
                else
                {
                    MessageBox.Show("Файл не был выбран");
                }
            }

        }

    }
}
