using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity;

namespace WebsiteBanDienThoai.Controllers
{
    public class SearchController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Search
       

       /* public ActionResult Index(string sortOrder)
        {
            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.TenSortParm = sortOrder == "ten" ? "ten_desc" : "ten";
            var models = db.SANPHAMs.AsQueryable();
            switch (sortOrder)
            {
                case "ma_desc":
                    models = models.OrderByDescending(s => s.MaSP);
                    break;
                case "ten":
                    models = models.OrderBy(s => s.TenSP);
                    break;
                case "ten_desc":
                    models = models.OrderByDescending(s => s.TenSP);
                    break;
                default:
                    models = models.OrderBy(s => s.MaSP);
                    break;
            }
            return View(models.ToList());
        }*/
        public ActionResult Search(string strSearch, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
          
            ViewBag.Search = strSearch;
            ViewBag.TenSortParm = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.GiaSortParm = sortOrder == "gia" ? "gia_desc" : "gia";
            ViewBag.GiaTSortParm = sortOrder == "gia" ? "gia_descc" : "gia";

           
            var models = db.SANPHAMs.AsQueryable();

            if (strSearch != null)
            {
                page = 1;
            }
            else
            {
                strSearch = currentFilter;
            }

            ViewBag.CurrentFilter = strSearch;

            if (!string.IsNullOrEmpty(strSearch))
            {
                //var kq = from s in db.SACHes where s.TenSach.Contains(strSearch) select s;
                //var kq = from s in db.SACHes where s.MaCD == int.Parse(strSearch) select s;
                //var kq = from s in db.SACHes where s.MaCD,Contains(strSearch) select s;
                //var kq = from s in db.SACHes where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 orderby s.SoLuongBan ascending select s;
                //var kq = from s in db.SANPHAMs where s.SoLuongBan>int.Parse(strSearch) orderby s.SoLuongBan ascending select s;
                //var kq = from s in db.SANPHAMs where s.TenSP.Contains(strSearch) || s.MoTa.Contains(strSearch) select s; //có nghĩa chỉ cần s.TenSach có trong chuỗi người dùng nhập
                //return View(kq);
                models = models.Where(s => s.TenSP.Contains(strSearch) || s.MoTa.Contains(strSearch));

            }

           
            switch (sortOrder)
            {
                case "ten_desc":
                    models = models.OrderByDescending(s => s.TenSP);
                    break;
                case "gia":
                    models = models.OrderBy(s => s.GiaBan);
                    break;
                case "gia_descc":
                    models = models.OrderByDescending(s => s.GiaBan);
                    break;
                default:
                    models = models.OrderBy(s => s.TenSP);
                    break;
            }

            



            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(models.ToPagedList(pageNumber, pageSize));



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