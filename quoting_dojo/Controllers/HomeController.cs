using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quoting_dojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult SubmitForm(string name, string quote)
        {
            DbConnector.Execute($"INSERT INTO Users (Name, Quote, Created_at, Updated_at) VALUES ('{name}', '{quote}', NOW(), NOW())");
            return RedirectToAction("ShowQuotes");
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult ShowQuotes()
        {
            var allUsers = DbConnector.Query("SELECT * FROM Users ORDER BY Created_at DESC");
            ViewBag.allUsers = allUsers;
            return View();
        }
    }
}
