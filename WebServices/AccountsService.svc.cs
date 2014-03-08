﻿using System.Linq;
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
        private readonly ClientAccountsDataContext _db;

        public AccountsService()
        {
            _db = new ClientAccountsDataContext();
        }

        public Account AddClientAccount(string clientId, Account request)
        {
            var parsedClientId = long.Parse(clientId);
            var client = _db.Clients.Find(parsedClientId);
            
            if(client == null)
                throw new EntityNotFoundException<Client>(clientId);

            var account = Mapper.Map<AutomatedTesting.Account>(request);
            
            client.Accounts.Add(account);
            
            _db.SaveChanges();

            return Mapper.DynamicMap<Account>(account);
        }

        public Account[] GetClientAccounts(string clientId)
        {
            var parsedClientId = long.Parse(clientId);
            var accounts = _db.Accounts.Where(x => x.ClientId == parsedClientId);

            var mapped = Mapper.Map<Account[]>(accounts);
            return mapped.ToArray();
        }

        public decimal GetClientTotalAccountBalance(string clientId)
        {
            var parsedClientId = long.Parse(clientId);
            var accounts = _db.Accounts.Where(x => x.ClientId == parsedClientId);

            var calculator = new ClientAccountBalancesCalculator();
            var totalBalance = calculator.CalculateTotalBalance(accounts);

            return totalBalance;
        }

        public void UpdateClientAccountBalance(string clientId, string accountId, decimal balance)
        {
            var parsedAccountId = long.Parse(accountId);
            var parsedClientId = long.Parse(clientId);

            var account = _db.Accounts.SingleOrDefault(x => 
                   x.ClientId == parsedClientId 
                && x.Id == parsedAccountId);

            if(account == null)
                throw new EntityNotFoundException<Account>(accountId);

            account.Balance = balance;

            _db.SaveChanges();
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
