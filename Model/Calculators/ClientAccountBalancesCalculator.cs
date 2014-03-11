using System.Collections.Generic;
using System.Linq;

namespace AutomatedTesting.Calculators
{
    public class ClientAccountBalancesCalculator
    {
        public virtual decimal CalculateTotalBalance(IEnumerable<Account> accounts)
        {
            return accounts.Where(x => x != null).Sum(x => x.Balance);
        } 
    }
}
