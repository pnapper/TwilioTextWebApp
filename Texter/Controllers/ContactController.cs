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

    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Contacts.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            contact.User = currentUser;
            _db.Contacts.Add(contact);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisContact = _db.Contacts
                                  .Include(x => x.Messages)
                                  .FirstOrDefault(items => items.ContactId == id);
            return View(thisContact);
        }

        public IActionResult Edit(int id)
        {
            var thisContact = _db.Contacts.FirstOrDefault(names => names.ContactId == id);
            return View(thisContact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            contact.User = currentUser;
            _db.Contacts.Update(contact);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisContact = _db.Contacts.FirstOrDefault(names => names.ContactId == id);
            return View(thisContact);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisContact = _db.Contacts.FirstOrDefault(names => names.ContactId == id);
            _db.Remove(thisContact);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}