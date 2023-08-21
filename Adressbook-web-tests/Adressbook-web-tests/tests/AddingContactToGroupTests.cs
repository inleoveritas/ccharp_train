using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class AddingContactToGroupTests : TestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            //Создать группу если не существует
            GroupData? group = groups.Count > 0 ? groups[0] : null;

            if (group is null)
            {
                GroupData newGroup = new GroupData("Test", "header", "footer");
                app.Groups.CreateGroup(newGroup);
                Thread.Sleep(5000);
                List<GroupData> updatedGroups = GroupData.GetAll();
                group = updatedGroups[updatedGroups.IndexOf(newGroup)];
            }       

            List<ContactData> oldList = group.GetContacts();
            var contactsNotInGroup = ContactData.GetAll().Except(oldList).ToList();
            ContactData? contact = contactsNotInGroup.Count() > 0 ? contactsNotInGroup.First() : null;

            if (contact is null)
            {
                ContactData newContact = new ContactData("Pavel", "Mazin");
                app.Contact.CreateContact(newContact);
                Thread.Sleep(5000);
                List<ContactData> updatedContacts = ContactData.GetAll();
                contact = updatedContacts[updatedContacts.IndexOf(newContact)];
            }

            app.Contact.AddContactToGroup(contact, group);

            Thread.Sleep(5000);
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
