using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Texter.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Texter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        public IActionResult SendMessage()
        {

            ViewBag.To = new SelectList(_db.Contacts, "Phone", "FullName");
          
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            Console.WriteLine(newMessage);
            newMessage.Send();
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
