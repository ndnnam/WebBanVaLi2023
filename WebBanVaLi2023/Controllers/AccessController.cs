using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebBanVaLi2023.Models;
using XSystem.Security.Cryptography;

namespace WebBanVaLi2023.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private bool CheckIfUserExists()
        {
            bool userExists = db.TUsers.Any();
            return userExists;
        }
        string BamMatKhau(string matKhau)
        {
            byte[] tam = ASCIIEncoding.ASCII.GetBytes(matKhau);
            byte[] bamDuLieu = new MD5CryptoServiceProvider().ComputeHash(tam);
            string bamMatKhau = "";
            foreach (byte b in bamDuLieu)
            {
                bamMatKhau += b;
            }
            return bamMatKhau;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(BamMatKhau(user.Password))).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    if (u.LoaiUser == 0)
                    {
                        return RedirectToAction("homeadmin", "admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult Register()
        {
            bool userExists = CheckIfUserExists();
            if (userExists)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }
        [HttpPost]
        public IActionResult Register(TUser user)
        {
            bool userExists = CheckIfUserExists();
            if (userExists)
            {
                if (ModelState.IsValid)
                {
                    user.Password = BamMatKhau(user.Password);
                    user.LoaiUser = 1;
                    db.TUsers.Add(user);
                    db.SaveChanges();
                    HttpContext.Session.SetString("UserName", user.Username.ToString());
                    return RedirectToAction("Login", "Access");
                }
            }
            else
            {
                ModelState.AddModelError("Username", "Tên người dùng đã tồn tại. Vui lòng chọn một tên khác.");
            }
            return View();
        }
    }
}
