using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests.tests
{
    public class RemoveContactFromGroupTests : TestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            GroupData? group = GroupData.GetAll().FirstOrDefault();
            ContactData? contact = null;

            if (group is null)
            {
                GroupData newGroup = new GroupData("Test", "header", "footer");
                app.Groups.CreateGroup(newGroup);
                Thread.Sleep(5000);
                List<GroupData> updatedGroups = GroupData.GetAll();
                group = updatedGroups[updatedGroups.IndexOf(newGroup)];
            }

            List<ContactData> oldList = group.GetContacts();
            var contactsInGroup = ContactData.GetAll().Intersect(oldList).ToList();
            contact = contactsInGroup.Count() > 0 ? contactsInGroup.First() : null;

            if (contact is null)
            {
                ContactData newContact = new ContactData("Pavel", "Mazin");
                app.Contact.CreateContact(newContact);
                Thread.Sleep(5000);
                List<ContactData> updatedContacts = ContactData.GetAll();
                contact = updatedContacts[updatedContacts.IndexOf(newContact)];
            }

            app.Contact.RemoveContactFromGroup(contact, group);

            Thread.Sleep(5000);
            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
    
}
