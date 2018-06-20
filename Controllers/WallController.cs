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
            ViewBag.Messages = _context.Messages.Include(User=>User.MessageSent).Where(x=>x.MessageReceivedId == WallId);
            ViewBag.Comments = _context.Comments.Include(User=>User.User).Include(Message=>Message.Message);
            User Profile = _context.Users.SingleOrDefault(User=>User.UserId == WallId);
            ViewBag.Profile = Profile;
            return View("Wall");
        }
        [HttpPost]
        [Route("users/show/postmessage")]
        public IActionResult PostMessage(Message NewMessage){
            // System.Console.WriteLine("NEW MESSAGE", message);
            // System.Console.WriteLine(message);
            // NewMessage.message = message;
            int? WallId = HttpContext.Session.GetInt32("profileid");
            int? SenderId = HttpContext.Session.GetInt32("userid");
            NewMessage.MessageReceivedId = (int)WallId;
            NewMessage.MessageSentId = (int)SenderId;
            System.Console.WriteLine("NEW MESSAGE");
            System.Console.WriteLine(NewMessage.message);
            _context.Messages.Add(NewMessage);
            _context.SaveChanges();
            return RedirectToAction("DisplayWall", new{user_id=WallId});
        }
        [HttpPost]
        [Route("users/show/postcomment")]
        public IActionResult PostComment(Comment NewComment){
            System.Console.WriteLine(NewComment);
            int? WallId = HttpContext.Session.GetInt32("profileid");
            int? SenderId = HttpContext.Session.GetInt32("userid");
            NewComment.UserId = (int)SenderId;
            _context.Comments.Add(NewComment);
            _context.SaveChanges();
            return RedirectToAction("DisplayWall", new{user_id=WallId});
        }
    }
}