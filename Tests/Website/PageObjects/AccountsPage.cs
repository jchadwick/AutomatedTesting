using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Tests.Website.PageObjects
{
    public class AccountsPage
    {
        private readonly IWebDriver _driver;

        [FindsBy(How = How.Id)]
        private IWebElement clientSelector;

        public AccountsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public decimal? GetTotalAccountsBalance()
        {
            var element = _driver.FindElement(By.ClassName("total-balance"));

            if (element == null)
                return null;

            return element.Text.ParseDecimal();
        }

        public IEnumerable<AccountSection> GetAccounts()
        {
            IEnumerable<IWebElement> accountElements = null;

            new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(_ =>
                {
                    accountElements = _driver.FindElements(By.ClassName("account"));
                    
                    if (!accountElements.Any())
                        return false;

                    var account = new AccountSection();
                    PageFactory.InitElements(accountElements.First(), account);

                    return account.Balance.HasValue;
                });

            foreach (var element in accountElements)
            {
                var account = new AccountSection();
                PageFactory.InitElements(element, account);
                yield return account;
            }
        }

        public IEnumerable<ClientSelection> GetClients()
        {
            var clientOptions = clientSelector.FindElements(By.TagName("option"));
            
            var clients = 
                clientOptions
                    .Where(x => !string.IsNullOrEmpty(x.Text))
                    .Select(x => new ClientSelection
                    {
                        Id = x.GetAttribute("value").ParseLong().Value,
                        Name = x.Text
                    })
                    .ToArray();

            return clients;
        }


        public void SelectClient(long clientId)
        {
            var clientIdString = clientId.ToString();
            var clientOptions = clientSelector.FindElements(By.TagName("option"));
            var option = clientOptions.FirstOrDefault(x => x.GetAttribute("value") == clientIdString);

            if(option == null)
                throw new ApplicationException(string.Format("Option for client ID {0} not found", clientId));

            option.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(_ => GetAccounts().Any());
        }

        private void WaitUntilLoaded()
        {
            new WebDriverWait(_driver, TimeSpan.FromMinutes(1))
                .Until(_ => GetClients().Any());
        }

        public static AccountsPage NavigateTo(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:8080/index.htm");

            var page = new AccountsPage(driver);
            PageFactory.InitElements(driver, page);
            
            page.WaitUntilLoaded();

            return page;
        }


        public class AccountSection
        {
            [FindsBy(How = How.ClassName)]
            private IWebElement name;

            [FindsBy(How = How.ClassName)]
            private IWebElement balance;


            public decimal? Balance
            {
                get { return balance.Text.Replace("$", string.Empty).ParseDecimal(); }
            }

            public string Name
            {
                get { return name.Text; }
            }
        }

        public class ClientSelection
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
    }

    public static class StringExtensions
    {
        public static decimal? ParseDecimal(this string source)
        {
            decimal value;

            if (decimal.TryParse(source, out value))
                return value;

            return null;
        }

        public static long? ParseLong(this string source)
        {
            long value;

            if (long.TryParse(source, out value))
                return value;

            return null;
        }
    }
}
