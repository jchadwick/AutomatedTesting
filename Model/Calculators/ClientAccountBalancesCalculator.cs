using System.Collections.Generic;
using System.Linq;

namespace AutomatedTesting.Calculators
{
    public class ClientAccountBalancesCalculator
    {
        public decimal CalculateTotalBalance(IEnumerable<Account> accounts)
        {
            return accounts.Sum(x => x.Balance);
        } 
    }
}
