using System;
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
    public class GroupRemovalTests : TestBase
    {


        [Test]
        public void GroupRemovalTest()
        {
            GoToHomePage();
            Login(new AccountData ("admin", "secret"));
            GoToTheGroupPage();
            SelectGroup(1);
            DeleteGroup();
            Return();
        }






 
    }
}

