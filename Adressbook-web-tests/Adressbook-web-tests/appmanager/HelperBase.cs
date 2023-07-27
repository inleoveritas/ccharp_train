using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Adressbook_web_tests
{
    public class HelperBase
    {
        protected IWebDriver driver;
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager; 
            driver = manager.Driver;
        }
    }
}