using System.Data.SqlClient;

namespace EtruscanUnitDb
{
        class DbRead
        {               
                public DbRead()
                {
                        string connectionString = @"data source=gesuntight;initial catalog=EtruscanUnitDb;trusted_connection=true;MultipleActiveResultSets=True";
                        SqlConnection dbConnection = new SqlConnection(connectionString);
                        string queryString = "SELECT * FROM [dbo].[Units]";
                        SqlCommand command = new SqlCommand(queryString, dbConnection);
                        SqlDataReader dbReader;
                        using(dbConnection)
                        {
                                try
                                {
                                        dbConnection.Open();
                                        dbReader = command.ExecuteReader();
                                        while(dbReader.Read())
                                        {
                                                Object[] values = new object[dbReader.FieldCount];
                                                int fieldCount = dbReader.GetValues(values);
                                                for(int i = 0; i < fieldCount; i++)
                                                {
                                                        Console.WriteLine(values[i]);
                                                }
                                        }
                                        dbReader.Close();
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