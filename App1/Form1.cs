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
            button2.Text = "Распечатать";
        }
        public string FilePath;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.InitialDirectory = "C:\\Users\\Admin\\Desktop";
                OFD.Filter = "All files (*.*)|*.*|docx files (*.docx)|*.docx|doc files (*.doc)|*.doc|txt files (*.txt)|*.txt|pdf files (*.pdf)|*.pdf";
                OFD.RestoreDirectory = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    FilePath = OFD.FileName;
                    
                    label1.Text = "Выбранный файл:" + FilePath;
                }
            }

        }

        public string[] filename;
        public string flagDrop;
      
        void panel1_DragDrop(object sender, DragEventArgs e)
        {
            filename = (string[])e.Data.GetData(DataFormats.FileDrop);
            label1.Text = "Выбранный файл:" + filename[0];
            button2.Text = "Распечатать";
            flagDrop = label1.Text;
        }

        void panel1_DragEnter(object sender, DragEventArgs e)
        {
           
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                label1.Text = "Отпустите мышь";
                e.Effect = DragDropEffects.Copy;

            }
        }
        void panel1_DragLeave(object sender, EventArgs e)
        {
            label1.Text = "Переместите файл";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Перетащите один файл")
            {
                MessageBox.Show("Файл не был выбран");
            }
            else if (label1.Text == flagDrop)
            {
                string exten = Path.GetExtension(filename[0]);
                if ((exten == ".pdf") || (exten == ".doc") || (exten == ".docx") || (exten == ".txt"))
                {
                    MyFile myfile = new MyFile(filename[0]);
                    myfile.PrintPDF(myfile.CopyFile());
                }
                else
                {
                    MessageBox.Show("Файл должен быть текстового формата") ;
                }
                   
            }
            else if (label1.Text == "Выбранный файл:" + FilePath)
            {
                string exten = Path.GetExtension(FilePath);
                if ((exten == ".pdf") || (exten == ".doc") || (exten == ".docx") || (exten == ".txt"))
                {
                    MyFile myfile = new MyFile(FilePath);
                    myfile.PrintPDF(myfile.CopyFile());
                }
                else
                {
                    MessageBox.Show("Файл должен быть текстового формата");
                }

            }
        }
    }
}
