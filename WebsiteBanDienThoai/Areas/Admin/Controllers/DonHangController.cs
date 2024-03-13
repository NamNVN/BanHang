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
    public class DonHangController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            int iSize = 6;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPagenum, iSize));
        }
        public ActionResult Details1(int id)
        {
            var ddh = db.CHITIETDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        public ActionResult Delete(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaSP == id);
            if (ctdh.Count() > 0)
            {
                //Nội dung sẽ hiển thị khi sách cần xóa đã có trong table ChiTietDonHang
                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết đặt hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết mã sản phẩm này trong bảng Chi tiết đặt hàng";
                return RedirectToAction("Index", "Sach");
            }

            db.DONDATHANGs.DeleteOnSubmit(ddh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
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
                var ddh = db.DONDATHANGs.Where(n => n.MaDonHang == int.Parse(Request.Form["MaDonHang"])).SingleOrDefault();
                ddh.DaThanhToan = bool.Parse(f["DaThanhToan"]);
                ddh.NgayDat = Convert.ToDateTime(f["NgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["NgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);


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