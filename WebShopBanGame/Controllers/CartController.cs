using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebShopBanGame.Models;
using WebShopBanGame.MoMo;
using Newtonsoft.Json.Linq;

namespace WebShopBanGame.Controllers
{
    public class CartController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();       
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            return View(cart);            
        }

        string LayMaHD()
        {
            var maMax = db.HOADONs.ToList().Select(n => n.MaHD).Max();
            int maHD = int.Parse(maMax.Substring(2)) + 1;
            string HD = String.Concat("00", maHD.ToString());
            return "HD" + HD.Substring(maHD.ToString().Length - 1);
        }

        public RedirectToRouteResult ThemVaoGio(string MaSP)
        {
                       
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<CartItem>();
            }

            List<CartItem> cart = Session["cart"] as List<CartItem>;           

            if (cart.FirstOrDefault(x => x.MaSP == MaSP) == null)
            {
                SANPHAM sp = db.SANPHAMs.Find(MaSP);
                var newItem = new CartItem();
                newItem.MaSP = MaSP;
                newItem.TenSP = sp.TenSP;
                newItem.Anh = sp.Anh1;
                newItem.SLuong = 1;
                //Session["SL"] = newItem.SLuong;
                newItem.Gia = (int)sp.Gia;
                cart.Add(newItem);
                //try
                //{

                //}
                //catch (DbEntityValidationException ex)
                //{
                //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                //    {
                //        foreach (var validationError in entityValidationErrors.ValidationErrors)
                //        {
                //            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                //        }
                //    }
                //}

            }
            else
            {
                CartItem cardItem = cart.FirstOrDefault(x => x.MaSP == MaSP);
                cardItem.SLuong++;
                Session["SL"] = cardItem.SLuong;
            }

            return RedirectToAction("Index", "Home");
        }

        public RedirectToRouteResult SuaSoLuong(string MaSP, int SLuongMoi)
        {                     
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem itemSua = cart.FirstOrDefault(x => x.MaSP == MaSP);

            if(itemSua != null)
            {
                itemSua.SLuong = SLuongMoi;
                Session["SL"] = itemSua.SLuong;
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult XoaKhoiGio(string MaSP)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem itemXoa = cart.FirstOrDefault(x => x.MaSP == MaSP);

            if (itemXoa != null)
            {
                cart.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        
        [ChildActionOnly]
        public PartialViewResult _CartPartial()
        {
            //List<CartItem> cart = Session["cart"] as List<CartItem>;         
            //return PartialView(cart);
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<CartItem>();
            }

            List<CartItem> cart = Session["cart"] as List<CartItem>;

            return PartialView(cart);
        }


        [ChildActionOnly]
        public PartialViewResult _ThanhToanPartial()
        {
            return PartialView("_ThanhToanPartial");
        }
        //public ActionResult ThanhToan()
        //{
        //    List<CartItem> cart = Session["cart"] as List<CartItem>;
        //    return View(cart);
        //}
        public ActionResult ThanhToan(string MaKH)
        {
            try
            {
                List<CartItem> cart = Session["cart"] as List<CartItem>;
                HOADON _hoadon = new HOADON();
                _hoadon.MaHD = LayMaHD();               
                _hoadon.NgayBan = DateTime.Now;
                _hoadon.MaKH = MaKH;

                _hoadon.TrangThai = false;

                db.HOADONs.Add(_hoadon);
                Session["MaHD"] = _hoadon.MaHD;
                Session["MaHDC"] = _hoadon.MaHD;
                foreach (var item in cart)
                {
                    CHITIETHOADON _cthd = new CHITIETHOADON();
                    _cthd.MaHD = _hoadon.MaHD;
                    _cthd.MaSP = item.MaSP;
                    _cthd.SL = item.SLuong;
                    _cthd.Gia = item.Gia;

                    db.CHITIETHOADONs.Add(_cthd);

                    DSCDKEY _dscdk = new DSCDKEY();
                    _dscdk.MaHD = _hoadon.MaHD;
                    _dscdk.MaSP = item.MaSP;
                    _dscdk.CDKey = CDKEY(16);

                    db.DSCDKEYs.Add(_dscdk);
                }

                db.SaveChanges();
                return RedirectToAction("PaymentWithMomo", "Payment");
                cart.Clear();
                Session.Remove("MaHD");
                //return RedirectToAction("Details", "HOADONs", new { id = _hoadon.MaHD });
                return RedirectToAction("Message", new { mess = "Thanh toán thành công" });
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                return Content("Lỗi");
            }
        }

        public ActionResult Message(string mess)
        {
            ViewBag.Message = mess;
            return View();
        }

        public static string CDKEY(int numberRD)
        {
            string randomStr = "";
            try
            {

                string[] myIntArray = new string[numberRD];
                int x;
                //that is to create the random # and add it to uour string
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }


        //public ActionResult XoaHD(string id)
        //{
        //    List<CHITIETHOADON> _cTHD = db.CHITIETHOADONs.Where(x => x.MaHD == id).ToList();
        //    db.CHITIETHOADONs.Remove(_cTHD.ToList());
        //    return View();
        //}


        //public ActionResult ThanhToan(string MaKH)
        //{
        //    try
        //    {
        //        List<CartItem> cart = Session["cart"] as List<CartItem>;
        //        HOADON _hoadon = new HOADON();
        //        _hoadon.MaHD = LayMaHD();
        //        Session["MaHD"] = _hoadon.MaHD;
        //        _hoadon.NgayBan = DateTime.Now;
        //        _hoadon.MaKH = MaKH;
        //        _hoadon.TrangThai = false;

        //        db.HOADONs.Add(_hoadon);
        //        foreach (var item in cart)
        //        {
        //            CHITIETHOADON _cthd = new CHITIETHOADON();
        //            _cthd.MaHD = _hoadon.MaHD;
        //            _cthd.MaSP = item.MaSP;
        //            _cthd.SL = item.SLuong;
        //            _cthd.Gia = item.Gia;

        //            db.CHITIETHOADONs.Add(_cthd);

        //            DSCDKEY _dscdk = new DSCDKEY();
        //            _dscdk.MaHD = _hoadon.MaHD;
        //            _dscdk.MaSP = item.MaSP;
        //            _dscdk.CDKey = CDKEY(16);

        //            db.DSCDKEYs.Add(_dscdk);
        //        }

        //        db.SaveChanges();
        //        cart.Clear();

        //        string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
        //        string partnerCode = "MOMOO4DT20220605";
        //        string accessKey = "qcGi0nwvXhzQ09J2";
        //        string serectkey = "eak52lxTAqOHELgIUdcB2LWX342N3MU8";
        //        string orderInfo = "Thanh toán hóa đơn";
        //        string returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
        //        string notifyurl = ConfigurationManager.AppSettings["notifyUrl"].ToString();
        //        //string returnUrl = "https://localhost:44394/Home/ConfirmPaymentClient";
        //        //string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

        //        string amount = Convert.ToInt32(cart.Sum(x => x.Gia * x.SLuong)).ToString(); ;
        //        string orderid = _hoadon.MaHD;
        //        string requestId = _hoadon.MaKH;
        //        string extraData = "";

        //        //Before sign HMAC SHA256 signature
        //        string rawHash = "partnerCode=" +
        //            partnerCode + "&accessKey=" +
        //            accessKey + "&requestId=" +
        //            requestId + "&amount=" +
        //            amount + "&orderId=" +
        //            orderid + "&orderInfo=" +
        //            orderInfo + "&returnUrl=" +
        //            returnUrl + "&notifyUrl=" +
        //            notifyurl + "&extraData=" +
        //            extraData;

        //        MoMoSecurity crypto = new MoMoSecurity();
        //        //sign signature SHA256
        //        string signature = crypto.signSHA256(rawHash, serectkey);

        //        //build body json request
        //        JObject message = new JObject
        //        {
        //        { "partnerCode", partnerCode },
        //        { "accessKey", accessKey },
        //        { "requestId", requestId },
        //        { "amount", amount },
        //        { "orderId", orderid },
        //        { "orderInfo", orderInfo },
        //        { "returnUrl", returnUrl },
        //        { "notifyUrl", notifyurl },
        //        { "extraData", extraData },
        //        { "requestType", "captureMoMoWallet" },
        //        { "signature", signature }

        //        };

        //        string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

        //        JObject jmessage = JObject.Parse(responseFromMomo);

        //        return Redirect(jmessage.GetValue("payUrl").ToString());

        //        //return RedirectToAction("Details", "HOADONs", new { id = _hoadon.MaHD });
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in entityValidationErrors.ValidationErrors)
        //            {
        //                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //            }
        //        }
        //        return Content("Lỗi");
        //    }
        //}

        //public ActionResult Payment()
        //{
        //    //request params need to request to MoMo system
        //    string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
        //    string partnerCode = "MOMOO4DT20220605";
        //    string accessKey = "qcGi0nwvXhzQ09J2";
        //    string serectkey = "eak52lxTAqOHELgIUdcB2LWX342N3MU8";
        //    string orderInfo = "Thanh toán hóa đơn";
        //    string returnUrl = "https://localhost:44394/Home/ConfirmPaymentClient";
        //    string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

        //    string amount = "1000";
        //    string orderid = LayMaHD();
        //    string requestId = DateTime.Now.Ticks.ToString();
        //    string extraData = "";

        //    //Before sign HMAC SHA256 signature
        //    string rawHash = "partnerCode=" +
        //        partnerCode + "&accessKey=" +
        //        accessKey + "&requestId=" +
        //        requestId + "&amount=" +
        //        amount + "&orderId=" +
        //        orderid + "&orderInfo=" +
        //        orderInfo + "&returnUrl=" +
        //        returnUrl + "&notifyUrl=" +
        //        notifyurl + "&extraData=" +
        //        extraData;

        //    MoMoSecurity crypto = new MoMoSecurity();
        //    //sign signature SHA256
        //    string signature = crypto.signSHA256(rawHash, serectkey);

        //    //build body json request
        //    JObject message = new JObject
        //    {
        //        { "partnerCode", partnerCode },
        //        { "accessKey", accessKey },
        //        { "requestId", requestId },
        //        { "amount", amount },
        //        { "orderId", orderid },
        //        { "orderInfo", orderInfo },
        //        { "returnUrl", returnUrl },
        //        { "notifyUrl", notifyurl },
        //        { "extraData", extraData },
        //        { "requestType", "captureMoMoWallet" },
        //        { "signature", signature }

        //    };

        //    string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

        //    JObject jmessage = JObject.Parse(responseFromMomo);

        //    return Redirect(jmessage.GetValue("payUrl").ToString());
        //}

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        //public ActionResult ConfirmPaymentClient()
        //{
        //    //hiển thị thông báo cho người dùng
        //    return View();
        //}

        //[HttpPost]
        //public void SavePayment()
        //{
        //    //cập nhật dữ liệu vào db
        //}
    }
}