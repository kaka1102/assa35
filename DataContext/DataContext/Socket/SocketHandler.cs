using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI;
using DataContext.DAL;

namespace DataContext.Socket
{
    public class SocketHandler : WebSocketHandler
    {


        public string ComputerName = System.Environment.MachineName;
        public string ip = HttpContext.Current.Request.UserHostAddress;
        public string Agent = HttpContext.Current.Request.UserAgent;    //Request.UserAgent;

        public static List<kenhlamviec> klv = new List<kenhlamviec>();

        public static List<useronline> users = new List<useronline>();
        public static string message = null;

        public static SocketHandler currentSK;
        public SessionUser session { get; set; }
        public useronline user { get; set; }
        public kenhlamviec kenh { get; set; }
        public SocketHandler(SessionUser ss, string newMessages)
        {
            message = newMessages;
            this.session = ss;
            currentSK = this;
        }

        public override void OnOpen()
        {
            if (session != null)
            {

                users.Where(m => m.session.email == session.email && m.session.sessionid != session.sessionid).All(m =>
                {
                    m.socket.Send(JsonConvert.SerializeObject(new { newlogin = true }));
                    return true;
                });

                useronline u = new useronline();
                u.session = session;
                u.socket = this;
                user = u;
                users.Add(u);

                var check = klv.Where(m => m.sessionid == session.sessionid || m.emaildangnhap == session.email).FirstOrDefault();
                if (check == null)
                {
                    kenhlamviec k = new kenhlamviec();
                    k.id = session.id;
                    k.sessionid = session.sessionid;
                    k.emaildangnhap = session.email;
                    k.tendaydu = session.hovaten;
                    k.idquyentk = session.idquyentaikhoan;
                    k.ip = ip;
                    k.Agent = Agent;
                    k.Tooken = session.Tooken;
                    k.ComputerName = ComputerName;
                    kenh = k;
                    klv.Add(k);
                }
            }
        }

        public override void OnMessage(string message)
        {

        }

        public override void OnClose()
        {
            //Xóa client
            users.Remove(user);
            klv.Remove(kenh);
        }
        public static void send(string msg)
        {
            if (currentSK != null)
            {
                currentSK.Send(msg);
            }

        }
    }
    public class useronline
    {
        public SessionUser session { get; set; }

        public SocketHandler socket { get; set; }
    }
    public class kenhlamviec
    {
        public SocketHandler socketKLV { get; set; }
        public int id { get; set; }
        public string tendaydu { get; set; }
        public string emaildangnhap { get; set; }
        public string sessionid { get; set; }
        public int idquyentk { get; set; }
        public string Tooken { get; set; }
        public string ip { get; set; }
        public string Agent { get; set; }
        public string ComputerName { get; set; }
        public string maxacminh { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool trangthaixacminh { get; set; }

        [System.ComponentModel.DefaultValue(0)]
        public int solannhapma { get; set; }
    }
}