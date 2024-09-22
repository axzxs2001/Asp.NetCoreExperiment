using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;

using (PdfDocument document = PdfDocument.Open(@"C:\Documents\document.pdf"))
{
    foreach (Page page in document.GetPages())
    {
        
        string pageText = page.Text;

        foreach (Word word in page.GetWords())
        {
            Console.WriteLine(word.Text);
        }
    }
}