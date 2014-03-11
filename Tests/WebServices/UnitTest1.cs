using System;
using AutomatedTesting;
using AutomatedTesting.DataAccess;
using AutomatedTesting.WebServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.WebServices
{
    [TestClass]
    public class ClientsServiceTests
    {
        [TestMethod]
        public void ShouldGetClientById()
        {
            var newClient = new Client { Name = DateTime.Now.ToLongDateString() };
            using (var db = new ClientAccountsDataContext())
            {
                db.Clients.Add(newClient);
                db.SaveChanges();
            }


            var service = new ClientsService();

            var client = service.GetClient(newClient.Id.ToString());

            Assert.IsNotNull(client);
            Assert.AreEqual(newClient.Name, client.Name);
        }

        [TestMethod]
        public void ShouldGetAllClients()
        {
            var service = new ClientsService();

            var clients = service.GetClients();

            Assert.IsNotNull(clients);
            Assert.AreNotEqual(0, clients.Length);
        }
    }
}
