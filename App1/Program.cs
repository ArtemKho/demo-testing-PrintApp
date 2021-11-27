using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
  
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                MessageBox.Show("File:" + Environment.GetCommandLineArgs()[1]);
                Application.Run(new Form2(Environment.GetCommandLineArgs()[1]));
                
            }
            else
            {
                Application.Run(new Form1());
            }
            //Application.Run(new Form1());
        }
    }
}
