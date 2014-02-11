using System;
using System.IO;
using System.Linq;
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
            client.IsConfim = false;
            dbWClient.WClients.Add(client);

            try {
                dbWClient.SaveChanges();
            }
            catch (Exception e) {
                ModelState.AddModelError("Email", e);
                return View("Index");
            }
            
            var message = System.IO.File.ReadAllText(Server.MapPath("~/Content/emailContext.html"));;

            EmailManager.Send(client.Email, "Команда Zabronim", message);

            return Redirect("Index");
        }

        public ActionResult GetPath() {
            
            var me = System.IO.File.ReadAllText(Server.MapPath("~/Content/emailContext.html"));
            ViewBag.Name = me;
            
            return View();
        }

        public ActionResult IsEmail_Available(string email) {
            var result = dbWClient.WClients.Any(c => c.Email == email) == false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
