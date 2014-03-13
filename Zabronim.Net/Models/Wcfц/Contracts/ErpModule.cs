using System;
using System.Security.Permissions;
using System.ServiceModel;

namespace Zabronim.Net.Models.Wcf.Contracts {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ErpModule : IErpModule {
        readonly IErpModuleCallback callback;

        public ErpModule() {
            callback = OperationContext.Current.GetCallbackChannel<IErpModuleCallback>();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Manager")]
        public string GetConnectionId(string erpClientId) {
            Console.WriteLine("Callback = {0}", callback.SetRequest("1"));
            return "1";
        }
    }

    public class ErpModuleCallbackHandler : IErpModuleCallback {

        public string SetRequest(string request) {
            return "2";
        }
    }
}
