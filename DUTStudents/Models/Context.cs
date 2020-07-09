using System.Data.Entity;
namespace DUTStudents.Models
{
    

    public class Context : DbContext
    {
        public Context() : base("name=Context")
        {
        }

        public DbSet<Students> Items { get; set; }
    }
}