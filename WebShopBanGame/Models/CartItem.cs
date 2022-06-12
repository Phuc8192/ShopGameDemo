using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShopBanGame.Models
{
    [Serializable]
    public class CartItem
    {
        //public SANPHAM sanpham { set; get; }
        public string MaSP { set; get; }
        public string TenSP { set; get; }
        public string Anh { set; get; }
        public int Gia { set; get; }
        public int SLuong { set; get; }
        public int ThanhTien
        {
            get
            {
                return (int)(SLuong * Gia);
            }
        }       
    }
}