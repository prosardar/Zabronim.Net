using System.ServiceModel;

namespace Zabronim.Net.Models.Wcf.Contracts
{
    [ServiceContract(CallbackContract = typeof(IErpModuleCallback))]
    public interface IErpModule
    {
        [OperationContract]
        string GetConnectionId(string erpClientId);
    }


    public interface IErpModuleCallback
    {
        [OperationContract]
        string SetRequest(string request);
    }
}