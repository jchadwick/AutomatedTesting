using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutomatedTesting
{
    public class Client : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; private set; }

        public Client()
        {
            Accounts = new Collection<Account>();
        }
    }
}