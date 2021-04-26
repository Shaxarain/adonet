using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Production;
using Lib.Persons;
using Lib.Sales;

namespace adonet
{
    class Program
    {
        public static void ADOcontracts()
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
/*            using (Prod db = new Prod())
            {
                foreach (Product u in db.Product)
                    Console.WriteLine("{0} - {1}", u.ProductNumber, u.Name);
            }*/
            Repository<Address> AdPer = new Repository<Address>(new Persons());
            Repository<BusinessEntity> BusPer = new Repository<BusinessEntity>(new Persons());
            Repository<BusinessEntityAddress> BusAdPer = new Repository<BusinessEntityAddress>(new Persons());
            Repository<PersonPhone> PhRep = new Repository<PersonPhone>(new Persons());
            Repository<StateProvince> StPer = new Repository<StateProvince>(new Persons());

            Repository<Customer> CusSal = new Repository<Customer>(new Sales());
            Repository<SalesPerson> SPSal = new Repository<SalesPerson>(new Sales());
            Repository<SalesOrderHeader> SOHSal = new Repository<SalesOrderHeader>(new Sales());
            Repository<SalesOrderDetail> SODSal = new Repository<SalesOrderDetail>(new Sales());
            Repository<ShoppingCartItem> SCISal = new Repository<ShoppingCartItem>(new Sales());
            Repository<SalesTerritory> STerSal = new Repository<SalesTerritory>(new Sales());

            Repository<Product> Pr = new Repository<Product>(new Prod());
            Repository<ProductCategory> CatPr = new Repository<ProductCategory>(new Prod());
            Repository<ProductDescription> DesPrl = new Repository<ProductDescription>(new Prod());
            Repository<ProductInventory> InvPr = new Repository<ProductInventory>(new Prod());
            Repository<ProductListPriceHistory> LPHProd = new Repository<ProductListPriceHistory>(new Prod());
            Repository<ProductPhoto> PhPr = new Repository<ProductPhoto>(new Prod());
            Repository<ProductProductPhoto> PhPrPr = new Repository<ProductProductPhoto>(new Prod());

            IEnumerable<Product> prod = Pr.Get();
            foreach (Product p in prod)
            {
                Console.WriteLine($"{p.Name} ({p.Color}) - {p.Size}");
            }

            Console.ReadKey();

        }
    }
}
