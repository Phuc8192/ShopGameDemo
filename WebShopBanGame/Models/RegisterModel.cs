using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopBanGame.Models
{
    public partial class RegisterModel
    {              

        [Key]
        public string MaKH { set; get; }

        [Required(ErrorMessage ="Yêu cầu nhập tên của bạn")]
        public string TenKH { set; get; }

        

        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string TenDN { set; get; }      

        [StringLength(24,MinimumLength = 6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]        
        public string MK { set; get; }

        [Compare("MK",ErrorMessage ="Xác nhận mật khẩu không đúng.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yêu cầu nhập xác nhận mật khẩu")]        
        public string XNhanMK { set; get; }

        
    }
}