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
    public class ContactsTests : TestBase
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
            
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.CreateContact(contact);

            oldContacts.Add(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        [Test]
        public void ContactsRemovalTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.RemoveContact(0);

            oldContacts.RemoveAt(0);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactsModificationTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

            ContactData newData = new ContactData("Max", "Korolkov");
            
            app.Contact.ModifyContact(0, newData);

            oldContacts[0] = newData;

            List<ContactData> newContacts = app.Contact.GetContactList();
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
 