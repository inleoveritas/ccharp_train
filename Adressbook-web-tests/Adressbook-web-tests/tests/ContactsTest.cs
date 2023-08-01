﻿using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Adressbook_web_tests
{
    [TestFixture]
    public class ContactsTests : TestBase
    {


        [Test]
        public void ContactsCreationTest()
        {
            ContactData contact = new ContactData("тестовый", "контакт");

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

            app.Contact.RemoveContact(1);

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
            
            app.Contact.ModifyContact(1, newData);

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
 