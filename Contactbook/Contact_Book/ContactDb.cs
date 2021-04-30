using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Book
{
    class ContactDB : DataAccesLayer, IDisposable
    {

        SQLiteConnection con = default(SQLiteConnection);
        string cs = @"URI=file:test.db";
        public ContactDB()
        {
            con = new SQLiteConnection(cs);
            con.Open();
            PrepareDB();
        }

        public void Dispose()
        {
            con.Close();
        }

        private void ExecuteNonQuery(string commandText)
        {
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = commandText;
            cmd.ExecuteNonQuery();
        }
        private void PrepareDB()
        {
            //SQLiteConnection.CreateFile("test.db");
            ExecuteNonQuery("DROP TABLE IF EXISTS contacts");
            ExecuteNonQuery("CREATE TABLE contacts(id STRING PRIMARY KEY, name TEXT, phone TEXT, address TEXT)");
        }

        public string CreateContact(ContactDTO contact)
        {
            string text = string.Format("INSERT INTO contacts(id, name, phone, address) VALUES('{0}', '{1}', '{2}', '{3}')"
                , contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address
                );

            ExecuteNonQuery(text);
            return contact.Id;
        }


        public void DeleteContactById(string id)
        {
            string text = "DELETE FROM contacts WHERE id = '"+id+"'";
            ExecuteNonQuery(text);
            
        }
    

        public string UpdateContact(string id, string name, string phone, string addr)
        {
            string text = "UPDATE contacts SET name = '"+name+ "', phone = '" + phone + "', address = '" + addr + "' WHERE id = '" +id+"'";
            ExecuteNonQuery(text);
            return id;
        }

        public List<ContactDTO> GetAllContacts()
        {
            List<ContactDTO> res = new List<ContactDTO>();
            
            string selectSql = "select * from contacts";
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                    
                }
            }
            return res;
        }
        public List<ContactDTO> SearchContact(string name)
        {
            
            List<ContactDTO> res = new List<ContactDTO>();
            
            string text = "SELECT * FROM contacts WHERE name LIKE '" + name + "'";
            using (SQLiteCommand command = new SQLiteCommand(text, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                }
            }
            
            
            return res;
            
        }

        public ContactDTO GetContactById(string id)
        {
            ContactDTO res = new ContactDTO();


            string selectSql = "select * from contacts where id = '"+id+"'";
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                }
            }
            return res;
        }

        public List<ContactDTO> GetOrderedName()
        {
            List<ContactDTO> res = new List<ContactDTO>();

            string selectSql = "SELECT * FROM contacts ORDER BY name ASC";
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);

                }
            }
            return res;
        }

        public List<ContactDTO> GetPagination(int limit)
        {
            List<ContactDTO> res = new List<ContactDTO>();

            string selectSql = "SELECT * FROM contacts LIMIT 2 OFFSET '"+limit+"'";
            using (SQLiteCommand command = new SQLiteCommand(selectSql, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);

                }
            }
            return res;
        }


    }
}
