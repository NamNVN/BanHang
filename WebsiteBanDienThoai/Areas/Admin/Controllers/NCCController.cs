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
    public class NCCController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/NCC
        public ActionResult Index(int? page)
        {
            int iSize = 6;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);
            return View(db.NCCs.ToList().OrderBy(n => n.MaNCC).ToPagedList(iPagenum, iSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NCC sach, FormCollection f, HttpPostedFileBase fFileUpload)
        {
 
            if (fFileUpload == null)
            {

                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị
                //các thông tin này lên trang
                ViewBag.TenNCC = f["sTenNCC"];
                ViewBag.DienThoai = f["sDienThoai"];
                ViewBag.DiaChi = f["sDiaChi"];
 
                return View();
            }
           

            else
            {
                if (ModelState.IsValid)
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
   
                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
 
                    //Kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);

                    }

                    //Lưu Sach vào CSDL
                    sach.TenNCC = f["sTenNCC"];
                    sach.DiaChi = f["sDiaChi"];
                    sach.DienThoai = f["sDienThoai"];
                    sach.AnhBia = sFileName;

                    db.NCCs.InsertOnSubmit(sach);
                    db.SubmitChanges();
                    //Về lại trang Quản lý sách
                    return RedirectToAction("Index");

                }
                return View();
            }
        }







        public ActionResult Details(int id)
        {
            var cd = db.NCCs.SingleOrDefault(n => n.MaNCC == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }

        public ActionResult Delete(int id)
        {
            var cd = db.NCCs.SingleOrDefault(n => n.MaNCC == id);
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
            var cd = db.NCCs.SingleOrDefault(n => n.MaNCC == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SANPHAMs.Where(n => n.MaNCC == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Nhà cung cấp này đã có sản phẩm trong cửa hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết sản phẩm này trong bảng sản phẩm";

                return RedirectToAction("Index", "NCC");
            }

            db.NCCs.DeleteOnSubmit(cd);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sach = db.NCCs.SingleOrDefault(n => n.MaNCC == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           

            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var sach = db.NCCs.SingleOrDefault(n => n.MaNCC == int.Parse(f["iMaNCC"]));
          

            if (ModelState.IsValid)
            {
                if (fFileUpload != null) //Kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    //Kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sach.AnhBia = sFileName;
                }

               
                //Lưu Sach vào CSDL
                sach.TenNCC = f["sTenNCC"];
                sach.DiaChi = f["sDiaChi"];
                sach.DienThoai = f["sDienThoai"];
               

                db.SubmitChanges();
                //Về lại trang Quản lý sách
                return RedirectToAction("Index");
            }
            return View(sach);
        }


    }

    
}