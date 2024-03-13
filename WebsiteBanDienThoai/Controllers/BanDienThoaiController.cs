using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace WebsiteBanDienThoai.Controllers
{
    public class BanDienThoaiController : Controller
    {
        dbBanOnlineDataContext data = new dbBanOnlineDataContext();
        // GET: BanDienThoai
        public ActionResult ThongTin()
        {
            return View();
        }

        public ActionResult ChitietHD(int id)
        {
            var sach = data.CHITIETDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);

        }

        public ActionResult TrangChu()
        {
            var listSachmoi = LaySachMoi(6);

            return View(listSachmoi);
        }

        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("TrangChu");
        }


        private List<SANPHAM> LaySachMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

      /*  private List<DIENTHOAINOIBAT> LaySachBan(int count)
        {
            return data.DIENTHOAINOIBATs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }

        private List<KHUYENMAI> LaySachBan1(int count)
        {
            return data.KHUYENMAIs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }


        private List<PHUKIENNOIBAT> LaySachBan2(int count)
        {
            return data.PHUKIENNOIBATs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }*/
        

        public ActionResult KHUYENMAIpartial()
        {
            var sp = data.SANPHAMs.Where(n => n.MaKM == 1).Take(4).ToList();
            return PartialView(sp);
        }


        public ActionResult SPNBpartial()
        {
            var sp = data.SANPHAMs.Where(n => n.MaNCC == 2).Take(4).ToList();
            return PartialView(sp);
        }

        public ActionResult PKNBpartial()
        {
            var pk = data.SANPHAMs.Where(n => n.MaNCC == 3).Take(4).ToList();
            return PartialView(pk);
        }

        public ActionResult KMSPpartial()
        {
           /* var km = data.SANPHAMs.Where(n => n.MaCD == 1).Take(4).ToList();
            return PartialView(km);*/

            var listSach = LaySachMoi(4);
            return PartialView(listSach);
        }

        public ActionResult slide()
        {
            var sl = data.SANPHAMs.Where(n => n.MaCD == 1).Take(4).ToList();
            return PartialView(sl);
        }





        public ActionResult ChiTietSP(int id)
        {
            /*var sach = from s in data.SANPHAMs where s.MaSP == id select s;*/

            var sach = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);

            /*return View(sach.Single());*/

            return View(sach);

        }

        /*public ActionResult ChiTietKM(int id)
        {
            var sach = from s in data.KHUYENMAIs where s.MaKM == id select s;
            return View(sach.Single());
        }

        public ActionResult ChiTietNB(int id)
        {
            var sach = from s in data.DIENTHOAINOIBATs where s.MaNB == id select s;
            return View(sach.Single());
        }

        public ActionResult ChiTietPK(int id)
        {
            var sach = from s in data.PHUKIENNOIBATs where s.MaPK == id select s;
            return View(sach.Single());
        }*/



        public ActionResult DanhMucPartial()
        {
           /* var listChuDe = from cd in data.TIEUDEs select cd;
            return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 1).ToList();
            return PartialView(listChuDe);
        }

        public ActionResult DanhMuc1Partial()
        {
            /* var listChuDe = from cd in data.TIEUDEs select cd;
             return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 2).ToList();
            return PartialView(listChuDe);
        }

        public ActionResult DanhMuc2Partial()
        {
            /* var listChuDe = from cd in data.TIEUDEs select cd;
             return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 3).ToList();
            return PartialView(listChuDe);
        }


        public ActionResult DanhMuc3Partial()
        {
            /* var listChuDe = from cd in data.TIEUDEs select cd;
             return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 4).ToList();
            return PartialView(listChuDe);
        }

        public ActionResult DanhMuc4Partial()
        {
            /* var listChuDe = from cd in data.TIEUDEs select cd;
             return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 5).ToList();
            return PartialView(listChuDe);
        }

        public ActionResult DanhMuc5Partial()
        {
            /* var listChuDe = from cd in data.TIEUDEs select cd;
             return PartialView(listChuDe);*/
            var listChuDe = data.TIEUDEs.Where(n => n.MaSX == 6).ToList();
            return PartialView(listChuDe);
        }










        public ActionResult DanhMucKPartial()
        {
            var listChuDe = from cd in data.NCCs select cd;
            return PartialView(listChuDe);
        }

        public ActionResult Danhmucsanpham(int iMaCD, int? page)
        {
            ViewBag.MaCD = iMaCD;
           



            var sach = from s in data.SANPHAMs where s.MaCD == iMaCD select s;
            


            //Tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 8;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            // var sach = from s in data.SANPHAMs where s.MaCD == iMaCD select s;

            return View(sach.ToPagedList(iPageNum, iSize));

        }

        public ActionResult DanhmucsanphamNCC(int iMaNCC, int? page)
        {
            ViewBag.MaNCC = iMaNCC;




            var sach = from s in data.SANPHAMs where s.MaNCC == iMaNCC select s;



            //Tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 8;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            // var sach = from s in data.SANPHAMs where s.MaCD == iMaCD select s;

            return View(sach.ToPagedList(iPageNum, iSize));

        }


        public ActionResult Danhmuckhuyenmai()
        {
            var pk = data.SANPHAMs.Where(n => n.MaKM == 1).ToList();
            return PartialView(pk);

        }






        /*  public ActionResult KMPartial()
          {
              var listSach = LaySachBan1(6);
              return PartialView(listSach);
          }

          public ActionResult NBPartial()
          {
              var listSachBan = LaySachBan(6);
              return PartialView(listSachBan);
          }


          public ActionResult PKPartial()
          {
              var listSachBan = LaySachBan2(6);
              return PartialView(listSachBan);
          }
  */

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }

        public ActionResult DangKy()
        {
            return RedirectToAction("DangNhap", "User");
        }

        public ActionResult Sapxep(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.GiaSortParm = sortOrder == "gia" ? "gia_desc" : "gia";
            ViewBag.GiaTSortParm = sortOrder == "gia" ? "gia_descc" : "gia";
            var models = data.SANPHAMs.AsQueryable();




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
            return View(models.ToList());

        }







    }
}