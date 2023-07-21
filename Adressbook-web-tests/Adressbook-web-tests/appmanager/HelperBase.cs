using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Adressbook_web_tests
{
    public class HelperBase
    {
        protected IWebDriver driver;

        public HelperBase(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}