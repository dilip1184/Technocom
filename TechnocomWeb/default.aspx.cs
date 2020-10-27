using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Drawing;

namespace TechnocomWeb
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  GeneratePDF();
            Response.Redirect("LoginPage.aspx", false);
        }
        //protected void GeneratePDF()
        //{
        //    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A4, 10, 10, 10, 10);

        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        document.Open();

        //        Chunk chunk = new Chunk("This is from chunk. ");
        //        document.Add(chunk);

        //        Phrase phrase = new Phrase("This is from Phrase.");
        //        document.Add(phrase);

        //        Paragraph para = new Paragraph("This is from paragraph.");
        //        document.Add(para);

        //        string text = "you are successfully created PDF file.";
        //        Paragraph paragraph = new Paragraph();
        //        paragraph.SpacingBefore = 10;
        //        paragraph.SpacingAfter = 10;
        //        paragraph.Alignment = Element.ALIGN_LEFT;
        //        paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, Color.GREEN);
        //        paragraph.Add(text);
        //        document.Add(paragraph);

        //        document.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();
        //        Response.Clear();
        //        Response.ContentType = "application/pdf";

        //        string pdfName = "User";
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");
        //        Response.ContentType = "application/pdf";
        //        Response.Buffer = true;
        //        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        //        Response.BinaryWrite(bytes);
        //        Response.End();
        //        Response.Close();
        //    }
        //}

        //public static string SendSMS(string mobile, string message)
        //{
        //    try
        //    {
        //        string smsApiUrl1 = "http://sms.covalentsoftwares.com/newsendsms/sms_request.php";

        //        string myParameters = "username=smartlogik&password=mcDon@lds!234&smsfrom={0}&receiver={1}&content={2}";

        //        myParameters = string.Format(myParameters, "PMSPUB", mobile, message);

        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            wc.UploadString(new Uri(smsApiUrl1), myParameters);
        //        }
        //    }
        //    catch
        //    {
        //        // SqlContext.Pipe.Send(ex.Message.ToString());
        //    }
        //    return string.Empty;
        //}
    }
}