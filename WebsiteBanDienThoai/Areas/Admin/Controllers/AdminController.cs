using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebsiteBanDienThoai.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            int iSize = 7;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);

            return View(db.ADMINs.ToList().OrderBy(n => n.MaAdmin).ToPagedList(iPagenum, iSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ADMIN model)
        {
            if (ModelState.IsValid)
            {
                var kh = new ADMIN();
                kh.HoTen = model.HoTen;
                kh.TenDN = model.TenDN;
                kh.MatKhau = model.MatKhau;
                kh.Email = model.Email;
                kh.DiaChi = model.DiaChi;
                kh.DienThoai = model.DienThoai;
                kh.NgaySinh = model.NgaySinh;
                kh.Quyen = model.Quyen;

                db.ADMINs.InsertOnSubmit(kh);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAdmin == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var kh = db.ADMINs.Where(n => n.MaAdmin == int.Parse(Request.Form["MaAdmin"])).SingleOrDefault();
                kh.HoTen = f["HoTen"];
                kh.TenDN = f["TenDN"];
                kh.MatKhau = f["MatKhau"];
                kh.Email = f["Email"];
                kh.DiaChi = f["DiaChi"];
                kh.DienThoai = f["DienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["NgaySinh"]);
                kh.Quyen = int.Parse(f["Quyen"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAdmin == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAdmin == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           

            db.ADMINs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var kh = db.ADMINs.SingleOrDefault(n => n.MaAdmin == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
    }
}