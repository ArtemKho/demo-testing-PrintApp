using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


namespace App1
{
    class MyFile
    {
        public string Filepath;

        public MyFile(string path)
        {
            Filepath = path; 
        }

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

        public string Word2PDF(string path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object oMissing = System.Reflection.Missing.Value;

            FileInfo wordFile = new FileInfo(path);

            word.Visible = false;
            word.ScreenUpdating = false;

            Object filename = (Object)wordFile.FullName;

            Document doc = word.Documents.Open(ref filename, ref oMissing, 
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            object outputFilename = wordFile.Directory + "\\" + "DWCopy " + wordFile.Name.Replace(".docx", ".pdf");
            object fileformat = WdSaveFormat.wdFormatPDF;
            
            doc.SaveAs2(ref outputFilename,
                ref fileformat, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            
            object savechanges = WdSaveOptions.wdSaveChanges;
            ((Document)doc).Close(ref savechanges, ref oMissing, ref oMissing);
            doc = null;

            ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;

            MessageBox.Show("The file has been converted to PDF");
            MessageBox.Show(outputFilename.ToString());

            return outputFilename.ToString();
        }

        public string TXT2PDF(string path)
        {
            try
            {
                string line = null;
                System.IO.TextReader readFile = new StreamReader(path);
                int yPoint = 0;

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "TXT to PDF";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Verdana", 20, XFontStyle.Regular);

                while (true)
                {
                    line = readFile.ReadLine();
                    if (line == null)
                    {
                        break; // TODO: might not be correct. Was : Exit While
                    }
                    else
                    {
                        graph.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        yPoint = yPoint + 40;
                    }
                }

                string FileName = Path.GetFileName(path).Replace(".txt", ".pdf"); 

                string pdfFilename =Path.GetDirectoryName(path) + "\\" + "DWCopy " + FileName;  

                pdf.Save(pdfFilename);
                readFile.Close();
                readFile = null;
                Process.Start(pdfFilename);
                return pdfFilename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
        
        public string CopyFile()
        {
            string FullCopyFileName;
            string CopyFile;
            string FileLocation;
            string OnlyFileName = System.IO.Path.GetFileName(Filepath);
            if (OnlyFileName.Substring(0, 3) == "DWCopy " & Path.GetExtension(OnlyFileName)== ".pdf")
            {
                FullCopyFileName = Filepath;
                return FullCopyFileName;
            }
            else if (Path.GetExtension(OnlyFileName)==".pdf")
            {
                CopyFile = "DWCopy " + OnlyFileName;
                FileLocation = System.IO.Path.GetDirectoryName(Filepath);
                FullCopyFileName = FileLocation + "\\" + CopyFile;
                if (File.Exists(FullCopyFileName))
                {
                    try
                    {
                        File.Delete(FullCopyFileName);
                        System.IO.File.Copy(Filepath, FullCopyFileName);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Удалите файл:" + FullCopyFileName);
                    }

                }
                else
                {
                    System.IO.File.Copy(Filepath, FullCopyFileName);
                }
                return FullCopyFileName;
            }
            
            return Convert2PDF(Filepath);
            
        }

       public string Convert2PDF(string path)
        {
            if (Path.GetExtension(path) == ".docx")
            {
                return Word2PDF(path);
            }
            else if (Path.GetExtension(path) == ".txt")
            {
                return TXT2PDF(path);
            }
            else
            {
                return path;
            }
        }
    }
}
