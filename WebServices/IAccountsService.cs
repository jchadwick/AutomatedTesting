using System.ServiceModel;
using System.ServiceModel.Web;

namespace AutomatedTesting.WebServices
{
    using Account = Contracts.Account;

    [ServiceContract]
    public interface IAccountsService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "clients/{clientId}/accounts",
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Account AddClientAccount(string clientId, Account account);

        [OperationContract]
        [WebGet(UriTemplate = "clients/{clientId}/accounts", ResponseFormat = WebMessageFormat.Json)]
        Account[] GetClientAccounts(string clientId);

        [OperationContract]
        [WebGet(UriTemplate = "clients/{clientId}/accounts/balance", ResponseFormat = WebMessageFormat.Json)]
        decimal GetClientTotalAccountBalance(string clientId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "clients/{clientId}/accounts/{accountId}/balance",
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void UpdateClientAccountBalance(string clientId, string accountId, decimal balance);
    }
}
