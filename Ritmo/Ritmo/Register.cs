﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Register
    {
        public string Message;

        String sql;
        private bool mailexists = false;
        private int _numberOfIterations = 50000; //set the number of iterations used in PBKDF2 to avoid Brute force

        //register for normal user
        public Register(string name, string mail, string password)
        {
            name = name.ToLower();

            //Check if the filled email exist
            sql = $"SELECT email FROM Person WHERE email = '{mail}'";
            List<Dictionary<string, object>> EmailExist = Database.DatabaseConnector.SelectQueryDB(sql);
            
            if(EmailExist.Count > 0)//if the list contains more than one record, it means the email is already in use
            {
                mailexists = true;
            }
            
            if (!mailexists)//if the mail is not in use
            {
                if(password.Length <= 8 && password.Length >= 20)
                {
                    string HashedPassword = GenerateHash(password); //Generate a PBKDF2 password hashed

                    sql = "INSERT INTO Person VALUES (" + "'" + mail + "', '" + HashedPassword + "', '" + name + "', " + 1 + ")";
                    Database.DatabaseConnector.InsertQueryDB(sql);

                    Message = "Your account has been successfully created";
                }
                else
                {
                    Message = "Password must be minimum 8 and maximum 20 characters";
                }
                
            }
            else
            {
                Message = "This email already exists";
            }
            
        }


        //register for artist
        public Register(string mail)
        {
            //Check if the filled email exist
            sql = "SELECT email FROM Person WHERE email = " + "'" + mail + "'";
            List<Dictionary<string, object>> EmailExist = Database.DatabaseConnector.SelectQueryDB(sql);

            if (EmailExist.Count > 0)//if the list contains more than one record, it means the email is already in use
            {
                mailexists = true;
            }

            if (!mailexists)//if the mail is not in use
            {
                sql = "INSERT INTO Person VALUES (" + "'" + mail + "', '" + "', '"  + "', " + 2 + ")";
                Database.DatabaseConnector.InsertQueryDB(sql);
                Message = "Your account has been successfully created";
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
            var salt = GenerateSalt();//generate a salt, a 24 array of random bytes

            var rfc2898 = new Rfc2898DeriveBytes(password, salt, _numberOfIterations);//PBKDF2 function to hash the password

            var saltAsString = Convert.ToBase64String(salt);//convert the salt to a string so it can be stored in teh database
            var hash = Encoding.Default.GetString(rfc2898.GetBytes(32)); //convert hash to string

            return _numberOfIterations + ":" + saltAsString + ":" + hash;
        }

        public static byte[] GenerateSalt()//generate a array of random bytes
        {
            var salt = new byte[24];

            var randomProvider = new RNGCryptoServiceProvider();
            randomProvider.GetBytes(salt);

            return salt;
        }
    }
}
