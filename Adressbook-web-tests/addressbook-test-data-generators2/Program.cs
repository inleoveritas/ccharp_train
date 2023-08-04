using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Adressbook_web_tests;



namespace addressbook_test_data_generators
{
    class Program
    {
        static int Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            var data = new object();

            switch (type)
            {
                case "contacts":
                    data = generateContacts(count);
                    break;
                case "groups":
                    data = generateGroups(count);
                    break;
                default:
                    Console.Out.Write("Unrecognized type: " + type);
                    writer.Close();
                    return -1;
            }

            switch (format)
            {
                case "xml":
                    writeDataToXMLFile(writer, data);

                    break;
                default:
                    Console.Out.Write("Unrecognized format: " + format);
                    writer.Close();
                    return -1;
            }

            writer.Close();
            return 0;
        }

        static List<GroupData> generateGroups(int count)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }
            return groups;
        }

        static List<ContactData> generateContacts(int count)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30))
                {
                    Address = TestBase.GenerateRandomString(90),
                    HomePhone = TestBase.GenerateRandomString(90),
                    MobilePhone = TestBase.GenerateRandomString(90),
                    WorkPhone = TestBase.GenerateRandomString(90),
                    FirstEmail = TestBase.GenerateRandomString(90),
                    SecondEmail = TestBase.GenerateRandomString(90),
                    ThirdEmail = TestBase.GenerateRandomString(90)
                });
            }
            return contacts;
        }

        static void writeDataToXMLFile(StreamWriter writer, object data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var type = data.GetType();

            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
                throw new ArgumentException("Type must be List<>, but was " + type.FullName, "data");

            new XmlSerializer(type).Serialize(writer, data);
        }
    }
}