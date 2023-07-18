﻿using System;
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
            GoToHomePage();
            Login(new AccountData ("admin", "secret"));
            PageContacts();
            FillContactsForm(new ContactData ("lev", "Myasnikov"));
            SubmitContactCreation();
            Logout();
        }

    }
}
