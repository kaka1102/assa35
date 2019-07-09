using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using DataContext;


namespace DataContext.AdminBusiness
{

    public class MenuAdminBusiness
    {
        SystemMess SysMess = new SystemMess();
        ASS_35Entities entity = new ASS_35Entities();

        public SystemMess LoadMenuAdminByUser()
        {
            List<ListMenu> lstMenu = new List<ListMenu>();
            var session = (SessionUser)HttpContext.Current.Session["uSession"];

            var Resulf = entity.m_danhmuc.Where(m => m.trangthai == 3 && m.idcha == 0 &&
                m.m_chucnangtaikhoan.Where(x => x.iddanhmuc == m.id && x.trangthai == 3 && x.idquyen == session.idquyentaikhoan).Any() &&
                ((session.idquyentaikhoan == 4 && session.trangthaiduatin == 0) ? (m.id != 4 && m.id != 7) : true)
                ).ToList().OrderBy(m => m.stt.Value).All(m =>
            {

                ListMenu m_dm = new ListMenu();
                m_dm.id = m.id;
                m_dm.tendanhmuc = DisplayUntils.ReturnMessageBylanguage(m.tendanhmuc, m.tendanhmucEn);
                m_dm.url = m.url;
                m_dm.physical = m.physical;
                m_dm.ismenu = m.ismenu.Value;
                m_dm.stt = m.stt.Value;
                m_dm.icon = m.icon;
                m_dm.idcha = m.idcha.Value;
                m_dm.trangthai = m.trangthai.Value;
                LoadChildren(m_dm);
                lstMenu.Add(m_dm);
                return true;
            });

            SysMess.LstData = lstMenu.Cast<object>().ToList();
            SysMess.IsSuccess = true;

            return SysMess;
        }

        public ListMenu LoadChildren(ListMenu lstMN)
        {
            List<ListMenu> lstMenu = new List<ListMenu>();
            var session = (SessionUser)HttpContext.Current.Session["uSession"];
            var Resulf = entity.m_danhmuc.Where(m => m.trangthai == 3 && m.idcha == lstMN.id && m.m_chucnangtaikhoan.Where(x => x.iddanhmuc == m.id && x.trangthai == 3 && x.idquyen == session.idquyentaikhoan).Any()).ToList().OrderBy(m => m.stt.Value).All(m =>
            {

                ListMenu m_dm = new ListMenu();
                m_dm.id = m.id;
                m_dm.tendanhmuc = DisplayUntils.ReturnMessageBylanguage(m.tendanhmuc, m.tendanhmucEn);
                m_dm.url = m.url;
                m_dm.physical = m.physical;
                m_dm.ismenu = m.ismenu.Value;
                m_dm.stt = m.stt.Value;
                m_dm.icon = m.icon;
                m_dm.idcha = m.idcha.Value;
                m_dm.trangthai = m.trangthai.Value;
                LoadChildren(m_dm);
                lstMenu.Add(m_dm);
                return true;
            });
            lstMN.lstMn = lstMenu;
            return lstMN;
        }
    }
}