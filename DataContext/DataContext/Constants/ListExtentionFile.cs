using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Constants
{
    public static class ListExtentionFile
    {
        public static class ExtentionFile
        {
            public static List<string> exImg = new List<string> { ".png", ".jpeg", ".jpg", ".gif" };
            public static List<string> exVideo = new List<string> { ".mp4", ".flv" };
            public static List<string> exDocument = new List<string> { ".pdf",".ppt",".pptx" };
        }
    }
}