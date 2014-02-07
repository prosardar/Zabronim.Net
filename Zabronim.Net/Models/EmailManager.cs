using System.Configuration;
using System.Web.Helpers;

namespace Zabronim.Net.Models {
    public class EmailManager {
        public static void Send(string recipientEmail, string subject, string body) {
            var mailSection = (System.Net.Configuration.SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var network = mailSection.Network;

            WebMail.From = mailSection.From;
            WebMail.SmtpServer = network.Host;
            WebMail.UserName = network.UserName;
            WebMail.Password = network.Password;
            WebMail.SmtpPort = network.Port;
            WebMail.SmtpUseDefaultCredentials = network.DefaultCredentials;

            WebMail.Send(recipientEmail, subject, body);
        }
    }
}