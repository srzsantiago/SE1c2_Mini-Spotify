using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Authentication;
using System.Text;

namespace Ritmo.Database
{
    public static class DatabaseConnector
    {
        //SSH connection and database connection attributes
        private static SshClient sshClient = new SshClient("145.44.233.191", 22, "student", new PrivateKeyFile(new Uri(@"SSH\id_rsa", UriKind.RelativeOrAbsolute).ToString()));
        private static SqlConnection dbConn;

        //Creates an SSH connection to the server
        public static void ConnectSSH()
        {
            try
            {
                sshClient.Connect();
                if (sshClient.IsConnected)
                {
                    var portForwarded = new ForwardedPortLocal("127.0.0.1", 1433, "127.0.0.1", 1433);
                    sshClient.AddForwardedPort(portForwarded);
                    portForwarded.Start();
                    Console.WriteLine("Server reached.");
                }
                else
                {
                    Console.WriteLine("Client cannot be reached...");
                }

            }
            #region Exception handling SSH Connection
            catch (SshConnectionException e)
            {
                throw new SshConnectionException($"SSH connection failed: {e.Message}");
            }
            catch (SshAuthenticationException e)
            {
                throw new SshAuthenticationException($"SSH authentication failed: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Undetermined SSH error: {e.Message}");
            }
            #endregion

        }

        //Sets up connection with Ritmo database
        public static SqlConnection ConnectDB()
        {
            dbConn = new SqlConnection("SERVER=127.0.0.1;UID=SA;PASSWORD=IctSe1c_Groep2;DATABASE=Ritmo");
            dbConn.Open();
            if (dbConn.State == ConnectionState.Open)
            {
                Console.WriteLine("Database connection succeeded.");
                return dbConn;
            }
            else
            {
                throw new Exception("Connecting to database failed.");
            }
        }

        //Disconnects the SSH connection to the server
        public static void DisconnectSSH()
        {
            sshClient.Disconnect();
        }

        //Disconnects the database connection
        public static void DisconnectDB()
        {
            dbConn.Close();
            if (dbConn.State == ConnectionState.Closed)
                Console.WriteLine("Database connection ended.");
            else
                throw new Exception("Closing database connection failed");
        }

        //Sets up connection to database. Sets up query and executes it. Disposes querycommand when it's done.
        //All SQL query method call this method
        //Returns SqlDataReader so that SelectQueryDB can read the returned records.
        private static SqlDataReader ExecuteQuery(string query)
        {
            try
            {
                if (dbConn == null || dbConn.State == ConnectionState.Closed)
                    ConnectDB();
                using (SqlCommand QueryCommand = new SqlCommand(query, dbConn))
                {
                    return QueryCommand.ExecuteReader();
                }
            }
            #region Exception Handling
            catch (SqlException e)
            {
                DisconnectDB();
                throw new Exception($"Database or query failure: {e.Message}");
            }
            catch (Exception e)
            {
                DisconnectDB();
                throw new Exception($"Undetermined database or query failure: {e.Message}");
            }
            #endregion
        }

        //Sets up Connection to database and calls overloaded ExecuteQuery
        private static void ExecuteQuery(string query, QueryType queryType)
        {
            try
            {
                using (ConnectDB())
                {
                    if (ExecuteQuery(query).RecordsAffected == 0)
                    {
                        throw new Exception($"{queryType} query altered no records.");
                    }
                }
            }
            finally
            {
                DisconnectDB();
            }
        }

        //Queries the database and inserts data with the given query
        public static void InsertQueryDB(string query)
        {
            QueryType queryType = QueryType.INSERT;

            CorrectQueryUsed(queryType, query);
            ExecuteQuery(query, queryType);
        }

        //Queries the database and deletes record with the given query
        public static void DeleteQueryDB(string query)
        {
            QueryType queryType = QueryType.DELETE;

            CorrectQueryUsed(queryType, query);
            ExecuteQuery(query, queryType);
        }

        //Queries the database and updates it with the given query
        public static void UpdateQueryDB(string query)
        {
            QueryType queryType = QueryType.UPDATE;

            CorrectQueryUsed(queryType, query);
            ExecuteQuery(query, queryType);
        }

        //Queries the database and returns a List filled with Dictionary. 
        //List represent rows. 
        //Dictionary represent columns. Key is columnname. Value is database value.
        public static List<Dictionary<string, object>> SelectQueryDB(string query)
        {
            try
            {
                CorrectQueryUsed(QueryType.SELECT, query);

                //Executes query 
                SqlDataReader reader = ExecuteQuery(query);

                List<Dictionary<string, object>> rowsWithResults = new List<Dictionary<string, object>>(); //List that represent the rows. Its filled with a dictionary that represent the columns.
                Dictionary<string, object> Results = new Dictionary<string, object>(); //Dictionary that represent the column. Key is the column name. Value is corresponding column value.

                //goes through all results and places them in a collection
                while (reader.Read())
                {
                    //Get results and place them in dictionary Results
                    for (int column = 0; column < reader.FieldCount; column++)
                    {
                        Results.Add(reader.GetName(column), reader.GetValue(column));
                    }
                    //Checks if all columns have been passed and places the row in the List.
                    rowsWithResults.Add(Results); //Rows gets added
                    Results = new Dictionary<string, object>(); //Row gets cleared to be re-used.
                }

                return rowsWithResults;
            }
            #region Querying Exception Handling
            catch (SqlException e)
            {
                throw new Exception($"Database or query failure: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Undetermined database or query failure: {e.Message}");
            }
            #endregion
            finally
            {
                DisconnectDB();
            }
        }

        //Prints results to console. For testing purposes.
        public static void SelectQueryDBPrint(string query)
        {
            //Calls QueryDB methods with given query. Loops through the dictionaries in the returned List
            foreach (Dictionary<string, object> row in SelectQueryDB(query))
            {
                foreach (var column in row)
                {
                    Console.Write($"[{column.Key}: {column.Value}] ");
                }
                Console.WriteLine();
            }
        }

        //Checks if the Query corresponds to the correct method.
        private static void CorrectQueryUsed(QueryType queryType, string query)
        {
            if (!query.ToUpper().StartsWith(queryType.ToString()))
                throw new Exception($"{queryType} was not used in its corresponding method!");
        }

        public static bool IsSSHConnected()
        {
            return sshClient.IsConnected;
        }
    }
}
