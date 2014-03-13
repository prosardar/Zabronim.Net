using System.Security.Permissions;
using System.ServiceModel;
using Zabronim.Net.ZaEnviroment;

namespace Zabronim.Net.Models.Wcf.Contracts {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ErpModule : IErpModule {
        readonly IErpModuleCallback callback;

        public ErpModule() {
            callback = OperationContext.Current.GetCallbackChannel<IErpModuleCallback>();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Users")]
        public string GetConnectionId(string erpClientId) {
            ZLogger.Info("Callback = {0}", callback.SetRequest("1"));
            return "1";
        }
    }
}
