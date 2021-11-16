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
using System.Diagnostics; 

namespace App1
{
    public partial class Form2 : Form
    {
        public string path;
        public void PrintPDF(string Filepath)
        {

            using (PrintDialog Dialog = new PrintDialog())
            {
                Dialog.ShowDialog();

                ProcessStartInfo printProcessInfo = new ProcessStartInfo();
                printProcessInfo.Verb = "print";
                printProcessInfo.CreateNoWindow = true;
                printProcessInfo.FileName = Filepath;
                printProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process printProcess = new Process();
                printProcess.StartInfo = printProcessInfo;
                printProcess.Start();
                printProcess.WaitForInputIdle();
                if (printProcess.CloseMainWindow() == true)
                {
                    printProcess.Kill();
                }
            }
        }

        public Form2(string Filepath)
        {
            path = Filepath;
            InitializeComponent();
            this.label2.Text = Filepath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            PrintPDF(path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
