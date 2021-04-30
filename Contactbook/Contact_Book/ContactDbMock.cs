using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Book
{
    class ContactDbMock 
    {
        List<ContactDTO> contacts = new List<ContactDTO>();
        public string CreateContact(ContactDTO contact)
        {
            contacts.Add(contact);
            return contact.Id;
        }

        public void DeleteContactById(string id)
        {
            ContactDTO con = contacts.Find(x => x.Id == id);
            if(con != null)
            {
                contacts.Remove(con);
                //return true;
            }

            //return false;
        }

        public List<ContactDTO> GetAllContacts()
        {
            return contacts;
        }

        public ContactDTO GetContactById(string id)
        {
            return contacts.Find(x => x.Id == id);
        }

        public string UpdateContact(string id, string str)
        {
            throw new NotImplementedException();
        }
    }
}
