using System.Security.Permissions;
using System.ServiceModel;
using Zabronim.Net.ZaEnviroment;

namespace Zabronim.Net.Models.Wcf.Contracts {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class ErpModule : IErpModule {
        readonly IErpModuleCallback callback;

        public ErpModule() {
            ZLogger.Info("ErpModule constructor");
            callback = OperationContext.Current.GetCallbackChannel<IErpModuleCallback>();
        }
        
        public string GetConnectionId(string erpClientId) {
            ZLogger.Info("ErpModule GetConnection");
            ZLogger.Info("Callback = {0}", callback.SetRequest("1"));
            return "1";
        }
    }
}
