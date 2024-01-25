using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Split_Combine.Classes.PDF
{
    class PDFController
    {
        /// <summary>
        /// Merge PDF files.
        /// </summary>
        /// <param name="fileNames"></param>
        public void Merge(List<string> fileNames)
        {
            try
            {
                FileInfo file = new FileInfo("merge.pdf");
                if (file.Exists) file.Delete();
                fileNames.RemoveAll(x => x.Equals("merge.pdf"));
                // step 1: creation of a document-object
                Document document = new Document();
                //create newFileStream object which will be disposed at the end
                using (FileStream newFileStream = new FileStream("merge.pdf", FileMode.Create))
                {
                    // step 2: we create a writer that listens to the document
                    PdfCopy writer = new PdfCopy(document, newFileStream);

                    // step 3: we open the document
                    document.Open();

                    foreach (string fileName in fileNames)
                    {
                        // we create a reader for a certain document
                        PdfReader reader = new PdfReader(fileName);
                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }

                    // step 5: we close the document and writer
                    writer.Close();
                    document.Close();
                }//disposes the newFileStream object
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TIME : {DateTime.Now}.\n{ex}");
            }
        }

    }
}
