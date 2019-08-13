using Database_of_email_addresses.DBController.DBContexts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Database_of_email_addresses.DBController
{
    public static class DBInitializator
    {
        public async static void Initialize(AddressContext addrContext)
        {
            DataTable addresses = new DataTable("Addresses");
            addresses.Columns.Add("Country", typeof(string));
            addresses.Columns.Add("Area", typeof(string));
            addresses.Columns.Add("City", typeof(string));
            addresses.Columns.Add("Street", typeof(string));
            addresses.Columns.Add("Housing", typeof(string));
            addresses.Columns.Add("House", typeof(int));
            addresses.Columns.Add("PostCode", typeof(string));
            addresses.Columns.Add("Date", typeof(string));

            int k = 1;

            if (!addrContext.Addresses.Any())
            {
                for (int cc = 1; cc < 3; cc++)
                {
                    for (int i = 1; i < 10001; i++)
                    {
                        for (int r = 1; r < 51; r++)
                        {
                            addresses.Rows.Add("Страна" + cc.ToString(), "Область" + k++.ToString(), "Город" + k++.ToString(), "Улица" + k++.ToString(), "Корпус" + k++.ToString(), k++, k++.ToString());
                        }
                    }

                    AddDataToSqlDB(addresses);
                    addresses.Clear();
                }
            }
        }

        public static void AddDataToSqlDB(DataTable addresses)
        {
            using (var connection = new SqlConnection(@"Server=DESKTOP-COESAPT\SQLEXPRESS;Database=emailaddressesstoredb;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                SqlTransaction transaction = null;
                connection.Open();
                try
                {
                    transaction = connection.BeginTransaction();
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
                    {
                        sqlBulkCopy.BulkCopyTimeout = 120;
                        sqlBulkCopy.DestinationTableName = "Addresses";
                        sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                        sqlBulkCopy.ColumnMappings.Add("Area", "Area");
                        sqlBulkCopy.ColumnMappings.Add("City", "City");
                        sqlBulkCopy.ColumnMappings.Add("Street", "Street");
                        sqlBulkCopy.ColumnMappings.Add("Housing", "Housing");
                        sqlBulkCopy.ColumnMappings.Add("House", "House");
                        sqlBulkCopy.ColumnMappings.Add("PostCode", "PostCode");

                        sqlBulkCopy.WriteToServer(addresses);
                        sqlBulkCopy.Close();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    connection.Dispose();
                    transaction.Dispose();
                }
                connection.Dispose();
                transaction.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
