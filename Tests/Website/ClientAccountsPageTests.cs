using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Tests.Website.PageObjects;

namespace Tests.Website
{
    [TestClass]
    public class ClientAccountsPageTests
    {
        private static IWebDriver _driver;
        private static TestContext _context;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _context = context;
            _driver = new ChromeDriver(new ChromeOptions { LeaveBrowserRunning = false });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver.Close();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (_context.CurrentTestOutcome == UnitTestOutcome.Passed)
                return;

            var filename = string.Format("{0} FAILED.png", _context.TestName);
            var filepath = Path.Combine(_context.TestRunResultsDirectory, "screenshots", filename);

            _driver.TakeScreenshot().SaveAsFile(filepath, ImageFormat.Png);
        }

        [TestMethod]
        public void ShouldDisplayListOfAvailableClients()
        {
            var page = AccountsPage.NavigateTo(_driver);

            var clients = page.GetClients().ToArray();
            
            Assert.AreNotEqual(0, clients.Length);
            Assert.IsNotNull(clients.FirstOrDefault(x => x.Name == "Frank Sinatra"));
        }

        [TestMethod]
        public void ShouldDisplayListOfAccountsAfterClientIsSelected()
        {
            var page = AccountsPage.NavigateTo(_driver);

            var clients = page.GetClients().ToArray();
            Assert.AreNotEqual(0, clients.Length);

            page.SelectClient(clients.First().Id);

            var accounts = page.GetAccounts().ToArray();
            Assert.AreNotEqual(0, accounts.Length);
        }
    }
}
