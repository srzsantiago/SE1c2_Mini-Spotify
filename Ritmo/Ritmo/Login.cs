using System;
using System.Collections.Generic;
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
        public string email = "gebruiker1@mail.com";
        public string password = "gebruiker1";
        public AccessLevel accessdb = AccessLevel.admin;

        // login function
        public Login(string email, string password)
        {
            /* SqlCommand command;
             * SqlDataReader dataReader;
             * String sql,Output = "";
             * 
             * GET EMAILADRESSES FROM DATABASE
             * sql = "SELECT mail FROM users WHERE mail = email";
             * command = new SqlCommand (sql, conn); // conn contains the connection with the database. 
             * dataReader = command.ExecuteReader();
             * 
             * while (dataReader.Read()){
             *  Output = Output + dataReader.GetValue(0)
             * }
             */

            // if(Output == email)
            if (this.email == email)
            {
                /* sql = "SELECT password FROM users WHERE mail = email";
                 * command = new SqlCommand (sql, conn); // conn contains the connection with the database. 
                 * dataReader = command.ExecuteReader();
                 * 
                 * while (dataReader.Read()){
                 *  Output = Output + dataReader.GetValue(0)
                 * }
                 */

                // if (output != password)
                if (this.password != password)
                {
                    loggedin = false;
                }
                else
                {
                    this.email = email;
                    this.password = password;
                    /* sql = "SELECT access FROM users WHERE mail = email";
                    * command = new SqlCommand (sql, conn); // conn contains the connection with the database. 
                    * dataReader = command.ExecuteReader();
                    * 
                    * while (dataReader.Read()){
                    *  Output = Output + dataReader.GetValue(0)
                    * }
                    */

                    //this.access = output;
                    this.access = accessdb;
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
