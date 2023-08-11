using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        public void GoToTheGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void GoToTheGroupContactsPage(String id)
        {
            driver.Navigate().GoToUrl(baseURL + $"?group={id}");

        }
    }
}
