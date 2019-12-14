using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Login
    {
        private readonly Person _user;
        public bool loggedin = false;
        AccessLevel access { get; set; }
        public AccessLevel accessdb = AccessLevel.user;


       
        public Login(string mail, string password)//LoginAttempt
        {
            mail = mail.ToLower(); //set the mail to lowerCase.

            //GET EMAILADRESSES FROM DATABASE
            string sql = "SELECT email FROM Person WHERE email = " + "'" + mail + "'";
            List<Dictionary<string, object>> Email = Database.DatabaseConnector.SelectQueryDB(sql);

            if (Email.Count > 0)//if there is a match with the given email
            {

                //get the password and rol for the given email from the databse
                sql = "SELECT password, rol FROM Person WHERE email = " + "'" + mail + "'";
                List<Dictionary<string, object>> PasswordAndRole = Database.DatabaseConnector.SelectQueryDB(sql);
                string databasePassword = PasswordAndRole.ElementAt(0).ElementAt(0).Value.ToString();//set databasePassword
                int databaseRole = Int32.Parse(PasswordAndRole.ElementAt(0).ElementAt(1).Value.ToString());//set databaseRole as a int


                if (!AuthenticateUser(mail, password, databasePassword))//check if the given password match the password from the database
                {
                    loggedin = false;
                }
                else
                {
                    //create a user in the application with the correspondent AccessLevel/Role
                    if (databaseRole == 1)
                    {
                        access = AccessLevel.user;
                        _user = new User(loggedin);
                    } else if (databaseRole == 2)
                    {
                        this.access = AccessLevel.artist;
                        _user = new Artist(this.loggedin, "naam moet uit db", "Producer moet uit db");
                    } else if (databaseRole == 3)
                    {
                        this.access = AccessLevel.admin;
                        _user = new Administrator(this.loggedin);
                    }
                    
                    loggedin = true;
                }  
            } else //the given email doesn't exist in our database
            {
                loggedin = false;
                Console.WriteLine("Username not found in our database");
            }

        }

        public bool AuthenticateUser(string username, string password, string databasePassword)
        {
            char[] delimiter = { ':' };//the databasePassword contains an ItarationNumber, and salt and a hash. These are separated by a :
            var split = databasePassword.Split(delimiter);//split the databasePassword
           
            var iterations = Int32.Parse(split[0]);
            var salt = Convert.FromBase64String(split[1]);
            var hash = split[2];

            var rfc2898 = new Rfc2898DeriveBytes(password, salt, iterations);//create a PBKDF2 hash with the same salt and iteration as the password stored in the database

            var PasswordToCheck = Encoding.Default.GetString(rfc2898.GetBytes(32));
            var storedPassword = hash;

            return PasswordToCheck == storedPassword; //compare the given password hash and the databasepassword hash
        }

    }
}
