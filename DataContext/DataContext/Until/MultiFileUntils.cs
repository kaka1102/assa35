using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class MultiFileUntils
    {
        public static string SaveMultiFile(HttpPostedFileBase imageFile, string url, List<string> ext)
        {
            try
            {
                string extension = System.IO.Path.GetExtension(imageFile.FileName.ToLower());
                var check = ext.FirstOrDefault(ob => ob.ToLower() == extension);
                if (check == null)
                {
                    return null;
                }
                string imageUrl = url + Guid.NewGuid() + extension;
                imageFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + imageUrl);
                return imageUrl;
            }
            catch
            {
                return null;
            }
        }

    }
}