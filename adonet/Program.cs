using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet
{
    class Program
    {
        public static void contracts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT iden.IIN, con.FromDate, con.Type, ib.Money FROM [Identity] as iden JOIN Clients as cl ON cl.IDIdentity = iden.ID JOIN Contracts as con ON cl.IDContract = con.ID JOIN IBAN as ib ON con.IDIBAN = ib.ID; ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                    while (reader.Read())
                    {
                        object iin = reader.GetValue(0);
                        object fd = reader.GetValue(1);
                        object type = reader.GetValue(2);
                        object m = reader.GetValue(3);

                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", iin, fd, type, m);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            contracts();
            Console.ReadKey();
        }
    }
}
