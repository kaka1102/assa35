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


namespace DataContext.ClientBusiness
{
    public class BS_TrangChu
    {
        ASS_35Entities entity = new ASS_35Entities();


        public List<m_diadiemtochuc> ShowListDiadiem()
        {
            try
            {
                List<m_diadiemtochuc> lstDD = new List<m_diadiemtochuc>();
                var Result = entity.m_diadiemtochuc.Where(m => m.trangthai == 3 && m.phienban == 1).ToList().All(m =>
                {
                    m_diadiemtochuc dd = new m_diadiemtochuc();
                    dd.id = m.id;
                    dd.tendiadiem = m.tendiadiem;
                    dd.avatar = m.avatar;
                    lstDD.Add(dd);
                    return true;
                });
                return lstDD;
            }
            catch (Exception)
            {
                List<m_diadiemtochuc> lst = new List<m_diadiemtochuc>();
                return lst;
            }
        }
        public m_diadiemtochuc ChiTietDiaDiem(int id)
        {

            try
            {
                var Result = entity.m_diadiemtochuc.Where(m => m.trangthai == 3 && m.id == id).ToList().Select(m => new m_diadiemtochuc
                {
                    id = m.id,
                    tendiadiem = m.tendiadiem,
                    noidung = m.noidung,
                    avatar = m.avatar
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                m_diadiemtochuc dt = new m_diadiemtochuc();
                return dt;
            }

        }
        public Video ShowVideoHot()
        {

            try
            {
                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "video").ToList().OrderByDescending(m => m.thoigianpheduyet).Select(m => new Video
                   {
                       id = m.id,
                       tenchude = m.tenchude,
                       tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                       gioithieu = m.gioithieu,
                       thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy"),
                       link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault()
                   }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                Video vd = new DAL.Video();
                return vd;
            }

        }
        public Video ShowVideoHotTrangChu()
        {

            try
            {
                var Result = entity.m_media.Where(m => m.id==131).ToList().Select(m => new Video
                {
                    id = m.id,
                    tenchude = m.tenchude,
                    tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                    gioithieu = m.gioithieu,
                    thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy"),
                    link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault()
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                Video vd = new DAL.Video();
                return vd;
            }

        }
        public List<Video> ShowVideoHot1()
        {

            try
            {
                List<Video> lst = new List<DAL.Video>();

                var ds = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "video").ToList().OrderByDescending(m => m.thoigianpheduyet).Take(5).All(m =>
                {
                    Video vd = new DAL.Video();
                    vd.id = m.id;
                    vd.tenchude = m.tenchude;
                    vd.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    vd.gioithieu = m.gioithieu;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                    vd.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault();
                    lst.Add(vd);
                    return true;
                });
                return lst;
            }
            catch (Exception)
            {
                List<Video> lst = new List<DAL.Video>();
                return lst;
            }

        }
        public Video ShowImageHot()
        {

            try
            {
                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "images").ToList().OrderByDescending(m => m.thoigianpheduyet).Select(m => new Video
                {
                    id = m.id,
                    tenchude = m.tenchude,
                    tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                    gioithieu = m.gioithieu,
                    thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy"),
                    link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").Select(a => a.duongdanfile).FirstOrDefault()
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                Video vd = new DAL.Video();
                return vd;
            }

        }
        public List<Video> ShowImageHot1()
        {

            try
            {
                List<Video> lst = new List<DAL.Video>();

                var ds = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "images").ToList().OrderByDescending(m => m.thoigianpheduyet).Take(5).All(m =>
                {
                    Video vd = new DAL.Video();
                    vd.id = m.id;
                    vd.tenchude = m.tenchude;
                    vd.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    vd.gioithieu = m.gioithieu;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd-MM-yyyy");
                    vd.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").Select(a => a.duongdanfile).FirstOrDefault();
                    lst.Add(vd);
                    return true;
                });
                return lst;
            }
            catch (Exception)
            {
                List<Video> lst = new List<DAL.Video>();
                return lst;
            }

        }
        public List<DetailsPost> ShowTinTuc()
        {
            try
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();

                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 1).ToList().OrderByDescending(m => m.thoigianpheduyet).Take(6).All(m =>
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

                return lstPost;
            }
            catch (Exception)
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();
                return lstPost;
            }
        }

        public List<SuKien> ShowSuKien()
        {
            try
            {
                List<SuKien> lstPost = new List<DAL.SuKien>();

                var Result = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 1).ToList().OrderByDescending(m => m.thoigianduyet).Take(4).All(m =>
                {
                    SuKien vd = new DAL.SuKien();

                    vd.id = m.id;
                    vd.ghichu = m.ghichu;
                    vd.thoigianduyet = m.thoigianduyet.Value.ToString("dd/MM/yyyy");
                    vd.tencuochop = m.tencuochop;
                    vd.noidung = m.noidung;
                    //  vd.diadiem = m.m_diadiemtochuc.tendiadiem;
                    vd.diadiem = m.diadiemtochuc;
                    vd.ngaydienra = m.ngaydienra.ToString("dd-MM-yyyy");
                    vd.thoigianbatdau = m.thoigianbatdau.ToString();
                    vd.thoigianketthuc = m.thoigianketthuc.ToString();
                    lstPost.Add(vd);

                    return true;
                });

                return lstPost;
            }
            catch (Exception)
            {
                List<SuKien> lstPost = new List<DAL.SuKien>();
                return lstPost;
            }
        }

