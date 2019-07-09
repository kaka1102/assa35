using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class returnHTMLPage
    {
        public string outputHTMLPage(int total, int currPage, string url)
        {
            string path = HttpContext.Current.Request.Path;
            string type = HttpContext.Current.Request.Url.AbsolutePath;
            string pageHTML = "";

            if (total <= 5)
            {
                for (int i = 1; i <= total; i++)
                {
                    if (i == currPage)
                    {
                        pageHTML = pageHTML + @"<li class='active'><span>" + currPage + "</span></li>&nbsp;";
                    }
                    else
                    {
                        pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + i + "" + url + "'>" + i + "</a></li>&nbsp;";
                    }
                }
            }
            else
            {
                if (currPage == 1)
                {
                    pageHTML = pageHTML + @"<li  class='active'><span>" + currPage + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 2 + "" + url + "'>" + 2 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 3 + "" + url + "'>" + 3 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 4 + "" + url + "'>" + 4 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
                else if (currPage == 2)
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + currPage + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 3 + "" + url + "'>" + 3 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 4 + "" + url + "'>" + 4 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
                else if (currPage == 3)
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 2 + "" + url + "'>" + 2 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + currPage + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 4 + "" + url + "'>" + 4 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
                else if (currPage == total)
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 3) + "" + url + "'>" + (total - 3) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 2) + "" + url + "'>" + (total - 2) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 1) + "" + url + "'>" + (total - 1) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + currPage + "</span></li>&nbsp;";
                }
                else if (currPage == (total - 1))
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 3) + "" + url + "'>" + (total - 3) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 2) + "" + url + "'>" + (total - 2) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + (total - 1) + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
                else if (currPage == (total - 2))
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 3) + "" + url + "'>" + (total - 3) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + (total - 2) + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (total - 1) + "" + url + "'>" + (total - 1) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
                else
                {
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + 1 + "" + url + "'>" + 1 + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (currPage - 1) + "" + url + "'>" + (currPage - 1) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li class='active'><span>" + currPage + "</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + (currPage + 1) + "" + url + "'>" + (currPage + 1) + "</a></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><span>...</span></li>&nbsp;";
                    pageHTML = pageHTML + @"<li><a href='" + path + "?page=" + total + "" + url + "'>" + total + "</a></li>&nbsp;";
                }
            }
            return pageHTML;
        }
    }
}