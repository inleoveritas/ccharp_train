using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adressbook_web_tests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

     

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }
       
        [Column(Name = "address")]
        public string Address { get; set; }
       
        [Column(Name = "home")]
        public string HomePhone { get; set; }
       
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
       
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        
        [Column(Name = "email")]
        public string FirstEmail { get; set; }
       
        [Column(Name = "email2")]
        public string SecondEmail { get; set; }
       
        [Column(Name = "email3")]
        public string ThirdEmail { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }

                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

            public string AllEmails{
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }

                else
                {
                    return (CleanUp(FirstEmail) + CleanUp(SecondEmail) + CleanUp(ThirdEmail)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "") 
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }
        

        public bool Equals(ContactData other)
        { 
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override bool Equals(object other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (!(other is ContactData))
                throw new ArgumentException("obj is not ContactData");
            var data = other as ContactData;
            if (data == null)
                return false;
            return Firstname == data.Firstname && Lastname == data.Lastname;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Firstname.GetHashCode();
                hash = hash * 23 + Lastname.GetHashCode();
                return hash;
            }
        }

        private bool hasPhone()
        {
            return !(String.IsNullOrWhiteSpace(HomePhone) && String.IsNullOrWhiteSpace(MobilePhone) && String.IsNullOrWhiteSpace(WorkPhone));
        }
        private bool hasEmail()
        {
            return !(String.IsNullOrWhiteSpace(FirstEmail) && String.IsNullOrWhiteSpace(SecondEmail) && String.IsNullOrWhiteSpace(ThirdEmail));
        }

        public override string ToString()
        {
            string text = "";
            /*text = $"{Firstname} {Lastname}"
                + "\n" + $"{Address}"
                + "\n" + $"H: {HomePhone}"
                + "\n" + $"M: {MobilePhone}"
                + "\n" + $"W: {WorkPhone}" + "\n"
                + "\n" + $"{FirstEmail}"
                + "\n" + $"{SecondEmail}"
                + "\n" + $"{ThirdEmail}";
            */
            text = $"{Firstname} {Lastname}";
            if (!String.IsNullOrWhiteSpace(Address))
            {
                text += "\n" + $"{Address}";
            }
            if (hasPhone())
            {
                text += "\n";

                if (!String.IsNullOrWhiteSpace(HomePhone))
                {
                    text += "\n" + $"H: {HomePhone}";
                }
                if (!String.IsNullOrWhiteSpace(MobilePhone))
                {
                    text += "\n" + $"M: {MobilePhone}";
                }
                if (!String.IsNullOrWhiteSpace(WorkPhone))
                {
                    text += "\n" + $"W: {WorkPhone}";
                }
            }
            if (hasEmail())
            {
                text += "\n";

                if (!String.IsNullOrWhiteSpace(FirstEmail))
                {
                    text += "\n" + $"{FirstEmail}";
                }
                if (!String.IsNullOrWhiteSpace(SecondEmail))
                {
                    text += "\n" + $"{SecondEmail}";
                }
                if (!String.IsNullOrWhiteSpace(ThirdEmail))
                {
                    text += "\n" + $"{ThirdEmail}";
                }
            }

            return text;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }
            int res = Lastname.CompareTo(other.Lastname);

            if (res == 0)
            {
                res = Firstname.CompareTo(other.Firstname); 
            }
            return res;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000 - 00 - 00 00:00:00") select c).ToList();
            }
        }

        public static List<ContactData> GetAllContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }
    }
}
