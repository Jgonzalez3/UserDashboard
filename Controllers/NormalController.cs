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
        public IActionResult EditInfo(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            return RedirectToAction("EditUser");
        }
        [HttpPost]
        [Route("/updatepw")]
        public IActionResult UpdatePassword(string password){
            int? UserId = HttpContext.Session.GetInt32("userid");
            return RedirectToAction("EditUser");
        }
        [HttpPost]
        [Route("/updatedesc")]
        public IActionResult EditDescription(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            
            return RedirectToAction("EditUser");
        }
    }
}