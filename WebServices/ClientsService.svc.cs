using System.ServiceModel.Activation;
using AutoMapper;
using AutomatedTesting.DataAccess;

namespace AutomatedTesting.WebServices
{
    using Client = Contracts.Client;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ClientsService : IClientsService
    {
        private readonly ClientAccountsDataContext _db;

        public ClientsService()
        {
            _db = new ClientAccountsDataContext();
        }

        public Client[] GetClients()
        {
            var clients = _db.Clients;
            
            return Mapper.Map<Client[]>(clients);
        }

        public Client GetClient(string clientId)
        {
            var parsedClientId = long.Parse(clientId);
            
            var client = _db.Clients.Find(parsedClientId);

            return Mapper.Map<Client>(client);
        }


        static ClientsService()
        {
            Mapper.CreateMap<AutomatedTesting.Client, Client>();
            Mapper.CreateMap<Client, AutomatedTesting.Client>()
                .ForMember(dest => dest.Accounts, _ => _.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
