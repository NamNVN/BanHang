using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;

namespace WebsiteBanDienThoai.Models
{
    public class GioHang
    {
        dbBanOnlineDataContext db = new dbBanOnlineDataContext();

        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }

      


        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int ms)
        {
            iMaSP = ms;
            

            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sAnhBia = s.AnhBia;
            dDonGia = double.Parse(s.GiaBan.ToString());

           


            iSoLuong = 1;
        }

       
    }
}