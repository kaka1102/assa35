using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
namespace DataContext.Until
{
    public class ReadFilePdf
    {
        public void HienThiFileThanhPhan(string path)
        {
            if(File.Exists(path)){
                string pdfPath = HttpContext.Current.Server.MapPath(path);
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(pdfPath);

                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-length", buffer.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(buffer);
            }
            else
            {

            }
            
            
        }
    }
}