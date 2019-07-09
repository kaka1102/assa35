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
using System.Data.Entity.Core.Objects;
using System.Threading;

namespace DataContext.ClientBusiness
{

    public class BS_HomeEn
    {
        ASS_35Entities entity = new ASS_35Entities();
        public List<Video> ShowSliderImage()
        {
            try
            {
                List<Video> lstVideo = new List<DAL.Video>();

                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 2 && m.theloai == "images").ToList().OrderByDescending(m => m.thoigianpheduyet).All(m =>
                {
                    Video vd = new DAL.Video();

                    vd.id = m.id;
                    vd.tenchude = m.tenchude;
                    vd.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    vd.gioithieu = m.gioithieu;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
                    vd.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").Select(a => a.duongdanfile).FirstOrDefault();
                    lstVideo.Add(vd);

                    return true;
                });

                return lstVideo;
            }
            catch (Exception)
            {
                List<Video> lst = new List<DAL.Video>();
                return lst;
            }

        }

        public List<m_diadiemtochuc> ShowSlideVenueEn()
        {
            try
            {
                List<m_diadiemtochuc> lstVideo = new List<m_diadiemtochuc>();

                var Result = entity.m_diadiemtochuc.Where(m => m.trangthai == 3 && m.phienban == 2).ToList().OrderByDescending(m => m.thoigianduyet).All(m =>
                {
                    m_diadiemtochuc vd = new m_diadiemtochuc();

                    vd.id = m.id;
                    vd.tendiadiem = m.tendiadiem;
                    vd.noidung = m.noidung;
                    vd.avatar = m.avatar;
                    lstVideo.Add(vd);

                    return true;
                });

                return lstVideo;
            }
            catch (Exception)
            {
                List<m_diadiemtochuc> lst = new List<m_diadiemtochuc>();
                return lst;
            }

        }
        public List<DetailsPost> ShowSlidePostEn()
        {
            try
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();

                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 2).ToList().OrderByDescending(m => m.thoigianpheduyet).All(m =>
                {
                    DetailsPost vd = new DAL.DetailsPost();

                    vd.id = m.id;
                    vd.tieude = m.tieude;
                    vd.gioithieu = m.gioithieu;
                    vd.noidung = m.noidung;
                    vd.avatar = m.avatar;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                    lstPost.Add(vd);

                    return true;
                });
                return lstPost;
            }
            catch (Exception)
            {
                List<DetailsPost> lst = new List<DAL.DetailsPost>();
                return lst;
            }
        }
        public m_baivietgioithieu ShowIntroduceHome()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/home").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }

        public m_baivietgioithieu ShowIntroduceOverview()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/general-introduction").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }

        public m_baivietgioithieu ShowIntroductionAssaVN()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/assa-vn").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }

        //public ListProgramme ShowListProgramme()
        //{
        //    try
        //    {
        //        string Ngay1 = DateTime.Now.ToString("yyyy-MM-dd");
        //        string Ngay2 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        //        string Ngay3 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");


        //        DateTime D1, D2, D3;
        //        DateTime.TryParse(Ngay1, out D1);
        //        DateTime.TryParse(Ngay2, out D2);
        //        DateTime.TryParse(Ngay3, out D3);

        //        ListProgramme lstList = new DAL.ListProgramme();

        //        List<Programmer> lsNgay1 = new List<DAL.Programmer>();
        //        List<Programmer> lsNgay2 = new List<DAL.Programmer>();
        //        List<Programmer> lsNgay3 = new List<DAL.Programmer>();

        //        string DtoD = ParseDayToText.Convert(Ngay1) + " to " + ParseDayToText.Convert(Ngay3);

        //        var dsngay1 = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 2 && m.ngaydienra == D1).ToList().All(m =>
        //        {
        //            Programmer pg = new DAL.Programmer();
        //            pg.id = m.id;
        //            pg.avatar = m.avatar;
        //            pg.ghichu = m.ghichu;
        //            pg.ngaydienra = m.ngaydienra.ToString("dd/MM/yyyy");
        //            pg.noidung = m.noidung;
        //            pg.tencuochop = m.tencuochop;
        //            pg.thoigianbatdau = m.thoigianbatdau.ToString();
        //            pg.thoigianketthuc = m.thoigianketthuc.ToString();
        //            lsNgay1.Add(pg);
        //            return true;
        //        });

        //        var dsngay2 = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 2 && m.ngaydienra == D2).ToList().All(m =>
        //        {
        //            Programmer pg = new DAL.Programmer();
        //            pg.id = m.id;
        //            pg.avatar = m.avatar;
        //            pg.ghichu = m.ghichu;
        //            pg.ngaydienra = m.ngaydienra.ToString("dd/MM/yyyy");
        //            pg.noidung = m.noidung;
        //            pg.tencuochop = m.tencuochop;
        //            pg.thoigianbatdau = m.thoigianbatdau.ToString();
        //            pg.thoigianketthuc = m.thoigianketthuc.ToString();
        //            lsNgay2.Add(pg);
        //            return true;
        //        });

        //        var dsngay3 = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 2 && m.ngaydienra == D3).ToList().All(m =>
        //        {
        //            Programmer pg = new DAL.Programmer();
        //            pg.id = m.id;
        //            pg.avatar = m.avatar;
        //            pg.ghichu = m.ghichu;
        //            pg.ngaydienra = m.ngaydienra.ToString("dd/MM/yyyy");
        //            pg.noidung = m.noidung;
        //            pg.tencuochop = m.tencuochop;
        //            pg.thoigianbatdau = m.thoigianbatdau.ToString();
        //            pg.thoigianketthuc = m.thoigianketthuc.ToString();
        //            lsNgay3.Add(pg);
        //            return true;
        //        });
        //        lstList.lstNgay1 = lsNgay1;
        //        lstList.lstNgay2 = lsNgay2;
        //        lstList.lstNgay3 = lsNgay3;
        //        lstList.DateToDate = DtoD;

        //        lstList.Date1 = ParseDayToText.Convert(Ngay1);
        //        lstList.Date2 = ParseDayToText.Convert(Ngay2);
        //        lstList.Date3 = ParseDayToText.Convert(Ngay3);
        //        return lstList;
        //    }
        //    catch (Exception)
        //    {
        //        ListProgramme lst = new ListProgramme();
        //        return lst;
        //    }
        //}

        public List<MeetingEvent> GetListProgram()
        {
            try
            {
                List<MeetingEvent> lstList = new List<DAL.MeetingEvent>();
                var Result = entity.m_sukienhop.Where(a => a.trangthai == 3 && a.phienban == 2).ToList().OrderBy(a=>a.ngayhop.Value).Take(5).All(a =>
                {
                    MeetingEvent lst = new DAL.MeetingEvent();

                    lst.id = a.id;
                    lst.tencuochop = a.tencuochop;
                    lst.diadiem = a.diadiem;
                    lst.ngayhop = a.ngayhop.Value.ToString("MM/dd/yyyy");
                    lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                    lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.id && m.trangthai.Value == 3).ToList().Select(m => new DetailMeetingEvent
                    {
                        id = m.id,
                        ten = m.ten,
                        diadiem = m.diadiem,
                        thoigianbatdau = m.thoigianbatdau.ToString(),
                        thoigianketthuc = m.thoigianketthuc.ToString(),
                        // ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                        noidung = m.noidung
                    }).ToList();
                    lstList.Add(lst);
                    return true;
                });

                return lstList;
            }
            catch (Exception)
            {
                List<MeetingEvent> lst = new List<MeetingEvent>();
                return lst;
            }
        }

        public List<DateMeeting> GetListDateMeeting()
        {
            //try
            //{
                List<DateMeeting> lstDate = new List<DateMeeting>();
                var result1 = entity.m_sukienhop.Where(a=> a.trangthai == 3).Select(b => b.ngayhop.Value).Distinct().ToList();
                foreach(DateTime i in result1)
                {
                    DateMeeting datemt = new DateMeeting();
                    List<MeetingEvent> lstList = new List<DAL.MeetingEvent>();
                    var Result = entity.m_sukienhop.Where(a => a.trangthai == 3 && a.phienban == 2).ToList().OrderBy(a => a.ngayhop.Value).Take(10).All(a =>
                    {
                        MeetingEvent lst = new DAL.MeetingEvent();

                        lst.id = a.id;
                        lst.tencuochop = a.tencuochop;
                        lst.diadiem = a.diadiem;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                        lst.ngayhop = a.ngayhop.Value.ToString("dd MMMM yyyy");
                        lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                        lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.id && m.trangthai.Value == 3).ToList().Select(m => new DetailMeetingEvent
                        {
                            id = m.id,
                            ten = m.ten,
                            diadiem = m.diadiem,
                            thoigianbatdau = m.thoigianbatdau.ToString(@"hh\:mm"),
                            thoigianketthuc = m.thoigianketthuc.Value.ToString(@"hh\:mm"),
                            // ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                            noidung = m.noidung
                        }).ToList();
                        lstList.Add(lst);
                        return true;
                    });
                    datemt.ngayhop = i.ToString("dd MMMM yyyy");
                    datemt.listhop = lstList;
                    lstDate.Add(datemt);
                }
                return lstDate;

            //}
            //catch (Exception)
            //{
            //    List<DateMeeting> lst = new List<DateMeeting>();


            //    return lst;
            //}
        }

        public List<DateTime> Test()
        {
            var result1 = entity.m_sukienhop.Select(b => b.ngayhop.Value).Distinct().ToList();
            return result1;
        }
        /// <summary>
        ///  get list program by date to day
        /// </summary>
        /// <returns></returns>
        public ReturnMeetingClient ShowListProgramme()
        {
            try
            {
                string Ngay1 = DateTime.Now.ToString("yyyy-MM-dd");
                string Ngay2 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                string Ngay3 = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");

                DateTime date1 = DateTime.Parse(Ngay1);
                DateTime date2 = DateTime.Parse(Ngay2);
                DateTime date3 = DateTime.Parse(Ngay3);


                ReturnMeetingClient lstList = new DAL.ReturnMeetingClient();

                List<MeetingEvent> lsNgay1 = new List<DAL.MeetingEvent>();
                List<MeetingEvent> lsNgay2 = new List<DAL.MeetingEvent>();
                List<MeetingEvent> lsNgay3 = new List<DAL.MeetingEvent>();

                string DtoD = ParseDayToText.Convert(date1.ToString("dd/MM/yyyy")) + " to " + ParseDayToText.Convert(date3.ToString("dd/MM/yyyy"));


                var Result = entity.SP_MEETING(Ngay1).ToList().All(a =>
                {
                    MeetingEvent lst = new DAL.MeetingEvent();

                    lst.id = a.idhop;
                    lst.tencuochop = a.tencuochop;
                    lst.diadiem = a.diadiemhop;
                    lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                    lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.idhop && m.trangthai.Value == 3 && m.ngayhop.Value.Equals(date1) == true).ToList().Select(m => new DetailMeetingEvent
                    {
                        id = m.id,
                        ten = m.ten,
                        diadiem = m.diadiem,
                        thoigianbatdau = m.thoigianbatdau.ToString(),
                        thoigianketthuc = m.thoigianketthuc.ToString(),
                        ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                        noidung = m.noidung
                    }).ToList();
                    lsNgay1.Add(lst);
                    return true;
                });

                var Result2 = entity.SP_MEETING(Ngay2).ToList().All(a =>
                {
                    MeetingEvent lst = new DAL.MeetingEvent();

                    lst.id = a.idhop;
                    lst.tencuochop = a.tencuochop;
                    lst.diadiem = a.diadiemhop;
                    lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                    lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.idhop && m.trangthai.Value == 3 && m.ngayhop.Value.Equals(date2) == true).ToList().Select(m => new DetailMeetingEvent
                    {
                        id = m.id,
                        ten = m.ten,
                        diadiem = m.diadiem,
                        thoigianbatdau = m.thoigianbatdau.ToString(),
                        thoigianketthuc = m.thoigianketthuc.ToString(),
                        ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                        noidung = m.noidung
                    }).ToList();
                    lsNgay2.Add(lst);
                    return true;
                });

                var Result3 = entity.SP_MEETING(Ngay3).ToList().All(a =>
                {
                    MeetingEvent lst = new DAL.MeetingEvent();

                    lst.id = a.idhop;
                    lst.tencuochop = a.tencuochop;
                    lst.diadiem = a.diadiemhop;
                    lst.thoigianduyet = (a.thoigianduyet == null ? DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Notapproved, SystemMessageConst.systemmessage.NotapprovedEn) : a.thoigianduyet.Value.ToString("dd/MM/yyyy"));
                    lst.chitiet = entity.m_chitietsukienhop.Where(m => m.idsukienhop == a.idhop && m.trangthai.Value == 3 && m.ngayhop.Value.Equals(date3) == true).ToList().Select(m => new DetailMeetingEvent
                    {
                        id = m.id,
                        ten = m.ten,
                        diadiem = m.diadiem,
                        thoigianbatdau = m.thoigianbatdau.ToString(),
                        thoigianketthuc = m.thoigianketthuc.ToString(),
                        ngayhop = m.ngayhop.Value.ToString("dd/MM/yyyy"),
                        noidung = m.noidung
                    }).ToList();
                    lsNgay3.Add(lst);
                    return true;
                });


                lstList.lstNgay1 = lsNgay1;
                lstList.lstNgay2 = lsNgay2;
                lstList.lstNgay3 = lsNgay3;
                lstList.DateToDate = DtoD;

                lstList.Date1 = ParseDayToText.Convert(date1.ToString("dd/MM/yyyy"));
                lstList.Date2 = ParseDayToText.Convert(date2.ToString("dd/MM/yyyy"));
                lstList.Date3 = ParseDayToText.Convert(date3.ToString("dd/MM/yyyy"));
                return lstList;
            }
            catch (Exception)
            {
                ReturnMeetingClient lst = new ReturnMeetingClient();
                return lst;
            }
        }
        public m_baivietgioithieu ShowIntroduceAgenda()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/agenda").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }

        public List<ListMedia> ShowIntroduceReportAndDocument()
        {
            try
            {
                List<ListMedia> lt = new List<DAL.ListMedia>();

                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 2 && m.theloai == "document").ToList().All(m =>
                {
                    ListMedia md = new DAL.ListMedia();
                    md.id = m.id;
                    md.tenchude = m.tenchude;
                    md.loaitl = m.ghichu;
                    md.lstMedia = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3).ToList().Select(a => new media
                    {
                        id = a.id,
                        duongdanfile = a.duongdanfile,
                        tenfile = a.tenfile
                    }).ToList();

                    //id = m.id,
                    //tenchude = m.tenchude,
                    //tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                    //gioithieu = m.gioithieu,
                    //thoigianpheduyet = m.thoigianpheduyet.Value.ToString("HH:mm dd/MM/yyyy"),
                    //lstMedia = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3).ToList().Select(a => new media
                    //{
                    //    id = a.id,
                    //    duongdanfile = a.duongdanfile
                    //}).ToList()
                    lt.Add(md);
                    return true;
                });

                return lt;
            }
            catch (Exception)
            {
                List<ListMedia> lt = new List<DAL.ListMedia>();
                return lt;
            }

        }

        public ReturnPost ShowIntroducePressRealease(int currPage)
        {
            try
            {
                ReturnPost data = new DAL.ReturnPost();
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_LOADALL_NEWS(currPage, 5, totalCount).ToList().All(m =>
                {
                    DetailsPost vd = new DAL.DetailsPost();

                    vd.id = m.id;
                    vd.tieude = m.tieude;
                    vd.gioithieu = m.gioithieu;
                    vd.avatar = m.avatar;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                    lstPost.Add(vd);
                    return true;
                });


                int total = int.Parse(totalCount.Value.ToString());
                if (total % 5 == 0)
                {
                    total = total / 5;
                }
                else
                {
                    total = (total / 5) + 1;
                }
                string pageHTML = "";

                string url = "";


                pageHTML = new returnHTMLPage().outputHTMLPage(total, currPage, url);
                string path = HttpContext.Current.Request.Path;
                int pageTr = 0;
                int pageS = 0;
                if (currPage == 1)
                {
                    pageTr = 1;
                }
                else
                {
                    pageTr = currPage - 1;
                }
                if (currPage == total)
                {
                    pageS = total;
                }
                else
                {
                    pageS = currPage + 1;
                }
                string HTMLPage = "";
                HTMLPage = HTMLPage + @"<ul class='pagination' style='float: right;'>";
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=1" + url + "' id='pageDau'>First</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + pageTr + "" + url + "' id='pageTruoc'>Prev</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + pageHTML;
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=" + pageS + "" + url + "' id='pageSau'>Next</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + total + "" + url + "' id='pageCuoi'>Last</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + @"</ul>";

                data.dataPage = HTMLPage;
                data.lstPost = lstPost;

                return data;
            }
            catch (Exception)
            {
                ReturnPost rt = new DAL.ReturnPost();
                return rt;
            }
        }

        public DetailsPost DetailsNew(int id)
        {

            try
            {
                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 2 && m.id == id).ToList().Select(m => new DetailsPost
                {
                    id = m.id,
                    tieude = m.tieude,
                    gioithieu = m.gioithieu,
                    noidung = m.noidung,
                    avatar = m.avatar,
                    tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                    thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy"),
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                DetailsPost dt = new DAL.DetailsPost();
                return dt;
            }

        }

        public List<DetailsPost> ShowListNewsInvolve(int id)
        {
            try
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();
                var Post = entity.m_baiviet.Where(z => z.id == id).Select(z => z.thoigianpheduyet.Value).FirstOrDefault();

                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 2 & m.id != id && m.thoigianpheduyet.Value <= Post).ToList().OrderByDescending(m => m.thoigianpheduyet).Take(4).All(m =>
                {
                    DetailsPost vd = new DAL.DetailsPost();

                    vd.id = m.id;
                    vd.tieude = m.tieude;
                    vd.gioithieu = m.gioithieu;
                    vd.avatar = m.avatar;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                    lstPost.Add(vd);

                    return true;
                });

                if (lstPost.Count == 0)
                {
                    var Result2 = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 2 & m.id != id).ToList().OrderByDescending(m => m.thoigianpheduyet).Take(4).All(m =>
                    {
                        DetailsPost vd = new DAL.DetailsPost();

                        vd.id = m.id;
                        vd.tieude = m.tieude;
                        vd.gioithieu = m.gioithieu;
                        vd.avatar = m.avatar;
                        vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                        lstPost.Add(vd);

                        return true;
                    });
                }
                return lstPost;
            }
            catch (Exception)
            {
                List<DetailsPost> lst = new List<DAL.DetailsPost>();

                return lst;
            }
        }

        public m_lienhe ShowContact()
        {
            try
            {
                var Result = entity.m_lienhe.Where(m => m.trangthai == 3 && m.phienban == 2).ToList().Select(m => new m_lienhe
                {

                    id = m.id,
                    noidung = m.noidung
                }).FirstOrDefault();
                return Result;
            }
            catch (Exception)
            {
                m_lienhe lstPost = new m_lienhe();
                return lstPost;
            }
        }
        public m_baivietgioithieu ShowIntroduceCustom()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/custom").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }
        public m_baivietgioithieu ShowIntroduceGeneral()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/general").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }
        public m_baivietgioithieu ShowIntroduceFlightInformation()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/flightinformation").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }
        public m_baivietgioithieu ShowIntroduceVenue()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/venue").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }
        public List<m_speaker> ShowListPortfolio(string type)
        {
            try
            {
                List<m_speaker> lstPost = new List<m_speaker>();

                var Result = entity.m_speaker.Where(m => m.trangthai == 3 && m.type == type).ToList().OrderBy(m => m.stt).All(m =>
                {
                    m_speaker vd = new m_speaker();

                    vd.id = m.id;
                    vd.hoten = m.hoten;
                    vd.chucvu = m.chucvu;
                    vd.avatar = m.avatar;
                    vd.file_abstract = m.file_abstract;
                    vd.presentation = m.presentation;
                    vd.noidung = m.noidung;
                    lstPost.Add(vd);

                    return true;
                });

                return lstPost;
            }
            catch (Exception)
            {
                List<m_speaker> lstPost = new List<m_speaker>();
                return lstPost;
            }
        }
    }
}