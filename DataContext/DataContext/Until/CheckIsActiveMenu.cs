using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class CheckIsActiveMenu
    {

        public static bool IsActiveMenu(string url, string urlSql)
        {
            ASS_35Entities entity = new ASS_35Entities();
            var Children = entity.m_menuclient.Where(m => m.url == url && m.trangthai == 3).FirstOrDefault();
            bool kq = false;
            if (url == urlSql)
            {
                kq = true;
            }
            else if ((string.IsNullOrEmpty(url) || url == "/") && (urlSql == "/trang-chu" || urlSql == "/home"))
            {
                kq = true;
            }
            else if (Children != null && Children.idcha != 0)
            {
                var Parent = entity.m_menuclient.Where(m => m.id == Children.idcha).FirstOrDefault();
                if (Parent.url == urlSql)
                {
                    kq = true;
                }
            }
            else if (url.IndexOf("/lib-video/") != -1 && urlSql == "/video-hinh-anh")
            {
                kq = true;
            }
            else if (url.IndexOf("/lib-image/") != -1 && urlSql == "/video-hinh-anh")
            {
                kq = true;
            }
            else if (url.IndexOf("/chi-tiet/") != -1 && urlSql == "/tin-tuc-su-kien")
            {
                kq = true;
            }
            else if (url.IndexOf("/chi-tiet-su-kien") != -1 && urlSql == "/tin-tuc-su-kien")
            {
                kq = true;
            }
            return kq;
        }
    }
}