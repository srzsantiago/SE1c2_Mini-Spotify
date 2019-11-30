using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Login
    {
        private Person _user;
        public bool loggedin = false;
        AccessLevel access { get; set; }

        // Hard-coded credentials, uncomment code in 'Login' to use database credentials. 
        public string emailThis = "";
        public string passwordThis = "";
        public AccessLevel accessdb = AccessLevel.user;

        // login function
        public Login(string mail, string password)
        {
            
 
            String sql,Output = "";
            int level = 1;
            

            //GET EMAILADRESSES FROM DATABASE
            sql = "SELECT email FROM Person WHERE email = " + "'" + mail + "'";
            List<Dictionary<string, object>> Email = Database.DatabaseConnector.SelectQueryDB(sql);
            string databaseMail = "";

            foreach(var dictionary in Email)
            {
                foreach (var keyValue in dictionary)
                {
                    databaseMail =  keyValue.Value.ToString();
                }
            }

            if(databaseMail == mail)
            //if (this.email == email)
            {
                sql = "SELECT password FROM Person WHERE email = " + "'" + mail + "'";
                List<Dictionary<string, object>> Password = Database.DatabaseConnector.SelectQueryDB(sql);
                string databasePassword = "";

                foreach (var dictionary in Password)
                {
                    foreach (var keyValue in dictionary)
                    {
                        databasePassword = keyValue.Value.ToString();
                    }
                }


                if (databasePassword != password)
                //if (this.password != password)
                {
                    loggedin = false;
                }
                else
                {
                    emailThis = mail;
                    passwordThis = password;
                    sql = "SELECT rol FROM Person WHERE email = " + "'" + mail + "'";
                    List<Dictionary<string, object>> Role = Database.DatabaseConnector.SelectQueryDB(sql);
                    int databaseRole = 1;

                    foreach (var dictionary in Role)
                    {
                        foreach (var keyValue in dictionary)
                        {
                            string databaseRoleString = keyValue.Value.ToString();
                            databaseRole = Int32.Parse(databaseRoleString);
                        }
                    }

                    if (databaseRole == 1)
                    {
                        this.access = AccessLevel.user;
                    } else if (databaseRole == 2)
                    {
                        this.access = AccessLevel.artist;
                    } else if (databaseRole == 3)
                    {
                        this.access = AccessLevel.admin;
                    }
                    //this.access = accessdb;
                    loggedin = true;
                    // create new user with the loggedin bool and the access enum value
                    if (access == AccessLevel.user)
                    {
                        _user = new User(this.loggedin);
                    }
                    else if (access == AccessLevel.artist)
                    {
                        _user = new Artist(this.loggedin, "naam moet uit db", "Producer moet uit db");
                    }
                    else if (access == AccessLevel.admin)
                    {
                        _user = new Administrator(this.loggedin);
                    }
                }  
            } else
            {
                loggedin = false;
                Console.WriteLine("Username not found in our database");
            }

        }
    }
}
