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
    public class NormalController : Controller{
        private UserDashboardContext _context;
        public NormalController(UserDashboardContext context){
            _context = context;
        }
        [HttpGet]
        [Route("/dashboard")]
        public IActionResult AllUsers(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Index", "Home");
            }
            User thisUser = _context.Users.SingleOrDefault(x=>x.UserId==UserId);
            ViewBag.UserName = thisUser.firstname;
            ViewBag.AllUsers = _context.Users;
            return View("AllUsers");
        }
        [HttpGet]
        [Route("users/edit")]
        public IActionResult EditUser(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Index", "Home");
            }
            User thisuser = _context.Users.SingleOrDefault(x=>x.UserId==UserId);
            ViewBag.UserLoggedin = thisuser;
            return View("EditUser");
        }
        [HttpPost]
        [Route("/editinfo")]
        public IActionResult EditInfo(UpdateViewModel model){
            int? UserId = HttpContext.Session.GetInt32("userid");
            User thisUser = _context.Users.SingleOrDefault(x=>x.UserId == UserId);
            if(ModelState.IsValid){
                if(thisUser.email != model.email){
                    List<User> Checkemail = _context.Users.Where(x=>x.email == model.email).ToList();
                    if(Checkemail.Count>0){
                        TempData["invalidemail"] = $"Email entered already used. Please use another email.";
                        return RedirectToAction("EditUser");
                    }
                    TempData["email"] = $"Email updated successfully from {thisUser.email}";
                    thisUser.email = model.email;
                }
                if(thisUser.firstname != model.firstname){
                    TempData["firstname"] = $"First name updated successfully from {thisUser.firstname}";
                    thisUser.firstname = model.firstname;
                }
                if(thisUser.lastname != model.lastname){
                    TempData["lastname"] = $"Last name updated successfully from {thisUser.lastname}";
                    thisUser.lastname = model.lastname;
                }
                _context.SaveChanges();
                RedirectToAction("EditUser");
            }
            ViewBag.UserLoggedin = thisUser;
            return View("EditUser");
        }
        [HttpPost]
        [Route("/updatepw")]
        public IActionResult UpdatePassword(string password, string passwordconfirm){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(password == null || password.Length < 8){
                TempData["invalidPW"]="Password required and must be at least 8 characters";
                return RedirectToAction("EditUser");
            }
            if(password == passwordconfirm){
                User thisProfile = _context.Users.SingleOrDefault(User=> User.UserId == UserId);
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                thisProfile.password = Hasher.HashPassword(thisProfile, password);
                TempData["PWupdate"] = "Password updated successfully";
                _context.SaveChanges();
                return RedirectToAction("EditUser");
            }
            TempData["PWmismatch"] = "Passwords do not match. Try Again.";
            return RedirectToAction("EditUser");
        }
        [HttpPost]
        [Route("/updatedesc")]
        public IActionResult EditDescription(string description){
            int? UserId = HttpContext.Session.GetInt32("userid");
            User thisProfile = _context.Users.SingleOrDefault(User=> User.UserId == UserId);
            if(thisProfile.description != description){
                TempData["descUpdate"] = $"Description updated successfully!";
                thisProfile.description = description;
                _context.SaveChanges();
            }
            return RedirectToAction("EditUser");
        }
    }
}