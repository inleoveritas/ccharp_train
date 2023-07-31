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

            List<GroupData> oldgroups = app.Groups.GetGroupList();

            app.Groups.CreateGroup(group);

            oldgroups.Add(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Assert.AreEqual(oldgroups, newGroups);

        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.CreateGroup(group);

            oldGroups.Insert(0, group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
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
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

