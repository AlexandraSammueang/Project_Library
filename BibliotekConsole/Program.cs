using BibliotekConsole.DBModels;

namespace BibliotekConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static string connString = @"data source =.\SQLEXPRESS; initial catalog= Bibliotek; persist security info=true; integrated security = true;";

        //Scaffold-DbContext "Server=.\SQLExpress;Database=Bibliotek;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        static void Main(string[] args)
        {
            //CreateAccount();
            Login();


            //    Console.WriteLine("Skriv ut alla ISBN från databas med Dapper:");

            //    var list = GetISBNDapper();
            //    foreach (var item in list)
            //        Console.WriteLine(item.Isbn);

            //    Console.WriteLine()
            //        ;
            //    Console.WriteLine("Skriv ut alla ISBN från databas med EF");

            //    var listLINQ = GetISBNLINQ();
            //    foreach (var item in listLINQ)
            //        Console.WriteLine(item.Isbn);
        }
        //public static List<Product> GetISBNDapper()
        //{
        //    var ISBNList = new List<Product>();
        //    var sql = "select isbn from Products";
        //    using (var connection = new SqlConnection(connString))
        //    {
        //        connection.Open();
        //        ISBNList = connection.Query<Product>(sql).ToList();
        //    }
        //    return ISBNList;
        //}

        //public static List<Product> GetISBNLINQ()
        //{
        //    var isbnlist = new List<Product>();
        //    using (var context = new BibliotekContext())
        //    {
        //        isbnlist = context.Products.Select(x => new Product()
        //        {
        //            Isbn = x.Isbn
        //        }).ToList();
        //    }
        //    return isbnlist;

        //}

        public static void CreateAccount()
        {
            bool isValid = false;
            User user = new User();
            string confirmPassword;


            do
            {
                Console.Write("Användarnamn: ");
                string answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    using (var context = new BibliotekContext())
                    {
                        var checkUsernameAvailability = context.Users.Where(x => x.Username == answer);
                        if (checkUsernameAvailability.Any())
                        {
                            Console.WriteLine("Användarnamnet upptaget. Var vänlig välj ett annat.");
                            return;
                        }
                        else
                            user.Username = answer;
                    }
                }

                Console.Write("Lösenord: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.Password = answer;
                }

                Console.Write("Bekräfta lösenord: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    confirmPassword = answer;

                    if (confirmPassword != user.Password)
                    {
                        Console.WriteLine("Lösenord matchar inte. Försök igen");
                        return;
                    }

                    else if (confirmPassword == user.Password)
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(answer, 10);
                    }
                }

                Console.Write("Förnamn: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.Firstname = answer;
                }


                Console.Write("Efternamn: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.Lastname = answer;
                }

                Console.Write("Adress: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.Address = answer;
                }

                Console.Write("Postkod: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.PostalCode = answer;
                }

                Console.Write("Stad: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.City = answer;
                }

                Console.Write("Telefonnummer: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.PhoneNumber = answer;
                }

                Console.Write("Email: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
                {
                    user.Email = answer;
                    isValid = true;
                }

                else
                {
                    Console.WriteLine("Ett eller flera fält felaktigt angivna. Var vänlig fyll i igen.");
                }

            } while (!isValid);

            if (user.Email != null)
            {
                using (var context = new BibliotekContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    Console.WriteLine($"{user.Username}s konto har skapats och lagts till i databasen");
                }

                Login();
            }

            else
            {
                Console.WriteLine("Info om användare saknas. Var vänlig fyll i igen för att skapa konto.");
            }
        }

        public static void Login()
        {
            var userPassword = new List<User>();

            Console.WriteLine("LOGIN\n---------");
            Console.Write("Användarnamn: ");
            string userName = Console.ReadLine();
            bool isValid = false;

            using (var context = new BibliotekContext())
            {
                //    var isbnlist = new List<Product>();
                //    using (var context = new BibliotekContext())
                //    {
                //        isbnlist = context.Products.Select(x => new Product()
                //        {
                //            Isbn = x.Isbn
                //        }).ToList();
                //    }
                userPassword = context.Users.Where(x => x.Username == userName).Select(x => new User() { Password = x.Password}).ToList();
                Console.WriteLine($"User password, encrypted: {userPassword[0].Password}");
                
                if (userName.Any())
                {
                    do
                    {
                        Console.Write("Lösenord: ");
                        string password = Console.ReadLine();
                        bool correctPassword = BCrypt.Net.BCrypt.Verify(password, userPassword[0].Password );
                        if (correctPassword == true)
                        {
                            Console.Clear();
                            Console.WriteLine($"GRATTIS {userName} , DU HAR LOGGAT IN!");
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Felatkigt lösenord. Försök igen.");
                        }

                    } while (!isValid);
                }

                else
                {
                    Console.WriteLine($"användarnamnet {userName} finns inte. Vill du registrera dig? (Ja/Nej)");
                    if (Console.ReadLine() == "ja")
                    {
                        CreateAccount();
                    }
                    else if (Console.ReadLine() == "nej") return;
                }
            }

        }

    }
}
