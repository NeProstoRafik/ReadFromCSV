using Microsoft.EntityFrameworkCore;
namespace DZ2
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var reader = new Read();

           var list= reader.ReadFromCSV(); // создал для вашего удобства чтения

            Console.WriteLine("введите слова для поиска");
            string line = Console.ReadLine();
            Console.WriteLine(".....................................");

            reader.Write(reader.Record(list, line));
          
        }
    }
    
}