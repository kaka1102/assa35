using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;

namespace DataContext.Until
{
    public class AccountUntil
    {
        ASS_35Entities entity = new ASS_35Entities();
        public static SessionUser GetAccount()
        {
            try
            {
                var result = (SessionUser)HttpContext.Current.Session["uSession"];
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetNamePersonAccount(int id, int type, int? idcoquan)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    if (type == SystemMessageConst.AccountType.Personal)
                    {
                        var result = entities.m_thongtincanhan.FirstOrDefault(ob => ob.idtaikhoan == id);
                        if (result != null)
                        {
                            return result.hovaten;
                        }
                        else
                        {
                            return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                                SystemMessageConst.systemmessage.NoDataEn);
                        }
                    }
                    else
                    {
                        if (idcoquan == null)
                        {
                            return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                                SystemMessageConst.systemmessage.NoDataEn);
                        }
                        var result = entities.m_thongtincoquan.FirstOrDefault(ob => ob.id == idcoquan);
                        if (result != null)
                        {
                            return result.tencoquan;
                        }
                        else
                        {
                            return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                                SystemMessageConst.systemmessage.NoDataEn);
                        }
                    }

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetNamePersonAccountDelegate(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_registration_info.FirstOrDefault(ob => ob.idtaikhoan == id);
                    if(result != null)
                    {
                        return result.organization + " - " + result.country;
                    }
                    else
                    {
                        return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                                SystemMessageConst.systemmessage.NoDataEn);
                    }
                    
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetFullNameByIdTK(int id)
        {
            string FullName = "";
            var Check = entity.m_taikhoan.Where(m => m.id == id).FirstOrDefault();
            if (Check == null)
            {
                FullName = "Không xác định";
            }
            else
            {
                if (Check.idloaitaikhoan == 2 && Check.idcoquan != null)
                {
                    var getCoquan = entity.m_thongtincoquan.Where(m => m.id == Check.idcoquan).Select(m => m.tencoquan).FirstOrDefault();
                    FullName = getCoquan;
                }
                else if (Check.idloaitaikhoan == 1)
                {
                    var getname = entity.m_thongtincanhan.Where(m => m.idtaikhoan == id).Select(m => m.hovaten).FirstOrDefault();
                    if (getname != null)
                    {
                        FullName = getname;
                    }
                    else
                    {
                        FullName = Check.email;
                    }
                }
                else
                {
                    FullName = Check.email;
                }
            }



            //var getname = entity.m_thongtincanhan.Where(m => m.idtaikhoan == id).Select(m => m.hovaten).FirstOrDefault();
            //if (getname != null)
            //{
            //    FullName = getname;
            //}
            //else
            //{
            //    var getemail = entity.m_taikhoan.Where(m => m.id == id).Select(m => m.email).FirstOrDefault();
            //    if (getemail != null)
            //    {
            //        FullName = getemail;
            //    }
            //    else
            //    {
            //        FullName = "Không xác định";
            //    }
            //}

            return FullName;
        }
        public string GetFullNameIdTK(int id)
        {
            string FullName = "";
            var Check = entity.m_taikhoan.Where(m => m.id == id).FirstOrDefault();
            if (Check == null)
            {
                FullName = "Không xác định";
            }
            else
            {
                if (Check.idloaitaikhoan == 2 && Check.idcoquan != null)
                {
                    var getCoquan = entity.m_thongtincoquan.Where(m => m.id == Check.idcoquan).Select(m => m.tencoquan).FirstOrDefault();
                    FullName = getCoquan;
                }
                else if (Check.idloaitaikhoan == 1)
                {
                    var getname = entity.m_registration_info.Where(m => m.idtaikhoan == id).Select(m => new m_registration_info(){firstname = m.firstname, lastname = m.lastname, middleinitial = m.middleinitial}).FirstOrDefault();
                    if (getname != null)
                    {
                        FullName = getname.firstname + " " + getname.middleinitial + " " + getname.lastname;
                    }
                    else
                    {
                        FullName = Check.email;
                    }
                }
                else
                {
                    FullName = Check.email;
                }
            }


            

            return FullName;
        }
    }
}