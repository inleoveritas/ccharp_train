using System.Text;
using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class GroupTests : TestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("lev");
            group.Header = "test";
            group.Footer = "group";

            app.Groups.CreateGroup(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.CreateGroup(group);
        }

        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.Remove(1);

        }

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("aaa");
            newData.Header = "bbb";
            newData.Footer = "ccc";

            app.Groups.Modify(1, newData);
        }
    }
}

