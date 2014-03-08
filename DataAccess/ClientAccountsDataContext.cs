using System.Data.Entity;

namespace AutomatedTesting.DataAccess
{
    public class ClientAccountsDataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }

        public ClientAccountsDataContext()
            : this("ClientAccounts")
        {
        }

        public ClientAccountsDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        static ClientAccountsDataContext()
        {
            Database.SetInitializer(new ClientAccountsDataContextDemoDataInitializer());
        }
    }

    public class ClientAccountsDataContextDemoDataInitializer
        : DropCreateDatabaseIfModelChanges<ClientAccountsDataContext>
    {
        protected override void Seed(ClientAccountsDataContext context)
        {
            var frank = new Client { Name = "Frank Sinatra" };
            
            frank.Accounts.Add(new Account { Name = "Primary Checking", Type = AccountType.Checking, Balance = 1231 });
            frank.Accounts.Add(new Account { Name = "Primary Savings", Type = AccountType.Savings, Balance = 44323 });
            frank.Accounts.Add(new Account { Name = "401(k)", Type = AccountType.Retirement, Balance = 221312 });
            
            context.Clients.Add(frank);

            
            var dean = new Client { Name = "Dean Martin" };

            dean.Accounts.Add(new Account { Name = "Las Vegas Checking", Type = AccountType.Checking, Balance = 4421 });
            dean.Accounts.Add(new Account { Name = "California Savings", Type = AccountType.Savings, Balance = 774292 });
            dean.Accounts.Add(new Account { Name = "Merrill Edge", Type = AccountType.Brokerage, Balance = 220232 });

            context.Clients.Add(dean);


            context.SaveChanges();
        }
    }
}
