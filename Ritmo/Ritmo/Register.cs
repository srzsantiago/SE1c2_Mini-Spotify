using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Register
    {
        public string   Message;

        String sql, Output = "";
        private bool mailexists = false;
        public string existingTestMail = "email@example.com"; 

        public Register(string name, string mail, string password, string confirmpw)
        {
            if(password == confirmpw)
            {
                sql = "SELECT email FROM Person WHERE email = " + "'" + mail + "'";
                List<Dictionary<string, object>> Email = Database.DatabaseConnector.SelectQueryDB(sql);
                List<string> databaseMail = new List<string>();

                foreach (var dictionary in Email)
                {
                    foreach (var keyValue in dictionary)
                    {
                        databaseMail.Add(keyValue.Value.ToString());
                    }
                }

                foreach (string dbmail in databaseMail)
                {
                    if(mail == dbmail)
                    {
                        this.mailexists = true;
                    }
                }

                //if (email != Output)
                if (this.mailexists == false)
                {
                    Message = "Your account has been successfully created";

                    sql = "INSERT INTO Person VALUES (" + "'" + mail + "', '" + password + "', '" + name + "', " + 1 + ")";
                    Database.DatabaseConnector.InsertQueryDB(sql);
                }
                else
                {
                    Message = "This email already exists";
                }
            }
            else
            {
                Message = "The passwords do not match";
            }
        }

        public Register(string mail)
        {
            sql = "SELECT email FROM Person WHERE email = " + "'" + mail + "'";
            List<Dictionary<string, object>> Email = Database.DatabaseConnector.SelectQueryDB(sql);
            List<string> databaseMail = new List<string>();

            foreach (var dictionary in Email)
            {
                foreach (var keyValue in dictionary)
                {
                    databaseMail.Add(keyValue.Value.ToString());
                }
            }

            foreach (string dbmail in databaseMail)
            {
                if (mail == dbmail)
                {
                    this.mailexists = true;
                }
            }

            if (this.mailexists == false)
            {
                sql = "INSERT INTO Person VALUES (" + "'" + mail + "', '" + "', '"  + "', " + 2 + ")";
                Database.DatabaseConnector.InsertQueryDB(sql);
            } else
            {
                Message = "This email already exists";
            }
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
