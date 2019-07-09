using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Microsoft.Web.WebSockets;

namespace DataContext.Socket
{
    public class SocketUtils : WebSocketHandler
    {
        public static List<SocketUtils> listSocket = new List<SocketUtils>();
        public static SocketUtils currentSocket;
        public string _mysession { get; set; }
        public static string message = null;
        public SocketUtils(string sessionid,string newmessage)
        {
            _mysession = sessionid;
            message = newmessage;
        }

        public override void OnOpen()
        {
            currentSocket = this;
            listSocket.Add(this);

//            listSocket.Where(s => s != this).All(s =>
//            {
//                s.Send(_mysession + " vừa tham gia");
//                s.Send("Message: "+message);
//                return true;
//            });
//            Send(message);
        }

        public override void OnMessage(string message)
        {
            listSocket.Where(s => s != this).All(s =>
            {
                s.Send(message);
                return true;
            });
        }

        public override void OnClose()
        {
            listSocket.Where(s => s != this).All(s =>
            {
//                s.Send(_mysession + " Vừa thoát");
                SocketMessage socketMessage = new SocketMessage();
                socketMessage.Type = 4;
                socketMessage.Message = _mysession;
                
                SocketProcessRequest.SocketRequestAll(_mysession, 2);
                return true;
            });
            listSocket.Remove(this);
        }
    }
}