using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit.Tests
{

    [TestFixture]
    public class GroupDeletionTest : TestBase
    {

        [Test]
        public void TestGroupDelete()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData group = oldGroups[0];

            app.Groups.Remove(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.Equals(oldGroups.Count - 1, newGroups.Count);
        }
    }
}
