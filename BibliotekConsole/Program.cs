using BibliotekConsole.DBModels;
using Dapper;
using System.Data.SqlClient;
namespace BibliotekConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static string connString = @"data source =.\SQLEXPRESS; initial catalog= Bibliotek; persist security info=true; integrated security = true;";

        //Scaffold-DbContext "Server=.\SQLExpress;Database=Bibliotek;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        static void Main(string[] args)
        {
            Console.WriteLine("Skriv ut alla ISBN från databas med Dapper:");

            var list = GetISBNDapper();
            foreach (var item in list)
                Console.WriteLine(item.Isbn);

            Console.WriteLine()
                ;
            Console.WriteLine("Skriv ut alla ISBN från databas med EF");

            var listLINQ = GetISBNLINQ();
            foreach (var item in listLINQ)
                Console.WriteLine(item.Isbn);

            Console.WriteLine("Böcker och lagersaldo");

            var product = getproduct();
            foreach (var item in product)
                Console.WriteLine($"Titel:{item.ProductName} finns som e-bock {item.EVersion}");
            



        }
        public static List<Product> GetISBNDapper()
        {
            var ISBNList = new List<Product>();
            var sql = "select isbn from Products";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                ISBNList = connection.Query<Product>(sql).ToList();
            }
            return ISBNList;
        }

        public static List<Product> getproduct()
        {
            var productlist = new List<Product>();
            //var sql = "select * from Products";
            var everison = "SELECT * from Products where E_Version = 1";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                productlist = connection.Query<Product>(everison).ToList();
            }
            return productlist;
        }


        public static List<Product> GetISBNLINQ()
        {
            var isbnlist = new List<Product>();
            using (var context = new BibliotekContext())
            {
                isbnlist = context.Products.Select(x =>  new Product()
                {
                    Isbn = x.Isbn
                }).ToList();
            }
            return isbnlist;
            
        }

    }
}