using System;
using AutomatedTesting;
using AutomatedTesting.Calculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ClientAccountBalancesCalculatorTests
    {
        [TestMethod]
        public void ShouldCalculateAccountBalances()
        {
            var calculator = new ClientAccountBalancesCalculator();
            var accounts = new[]
            {
                new Account {Balance = 100},
                new Account {Balance = 100},
                new Account {Balance = 100},
            };

            var actual = calculator.CalculateTotalBalance(accounts);
            Assert.AreEqual(300, actual);
        }

        [TestMethod]
        public void ShouldReturnZeroWhenAnEmptyArrayIsProvided()
        {
            var calculator = new ClientAccountBalancesCalculator();
            var accounts = new Account[0];

            var actual = calculator.CalculateTotalBalance(accounts);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ShouldAllowNegativeBalancesToDetractFromTotal()
        {
            var calculator = new ClientAccountBalancesCalculator();
            var accounts = new[]
            {
                new Account {Balance = 100},
                new Account {Balance = 100},
                new Account {Balance = -100},
            };

            var actual = calculator.CalculateTotalBalance(accounts);
            Assert.AreEqual(100, actual);
        }

        [TestMethod]
        public void ShouldIgnoreNullAccounts()
        {
            var calculator = new ClientAccountBalancesCalculator();
            var accounts = new [] { (Account)null };

            var actual = calculator.CalculateTotalBalance(accounts);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ShouldThrowExceptionIfANullAccountListIsPassed()
        {
            var calculator = new ClientAccountBalancesCalculator();

            try
            {
                calculator.CalculateTotalBalance(null);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                // Sucess
            }
        }
    }
}
