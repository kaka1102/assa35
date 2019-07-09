using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using DataContext;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using DataContext.DAL;
using System.Globalization;


namespace DataContext.ClientBusiness
{
    public class BS_LoadMenuClient
    {
        ASS_35Entities entity = new ASS_35Entities();
        SystemMess SysMess = new SystemMess();

        //   public List<m_menuclient> MenuClient()

        public LstMenuClient MenuClient()
        {

            LstMenuClient lstMenu = new LstMenuClient();
            List<MenuCL> lstM = new List<DAL.MenuCL>();
            try
            {
                string url = HttpContext.Current.Request.Path;
                string Version = DisplayUntils.GetVersionValues();

                var lstEn = entity.m_menuclient.Where(m => m.trangthai == 3 && m.type == "en").ToList().Select(m => m.url);
                var lstVi = entity.m_menuclient.Where(m => m.trangthai == 3 && m.type == "vi").ToList().Select(m => m.url);

                //   if (url == "/home" && Version == "vi")
                if (lstEn.Contains(url) == true && Version == "vi")
                {
                    HttpCookie cookie = new HttpCookie("Version");
                    cookie.Value = "en";
                    cookie.Expires = DateTime.Now.AddDays(1d);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    Version = "en";
                }
                //  else if (url == "/trang-chu" && Version == "en")
                else if (lstVi.Contains(url) == true && Version == "en")
                {
                    HttpCookie cookie = new HttpCookie("Version");
                    cookie.Value = "vi";
                    cookie.Expires = DateTime.Now.AddDays(1d);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    Version = "vi";
                    // url = ""  && Version = "en"
                }

                var Resulf = entity.m_menuclient.Where(m => m.trangthai == 3 && m.idcha == 0 && m.type == Version).ToList().OrderBy(m => m.stt.Value).All(m =>
                {
                    MenuCL ml = new DAL.MenuCL();
                    ml.id = m.id;
                    ml.tenmenu = m.tenmenu;
                    ml.url = m.url;
                    ml.isActive = CheckIsActiveMenu.IsActiveMenu(url, m.url);
                    LoadChildren(ml);

                    lstM.Add(ml);
                    return true;
                });

                lstMenu.lstMN = lstM;
                lstMenu.Version = Version;

                return lstMenu;
                //return Resulf;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public List<MenuCL> LoadChildren(MenuCL lstMN)
        {
            List<MenuCL> lstMenu = new List<MenuCL>();
            var Resulf = entity.m_menuclient.Where(m => m.trangthai == 3 && m.idcha == lstMN.id).ToList().OrderBy(m => m.stt.Value).All(m =>
            {

                MenuCL m_dm = new MenuCL();
                m_dm.id = m.id;
                m_dm.tenmenu = m.tenmenu;
                m_dm.url = m.url;
                LoadChildren(m_dm);
                lstMenu.Add(m_dm);
                return true;
            });
            lstMN.mnChil = lstMenu;
            return lstMenu;
        }
    }
}