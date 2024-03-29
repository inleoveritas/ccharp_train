﻿using Adressbook_web_tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;


        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";
            verificationErrors = new StringBuilder();;
            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        
        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public LoginHelper Auth
        { 
            get 
            { 
                return loginHelper; 
            } 
        }
        public NavigationHelper Navigator
        { 
            get 
            { 
                return navigator; 
            } 
        }
        public GroupHelper Groups
        { 
            get 
            { 
                return groupHelper; 
            } 
        }
        public ContactHelper Contact
        {
            get 
            { 
                return contactHelper;   
            }
        }


    }
}
