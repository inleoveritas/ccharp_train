using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V111.Storage;
using OpenQA.Selenium.Firefox;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class GroupTests : TestBase
    {
        public static IEnumerable<GroupData> RandomGroupdataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }



        [Test, TestCaseSource("RandomGroupdataProvider")]
        public void GroupCreationTest(GroupData group)
        {


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

