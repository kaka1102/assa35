using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;

namespace DataContext.Until
{
    public class StatusUntil
    {
        /// <summary>
        /// Kiểm tra là admin khi ấn nút đẩy lên server
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int CheckAddStatus(string status)
        {
            try
            {
                int _status;
                Int32.TryParse(status, out _status);
                if (_status == SystemMessageConst.StatusConst.SaveDraft)
                {
                    return SystemMessageConst.StatusConst.SaveDraft;
                }
                if (_status == SystemMessageConst.StatusConst.RemoveOnSite)
                {
                    return SystemMessageConst.StatusConst.RemoveOnSite;
                }
                if (_status == SystemMessageConst.StatusConst.Pending)
                {
                    if (AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
                    {
                        return SystemMessageConst.StatusConst.Active;
                    }
                    else
                    {
                        return SystemMessageConst.StatusConst.Pending;
                    } 
                }
                if (_status == SystemMessageConst.StatusConst.Active)
                {
                    if (AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
                    {
                        return SystemMessageConst.StatusConst.Active;
                    }
                    else
                    {
                        return SystemMessageConst.StatusConst.Pending;
                    }
                }
                return SystemMessageConst.StatusConst.SaveDraft;
            }
            catch (Exception)
            {
                return SystemMessageConst.StatusConst.SaveDraft;
            }
        }
        /// <summary>
        /// Kiểm tra bài viết có chính chủ ko , bài viết đã đẩy lên server và đc duyệt chưa
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool CheckAuthor(int? accId,int? status)
        {
            try
            {
                if (status == SystemMessageConst.StatusConst.Active || status == SystemMessageConst.StatusConst.Pending)
                {
                    return true;
                }
                var myId = AccountUntil.GetAccount().id;

                if (myId != accId)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Kiểm tra nút chọn ngôn ngữ -- quyền admin hoặc bài viết chưa active
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool CheckChooseLanguage(int? status)
        {
            try
            {
                var roleId = AccountUntil.GetAccount().idquyentaikhoan;

                if (roleId == SystemMessageConst.RoleConst.Admin)
                {
                    return true;
                }
                if (status != SystemMessageConst.StatusConst.Active)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Kiểm tra thông báo đó có đc tag cho người này ko
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool CheckNotifiPerson(List<m_chitietthongbao> lstNotifiDetail,int? idAcc)
        {
            try
            {
                if (lstNotifiDetail == null || idAcc == null)
                {
                     return false;
                }
                var check = lstNotifiDetail.FirstOrDefault(ob => ob.idtaikhoan == idAcc);
                if (check != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}