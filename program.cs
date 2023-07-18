using System;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the BookStore Application!");

            int numOfBooks = InputValue(1, 30);

            Book[] books = new Book[numOfBooks];

            GetBookData(numOfBooks, books);

            DisplayAllBooks(books);

            GetLists(numOfBooks, books);

            Console.WriteLine("\nThank you for using the BookStore Application! Press any key to exit.");
            Console.ReadKey();
        }

        public static int InputValue(int min, int max)
        {
            int value;
            bool isValid;

            do
            {
                Console.Write($"Please enter a number between {min} and {max}: ");
                string input = Console.ReadLine();

                isValid = int.TryParse(input, out value) && value >= min && value <= max;

                if (!isValid)
                    Console.WriteLine("Invalid input. Please try again.");
            } while (!isValid);

            return value;
        }

        public static bool IsValid(string id)
        {
            if (id.Length != 5)
                return false;

            if (!char.IsUpper(id[0]) || !char.IsUpper(id[1]))
                return false;

            if (!char.IsDigit(id[2]) || !char.IsDigit(id[3]) || !char.IsDigit(id[4]))
                return false;

            return true;
        }

        private static void GetBookData(int num, Book[] books)
        {
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"\nBook #{i + 1}:");
                string bookId;

                do
                {
                    Console.Write("Enter book id: ");
                    bookId = Console.ReadLine();

                    if (!IsValid(bookId))
                        Console.WriteLine("Invalid book id format. Please try again.");

                } while (!IsValid(bookId));

                Console.Write("Enter book title: ");
                string bookTitle = Console.ReadLine();

                Console.Write("Enter number of pages: ");
                int numOfPages = int.Parse(Console.ReadLine());

                Console.Write("Enter price: ");
                double price = double.Parse(Console.ReadLine());

                books[i] = new Book(bookId, bookTitle, numOfPages, price);
            }
        }

        public static void DisplayAllBooks(Book[] books)
        {
            Console.WriteLine("\nInformation of all Books:\n");
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        private static void GetLists(int num, Book[] books)
        {
            Console.WriteLine("\nValid book categories:");
            Console.WriteLine("CS - Computer Science");
            Console.WriteLine("IS - Information System");
            Console.WriteLine("SE - Security");
            Console.WriteLine("SO - Society");
            Console.WriteLine("MI - Miscellaneous");

            while (true)
            {
                Console.Write("\nEnter a category code (or 'exit' to quit): ");
                string categoryCode = Console.ReadLine().ToUpper();

                if (categoryCode == "EXIT")
                    break;

                Console.WriteLine($"\nBooks in the category '{categoryCode}':");
                int count = 0;
                foreach (var book in books)
                {
                    if (book.CategoryCode == categoryCode)
                    {
                        Console.WriteLine(book.ToString());
                        count++;
                    }
                }

                Console.WriteLine($"Number of books in the category '{categoryCode}': {count}");
            }
        }
    }

    class Book
    {
        public static string[] categoryCodes = { "CS", "IS", "SE", "SO", "MI" };
        public static string[] categoryNames = { "Computer Science", "Information System", "Security", "Society", "Miscellaneous" };

        private string bookId;
        private string categoryNameOfBook;

        public string BookTitle { get; set; }
        public int NumOfPages { get; set; }
        public double Price { get; set; }

        public string BookId
        {
            get { return bookId; }
            set
            {
                bookId = value;
                categoryNameOfBook = GetCategoryNameFromId(value);
            }
        }

        public string CategoryName
        {
            get { return categoryNameOfBook; }
        }

        public string CategoryCode
        {
            get { return bookId.Substring(0, 2); }
        }

        public Book()
        {
        }

        public Book(string bookId, string bookTitle, int numOfPages, double price)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            NumOfPages = numOfPages;
            Price = price;
        }

        private string GetCategoryNameFromId(string id)
        {
            string categoryCode = id.Substring(0, 2);
            int index = Array.IndexOf(categoryCodes, categoryCode);
            if (index == -1)
                return categoryCodes[4];

            return categoryNames[index];
        }

        public override string ToString()
        {
            return $"Book ID: {BookId}\nCategory: {CategoryName}\nTitle: {BookTitle}\nPages: {NumOfPages}\nPrice: ${Price}\n";
        }
    }
}