        public m_lienhe ShowLienHe()
        {
            try
            {
                var Result = entity.m_lienhe.Where(m => m.trangthai == 3 && m.phienban == 1).ToList().Select(m => new m_lienhe
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
        public List<Video> ShowSliderVideo()
        {

            try
            {
                List<Video> lstVideo = new List<DAL.Video>();

                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "video").ToList().OrderByDescending(m => m.thoigianpheduyet).All(m =>
                {
                    Video vd = new DAL.Video();

                    vd.id = m.id;
                    vd.tenchude = m.tenchude;
                    vd.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    vd.gioithieu = m.gioithieu;
                    vd.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
                    vd.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault();
                    lstVideo.Add(vd);

                    return true;
                });

                return lstVideo;
            }
            catch (Exception)
            {
                List<Video> lstVideo = new List<DAL.Video>();
                return lstVideo;
            }

        }

        public List<Video> ShowSliderImage()
        {

            try
            {
                List<Video> lstVideo = new List<DAL.Video>();

                var Result = entity.m_media.Where(m => m.trangthai == 3 && m.phienban == 1 && m.theloai == "images").ToList().OrderByDescending(m => m.thoigianpheduyet).All(m =>
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


        public List<DetailsPost> ShowSlidePost()
        {
            try
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();

                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 1).ToList().OrderByDescending(m => m.thoigianpheduyet).All(m =>
                {
                    DetailsPost vd = new DAL.DetailsPost();

                    vd.id = m.id;
                    vd.tieude = m.tieude;
                    vd.gioithieu = m.gioithieu;
                    vd.noidung = m.noidung;
                    vd.tacgia = m.tacgia;
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

        public List<m_slidetrangchu> ShowSlideImage()
        {
            try
            {
                List<m_slidetrangchu> list = new List<m_slidetrangchu>();
                list = entity.m_slidetrangchu.Where<m_slidetrangchu>(a => a.trangthai == 3 && a.phienban == 1)
                    .OrderBy(a => a.stt).ToList();
                return list;
            }
            catch ( Exception e)
            {
                return null;
            }
        }
        public ReturnData GetListImage(int moidang, int xemnhieu, int currPage, string curpage)
        {
            try
            {
                ReturnData data = null;
                List<Video> lstVideo = new List<DAL.Video>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_LIBRARY_IMAGES(moidang, xemnhieu, currPage, 16, totalCount).ToList().All(m =>
                {
                    Video lst = new DAL.Video();

                    lst.id = m.id;
                    lst.tenchude = m.tenchude;
                    lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    lst.gioithieu = m.gioithieu;
                    lst.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
                    lst.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").Select(a => a.duongdanfile).FirstOrDefault();
                    lstVideo.Add(lst);

                    return true;
                });


                int total = int.Parse(totalCount.Value.ToString());
                if (total % 16 == 0)
                {
                    total = total / 16;
                }
                else
                {
                    total = (total / 16) + 1;
                }

                if (currPage <= total)
                {
                    data = new DAL.ReturnData();
                    string pageHTML = "";

                    string url = "";
                    string url1 = HttpContext.Current.Request.QueryString["moidang"];
                    string url2 = HttpContext.Current.Request.QueryString["xemnhieu"];
                    if (url1 == "1")
                    {
                        url = "&moidang=1";
                    }
                    if (url2 == "1")
                    {
                        url = "&xemnhieu=1";
                    }


                    pageHTML = new returnHTMLPage().outputHTMLPage(total, currPage, url);
                    //string path = HttpContext.Current.Request.Path;
                    //string path = H.UrlReferrer.ToString();
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
                    HTMLPage = HTMLPage + @"<ul class='pagination'>";
                    HTMLPage = HTMLPage + @"<li>";
                    HTMLPage = HTMLPage + @"<a href='" + curpage + "#page=1" + url + "' id='pageDau'>Đầu</a></li>";
                    HTMLPage = HTMLPage + @"<li><a href='" + curpage + "#page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
                    HTMLPage = HTMLPage + @"</li>";
                    HTMLPage = HTMLPage + pageHTML;
                    HTMLPage = HTMLPage + @"<li>";
                    HTMLPage = HTMLPage + @"<a href='" + curpage + "#page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
                    HTMLPage = HTMLPage + @"<li><a href='" + curpage + "#page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
                    HTMLPage = HTMLPage + @"</li>";
                    HTMLPage = HTMLPage + @"</ul>";

                    data.dataPage = HTMLPage;
                    data.lstMDA = lstVideo;
                }
                return data;
            }
            catch (Exception)
            {
                ReturnData data = new DAL.ReturnData();
                return data;
            }
        }
        //public ReturnData GetListImage(int moidang, int xemnhieu, int currPage)
        //{
        //    try
        //    {
        //        ReturnData data = null;
        //        List<Video> lstVideo = new List<DAL.Video>();

        //        ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

        //        var Result = entity.SP_LIBRARY_IMAGES(moidang, xemnhieu, currPage, 16, totalCount).ToList().All(m =>
        //        {
        //            Video lst = new DAL.Video();

        //            lst.id = m.id;
        //            lst.tenchude = m.tenchude;
        //            lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
        //            lst.gioithieu = m.gioithieu;
        //            lst.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
        //            lst.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").Select(a => a.duongdanfile).FirstOrDefault();
        //            lstVideo.Add(lst);

        //            return true;
        //        });


        //        int total = int.Parse(totalCount.Value.ToString());
        //        if (total % 16 == 0)
        //        {
        //            total = total / 16;
        //        }
        //        else
        //        {
        //            total = (total / 16) + 1;
        //        }

        //        if (currPage <= total)
        //        {
        //            data = new DAL.ReturnData();
        //            string pageHTML = "";

        //            string url = "";
        //            string url1 = HttpContext.Current.Request.QueryString["moidang"];
        //            string url2 = HttpContext.Current.Request.QueryString["xemnhieu"];
        //            if (url1 == "1")
        //            {
        //                url = "&moidang=1";
        //            }
        //            if (url2 == "1")
        //            {
        //                url = "&xemnhieu=1";
        //            }


        //            pageHTML = new returnHTMLPage().outputHTMLPage(total, currPage, url);
        //            string path = HttpContext.Current.Request.Path;
        //            int pageTr = 0;
        //            int pageS = 0;
        //            if (currPage == 1)
        //            {
        //                pageTr = 1;
        //            }
        //            else
        //            {
        //                pageTr = currPage - 1;
        //            }
        //            if (currPage == total)
        //            {
        //                pageS = total;
        //            }
        //            else
        //            {
        //                pageS = currPage + 1;
        //            }
        //            string HTMLPage = "";
        //            HTMLPage = HTMLPage + @"<ul class='pagination'>";
        //            HTMLPage = HTMLPage + @"<li>";
        //            HTMLPage = HTMLPage + @"<a href='" + path + "?page=1" + url + "' id='pageDau'>Đầu</a></li>";
        //            HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
        //            HTMLPage = HTMLPage + @"</li>";
        //            HTMLPage = HTMLPage + pageHTML;
        //            HTMLPage = HTMLPage + @"<li>";
        //            HTMLPage = HTMLPage + @"<a href='" + path + "?page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
        //            HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
        //            HTMLPage = HTMLPage + @"</li>";
        //            HTMLPage = HTMLPage + @"</ul>";

        //            data.dataPage = HTMLPage;
        //            data.lstMDA = lstVideo;
        //        }
        //        return data;
        //    }
        //    catch (Exception)
        //    {
        //        ReturnData data = new DAL.ReturnData();
        //        return data;
        //    }
        //}
        public ReturnData GetListVideo(int moidang, int xemnhieu, int currPage, string curpage)
        {
            try
            {
                ReturnData data = new DAL.ReturnData();
                List<Video> lstVideo = new List<DAL.Video>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_LIBRARY_VIDEO(moidang, xemnhieu, currPage, 16, totalCount).ToList().All(m =>
                {
                    Video lst = new DAL.Video();

                    lst.id = m.id;
                    lst.tenchude = m.tenchude;
                    lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
                    lst.gioithieu = m.gioithieu;
                    lst.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
                    lst.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault();
                    lstVideo.Add(lst);

                    return true;
                });


                int total = int.Parse(totalCount.Value.ToString());
                if (total % 16 == 0)
                {
                    total = total / 16;
                }
                else
                {
                    total = (total / 16) + 1;
                }
                string pageHTML = "";

                string url = "";
                string url1 = HttpContext.Current.Request.QueryString["moidang"];
                string url2 = HttpContext.Current.Request.QueryString["xemnhieu"];
                if (url1 == "1")
                {
                    url = "&moidang=1";
                }
                if (url2 == "1")
                {
                    url = "&xemnhieu=1";
                }


                pageHTML = new returnHTMLPage().outputHTMLPage(total, currPage, url);
                //string path = HttpContext.Current.Request.Path;
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
                HTMLPage = HTMLPage + @"<ul class='pagination'>";
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + curpage + "#page=1" + url + "' id='pageDau'>Đầu</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + curpage + "#page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + pageHTML;
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + curpage + "#page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + curpage + "#page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + @"</ul>";

                data.dataPage = HTMLPage;
                data.lstMDA = lstVideo;

                return data;
            }
            catch (Exception)
            {
                ReturnData data = new DAL.ReturnData();
                return data;
            }
        }
        //public ReturnData GetListVideo(int moidang, int xemnhieu, int currPage)
        //{
        //    try
        //    {
        //        ReturnData data = new DAL.ReturnData();
        //        List<Video> lstVideo = new List<DAL.Video>();

        //        ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

        //        var Result = entity.SP_LIBRARY_VIDEO(moidang, xemnhieu, currPage, 16, totalCount).ToList().All(m =>
        //        {
        //            Video lst = new DAL.Video();

        //            lst.id = m.id;
        //            lst.tenchude = m.tenchude;
        //            lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
        //            lst.gioithieu = m.gioithieu;
        //            lst.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
        //            lst.link = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "video").Select(a => a.duongdanfile).FirstOrDefault();
        //            lstVideo.Add(lst);

        //            return true;
        //        });


        //        int total = int.Parse(totalCount.Value.ToString());
        //        if (total % 16 == 0)
        //        {
        //            total = total / 16;
        //        }
        //        else
        //        {
        //            total = (total / 16) + 1;
        //        }
        //        string pageHTML = "";

        //        string url = "";
        //        string url1 = HttpContext.Current.Request.QueryString["moidang"];
        //        string url2 = HttpContext.Current.Request.QueryString["xemnhieu"];
        //        if (url1 == "1")
        //        {
        //            url = "&moidang=1";
        //        }
        //        if (url2 == "1")
        //        {
        //            url = "&xemnhieu=1";
        //        }


        //        pageHTML = new returnHTMLPage().outputHTMLPage(total, currPage, url);
        //        string path = HttpContext.Current.Request.Path;
        //        int pageTr = 0;
        //        int pageS = 0;
        //        if (currPage == 1)
        //        {
        //            pageTr = 1;
        //        }
        //        else
        //        {
        //            pageTr = currPage - 1;
        //        }
        //        if (currPage == total)
        //        {
        //            pageS = total;
        //        }
        //        else
        //        {
        //            pageS = currPage + 1;
        //        }
        //        string HTMLPage = "";
        //        HTMLPage = HTMLPage + @"<ul class='pagination'>";
        //        HTMLPage = HTMLPage + @"<li>";
        //        HTMLPage = HTMLPage + @"<a href='" + path + "?page=1" + url + "' id='pageDau'>Đầu</a></li>";
        //        HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
        //        HTMLPage = HTMLPage + @"</li>";
        //        HTMLPage = HTMLPage + pageHTML;
        //        HTMLPage = HTMLPage + @"<li>";
        //        HTMLPage = HTMLPage + @"<a href='" + path + "?page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
        //        HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
        //        HTMLPage = HTMLPage + @"</li>";
        //        HTMLPage = HTMLPage + @"</ul>";

        //        data.dataPage = HTMLPage;
        //        data.lstMDA = lstVideo;

        //        return data;
        //    }
        //    catch (Exception)
        //    {
        //        ReturnData data = new DAL.ReturnData();
        //        return data;
        //    }
        //}
        public ReturnPost GetListTinTuc(int currPage)
        {
            try
            {
                ReturnPost data = new DAL.ReturnPost();
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_LOADALL_TINTUC(currPage, 5, totalCount).ToList().All(m =>
                {
                    DetailsPost vd = new DAL.DetailsPost();

                    vd.id = m.id;
                    vd.tieude = m.tieude;
                    vd.gioithieu = m.gioithieu;
                    vd.tacgia = m.tacgia;
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
                HTMLPage = HTMLPage + @"<ul class='pagination'>";
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=1" + url + "' id='pageDau'>Đầu</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + pageHTML;
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
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

        public ReturnSuKien GetListSuKien(int currPage)
        {
            try
            {
                ReturnSuKien data = new DAL.ReturnSuKien();
                List<SuKien> lstPost = new List<DAL.SuKien>();

                ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

                var Result = entity.SP_LOADALL_SUKIEN(currPage, 5, totalCount).ToList().All(m =>
                {
                    SuKien vd = new DAL.SuKien();

                    vd.id = m.id;
                    vd.ghichu = m.ghichu;
                    vd.thoigianduyet = m.thoigianduyet.Value.ToString("dd/MM/yyyy");
                    vd.tencuochop = m.tencuochop;
                    vd.noidung = m.noidung;
                    vd.diadiem = m.diadiemtochuc;
                    //  vd.diadiem = entity.m_diadiemtochuc.Where(z => z.id == m.iddiadiemtochuc).Select(z => z.tendiadiem).FirstOrDefault();
                    vd.ngaydienra = m.ngaydienra.ToString("dd-MM-yyyy");
                    vd.thoigianbatdau = m.thoigianbatdau.ToString();
                    vd.thoigianketthuc = m.thoigianketthuc.ToString();
                    vd.avatar = m.avatar;
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
                HTMLPage = HTMLPage + @"<ul class='pagination'>";
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=1" + url + "' id='pageDau'>Đầu</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + pageTr + "" + url + "' id='pageTruoc'>Trước</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + pageHTML;
                HTMLPage = HTMLPage + @"<li>";
                HTMLPage = HTMLPage + @"<a href='" + path + "?page=" + pageS + "" + url + "' id='pageSau'>Sau</a></li>";
                HTMLPage = HTMLPage + @"<li><a href='" + path + "?page=" + total + "" + url + "' id='pageCuoi'>Cuối</a>";
                HTMLPage = HTMLPage + @"</li>";
                HTMLPage = HTMLPage + @"</ul>";

                data.dataPage = HTMLPage;
                data.lstPost = lstPost;

                return data;
            }
            catch (Exception)
            {
                ReturnSuKien rt = new DAL.ReturnSuKien();
                return rt;
            }
        }

        public ListMedia ChiTietMedia(int id)
        {

            try
            {
                var Result = entity.m_media.Where(m =>  m.phienban == 1 && m.id == id).ToList().Select(m => new ListMedia
                {
                    id = m.id,
                    tenchude = m.tenchude,
                    tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value),
                    gioithieu = m.gioithieu,
                    thoigianpheduyet = m.thoigianpheduyet.Value.ToString("HH:mm dd/MM/yyyy"),
                    lstMedia = entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3).ToList().Select(a => new media
                      {
                          id = a.id,
                          trichdan =a.caption,
                          duongdanfile = a.duongdanfile
                      }).ToList()
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                ListMedia lt = new DAL.ListMedia();
                return lt;
            }

        }

        public DetailsPost ChiTietTinTuc(int id)
        {

            try
            {
                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 1 && m.id == id).ToList().Select(m => new DetailsPost
                {
                    id = m.id,
                    tieude = m.tieude,
                    gioithieu = m.gioithieu,
                    noidung = m.noidung,
                    tacgia = m.tacgia,
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

        public List<DetailsPost> ShowListTinTucLienQuan(int id)
        {
            try
            {
                List<DetailsPost> lstPost = new List<DAL.DetailsPost>();
                var Post = entity.m_baiviet.Where(z => z.id == id).Select(z => z.thoigianpheduyet.Value).FirstOrDefault();

                var Result = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 1 && m.id != id && m.thoigianpheduyet.Value <= Post).ToList().OrderByDescending(m => m.thoigianpheduyet).Take(4).All(m =>
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
                    var Result2 = entity.m_baiviet.Where(m => m.trangthai == 3 && m.phienban == 1 && m.id != id).ToList().OrderByDescending(m => m.thoigianpheduyet).Take(4).All(m =>
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


        public SuKien ChiTietsuKien(int id)
        {
            try
            {
                var Result = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 1 && m.id == id).ToList().Select(m => new SuKien
                {
                    id = m.id,
                    ghichu = m.ghichu,
                    thoigianduyet = m.thoigianduyet.Value.ToString("dd/MM/yyyy"),
                    tencuochop = m.tencuochop,
                    noidung = m.noidung,
                    // diadiem = m.m_diadiemtochuc.tendiadiem,
                    diadiem = m.diadiemtochuc,
                    ngaydienra = m.ngaydienra.ToString("dd-MM-yyyy"),
                    thoigianbatdau = m.thoigianbatdau.ToString(),
                    thoigianketthuc = m.thoigianketthuc.ToString(),
                }).FirstOrDefault();

                return Result;
            }
            catch (Exception)
            {
                SuKien dt = new DAL.SuKien();
                return dt;
            }

        }
        public List<SuKien> ShowListSuKienLienQuan(int id)
        {
            try
            {
                List<SuKien> lstPost = new List<DAL.SuKien>();
                var Post = entity.m_cuochop.Where(z => z.id == id).Select(z => z.ngaydienra).FirstOrDefault();

                var Result = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 1 & m.id != id && m.ngaydienra == Post).ToList().All(m =>
                {
                    SuKien vd = new DAL.SuKien();

                    vd.id = m.id;
                    vd.ghichu = m.ghichu;
                    vd.thoigianduyet = m.thoigianduyet.Value.ToString("dd/MM/yyyy");
                    vd.tencuochop = m.tencuochop;
                    vd.noidung = m.noidung;
                    vd.diadiem = m.diadiemtochuc;
                    vd.ngaydienra = m.ngaydienra.ToString("dd-MM-yyyy");
                    vd.thoigianbatdau = m.thoigianbatdau.ToString();
                    vd.thoigianketthuc = m.thoigianketthuc.ToString();
                    vd.avatar = m.avatar;

                    lstPost.Add(vd);

                    return true;
                });

                if (lstPost.Count == 0)
                {
                    var Result2 = entity.m_cuochop.Where(m => m.trangthai == 3 && m.phienban == 1 & m.id != id).ToList().All(m =>
                    {
                        SuKien vd = new DAL.SuKien();

                        vd.id = m.id;
                        vd.ghichu = m.ghichu;
                        vd.thoigianduyet = m.thoigianduyet.Value.ToString("dd/MM/yyyy");
                        vd.tencuochop = m.tencuochop;
                        vd.noidung = m.noidung;
                        vd.diadiem = m.diadiemtochuc;
                        vd.ngaydienra = m.ngaydienra.ToString("dd-MM-yyyy");
                        vd.thoigianbatdau = m.thoigianbatdau.ToString();
                        vd.thoigianketthuc = m.thoigianketthuc.ToString();

                        lstPost.Add(vd);

                        return true;
                    });
                }
                return lstPost;
            }
            catch (Exception)
            {
                List<SuKien> lst = new List<DAL.SuKien>();

                return lst;
            }
        }

        public m_baivietgioithieu ShowGioiThieuHoiNghi()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/gioi-thieu").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }
        public m_baivietgioithieu ShowGioiThieuAssaVN()
        {
            try
            {
                var danhsach = entity.m_baivietgioithieu.Where(m => m.url == "/assa-viet-nam").FirstOrDefault();
                return danhsach;
            }
            catch (Exception)
            {
                m_baivietgioithieu bv = new m_baivietgioithieu();
                return bv;
            }
        }

        //public List<ListMedia> GetListImage()
        //{
        //    List<ListMedia> lstMD = new List<DAL.ListMedia>();

        //    ObjectParameter totalCount = new ObjectParameter("totalCount", typeof(int));

        //    int moidang = 0, xemnhieu = 0, currPage = 1;
        //    var Result = entity.SP_LIBRARY_IMAGES(moidang, xemnhieu, currPage, 10, totalCount).ToList().All(m =>
        //    {
        //        ListMedia lst = new DAL.ListMedia();

        //        lst.id = m.id;
        //        lst.tenchude = m.tenchude;
        //        lst.tennguoitao = new AccountUntil().GetFullNameByIdTK(m.idnguoitao.Value);
        //        lst.gioithieu = m.gioithieu;
        //        lst.thoigianpheduyet = m.thoigianpheduyet.Value.ToString("dd/MM/yyyy");
        //        entity.m_mediadetails.Where(a => a.idmedia == m.id && a.trangthai == 3 && a.kieufile == "images").ToList().Select(a => new
        //        {
        //            a.id,
        //            a.duongdanfile
        //        });
        //        lstMD.Add(lst);

        //        return true;
        //    });

        //    return lstMD;
        //}
    }
}