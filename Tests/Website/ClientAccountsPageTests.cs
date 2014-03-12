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

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
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
            var tempDir = @"C:\Temp\Screenshots\";

            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            var fileName = tempDir + DateTime.Now.ToString("yyyyMMdd_hhmmsstt") + ".png";
            _driver.TakeScreenshot().SaveAsFile(fileName, ImageFormat.Png);
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
