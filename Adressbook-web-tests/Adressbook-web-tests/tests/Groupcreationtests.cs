using System.Text;
using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class AdressbookWebTests : TestBase
    {


        [Test]
        public void GroupCreationTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToTheGroupPage();
            app.Groups.InitNewGroupCreation();
            GroupData group = new GroupData("lev");
            group.Header = "test";
            group.Footer = "group";
            app.Groups.FillGroupForm(group);
            app.Groups.SubmitGroupCreation();
            app.Groups.Return();
        }











    }
}
