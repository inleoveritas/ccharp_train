using Adressbook_web_tests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        private List<GroupData> groupCache = null;

        public GroupHelper CreateGroup(GroupData group) 
        {
            manager.Navigator.GoToTheGroupPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            Return();

            return this;

        }
        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToTheGroupPage();
            if (!GroupExists(p))
            {
                CreateGroup(new GroupData("lev", "Group", "Test"));
            }
            SelectGroup(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            Return();

            return this;
        }

        public GroupHelper Modify(GroupData? group, GroupData newData)
        {
            manager.Navigator.GoToTheGroupPage();
            dynamic id = 0;
            if (group is null)
            {
                CreateGroup(new GroupData("lev", "Group", "Test"));
            }
            else
            {
                id = group.Id;
            }
            SelectGroup(id);   
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            Return();
            return this;
        }

        public GroupHelper Remove(int p)
        {
            manager.Navigator.GoToTheGroupPage();
            if (!GroupExists(p))
            {
                CreateGroup(new GroupData("lev", "Group", "Test"));
            }
            SelectGroup(p);
            DeleteGroup();
            Return();
            return this;
        }
        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToTheGroupPage();

            SelectGroup(group.Id);
            DeleteGroup();
            Return();
            return this;
        }
        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }


        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper Return()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index+1) + "]/input")).Click();
            return this;
        }
        public GroupHelper SelectGroup(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public bool GroupExists(int index)
        {
            var allGroups = driver.FindElements(By.XPath("//div[@id='content']/form/span/input"));

            if (allGroups.Count > index)
            {
                return true;
            }
            return false;
        }
        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public List<GroupData> GetGroupList()
        {
            if (groupCache is null) 
            {
                groupCache = new List<GroupData>();
                manager.Navigator.GoToTheGroupPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(element.Text));
                }
            }
            return new List<GroupData>(groupCache);    
        }


    }
}
