using System.Linq;
using System.ServiceModel.Activation;
using AutoMapper;
using AutomatedTesting.Calculators;
using AutomatedTesting.DataAccess;

namespace AutomatedTesting.WebServices
{
    using Account = Contracts.Account;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AccountsService : IAccountsService
    {
        private readonly IRepository _repository;

        // Default constructor required for default WCF service factory
        // (Or you can use a WCF service factory that understands dependency injection!)
        public AccountsService()
            : this(new DbContextRepository(new ClientAccountsDataContext()))
        {
        }

        public AccountsService(IRepository repository)
        {
            _repository = repository;
        }


        public Account AddClientAccount(string clientId, Account request)
        {
            var parsedClientId = long.Parse(clientId);
            var client = _repository.Single<Client>(parsedClientId);
            
            if(client == null)
                throw new EntityNotFoundException<Client>(clientId);

            var account = Mapper.Map<AutomatedTesting.Account>(request);
            
            client.Accounts.Add(account);
            
            _repository.SaveChanges();

            return Mapper.DynamicMap<Account>(account);
        }

        public Account[] GetClientAccounts(string clientId)
        {
            var parsedClientId = long.Parse(clientId);
            var accounts = _repository.Query<AutomatedTesting.Account>(x => x.ClientId == parsedClientId);

            var mapped = Mapper.Map<Account[]>(accounts);
            return mapped.ToArray();
        }

        public decimal GetClientTotalAccountBalance(string clientId)
        {
            var parsedClientId = long.Parse(clientId);
            var accounts = _repository.Query<AutomatedTesting.Account>(x => x.ClientId == parsedClientId);

            var calculator = new ClientAccountBalancesCalculator();
            var totalBalance = calculator.CalculateTotalBalance(accounts);

            return totalBalance;
        }

        public void UpdateClientAccountBalance(string clientId, string accountId, decimal balance)
        {
            var parsedAccountId = long.Parse(accountId);
            var parsedClientId = long.Parse(clientId);

            var account = 
                _repository
                    .Query<AutomatedTesting.Account>(x => 
                           x.ClientId == parsedClientId 
                        && x.Id == parsedAccountId)
                    .SingleOrDefault();

            if(account == null)
                throw new EntityNotFoundException<Account>(accountId);

            account.Balance = balance;

            _repository.SaveChanges();
        }


        static AccountsService()
        {
            Mapper.CreateMap<AutomatedTesting.Account, Account>();
            Mapper.CreateMap<Account, AutomatedTesting.Account>()
                .ForMember(dest => dest.Client, _ => _.Ignore());
            Mapper.AssertConfigurationIsValid();
        }
    }
}
