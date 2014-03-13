using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zabronim.Net.Models;
using Zabronim.Net.ZaEnviroment;
using Newtonsoft.Json;

namespace Zabronim.Net.Controllers.Api {
    public class MobileClientController : ApiController {
        public WClientContext mobileClientDb = new WClientContext();

        public List<MobileClient> Get() {
            return mobileClientDb.MobileClients.ToList();
        }

        public MobileClient Get(string id) {
            var client = mobileClientDb.MobileClients.Find(id);
            if (client == null) {
                const string msg = "Не найден мобильный клиент с текущим параметром";
                ZLogger.Error(msg);
                return null;
            }
            return client;
        }

        public void Post([FromBody]string value) {
        }

        [HttpGet]
        public string PutClient(string id) {
            var client = new MobileClient {
                Phone = id
            };

            try {
                mobileClientDb.MobileClients.Add(client);

                mobileClientDb.SaveChanges();
            }
            catch (Exception e) {
                const string msg = "При добавлении нового мобильного клиента произошла ошибка";
                ZLogger.ErrorException(msg, e);
                return string.Format("{0} {1}", msg, e.Message);
            }
            return client.Id.ToString();
        }

        [HttpGet]
        public string DeleteClient(string id) {
            try {
                var client = mobileClientDb.MobileClients.Find(id);
                if (client == null) {
                    return "Не найден мобильный клиент с текущим параметром";
                }

                mobileClientDb.MobileClients.Remove(client);

                mobileClientDb.SaveChanges();
            }
            catch (Exception e) {
                const string msg = "При удалении мобильного клиента произошла ошибка";
                ZLogger.ErrorException(msg, e);
                return string.Format("{0} {1}", msg, e.Message);
            }
            return "OK";
        }
    }
}
