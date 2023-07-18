using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Adressbook_web_tests
{
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.That(verificationErrors.ToString(), Is.EqualTo(""));
        }

        protected void GoToHomePage()
        {
          driver.Navigate().GoToUrl(baseURL);
        }
        protected void Login(AccountData account)
        {
          driver.FindElement(By.Name("user")).Click();
          driver.FindElement(By.Name("user")).SendKeys(account.Username);
          driver.FindElement(By.Id("LoginForm")).Click();
          driver.FindElement(By.Name("pass")).Click();
          driver.FindElement(By.Name("pass")).Clear();
          driver.FindElement(By.Name("pass")).SendKeys(account.Password);
          driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
        protected void GoToTheGroupPage()
        {
           driver.FindElement(By.LinkText("groups")).Click();
        }

         protected void InitNewGroupCreation()
         {
           driver.FindElement(By.Name("new")).Click();
         }
         protected void FillGroupForm(GroupData group)
         {
           driver.FindElement(By.Name("group_name")).Click();
           driver.FindElement(By.Name("group_name")).Clear();
           driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
           driver.FindElement(By.Name("group_header")).Click();
           driver.FindElement(By.Name("group_header")).Clear();
           driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
           driver.FindElement(By.Name("group_footer")).Click();
           driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
         }
         protected void SubmitGroupCreation()
         {
           driver.FindElement(By.Name("submit")).Click();
         }
         protected void Return()
         {
           driver.FindElement(By.LinkText("group page")).Click();
         }
         protected void SelectGroup(int index)
         {
           driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + "]/input")).Click();
         }
         protected void DeleteGroup()
         {
           driver.FindElement(By.Name("delete")).Click();
         }
        protected void PageContacts()
        {
            //Go to the page contacts
            driver.FindElement(By.LinkText("add new")).Click();
        }
        protected void FillContactsForm(ContactData group)
        {
            //Fill contacts form
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(group.Firstname);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(group.Lastname);
        }
        protected void SubmitContactCreation()
        {
            //Submit contact creation
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
        }
        protected void Logout()
        {
            //Logout
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
