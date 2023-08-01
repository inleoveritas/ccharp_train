using System.Security.Cryptography;
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

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.CreateGroup(group);



            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.CreateGroup(group);


            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }

        [Test]
        public void GroupModificationTest()
        {

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData newData = new GroupData("aaa");
            newData.Header = "";
            newData.Footer = "";

            app.Groups.Modify(0, newData);

            oldGroups[0] = newData;


            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

