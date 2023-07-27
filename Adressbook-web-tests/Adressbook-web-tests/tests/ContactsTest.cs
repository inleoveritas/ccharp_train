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
    public class ContactsTests : TestBase
    {


        [Test]
        public void ContactsCreationTest()
        {

            app.Contact.CreateContact(new ContactData("lev", "Myasnikov")).Logout();
        }

        [Test]
        public void ContactsRemovalTest()
        {

            app.Contact.RemoveContact(1);
        }

        [Test]
        public void ContactsModificationTest()
        {

            ContactData newData = new ContactData("Max", "Korolkov");
            app.Contact.ModifyContact(1, newData);
        }
    }
}
