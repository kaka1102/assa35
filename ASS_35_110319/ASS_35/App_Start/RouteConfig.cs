using DataContext.Until;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASS_35
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("login");
             routes.IgnoreRoute("admin");

            routes.MapRoute(
                            name: "Practical",
                            url: "practical",
                            defaults: new
                            {
                                controller = "Practical",
                                action = "ChangePractical"
                            }
                        );


            routes.MapRoute(
                            name: "Contact",
                            url: "contact",
                            defaults: new
                            {
                                controller = "Contact",
                                action = "Index"
                            }
                        );

            routes.MapRoute(
                                name: "detail-new",
                                url: "detail-new/{name}-{id}",
                                defaults: new
                                {
                                    controller = "DocumentEn",
                                    action = "DetailsNew",
                                    name = UrlParameter.Optional
                                }
                            );

            routes.MapRoute(
                           name: "document",
                           url: "document",
                           defaults: new
                           {
                               controller = "DocumentEn",
                               action = "Change"
                           }
                       );
            routes.MapRoute(
                               name: "NewsAndEvent",
                               url: "news-and-events",
                               defaults: new
                               {
                                   controller = "DocumentEn",
                                   action = "NewsAndEvents"
                               }
                           );

            routes.MapRoute(
                       name: "Program",
                       url: "program",
                       defaults: new
                       {
                           controller = "Programme",
                           action = "Index",
                       }
                   );

            routes.MapRoute(
                         name: "HoiNghi",
                         url: "gioi-thieu",
                         defaults: new
                         {
                             controller = "HoiNghi",
                             action = "Index",
                         }
                     );

            routes.MapRoute(
                         name: "ASSAVN",
                         url: "assa-viet-nam",
                         defaults: new
                         {
                             controller = "HoiNghi",
                             action = "AssaVN",
                         }
                     );

            routes.MapRoute(
                         name: "Overview",
                         url: "general-introduction",
                         defaults: new
                         {
                             controller = "Overview",
                             action = "Overview",
                         }
                     );

            routes.MapRoute(
                       name: "IntroAssaVN",
                       url: "assa-vn",
                       defaults: new
                       {
                           controller = "Overview",
                           action = "AssaVietNam",
                       }
                   );

            routes.MapRoute(
                            name: "chitietsukien",
                            url: "chi-tiet-su-kien/{name}-{id}",
                            defaults: new
                            {
                                controller = "TinTucSuKien",
                                action = "ChiTietSuKien",
                                name = UrlParameter.Optional
                            }
                        );

            routes.MapRoute(
                             name: "Lib-Video",
                             url: "lib-video/{name}-{id}",
                             defaults: new
                             {
                                 controller = "VideoHinhAnh",
                                 action = "ThuVienVideoChiTiet",
                                 name = UrlParameter.Optional
                             }
                         );

            routes.MapRoute(
                               name: "Lib-Image",
                               url: "lib-image/{name}-{id}",
                               defaults: new
                               {
                                   controller = "VideoHinhAnh",
                                   action = "ThuVienAnhChiTiet",
                                   name = UrlParameter.Optional
                               }
                           );

            routes.MapRoute(
                                 name: "chi-tiet",
                                 url: "chi-tiet/{name}-{id}",
                                 defaults: new
                                 {
                                     controller = "TinTucSuKien",
                                     action = "ChiTietTinTuc",
                                     name = UrlParameter.Optional
                                 }
                             );

            routes.MapRoute(
                            name: "TimKiem",
                            url: "tim-kiem",
                            defaults: new
                            {
                                controller = "Search",
                                action = "Index"
                            }
                        );

            routes.MapRoute(
                              name: "LienHe",
                              url: "lien-he",
                              defaults: new
                              {
                                  controller = "LienHe",
                                  action = "Index"
                              }
                          );

            routes.MapRoute(
                             name: "DiaDiem",
                             url: "dia-diem",
                             defaults: new
                             {
                                 controller = "DiaDiem",
                                 action = "Index"
                             }
                         );

            routes.MapRoute(
                          name: "diadiemchitiet",
                          url: "dia-diem-chi-tiet/{name}-{id}",
                          defaults: new
                          {
                              controller = "DiaDiem",
                              action = "ChiTietDiaDiem",
                              name = UrlParameter.Optional
                          }
                      );

            routes.MapRoute(
                                name: "Tintuc",
                                url: "tin-tuc",
                                defaults: new
                                {
                                    controller = "TinTucSuKien",
                                    action = "TinTuc"
                                }
                            );
            

            routes.MapRoute(
                              name: "Sukien",
                              url: "su-kien",
                              defaults: new
                              {
                                  controller = "TinTucSuKien",
                                  action = "SuKien"
                              }
                          );


            routes.MapRoute(
                              name: "Video",
                              url: "video",
                              defaults: new
                              {
                                  controller = "VideoHinhAnh",
                                  action = "Video"
                              }
                          );


            routes.MapRoute(
                             name: "HinhAnh",
                             url: "hinh-anh",
                             defaults: new
                             {
                                 controller = "VideoHinhAnh",
                                 action = "Index"
                             }
                         );



            routes.MapRoute(
                              name: "IndexEn",
                              url: "",
                              defaults: new
                              {
                                  controller = "HomeEn",
                                  action = "Index"
                              }
                          );
            routes.MapRoute(
                              name: "IndexVi",
                              url: "",
                              defaults: new
                              {
                                  controller = "HomeVi",
                                  action = "Index"
                              }
                          );

            routes.MapRoute(
                              name: "vi",
                              url: "vi",
                              defaults: new
                              {
                                  controller = "LoadMenuClient",
                                  action = "vi"
                              }
                          );
            routes.MapRoute(
                              name: "en",
                              url: "en",
                              defaults: new
                              {
                                  controller = "LoadMenuClient",
                                  action = "en"
                              }
                          );

            routes.MapRoute(
                               name: "trangchu",
                               url: "trang-chu",
                               defaults: new
                               {
                                   controller = "HomeVi",
                                   action = "Index"
                               }
                           );
            routes.MapRoute(
                              name: "HomeEn",
                              url: "home",
                              defaults: new
                              {
                                  controller = "HomeEn",
                                  action = "Index"
                              }
                          );
            routes.MapRoute(
                name: "ConfirmEmail",
                url: "ConfirmEmail",
                defaults: new
                {
                    controller = "Registration",
                    action = "ConfirmEmail"
                }

            );

            routes.MapRoute(
                                name: "Default",
                                url: "{controller}/{action}/{id}",
                                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                            );

        }
    }
}