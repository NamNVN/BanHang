using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;

namespace WebsiteBanDienThoai.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            //Gán giá trị cho đối tượng được tạo mới (ad)
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
              
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }


        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }



        public ActionResult Group()
        {
            //var kq = from s in db.SACHes group s by s.MaCD;
            var kq = db.SANPHAMs.GroupBy(s => s.MaCD);

            return View(kq);
        }
        public ActionResult ThongKe()
        {

            /*var kq = from s in db.SANPHAMs
                     group s by  s.MaCD into g
                     select new ReportInfo
                     {
                         Id = g.Key.ToString(),
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };*/
            var kq = from s in db.SANPHAMs
                     join cd in db.TIEUDEs on s.MaCD equals cd.MaCD
                     group s by new { cd.MaCD, cd.TenChuDe } into g
                     select new ReportInfo
                     {
                         Id = g.Key.MaCD.ToString(),
                         Name = g.Key.TenChuDe,
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };

            return View(kq);
        }






    }
}