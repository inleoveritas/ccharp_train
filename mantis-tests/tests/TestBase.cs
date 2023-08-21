using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace mantis_tests
{
    public class TestBase
    {
        public static bool PERFOM_LONG_UI_CHECKS = false;
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();

            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }

        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {

            int l = Convert.ToInt32(Math.Floor(rnd.NextDouble() * max));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++) 
            {
                double flt = rnd.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                char letter = Convert.ToChar(shift + 65);
                builder.Append(letter);
            }
            return builder.ToString();
        }
    }
}
