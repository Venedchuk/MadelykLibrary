using MadelykLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;

namespace MadelykLibrary
{
    public class OperationsWithDatabase
    {
        public void Initialize()
        {
            using (var db = new LibraryContext())
            {
                var statfil1 = new StatFillial()
                {
                    Id = Guid.NewGuid(),
                    FillialStat = "Village"
                };
                db.StatFillial.Add(statfil1);
                var statfil2 = new StatFillial()
                {
                    Id = Guid.NewGuid(),
                    FillialStat = "town"
                };
                db.StatFillial.Add(statfil2);
                var statfill3 = new StatFillial()
                {
                    Id = Guid.NewGuid(),
                    FillialStat = "Magistery"
                };
                db.StatFillial.Add(statfill3);

                var fill1 = new Fillial()
                {
                    Id = Guid.NewGuid(),
                    Number = 1,
                    Name="Young",
                    Stat_Fillial = statfil1
                };
                db.Fillial.Add(fill1);

                var fill2 = new Fillial()
                {
                    Id = Guid.NewGuid(),
                    Number = 2,
                    Name = "City",
                    Stat_Fillial = statfil1
                };
                db.Fillial.Add(fill2);
                var fill3 = new Fillial()
                {
                    Id = Guid.NewGuid(),
                    Number = 3,
                    Name = "Students",
                    Stat_Fillial = statfil1
                };
                db.Fillial.Add(fill3);
                var adr = new Adress()
                {
                    Id = Guid.NewGuid(),
                    City = "Zhytomyr",
                    Street = "Kyivs'ka",
                    Fillal = fill1,
                    House_number = "1"
                };
                db.Adresses.Add(adr);
                var reader = new Reader()
                {
                    Id = Guid.NewGuid(),
                    Name = "Bogdan",
                    Surname = "Madelyk",
                    Adress = adr
                };
                db.Readers.Add(reader);
                var category1 = new Category()
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Tales",
                    Description = "Tales for kids"
                };
                db.Categorys.Add(category1);
                var author = new Author()
                {
                    Id = Guid.NewGuid(),
                    Name = "Belyaev",
                    Surname = "Ivan"
                };
                db.Authors.Add(author);
                var book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Author = author,
                    Category = category1,
                    Name = "Man-Amfibian",
                    Count = 3
                };
                db.Books.Add(book);
                var pennystat1 = new PennyStat()
                {
                    Id = Guid.NewGuid(),
                    Status = "5 days",
                    Count_Day_Min = 1,
                    Count_Day_Max = 5,
                };
                db.PennyStat.Add(pennystat1);
                var pennystat2 = new PennyStat()
                {
                    Id = Guid.NewGuid(),
                    Status = "20 days",
                    Count_Day_Min = 6,
                    Count_Day_Max = 20,
                };
                db.PennyStat.Add(pennystat2);
                var pennystat3 = new PennyStat()
                {
                    Id = Guid.NewGuid(),
                    Status = "50 days",
                    Count_Day_Min = 21,
                    Count_Day_Max = 50,
                };
                db.PennyStat.Add(pennystat3);
                var pennystat0 = new PennyStat()
                {
                    Id = Guid.NewGuid(),
                    Status = "Reading",
                    Count_Day_Min = 0,
                };
                db.PennyStat.Add(pennystat0);

                var penny0 = new Penny()
                {
                    Id = Guid.NewGuid(),
                    pennyStat = pennystat0,
                    Price = 0
                };
                db.Penny.Add(penny0);
                var cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    Book = book,
                    Reader = reader,
                    Start_reading = DateTime.Now,
                    penny = penny0,
                    Status = "Reading"
                };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
        }

        internal List<Reader> GetAllReaders()
        {
            using (var db = new LibraryContext())
            {
                if (db.Readers.FirstOrDefault() != null)
                    return db.Readers.ToList();

                else
                    return new List<Reader>();
            }
        }

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
                if (db.Categorys.FirstOrDefault() != null)
                    return db.Categorys.ToList();

                else
                    return new List<Category>();
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
        public List<Fillial> GetAllFillials()
        {
            using (var db = new LibraryContext())
            {
                //var cat = new List<AuthorObs>();
                var cat = db.Fillial;
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
        public void AddReader(Reader reader, Adress adress, Fillial fillial)
        {
            using (var db = new LibraryContext())
            {
                adress.Fillal = db.Fillial.ToList().Find(x => x.Id==fillial.Id);
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
                        
                        var checkPenny = (item.Finish_reading - item.Start_reading).Value.Days;
                        if (checkPenny < 0 || checkPenny >= -5)
                        {
                            var penny = new Penny()
                            {
                                Id = Guid.NewGuid(),
                                Price = 10,
                                pennyStat = db.PennyStat.ToList().Find(x => x.Count_Day_Min == 6)
                            };
                            item.penny = penny;
                            db.Penny.Add(penny);
                          //  db.SaveChanges();
                        }                    
                        
                        if (checkPenny < -11)
                        {

                            var penny = new Penny()
                            {
                                Id = Guid.NewGuid(),
                                Price = 50,
                                pennyStat = db.PennyStat.ToList().Find(x => x.Count_Day_Min == 6)
                            };
                            db.Penny.Add(penny);
                           // db.SaveChanges();
                            item.penny = db.Penny.ToList().Find(x => x.Id == penny.Id);
                        }
                        
                        break;
                    }
                 
                }
                //var cart = db.Carts.SingleOrDefault(x => x.Reader.Name == addReaderNameForReturnBook && x.Reader.Surname == addReaderSurNameForReturnBook && x.Book.Id == selectedBookForGive.Id);
                //cart.Finish_reading = DateTime.Now;
                //cart.Status = "Finish";
                db.SaveChanges();
            }

        }

        internal void Pay(Penny selectedPenny)
        {
            using (var db = new LibraryContext())
            {
                selectedPenny.payed = "pay";
                var origin = db.Penny.Find(selectedPenny.Id);
                
                db.Entry(origin).CurrentValues.SetValues(selectedPenny);
                db.SaveChanges();
            }

        }

        internal ObservableCollection<Penny> GetPenny(string addReaderNameForReturnBook, string addReaderSurNameForReturnBook)
        {
            using (var db = new LibraryContext())
            {
                var cart = (db.Carts.Include(x => x.penny).Include(x => x.Reader).ToList());
                try
                {
                    var ret = cart.FindAll(x => x.Reader.Surname == addReaderSurNameForReturnBook || x.Reader.Name == addReaderNameForReturnBook).Select(x => x.penny);

                    var penny = new ObservableCollection<Penny>();
                    ret.Last();
                    foreach (var item in ret)
                    {
                        if(item!=null)
                        if (item.payed != "pay")
                            penny.Add(item);
                    }
                    return penny;
                }
                catch (Exception)
                {
                    return new ObservableCollection<Penny>();
                }
                
                
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
