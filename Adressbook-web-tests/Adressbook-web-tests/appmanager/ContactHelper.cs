using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        private List<ContactData> contactCache = null;

        public ContactHelper PageContacts()
        {
            //Go to the page contacts
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactsForm(ContactData contact)
        {
            //Fill contacts form
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            //Submit contact creation
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            PageContacts()
            .FillContactsForm(contact)
            .SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.XPath("//tr[@name='entry']"))[index].FindElement(By.XPath("//input[@type='checkbox']")).Click();
            return this;
        }
        public ContactHelper SelectContact(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public bool ContactExists(int index)
        {
            var allContacts = driver.FindElements(By.XPath("//tr[@name='entry']"));
            if (allContacts.Count > index)
            {
                return true;
            }
            return false;
        }
        public bool ContactExists(String id)
        {
            try
            {
                driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper RemoveContact(int index)
        {
            if (!ContactExists(index))
            {
                CreateContact(new ContactData("Lev", "Myasnikov"));
            }
            SelectContact(index)
            .DeleteContact()
            .AcceptAlert();
            return this;
        }

        public ContactHelper RemoveContact(ContactData contact)
        {
            SelectContact(contact.Id)
            .DeleteContact()
            .AcceptAlert();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper InitContactDetails()
        {
            driver.FindElement(By.XPath("//img[@alt='Details']")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }


        public ContactHelper ModifyContact(int index, ContactData contact)
        {
            if (!ContactExists(index))
            {
                CreateContact(new ContactData("Lev", "Myasnikov"));
            }
            SelectContact(index)
            .InitContactModification()
            .FillContactForm(contact)
            .SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper ModifyContact(ContactData? contact, ContactData newcontact)
        {
            dynamic id = 0;
            if (contact is null)
            {
                contact = new ContactData("Lev", "Myasnikov");
                CreateContact(contact);
                Thread.Sleep(5000); 
                List<ContactData> updatedContacts = ContactData.GetAll();
                id = updatedContacts[updatedContacts.IndexOf(contact)].Id;
            }
            else
            {
               id = contact.Id;
            }
            SelectContact(id)
            .InitContactModification()
            .FillContactForm(newcontact)
            .SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public ContactHelper Logout()
        {
            //Logout
            driver.FindElement(By.LinkText("Logout")).Click();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            if (contactCache is null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> entries = driver.FindElements(By.XPath("//tr[@name='entry']"));
                foreach (IWebElement entry in entries)

                {
                    string lastName = entry.FindElements(By.XPath(".//td"))[1].Text;
                    string firstName = entry.FindElements(By.XPath(".//td"))[2].Text;
                    contactCache.Add(new ContactData(firstName, lastName));
                }

            }
            return new List<ContactData>(contactCache);
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

        }

        public ContactData GetContactInformationFromEditorForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification();
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");


            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                FirstEmail = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail
            };
        }

        public string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetails();

            return driver.FindElement(By.Id("content")).GetAttribute("innerText").TrimEnd('\r', '\n').Replace("\r", "");
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            ClearGroupFiltet();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFiltet()
        {
            driver.FindElement(By.Name("group")).Click();
            driver.FindElement(By.XPath("//option[@value='']")).Click();
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToTheGroupContactsPage(group.Id);
            SelectContactInGroup(contact.Id);
            RemoveContactInGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void RemoveContactInGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectContactInGroup(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }


    }
}