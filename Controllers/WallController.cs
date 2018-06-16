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
    public class WallController : Controller{
        private UserDashboardContext _context;
        public WallController(UserDashboardContext context){
            _context = context;
        }
        [HttpGet]
        [Route("users/show/{user_id}")]
        public IActionResult DisplayWall(int user_id){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId==null){
                return RedirectToAction("Index", "Home");
            }
            User thisUser = _context.Users.SingleOrDefault(x=>x.UserId==UserId);
            ViewBag.User = thisUser;
            HttpContext.Session.SetInt32("profileid", user_id);
            int? WallId = HttpContext.Session.GetInt32("profileid");
            // May need better query to display all comments, messages, and posts to this user.
            User Profile = _context.Users.SingleOrDefault(User=>User.UserId == WallId);
            ViewBag.Profile = Profile;
            return View("Wall");
        }
        [HttpPost]
        [Route("users/show/postmessage")]
        public IActionResult PostMessage(Message NewMessage){
            int? WallId = HttpContext.Session.GetInt32("profileid");
            return RedirectToAction("DisplayWall", new{user_id=WallId});
        }
        [HttpPost]
        [Route("/users/show/postcomment")]
        public IActionResult PostComment(Comment NewComment){
            int? WallId = HttpContext.Session.GetInt32("profileid");
            return RedirectToAction("DisplayWall", new{user_id=WallId});
        }
    }
}