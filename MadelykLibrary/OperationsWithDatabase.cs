using MadelykLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadelykLibrary
{
    public class OperationsWithDatabase
    {

        public void AddAuthor(Author a)
        {
            using (var db = new LibraryContext())
            {
                a.Id = Guid.NewGuid();
                db.Authors.Add(a);
                db.SaveChanges();
            }
        }

        public List<Category> GetAllCategory()
        {
            using (var db = new LibraryContext())
            {
               

                return db.Categorys.ToList();
            }
        }
        public List<Author> GetAllAuthors()
        {
            using (var db = new LibraryContext())
            {
                //var cat = new List<AuthorObs>();
                var cat = db.Authors;
                return cat.ToList();
            }
        }
        public void AddBook(Book book)
        {
            using (var db = new LibraryContext())
            {
                book.Categoty = db.Categorys.ToList().Find(x => x.Id == book.Categoty.Id);
                book.Author = db.Authors.ToList().Find(x => x.Id == book.Author.Id);
                db.Books.Add(book);
                db.SaveChanges();
            }
        }

    }
}
