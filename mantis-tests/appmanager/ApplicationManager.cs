using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

       

        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.2.0/login_page.php";
            Registration = new RegistrationHelper(this);
            verificationErrors = new StringBuilder();;
        }
        public RegistrationHelper Registration { get; set; }
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
 


    }
}
