using DataContext.Constants;
using DataContext.Until;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.AdminBusiness
{
    public class ActivityBusiness
    {
        public string ValidateAddActivity(m_activity act)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            result = check.CheckRequiredField(act.actName, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NameActivity, SystemMessageConst.systemmessage.NameActivityEn), 3, 500);
            if(result.error != null)
            {
                return result.error;
            }
            result = check.CheckRequiredField(act.des, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DesActivity, SystemMessageConst.systemmessage.DesActivityEn), 5);
            if (result.error != null)
            {
                return result.error;
            }
           
            return null;
        }

        public List<ActivityInfomation_Result> GetListActivity (int status)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int myId = AccountUntil.GetAccount().id;
                    int roleId = AccountUntil.GetAccount().idquyentaikhoan;
                    var result = entities.ActivityInfomation(roleId, myId, status).OrderByDescending(a => a.createDate).ToList();
                    return result;
                }
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}