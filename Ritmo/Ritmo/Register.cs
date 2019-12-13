using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private int _numberOfIterations = 50000;

        public Register(string name, string mail, string password)
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

            string HashedPassword = GenerateHash(password);
            
            //if (email != Output)
            if (this.mailexists == false)
            {
                Message = "Your account has been successfully created";

                sql = "INSERT INTO Person VALUES (" + "'" + mail + "', '" + HashedPassword + "', '" + name + "', " + 1 + ")";
                Database.DatabaseConnector.InsertQueryDB(sql);
            }
            else
            {
                Message = "This email already exists";
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

        public string GenerateHash(string password)
        {
            var salt = GenerateSalt();
            var rfc2898 = new Rfc2898DeriveBytes(password, salt, _numberOfIterations);

            var saltAsString = Convert.ToBase64String(salt);
            var hash = Encoding.Default.GetString(rfc2898.GetBytes(32));
            return _numberOfIterations + ":" +
               saltAsString + ":" +
               hash;
        }

        public static byte[] GenerateSalt()
        {
            var salt = new byte[24];

            var randomProvider = new RNGCryptoServiceProvider();
            randomProvider.GetBytes(salt);

            return salt;
        }
    }
}
