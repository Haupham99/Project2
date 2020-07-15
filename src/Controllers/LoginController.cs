using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.Models;
using Project2.Services;

namespace Project2.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LoginController(IWebHostEnvironment webHostEnvironment, IAccountRepository accountRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            _accountRepository = accountRepository;
        }
        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Login/Login.cshtml");
        }
        
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Account account)
        {
            //if (ModelState.IsValid)
            //{
            Account account1 =  _accountRepository.GetAccount(account.username, account.password);
            if (account1 != null)
            {
                CookieOptions option = new CookieOptions();
                Response.Cookies.Append("accountName", account1.username.ToString(), option);
                Response.Cookies.Append("id", account1.id.ToString(), option);
                ViewBag.id = Request.Cookies["id"];
                return Redirect("/Home/Home");
            }
            else
            {
                HttpContext.Session.SetString("error", "Tài khoản hoặc mật khẩu không đúng");
                ViewBag.Error = HttpContext.Session.GetString("error");
                return View("~/Views/Home/Login/Login.cshtml");
            }
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return Redirect("/home/home");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("~/Views/Home/Login/Register.cshtml");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Account account)
        {
            Account account2 = _accountRepository.GetAccount(account.username);
            if(account2 == null)
            {
                Account account1 = _accountRepository.AddAccount(account);
                if (account1 != null)
                {
                    HttpContext.Session.SetString("success", "Tạo tài khoản thành công");
                    ViewBag.Success = HttpContext.Session.GetString("success");
                    return View("~/Views/Home/Login/Login.cshtml");
                }
            }
            else
            {
                HttpContext.Session.SetString("error", "Tài khoản đã tồn tại");
                ViewBag.Error = HttpContext.Session.GetString("error");
                return View("~/Views/Home/Login/Register.cshtml");
            }
            return Redirect("/register");
        }
    }
}