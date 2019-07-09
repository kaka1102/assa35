using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.Constants;
using DataContext.DAL;

namespace DataContext.Until
{
    public class FileUtils
    {
        public static string FileSave(HttpPostedFileBase imageFile,string url,List<string> ext)
        {
            try
            {
                string extension = System.IO.Path.GetExtension(imageFile.FileName).ToLower();
                var check = ext.FirstOrDefault(ob => ob == extension);
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

        public static SystemMess ValidateFileUpload(string url, string type,string name)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), name);
                    return systemMess;
                }
                FileInfo fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + url);
                bool ck_Type = false;
                if (type == "image")
                {
                    ck_Type = MimeDetective.Extension.Documents.DocumentExtensions.IsImages(fileInfo);
                }
                if (type == "pdf")
                {
                    ck_Type = MimeDetective.Extension.Documents.DocumentExtensions.IsPdf(fileInfo);
                }
                if (ck_Type)
                {
                    systemMess.IsSuccess = true;
                    return systemMess;
                }
                else
                {
                    string fullPath = HttpContext.Current.Request.MapPath(url);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    systemMess.IsSuccess = false;
                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.FileNotCorrectFormat, SystemMessageConst.ValidateConst.FileNotCorrectFormatEn), name);
                    return systemMess;
                }
              
            }
            catch(Exception e)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = e.ToString();
                return systemMess;
            }
        }
    }
}