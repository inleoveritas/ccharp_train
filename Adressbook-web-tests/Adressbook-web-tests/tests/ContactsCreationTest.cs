using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class ContactsWebTests : TestBase
    {


        [Test]
        public void TheContactsCreationTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contact.PageContacts();
            app.Contact.FillContactsForm(new ContactData("lev", "Myasnikov"));
            app.Contact.SubmitContactCreation();
            app.Contact.Logout();
        }

    }
}
