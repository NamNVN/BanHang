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
    public class SanPhamController : Controller
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iSize = 7;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);

            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPagenum, iSize));
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenPhu");
            ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload1, HttpPostedFileBase fFileUpload2, HttpPostedFileBase fFileUpload3)
        {
            //Đưa dữ liệu vào DropDown
            ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenPhu");

            ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");



            if (fFileUpload == null)
            {
                
                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị
                //các thông tin này lên trang
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.MauSac = f["sMauSac"];
                ViewBag.KichThuoc = f["sKichThuoc"];
                ViewBag.LuotXem = int.Parse(f["iLuotXem"]);
                ViewBag.DanhGia = int.Parse(f["iDanhGia"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.Gioitinh = f["sGioitinh"];
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", int.Parse(f["MaNCC"]));
                ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaKM = int.Parse(f["MaKM"]);
                

                
                return View();
            }
            if (fFileUpload1 == null)
            {

                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị
                //các thông tin này lên trang
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.MauSac = f["sMauSac"];
                ViewBag.KichThuoc = f["sKichThuoc"];
                ViewBag.LuotXem = int.Parse(f["iLuotXem"]);
                ViewBag.DanhGia = int.Parse(f["iDanhGia"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.Gioitinh = f["sGioitinh"];
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", int.Parse(f["MaNCC"]));
                ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaKM = int.Parse(f["MaKM"]);



                return View();
            }
            if (fFileUpload2 == null)
            {

                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị
                //các thông tin này lên trang
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.MauSac = f["sMauSac"];
                ViewBag.KichThuoc = f["sKichThuoc"];
                ViewBag.LuotXem = int.Parse(f["iLuotXem"]);
                ViewBag.DanhGia = int.Parse(f["iDanhGia"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.Gioitinh = f["sGioitinh"];
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", int.Parse(f["MaNCC"]));
                ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaKM = int.Parse(f["MaKM"]);



                return View();
            }
            if (fFileUpload3 == null)
            {

                //Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị
                //các thông tin này lên trang
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.MauSac = f["sMauSac"];
                ViewBag.KichThuoc = f["sKichThuoc"];
                ViewBag.LuotXem = int.Parse(f["iLuotXem"]);
                ViewBag.DanhGia = int.Parse(f["iDanhGia"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.Gioitinh = f["sGioitinh"];
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", int.Parse(f["MaNCC"]));
                ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaKM = int.Parse(f["MaKM"]);



                return View();
            }

            else
            {
                if (ModelState.IsValid)
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var sFileName1 = Path.GetFileName(fFileUpload1.FileName);
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    var sFileName3 = Path.GetFileName(fFileUpload3.FileName);


                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    var path1 = Path.Combine(Server.MapPath("~/Images"), sFileName1);
                    var path2 = Path.Combine(Server.MapPath("~/Images"), sFileName2);
                    var path3 = Path.Combine(Server.MapPath("~/Images"), sFileName3);


                    //Kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                        
                    }
                 /*   if (!System.IO.File.Exists(path1))
                    {
                        fFileUpload1.SaveAs(path1);

                    }
                    if (!System.IO.File.Exists(path2))
                    {
                        fFileUpload2.SaveAs(path2);

                    }
                    if (!System.IO.File.Exists(path3))
                    {
                        fFileUpload3.SaveAs(path3);

                    }*/



                    //Lưu Sach vào CSDL
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sp.AnhBia = sFileName;
                    sp.AnhBia1 = sFileName1;
                    sp.AnhBia2 = sFileName2;
                    sp.AnhBia3 = sFileName3;
                    sp.MauSac = f["sMauSac"];
                    sp.KichThuoc = f["sKichThuoc"];
                    sp.LuotXem = int.Parse(f["iLuotXem"]);
                    sp.DanhGia = int.Parse(f["iDanhGia"]);
                    sp.Gioitinh = f["sGioitinh"];
                    sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sp.MaCD = int.Parse(f["MaCD"]);
                    sp.MaNCC = int.Parse(f["MaNCC"]);
                    sp.MaKM = int.Parse(f["MaKM"]);


                    db.SANPHAMs.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    //Về lại trang Quản lý sách
                    return RedirectToAction("Index");

                }
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaSP == id);
            if (ctdh.Count() > 0)
            {
                //Nội dung sẽ hiển thị khi sản phẩm cần xóa đã có trong table ChiTietDonHang
                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết đặt hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết mã sản phẩm này trong bảng Chi tiết đặt hàng";
                return View(sach);
            }
            //Xóa hết thông tin của sản phẩm trong table NCC trước khi xóa sách này
            var ncc = db.NCCs.Where(vs => vs.MaNCC == id).ToList();
            if (ncc != null)
            {
                db.NCCs.DeleteAllOnSubmit(ncc);
            }
            db.SubmitChanges();

            //Xóa sách
            db.SANPHAMs.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Hiển thị danh sách chủ đề và nhà xuất bản đồng thời chọn chủ đề và nhà xuất bản của cuốn hiện tại
            ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe),"MaCD", "TenPhu", sach.MaCD);
            ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sach.MaNCC);

            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload1, HttpPostedFileBase fFileUpload2, HttpPostedFileBase fFileUpload3)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            ViewBag.MaCD = new SelectList(db.TIEUDEs.ToList().OrderBy(n => n.TenChuDe),"MaCD", "TenPhu", sach.MaCD);
            ViewBag.MaNCC = new SelectList(db.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",sach.MaNCC);

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

                if (fFileUpload1 != null) //Kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName1 = Path.GetFileName(fFileUpload1.FileName);
                    //Lấy đường dẫn lưu file
                    var path1 = Path.Combine(Server.MapPath("~/Images"), sFileName1);
                    //Kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path1))
                    {
                        fFileUpload1.SaveAs(path1);
                    }
                    sach.AnhBia1 = sFileName1;
                }

                if (fFileUpload2 != null) //Kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    //Lấy đường dẫn lưu file
                    var path2 = Path.Combine(Server.MapPath("~/Images"), sFileName2);
                    //Kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path2))
                    {
                        fFileUpload2.SaveAs(path2);
                    }
                    sach.AnhBia2 = sFileName2;
                }

                if (fFileUpload3 != null) //Kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName3 = Path.GetFileName(fFileUpload3.FileName);
                    //Lấy đường dẫn lưu file
                    var path3 = Path.Combine(Server.MapPath("~/Images"), sFileName3);
                    //Kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path3))
                    {
                        fFileUpload3.SaveAs(path3);
                    }
                    sach.AnhBia3 = sFileName3;
                }
                //Lưu Sach vào CSDL
                sach.TenSP = f["sTenSP"];
                sach.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
              
                sach.MauSac = f["sMauSac"];
                sach.KichThuoc = f["sKichThuoc"];
                sach.LuotXem = int.Parse(f["iLuotXem"]);
                sach.DanhGia = int.Parse(f["iDanhGia"]);
                sach.Gioitinh = f["sGioitinh"];
                sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sach.SoLuongBan = int.Parse(f["iSoLuong"]);
                sach.GiaBan = decimal.Parse(f["mGiaBan"]);
                sach.MaCD = int.Parse(f["MaCD"]);
                sach.MaNCC = int.Parse(f["MaNCC"]);
                sach.MaKM = int.Parse(f["MaKM"]);

                db.SubmitChanges();
                //Về lại trang Quản lý sách
                return RedirectToAction("Index");
            }
            return View(sach);
        }
    }
}