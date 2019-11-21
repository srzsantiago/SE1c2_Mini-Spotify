using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Register
    {
        /* SqlCommand command;
         * SqlDataReader dataReader;
         * String sql,Output = "";
         */
        private string  Name;
        private string  Email;
        private string  Password;
        private bool    Artist;

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
                    return "Your account has been successfully created";
                }
                else
                {
                    return "This email already exists";
                }
            }
            else
            {
                return "The passwords do not match";
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
            }
        }
    }
}
