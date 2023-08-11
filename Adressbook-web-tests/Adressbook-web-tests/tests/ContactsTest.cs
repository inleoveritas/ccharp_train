using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class ContactsTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactdataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Address = GenerateRandomString(90),
                    HomePhone = GenerateRandomString(90),
                    MobilePhone = GenerateRandomString(90),
                    WorkPhone = GenerateRandomString(90),
                    FirstEmail = GenerateRandomString(90),
                    SecondEmail = GenerateRandomString(90),
                    ThirdEmail = GenerateRandomString(90)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));

        }

        [Test, TestCaseSource(nameof(ContactDataFromXmlFile))]
        public void ContactsCreationTest(ContactData contact)
        {
            
            List<ContactData> oldContacts = ContactData.GetAllContacts();

            app.Contact.CreateContact(contact);

            oldContacts.Add(contact);

            List<ContactData> newContacts = ContactData.GetAllContacts();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        [Test]
        public void TestDBConnectivityForContacts()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUi = app.Contact.GetContactList();
            DateTime end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetAllContacts();
            end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));
        }

        [Test]
        public void ContactsRemovalTest()
        {
            List<ContactData> oldContacts = ContactData.GetAllContacts();
            ContactData toBeRemoved = oldContacts[0];

            app.Contact.RemoveContact(toBeRemoved);


       
            oldContacts.Remove(toBeRemoved);
            oldContacts.Sort();
            //Слишком быстро проходит у меня на машине, дожидаемся удаления из базы
            Thread.Sleep(5000);
            List<ContactData> newContacts = ContactData.GetAllContacts();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void ContactsModificationTest()
        {
            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData newData = new ContactData("Max", "Korolkov");
            ContactData? toBeModified = oldContacts.Count > 0 ? oldContacts[0] : null;

            app.Contact.ModifyContact(toBeModified, newData);

            if (toBeModified is null)
            {
                oldContacts.Add(newData);
            }
            else
            {
                oldContacts[oldContacts.IndexOf(toBeModified)] = newData;
            }
            //Слишком быстро проходит у меня на машине, дожидаемся изменений в базе
            Thread.Sleep(5000);
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]  

        public void ContactInformationTest()
        {
            ContactData fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditorForm(0);

            Assert.AreEqual (fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void ContactDetailsTest()
        {
            string fromForm = app.Contact.GetContactInformationFromEditorForm(0).ToString();
            string fromView = app.Contact.GetContactInformationFromDetails(0);
            
            Assert.AreEqual(fromForm, fromView);    
        }
    }
}
 