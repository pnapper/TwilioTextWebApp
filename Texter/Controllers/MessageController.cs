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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Texter.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View(_db.Messages.Include(x => x.Contact).ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(string To, string From, string Body, string Status, int ContactId)
        {
            var newMessage = new Message(To, From, Body, Status, ContactId);

            if (ModelState.IsValid)
            {
                _db.Messages.Add(newMessage);
                _db.SaveChanges();
            }

            return Json(newMessage);
        }

    }
}