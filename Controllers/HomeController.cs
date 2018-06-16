using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserDashboard.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserDashboard.Controllers
{
    public class HomeController : Controller
    {
        private UserDashboardContext _context;
        public HomeController(UserDashboardContext context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register(){
            HttpContext.Session.Clear();
            return View("Register");
        }
        [HttpPost]
        [Route("registeruser")]
        public IActionResult RegisterUser(RegisterViewModel model, User NewUser){
            if(ModelState.IsValid){
                List<User> Allusers = _context.Users.Where(User=>User.email == model.email).ToList();
                if(Allusers.Count>0){
                    TempData["Emailused"] = "This email has already been registered. Login or Register with new email.";
                    return View("Register");
                }
                List<User> Allemails = _context.Users.ToList();
                if(Allemails.Count == 0){
                    NewUser.level = 9;
                }
                else{
                    NewUser.level = 1;
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User Reg = _context.Users.SingleOrDefault(User=>User.email == NewUser.email);
                HttpContext.Session.SetInt32("userid", (int)Reg.UserId);
                HttpContext.Session.SetInt32("level", (int)Reg.level);
                if(Reg.level == 9){
                    return RedirectToAction("ManageUsers", "Admin");
                }
                return RedirectToAction("AllUsers", "Normal");
            }
            return View("Register");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(string loginemail, string loginpassword){
            User Login = _context.Users.SingleOrDefault(User=> User.email == loginemail);
            if(Login == null){
                TempData["Invalidemail"] = "Email not Registered. Have you Registered?";
                return View("Login");
            }
            if(Login != null && loginpassword != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(Login, Login.password, loginpassword)){
                    HttpContext.Session.SetInt32("userid", (int)Login.UserId);
                    HttpContext.Session.SetInt32("level", (int)Login.level);
                    if(Login.level == 9){
                        return RedirectToAction("ManageUsers", "Admin");
                    }
                    return RedirectToAction("AllUsers", "Normal");
                }
            }
            TempData["InvalidPW"] = "Invalid Password";
            return View("Login");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
