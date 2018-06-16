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

namespace UserDashboard.Controllers{
    public class AdminController : Controller{

        private UserDashboardContext _context;
        public AdminController(UserDashboardContext context){
            _context = context;
        }
        [HttpGet]
        [Route("/dashboard/admin")]
        public IActionResult ManageUsers(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Index", "Home");
            }
            User Adminuser = _context.Users.SingleOrDefault(User=>User.UserId == UserId);
            if(Adminuser.level != 9){
                return RedirectToAction("AllUsers", "Normal");
            }
            ViewBag.UserName = Adminuser.firstname;
            ViewBag.AllUsers = _context.Users;
            return View("ManageUser");
        }
        [HttpGet]
        [Route("/user/add")]
        public IActionResult AdminAddUser(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Index", "Home");
            }
            User Adminuser = _context.Users.SingleOrDefault(User=>User.UserId == UserId);
            if(Adminuser.level != 9){
                return RedirectToAction("AllUsers", "Normal");
            }
            ViewBag.UserName = Adminuser.firstname;
            return View("AddUser");
        }
        // fix the validation below for redirects and render view
        [HttpPost]
        [Route("/adduser")]
        public IActionResult AddUser(RegisterViewModel model, User NewUser){
            if(ModelState.IsValid){
                List<User> Allusers = _context.Users.Where(User=>User.email == model.email).ToList();
                if(Allusers.Count>0){
                    TempData["Emailused"] = "This email has already been registered. Register with a new email.";
                    return RedirectToAction("AdminAddUser");
                }
                NewUser.level = 1;
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                return RedirectToAction("ManageUsers");
            }
            int? UserId = HttpContext.Session.GetInt32("userid");
            User Adminuser = _context.Users.SingleOrDefault(User=>User.UserId == UserId);
            ViewBag.UserName = Adminuser.firstname;
            return RedirectToAction("AdminAddUser");
        }
        [HttpPost]
        [Route("/deleteuser")]
        public IActionResult DeleteUser(int user_id){
            User deleteuser = _context.Users.SingleOrDefault(x=>x.UserId==user_id);
            _context.Users.Remove(deleteuser);
            _context.SaveChanges();
            return RedirectToAction("ManageUsers");
        }
        [HttpGet]
        [Route("/users/edit/{user_id}")]
        public IActionResult AdminEditUser(int user_id){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Index", "Home");
            }
            User Adminuser = _context.Users.SingleOrDefault(User=>User.UserId == UserId);
            if(Adminuser.level != 9){
                return RedirectToAction("AllUsers", "Normal");
            }
            ViewBag.UserName = Adminuser.firstname;
            HttpContext.Session.SetInt32("profileid", user_id);
            ViewBag.Userinfo = _context.Users.SingleOrDefault(x=>x.UserId == user_id);
            return View("AdminEditUser");
        }
        [HttpPost]
        [Route("/users/adminedit")]
        public IActionResult EditUserLink(int user_id){
            return RedirectToAction("AdminEditUser", new{user_id=user_id});
        }
        // fix the validation below for redirects and render view
        [HttpPost]
        [Route("/admin_editinfo")]
        public IActionResult EditUserInfo(UpdateViewModel model, int level){
            int? AdminEdit = HttpContext.Session.GetInt32("profileid");
            if(ModelState.IsValid){
                User EditUser = _context.Users.SingleOrDefault(x=>x.UserId == AdminEdit);
                string currentfirstname = EditUser.firstname;
                string currentlastname = EditUser.lastname;
                string currentemail = EditUser.email;
                int currentlevel = EditUser.level;
                if(currentfirstname != model.firstname){
                    TempData["firstname"] = $"First Name updated successfully {model.firstname}";
                }
                if(currentfirstname != model.firstname){
                    TempData["lastname"] = $"Last Name updated successfully {model.lastname}";
                }
                if(currentemail != model.email){
                    List<User> Checkemail = _context.Users.Where(x=>x.email == model.email).ToList();
                    if(Checkemail.Count>0){
                        TempData["invalidemail"] = "Email entered is already in use. Please enter another or leave unchanged.";
                        return RedirectToAction("AdminEditUser", new{user_id=EditUser});
                    }
                    TempData["email"] = $"Email updated successfully to '{model.email}' ";
                }
                if(currentlevel != level){
                    if(level == 9){
                        TempData["level"] = "Level updated to Admin";
                    }
                    TempData["level"] = "Level updated to Normal";
                }
                _context.SaveChanges();
                return RedirectToAction("AdminEditUser", new{user_id=AdminEdit});
            }
            return RedirectToAction("AdminEditUser", new{user_id = AdminEdit});
        }
        [HttpPost]
        [Route("/admin_updateuserpw")]
        public IActionResult UpdateUserPassword(string password){
            int? AdminEdit = HttpContext.Session.GetInt32("profileid");
            if(password == null || password.Length < 8){
                TempData["invalidPW"]="Password required and must be at least 8 characters";
                return RedirectToAction("AdminEditUser", new{user_id=AdminEdit});
            }
            if(ModelState.IsValid){
                User userprofile = _context.Users.SingleOrDefault(User=> User.UserId == AdminEdit);
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                userprofile.password = Hasher.HashPassword(userprofile, userprofile.password);
                TempData["PWupdate"] = "Password updated successfully";
                _context.SaveChanges();
                return RedirectToAction("AdminEditUser", new{user_id=AdminEdit});
            }
            return RedirectToAction("AdminEditUser", new{user_id=AdminEdit});
        }
    }
}