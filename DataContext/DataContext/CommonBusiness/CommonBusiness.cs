using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;

namespace DataContext.CommonBusiness
{
    public class CommonBusiness
    {
        public List<m_quocgia> ListCountry()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_quocgia.ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<m_quyen> ListRole()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_quyen.Where(ob => ob.id == 2 || ob.id == 3).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<m_filethongbao> ListFileByNotifiId(int id,List<m_filethongbao> lstFile)
        {
            try
            {
//                var myId = AccountUntil.GetAccount().id;
//                var roleId = AccountUntil.GetAccount().idquyentaikhoan;
                var result = lstFile.Where(ob => ob.id_thongbao == id).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<m_diadiemtochuc> ListVenue()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_diadiemtochuc.Where(ob => ob.trangthai == SystemMessageConst.StatusConst.Active).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<m_trangthai> ListStatus()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_trangthai.Where(ob => ob.id != 0).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<m_quyen> ListAuByAccId(int accId)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_quyentaikhoan.Where(ob => ob.idtaikhoan == accId && ob.trangthai == SystemMessageConst.StatusConst.Active).Select(ob => ob.m_quyen)
                        .ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<m_chitietthongbao> GetListPersonReceiveNotification(int idNotifi)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_chitietthongbao
                        .Where(ob => ob.trangthai == SystemMessageConst.StatusConst.Active && ob.idthongbao == idNotifi)
                        .ToList();
                   
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<AccountInfo> GetListReAndDe()
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_taikhoan.Where(ob => ob.trangthai == SystemMessageConst.StatusConst.Active).Select(ob => new AccountInfo {
                        id = ob.id,
                        email = ob.email,
                        type = ob.m_quyentaikhoan.FirstOrDefault().idquyen.Value,
                        fullname = (ob.idloaitaikhoan == 1 ? ob.m_thongtincanhan.FirstOrDefault().hovaten : ob.m_thongtincoquan.tencoquan)
                        }).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}