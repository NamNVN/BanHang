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
    public class TieuDeController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/TieuDe
        public ActionResult Index(int? page)
        {
            int iSize = 6;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);
            return View(db.TIEUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPagenum, iSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TIEUDE model)
        {
            if (ModelState.IsValid)
            {
                var cd = new TIEUDE();
                cd.MaCD = model.MaCD;
                cd.TenChuDe = model.TenChuDe;
                cd.MaSX = model.MaSX;
                cd.TenPhu = model.TenPhu;
                db.TIEUDEs.InsertOnSubmit(cd);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public ActionResult Details(int id)
        {
            var cd = db.TIEUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }

        public ActionResult Delete(int id)
        {
            var cd = db.TIEUDEs.SingleOrDefault(n => n.MaCD == id);
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
            var cd = db.TIEUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SANPHAMs.Where(n => n.MaCD == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Chủ đề này đã có sản phẩm trong cửa hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết sản phẩm này trong bảng sản phẩm";

                return RedirectToAction("Index", "SanPham");
            }

            db.TIEUDEs.DeleteOnSubmit(cd);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cd = db.TIEUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var cd = db.TIEUDEs.Where(n => n.MaCD == int.Parse(Request.Form["MaCD"])).SingleOrDefault();
                cd.TenChuDe = f["TenChuDe"];
                cd.MaSX = int.Parse(f["MaSX"]);
                cd.TenPhu = f["TenPhu"];
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