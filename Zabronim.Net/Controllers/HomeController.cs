using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Zabronim.Net.Models;

namespace Zabronim.Net.Controllers {
    public class HomeController : Controller {
        private readonly WClientContext dbWClient;

        public HomeController() {
            dbWClient = new WClientContext();
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult SumbitEmail(WClient client) {
            if (client == null || ModelState.IsValid == false) {
                return View("Index");
            }
            
            client.UserId = null;
            dbWClient.WClients.Add(client);

            try {
                dbWClient.SaveChanges();
            }
            catch (Exception e) {
                ModelState.AddModelError("Email", e);
                return View("Index");
            }

            EmailManager.Send(client.Email, "Команда Zabronim", "Спасибо за интерес, ваша почта добавлена в список рассылок новостей.");

            return Redirect("Index");
        }

        public ActionResult IsEmail_Available(string email) {
            var result = dbWClient.WClients.Any(c => c.Email == email) == false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult G() {
            return View();
        }
    }
}
