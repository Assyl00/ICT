using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Book
{
    interface DataAccesLayer
    {
        ContactDTO GetContactById(string id);
        String CreateContact(ContactDTO contact);
        void DeleteContactById(string id);
        List<ContactDTO> GetAllContacts();
        String UpdateContact(string id, string name, string phone, string addr);

        List<ContactDTO> SearchContact(string name);

        List<ContactDTO> GetOrderedName();
        List<ContactDTO> GetPagination(int limit);
        

    }
    abstract class BaseContact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    class CreateContactCommand : BaseContact { }
    class ContactDTO : BaseContact //проекция контакта(на строрне датабейз)
    {
        public string Id { get; set; }
    }
    class BLL
    {
        DataAccesLayer dal = default(DataAccesLayer);
        public BLL(DataAccesLayer dal)
        {
            this.dal = dal;
        }

        public ContactDTO GetContact(string id)
        {
            return dal.GetContactById(id);
        }
        public string CreateContact(CreateContactCommand contact)
        {
            ContactDTO contact1 = new ContactDTO();
            contact1.Id = Guid.NewGuid().ToString();
            contact1.Name = contact.Name;
            contact1.Phone = contact.Phone;
            contact1.Address = contact.Address;

            return dal.CreateContact(contact1);
        }

        public void DeleteContact(string id)
        {
            dal.DeleteContactById(id);
            
        }
        public string UpdateContact(string id, string name, string phone, string addr)
        {

            return dal.UpdateContact(id, name, phone, addr);
        }

        public List<ContactDTO> SearchContact(string name)
        {
            if (name == "")
            {
                return dal.GetAllContacts();
            }
            return dal.SearchContact(name);
        }


        public List<ContactDTO> GetContacts()
        {
            
            return dal.GetAllContacts();
        }

        public ContactDTO GetContactById(string id)
        {
            return dal.GetContactById(id);
        }

        public List<ContactDTO> GetOrderedName()
        {
            return dal.GetOrderedName();
        }

        int offset = 0;
        int page = 1;
        public List<ContactDTO> GetPaginationNext()
        {
            if (offset <= GetContacts().Count)
            {
                offset += 2;
                page++;
                return dal.GetPagination(offset);
                
            }
            else
            {
                return dal.GetPagination(offset);
            }

        }

        public List<ContactDTO> GetPaginationPrev()
        {
            if (offset <= 0)
            {
                return dal.GetPagination(0);
            }
            else
            {
                offset -= 2;
                page--;
                return dal.GetPagination(offset);
            }

        }

        public List<ContactDTO> GetCurrentCont()
        {
            int PreviousPageOffSet = (page - 1) * offset;
            return dal.GetPagination(PreviousPageOffSet);
        }

    }
}
