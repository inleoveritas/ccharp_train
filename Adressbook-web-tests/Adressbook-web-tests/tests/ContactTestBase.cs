using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    public class ContactTestBase : TestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFOM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = app.Contact.GetContactList();
                List<ContactData> fromDB = ContactData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }

        }
    }
}
