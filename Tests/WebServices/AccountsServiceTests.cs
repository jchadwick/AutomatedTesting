using System;
using System.Linq;
using AutomatedTesting;
using AutomatedTesting.DataAccess;
using AutomatedTesting.WebServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.WebServices
{
    [TestClass]
    public class AccountsServiceTests
    {
        [TestClass]
        public class Integration
        {
            [ClassInitialize]
            public static void SetUp(TestContext context)
            {
                AppDomain.CurrentDomain.SetData(
                    "DataDirectory",
                    context.TestDeploymentDir);
            }

            [TestMethod, TestCategory("Integration")]
            public void ShouldUpdateClientAccountBalanceForExistingClientAndAccount()
            {
                var service = new AccountsService();

                const string clientId = "1";

                var accounts = service.GetClientAccounts(clientId);
                // This isn't the real test, but verify that this has worked so far
                Assert.AreNotEqual(0, accounts.Length);

                var accountToUpdate = accounts.First();
                var previousBalance = accountToUpdate.Balance;
                var newBalance = previousBalance + 1000;

                service.UpdateClientAccountBalance(accountToUpdate.ClientId.ToString(), accountToUpdate.Id.ToString(), newBalance);

                var updatedListOfAccounts = service.GetClientAccounts(clientId);
                var updatedAccount = updatedListOfAccounts.SingleOrDefault(x => x.Id == accountToUpdate.Id);

                Assert.IsNotNull(updatedAccount);
                Assert.AreEqual(newBalance, updatedAccount.Balance);
            }
        }

        [TestClass]
        public class Unit
        {
            [TestMethod]
            public void ShouldUpdateClientAccountBalanceForExistingClientAndAccount()
            {
                const long accountId = 1234;
                const decimal expectedBalance = 4000;

                var mockAccount = new Account();

                var mockDb = new Mock<IRepository>();

                mockDb
                    .Setup(x => x.Single<Account>(accountId))
                    .Returns(mockAccount);


                var service = new AccountsService(mockDb.Object);

                service.UpdateClientAccountBalance(null, accountId.ToString(), expectedBalance);

                Assert.AreEqual(expectedBalance, mockAccount.Balance);
            }
        }
    }
}
