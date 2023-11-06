using Microsoft.EntityFrameworkCore;
using MyDiary.Models;

namespace MyDiary.DAL
{
    public class MyDiaryDbContext : DbContext
    {
        public MyDiaryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
