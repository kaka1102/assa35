using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.ClientBusiness
{
    public class BS_LoadActivity
    {
        ASS_35Entities entity = new ASS_35Entities();

        public List<m_activity> GetListAct(int? actype)
        {
            List<m_activity> result = new List<m_activity>();
            if ( actype == 0)
            {
               result = entity.m_activity.Where(a => a.status == 3).ToList();
            }
            else
            {
                result = entity.m_activity.Where(a => a.status == 3 && a.idActType == actype).ToList();
            }
            return result;
        }

        public List<m_activity_detail> GetListOp(int actid)
        {
            var result = new List<m_activity_detail>();
            result = entity.m_activity_detail.Where(a => a.idActivity == actid && a.status ==3).ToList();
            return result;
        }

        public List<SP_GetListActById_Result> GetListActById(int idregis)
        {
            var result = new List<SP_GetListActById_Result>();
            result = entity.SP_GetListActById(idregis).ToList();
            return result;
        }

        public m_room_order GetHotelById(int idroom)
        {
            m_room_order resulte = entity.m_room_order.FirstOrDefault(a => a.id == idroom);
            return resulte;
        }
    }
}