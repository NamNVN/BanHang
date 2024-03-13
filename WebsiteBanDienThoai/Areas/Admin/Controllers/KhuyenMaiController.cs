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
    public class KhuyenMaiController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iSize = 7;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);

            return View(db.KHUYENMAIs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPagenum, iSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHUYENMAI model)
        {
            if (ModelState.IsValid)
            {
                var kh = new KHUYENMAI();
                kh.MaSP = model.MaSP;
                kh.MoTa = model.MoTa;
                kh.GiaBan = model.GiaBan;
                kh.PhanTram = model.PhanTram;
                kh.NgayBD = model.NgayKT;
                kh.NgayKT = model.NgayKT;
               

                db.KHUYENMAIs.InsertOnSubmit(kh);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            var cd = db.KHUYENMAIs.SingleOrDefault(n => n.MaSP == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var cd = db.KHUYENMAIs.SingleOrDefault(n => n.MaSP == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           

            db.KHUYENMAIs.DeleteOnSubmit(cd);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.KHUYENMAIs.SingleOrDefault(n => n.MaSP == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var sach = db.KHUYENMAIs.Where(n => n.MaSP == int.Parse(Request.Form["MaSP"])).SingleOrDefault();
                sach.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                sach.GiaBan = decimal.Parse(f["mGiaBan"]);
                sach.PhanTram = f["sPhanTram"];
                
                sach.NgayBD = Convert.ToDateTime(f["dNgayBD"]);
                sach.NgayKT = Convert.ToDateTime(f["dNgayKT"]);



                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
    }
}