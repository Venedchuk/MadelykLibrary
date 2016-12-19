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
                book.Category = db.Categorys.ToList().Find(x => x.Id == book.Category.Id);
                book.Author = db.Authors.ToList().Find(x => x.Id == book.Author.Id);
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
        public void AddReader(Reader reader, Adress adress)
        {
            using (var db = new LibraryContext())
            {
                adress.Id = Guid.NewGuid();
                db.Adresses.Add(adress);

                db.SaveChanges();
                reader.Id = Guid.NewGuid();
                reader.Adress = db.Adresses.ToList().Find(x =>x.Id == adress.Id);
                db.Readers.Add(reader);
                db.SaveChanges();
            }
        }
        public void AddCategory(Category category)
        {
            using (var db = new LibraryContext())
            {
               
                db.Categorys.Add(category);
                db.SaveChanges();
            }
        }

        public List<Book> GetBookFromCategory(Category category, Author author)
        {
            var Book = new List<Book>();
            using (var db = new LibraryContext())
            {
                
                if(category!=null&& author!=null)
                    Book = db.Books.ToList().FindAll(x=>x.Category.Id == category.Id&& x.Author.Id==author.Id);
                if (category == null)
                    Book = db.Books.ToList().FindAll(x=>x.Author == author);
                if (category == null)
                    Book = db.Books.ToList().FindAll(x => x.Category == category);
                if (category == null || author == null)
                {
                    Book = GetAllBooks();
                }
            }
            return Book;
        }
        
        public List<Book> GetAllBooks()
        {
            using (var db = new LibraryContext())
            {
                return db.Books.ToList();
            }
        }
        public string CheckExistUserAndBook(string addAuthorNameForGetBook, string addAuthorSurNameForGetBook, Book SelectedBook)
        {
            using (var db = new LibraryContext())
            {
               if((db.Readers.ToList().Find(x => x.Name == addAuthorNameForGetBook || x.Name == addAuthorSurNameForGetBook)==null))
                {
                    return "Not find reader";
                }
                var book = db.Books.ToList().Find(x => x.Id == SelectedBook.Id);
                if (book.Count<=0)
                {
                    return "Has't free book :(";
                }


            }
            return "Have a nice read";
        }
    }
}
