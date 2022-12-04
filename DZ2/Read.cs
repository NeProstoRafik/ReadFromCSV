using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ2
{
    public class Read
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreateDate { get; set; }
        public string Rubrics { get; set; }

      
        public List<Read> Record( List<Read> list, string line)
        {
            using Appcontet context = new Appcontet();
            context.Reads.AddRange(list.Select(p => new Read()
            {
                Text = p.Text,
                CreateDate = p.CreateDate,
                Rubrics = p.Rubrics,

            }));

            context.SaveChanges();

            var answer = (from p in context.Reads
                          where EF.Functions.Like(p.Text, $"%{line}%")

                          select p).Take(10).ToList();
            return answer;

           
        }
        public void Write(List<Read> answer)
        {
            if (answer.Count == 0)
            {
                Console.WriteLine("Нет сопадений");
            }
            else
            {
                foreach (var r in answer)
                {
                    Console.WriteLine(r.Text);
                    Console.WriteLine("_____________________________");
                }
            }
                
        }
        public List<Read> ReadFromCSV()
        {
            var list = fastCSV.ReadFile<Read>("posts.csv", true, ',', (o, c) =>
            {
                o.Text = c[0];
                o.CreateDate = c[1];
                o.Rubrics = c[2];

                return true;
            });

            return list;
        }
    }
    
  
    public class Appcontet : DbContext
    {
        public DbSet<Read> Reads { get; set; }
        public Appcontet()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=TestDB; Trusted_Connection=True");
            
        }
    }
    // я знаю что надо было вынести в отдельный класс
}
