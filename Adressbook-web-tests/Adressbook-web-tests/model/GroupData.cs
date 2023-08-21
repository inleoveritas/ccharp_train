using Bogus.DataSets;
using DocumentFormat.OpenXml.Wordprocessing;
using LinqToDB.Mapping;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbook_web_tests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>  
    {
        private string name;
        private string header = "";
        private string footer = "";
        public GroupData()
        {
        }
        public GroupData(string name) 
        { 
            Name = name; 
        }

        public bool Equals(GroupData other) 
        {
            if (Object.ReferenceEquals(other, null)) 
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public bool Equals(object other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (!(other is GroupData))
                throw new ArgumentException("obj is not GroupData");
            var data = other as GroupData;
            if (data == null)
                return false;
            return this.Name == data.Name;
        }

        public override int GetHashCode() 
        {
            return Name.GetHashCode();
        }

        public override string ToString() 
        { 
            return "name=" + Name + "\nheader= " + Header + "\nfooter=" + Footer;    
        
        }

        public int CompareTo(GroupData other) 
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }

        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }
        
        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<GroupData> GetAll() 
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts 
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000 - 00 - 00 00:00:00")
                       select c).Distinct().ToList();
            }
        }
    }
}
