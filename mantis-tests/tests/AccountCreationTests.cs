using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;


namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [Test]
        public void TestMethod1()
        {
            AccountData account = new AccountData()
            {
                Name = "Test",
                Password = "password",
                Email = "test@test.com"
            };

            app.Registration.Register(account);
        }
    }
}
