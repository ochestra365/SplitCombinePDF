using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Split_Combine.Classes.PDF;
namespace Split_Combine
{
    public partial class FrmMain : Form
    {
        List<string> ListPDF = new List<string>();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
            
            foreach(FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToUpper().Equals(".PDF"))
                {
                    this.ListPDF.Add(file.Name);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.ListPDF.Count > 0)
            {
                PDFController pdf = new PDFController();
                pdf.Merge(this.ListPDF);
            }
        }
    }
}
