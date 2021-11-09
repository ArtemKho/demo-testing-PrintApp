using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.IO;


namespace App1
{
    class File2PDF
    {
        public string Filepath;

        File2PDF(string path)
        {
            Filepath = path; 
        }
        void Word2PDF(string path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object oMissing = System.Reflection.Missing.Value;

            FileInfo wordFile = new FileInfo(path);




        }
    }
}
