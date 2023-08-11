using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V111.Storage;
using OpenQA.Selenium.Firefox;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class GroupTests : GroupTestBase
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


        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) 
                new XmlSerializer(typeof(List<GroupData>))
                    .Deserialize(new StreamReader(@"groups.xml"));

        }

        [Test, TestCaseSource(nameof(GroupDataFromXmlFile))]
        public void GroupCreationTest(GroupData group)
        {


            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.CreateGroup(group);



            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));
        }


        [Test]
        public void TestDBConnectivity2()
        {
            foreach (ContactData contact in GroupData.GetAll()[0].GetContacts())
            {
                Console.Out.WriteLine(contact.Deprecated);
            }
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];

            app.Groups.Remove(toBeRemoved);
           
            List <GroupData> newGroups = GroupData.GetAll();

            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }

        }

        [Test]
        public void GroupModificationTest()
        {

            List<GroupData> oldGroups = GroupData.GetAll();
           

            GroupData newData = new GroupData("aaa");
            newData.Header = "";
            newData.Footer = "";

            GroupData? toBeModified = oldGroups.Count > 0 ? oldGroups[0] : null;

            app.Groups.Modify(toBeModified, newData);

            if (toBeModified is null)
            {
                oldGroups.Add(newData);
            }
            else
            {
                oldGroups[oldGroups.IndexOf(toBeModified)] = newData;
            }
            Thread.Sleep(5000);
            List<GroupData> newGroups = GroupData.GetAll();
            //oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

