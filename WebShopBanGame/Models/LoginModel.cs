using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebShopBanGame.Models
{
    public class LoginModel
    {
        [Key]

        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string TenDN { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MK { set; get; }
    }
}