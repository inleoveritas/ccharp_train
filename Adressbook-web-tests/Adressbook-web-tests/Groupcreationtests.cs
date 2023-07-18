using System.Text;
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
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            GoToTheGroupPage();
            InitNewGroupCreation();
            GroupData group = new GroupData("lev");
            group.Header = "test";
            group.Footer = "group";
            FillGroupForm(group);
            SubmitGroupCreation();
            Return();
        }






 




    }
}
