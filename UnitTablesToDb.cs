namespace EtruscanUnitDb;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

public class UnitTablesToDb
{

        public UnitTablesToDb(string filePath, string connectionString, string tableName)
        {
                try{
                        ReadCSV csv = new ReadCSV(filePath);
                        string[] header = csv.content.First();
			// header contains column names

			            CreateTable(header, connectionString, tableName);
                        
                        string[][] body = csv.content.Where((a,b) => b != 0).ToArray();
                        // body contains values 
                        
                        FillTable(body, connectionString, tableName);
                }
                catch(Exception e)
                {
                        Console.WriteLine(e.ToString());
                }
                

        }

        public void CreateTable(string[] header, string connectionString, string tableName)
        {
                string createQueryDb = $"CREATE TABLE {tableName} (" 
                                        + "[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL, ";

                for(int i = 0; i < header.Length; i++)
                {
                        if(String.IsNullOrEmpty(header[i]))
                        {
                                break;
                        }
                        createQueryDb = createQueryDb 
                                        + " [" + header[i] + "] "
                                        + " [VARCHAR] (255) NULL, "
                                        ;
                }

                createQueryDb = createQueryDb + " );";

                //Test
                //Console.WriteLine(createQueryDb);

                //Create db table from read CSV
                SqlConnection dbConnection = new SqlConnection(connectionString);
                SqlCommand createCommand = new SqlCommand(createQueryDb, dbConnection);
                using(dbConnection)
                {
                        try
                        {
                                dbConnection.Open();
                                createCommand.ExecuteNonQuery();
                                dbConnection.Close();
                        }
                        catch(SqlException e)
                        {
                                
                                if(e.ToString().Contains("2714"))
                                {
                                        Console.WriteLine("Table could not be created since it already exists.");
                                        
                                }
                                else
                                {
                                        Console.WriteLine(e.ToString());
                                }
                                /*
                                Console.WriteLine(e.ToString());
                                Console.WriteLine(e.ErrorCode.ToString());
                                */
                        }
                        finally
                        {
                                if(dbConnection.State == System.Data.ConnectionState.Open)
                                {
                                        dbConnection.Close();
                                }
                        }
                }
        }

        public void FillTable(string[][] body, string connectionString, string tableName)
        {
                foreach(string[] line in body)
                {
                        string unitKey = line[0];
                        string insertRowQuery = $"INSERT INTO [dbo].[{tableName}] VALUES ( '";
                        for(int i = 0; i < line.Length - 1; i++)
                        {
                                string entry = line[i];
                                insertRowQuery = insertRowQuery + entry + "','";
                        }
                        insertRowQuery = insertRowQuery + line[line.Length - 1] + "');";
                        insertRowQuery = $"IF NOT EXISTS (SELECT 1 FROM [dbo].[{tableName}] WHERE [key] = '" 
                                        + unitKey + "')" + insertRowQuery;

                        //Test
                        //Console.WriteLine(insertRowQuery);
                        SqlConnection dbConnection = new SqlConnection(connectionString);
                        SqlCommand insertCommand = new SqlCommand(insertRowQuery, dbConnection);
                        using(dbConnection)
                        {
                                try
                                {
                                        dbConnection.Open();
                                        insertCommand.ExecuteNonQuery();
                                        dbConnection.Close();
                                }
                                catch(SqlException e)
                                {
                                        Console.WriteLine(e.ToString());
                                }
                        }
                }
        }
}