using System.Data.Entity;

namespace MadelykLibrary
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(): base(
            "MadelykLibrary"
            )
        {//initializer 
            //Database.SetInitializer<LibraryContext>(new CreateDatabaseIfNotExists<LibraryContext>());   
            Database.SetInitializer<LibraryContext>(new DropCreateDatabaseIfModelChanges<LibraryContext>());
            //Database.SetInitializer<LibraryContext>(new DropCreateDatabaseAlways<LibraryContext>());
            //Database.SetInitializer<LibraryContext>(new MadelykDBInitializer()); //create if need
        }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Fillial> Fillial { get; set; }
        public DbSet<StatFillial> StatFillial { get; set; }
        public DbSet<Penny> Penny { get; set; }
        public DbSet<PennyStat> PennyStat { get; set; }

    }
}
