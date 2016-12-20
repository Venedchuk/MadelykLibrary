using MadelykLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
        public bool CheckUser(string addAuthorNameForGetBook, string addAuthorSurNameForGetBook)
        {
            using (var db = new LibraryContext())
            {
                if (db.Readers.ToList().Find(x => x.Name == addAuthorNameForGetBook || x.Name == addAuthorSurNameForGetBook) == null)
                {
                    return false;
                }
                return true;
            }

        }
        public string CheckExistUserAndBook(string addAuthorNameForGetBook, string addAuthorSurNameForGetBook, Book SelectedBook)
        {
            using (var db = new LibraryContext())
            {
                if(!CheckUser(addAuthorNameForGetBook,addAuthorSurNameForGetBook))
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

        internal void GiveMeRead(Book selectedBook,string AddAuthorNameForGetBook,string AddAuthorSurNameForGetBook)
        {
            using (var db = new LibraryContext())
            {

                db.Books.ToList().Find(x => x.Id == selectedBook.Id).Count--;
                var cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    Start_reading = DateTime.Now,
                    Status = "Reading",
                    Book = db.Books.ToList().Find(x => x.Id == selectedBook.Id),
                    Reader = db.Readers.ToList().Find(x =>x.Name == AddAuthorNameForGetBook && x.Surname== AddAuthorSurNameForGetBook)
            };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
        }

        internal ObservableCollection<Book> GetBookWhatReading(string addReaderNameForReturnBook, string addReaderSurNameForReturnBook)
        {
            ObservableCollection<Cart> cart;
            var book = new ObservableCollection<Book>();
            using (var db = new LibraryContext())
            {
                cart = new ObservableCollection<Cart>(db.Carts.ToList().FindAll(x => x.Reader.Name == addReaderNameForReturnBook && x.Reader.Surname == addReaderSurNameForReturnBook && x.Status=="Reading"));
                foreach (var item in cart)
                {
                    book.Add(item.Book);
                }
                db.SaveChanges();
            }
            return book;
        }

        internal void ReturnBook(string addReaderNameForReturnBook, string addReaderSurNameForReturnBook, Book selectedBookForGive)
        {
            using (var db = new LibraryContext())
            {
                var returnedbooks = db.Books.SingleOrDefault(x => x.Id == selectedBookForGive.Id);//.Count++;
                returnedbooks.Count++;
                db.SaveChanges();
                foreach (var item in db.Carts)
                {
                    if (item.Reader.Name == addReaderNameForReturnBook && item.Reader.Surname == addReaderSurNameForReturnBook && item.Book.Id == selectedBookForGive.Id && item.Status=="Reading")
                    {
                        item.Finish_reading = DateTime.Now;
                        item.Status = "Finish";
                        break;
                    }
                }
                //var cart = db.Carts.SingleOrDefault(x => x.Reader.Name == addReaderNameForReturnBook && x.Reader.Surname == addReaderSurNameForReturnBook && x.Book.Id == selectedBookForGive.Id);
                //cart.Finish_reading = DateTime.Now;
                //cart.Status = "Finish";
                db.SaveChanges();
            }

        }

        internal ObservableCollection<CartObs> GetCartUser(string readerNamePrint, string readerSurNamePrint, bool isFinishRead)
        {
            List<Cart> cart;
                using (var db = new LibraryContext())
            {
                if(isFinishRead)
                    cart =(db.Carts.Include(x =>x.Book).ToList().FindAll(x => x.Reader.Name == readerNamePrint && x.Reader.Surname == readerSurNamePrint));
                else
                    cart = db.Carts.Include(x => x.Book).ToList().FindAll(x => x.Reader.Name == readerNamePrint && x.Reader.Surname == readerSurNamePrint && x.Status=="Reading");
            }
                var ForReturn = new ObservableCollection<CartObs>();
            foreach (var item in cart)
            {
                ForReturn.Add(new CartObs()
                {
                    Id = item.Id,
                    BookName = item.Book.Name,
                    Start_reading = item.Start_reading,
                    Finish_reading = item.Finish_reading,
                    Status = item.Status
                });
            }
            return ForReturn;
        }
    }
}
