using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Register
    {
        /* SqlCommand command;
         * SqlDataReader dataReader;
         * String sql,Output = "";
         */
        private string  Name;
        private string  Email;
        private string  Password;
        private bool    Artist;
        public string   Message;

        public string existingTestMail = "email@example.com"; 

        public Register(string name, string email, string password, string confirmpw)
        {
            if(password == confirmpw)
            {
                /*GET EMAILADRESSES FROM DATABASE
                * sql = "SELECT mail FROM users WHERE mail = email";
                * command = new SqlCommand(sql, conn); // conn contains the connection with the database. 
                * dataReader = command.ExecuteReader();
                *
                * while (dataReader.Read())
                * {
                *   Output = Output + dataReader.GetValue(0)
                * }
                */

                //if (email != Output)
                if(email != existingTestMail)
                {
                    Name        = name;
                    Email       = email;
                    Password    = password;
                    Artist      = false;
                    Message = "Your account has been successfully created";
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

        public Register(string email)
        {
            /*GET EMAILADRESSES FROM DATABASE
             * sql = "SELECT mail FROM users WHERE mail = email";
             * command = new SqlCommand(sql, conn); // conn contains the connection with the database. 
             * dataReader = command.ExecuteReader();
             *
             * while (dataReader.Read())
             * {
             *   Output = Output + dataReader.GetValue(0)
             * }
             */

            //if (email != Output)
            if (email != existingTestMail)
            {
                Email   = email;
                Artist  = true;
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
