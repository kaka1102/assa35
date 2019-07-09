using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class DeleteFileUntil
    {
        public bool DeleteFileByPath(string path)
        {
            try
            {
                bool status = false;
                var pathFile = HttpContext.Current.Server.MapPath(path);
                if (System.IO.File.Exists(pathFile))
                {
                    System.IO.File.Delete(pathFile);
                    status = true;
                }
                return status;
            }
            catch (Exception)
            {

                return true;
            }

        }
    }
}