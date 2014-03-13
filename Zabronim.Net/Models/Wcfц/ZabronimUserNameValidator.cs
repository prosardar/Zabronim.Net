using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Zabronim.Net.Models.Wcf {
    public class ZabronimUserNameValidator : UserNamePasswordValidator {
        public override void Validate(string userName, string password) {
            if (null == userName || null == password) {
                throw new ArgumentNullException();
            }

            if (userName != "sardar" && password != "sardar") {
                ZLogger.Info("Unknown Username {0} or Password", userName);
                throw new SecurityTokenException("Unknown Username or Password");
            }
        }
    }
}