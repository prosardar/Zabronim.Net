using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zabronim.Net.Models;

namespace Zabronim.Net.Controllers {
    public class HomeController : Controller {
        //
        // GET: /Home/
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult SumbitEmail(WClient client) {
            if (client == null || ModelState.IsValid == false) {
                return View("Index");
            }
            
            //db.Users.Add(user);

            try {
                //  db.SaveChanges();
            }
            catch (Exception e) {
                ModelState.AddModelError("Email", e);
                return View("Index");
            }
            return Redirect("Index");
        }

        public ActionResult G() {
            return View();
        }
    }
}
