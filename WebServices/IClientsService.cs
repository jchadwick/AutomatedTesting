using System.ServiceModel;
using System.ServiceModel.Web;

namespace AutomatedTesting.WebServices
{
    using Client = Contracts.Client;

    [ServiceContract]
    public interface IClientsService
    {
        [OperationContract]
        [WebGet(UriTemplate = "clients", ResponseFormat = WebMessageFormat.Json)]
        Client[] GetClients();

        [OperationContract]
        [WebGet(UriTemplate = "clients/{clientId}", ResponseFormat = WebMessageFormat.Json)]
        Client GetClient(string clientId);
    }
}