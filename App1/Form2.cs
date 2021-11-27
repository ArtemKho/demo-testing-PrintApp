using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;


namespace App1
{
    public partial class Form2 : Form
    {

        private void listAllPrinters()
        {

            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                this.listBox1.Items.Add(item.ToString());
            }
        }
        string pname;
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            pname = this.listBox1.SelectedItem.ToString();

            myPrinters.SetDefaultPrinter(pname);
            

        }
        string Filepath;
        public Form2(string path)
        {
            Filepath = path;
            InitializeComponent();
            listAllPrinters();
        }

        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            PrintDocument D = new PrintDocument();
            D.DocumentName = Filepath;
            D.Print();
        }
    }
}
