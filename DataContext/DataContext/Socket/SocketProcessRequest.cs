using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using DataContext.AdminBusiness;
using DataContext.Until;
using Microsoft.Web.WebSockets;

namespace DataContext.Socket
{
    public class SocketProcessRequest
    {
        public static bool ProcessRequest()
        {
//            var db = new NotificationManagerBusiness();
//            var resultMessage = db.GetMyNotificationNotRead();
//            SocketMessage socketMessage = new SocketMessage();
//            socketMessage.Message = resultMessage.Count.ToString();
//            string message = ConvertObjectToStringJson(socketMessage);
            HttpContext context = System.Web.HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                string userId = AccountUntil.GetAccount().id.ToString();
                var checkLogin =  SocketUtils.listSocket.FirstOrDefault(s =>
                    s._mysession == userId
                );
                if (checkLogin != null)
                {
                    SocketRequest("!!!", userId, 3);
                    return false;
                }
                else
                {
                     context.AcceptWebSocketRequest(new SocketUtils(userId, ": join"));
                        return true;
                }
            }
            return false;
        }

        public static void SocketRequest(string message, string id, int type)
        {
            SocketMessage socketMessage = new SocketMessage();
            socketMessage.Message = message;
            socketMessage.Type = type;
            socketMessage.Id = int.Parse(id);
            string _message = ConvertObjectToStringJson(socketMessage);
            var target = SocketUtils.listSocket.FirstOrDefault(m => m._mysession == id);

            if (target != null)
            {
                target.Send(_message);
            }
        }

        public static void SocketRequestAll(string message, int type)
        {
            SocketMessage socketMessage = new SocketMessage();
            socketMessage.Type = type;
            socketMessage.Message = message;
            string _message = ConvertObjectToStringJson(socketMessage);
            SocketUtils.listSocket.All(s =>
            {
                s.Send(_message);
                return true;
            });
        }

        public static string ConvertObjectToStringJson(SocketMessage socketMessage)
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(socketMessage);
            return jsonString.ToString();
        }
    }
}