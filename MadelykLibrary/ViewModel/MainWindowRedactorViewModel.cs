using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MadelykLibrary.Model;
using System;
using System.Collections.ObjectModel;

namespace MadelykLibrary
{
    class MainWindowRedactorViewModel : ViewModelBase
    {
        private static OperationsWithDatabase connect;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Author> Authors { get; set;}
        public ObservableCollection<Book> BooksFromCategory { get; set; }
        public ObservableCollection<Fillial> Fillials { get; set; }

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Book> BooksWhatReading { get; set; }
        public ObservableCollection<Penny> PennyNotPay { get; set; }
        public ObservableCollection<CartObs> CartGrid { get; set; }

        public MainWindowRedactorViewModel()
        {
            Update();
            Books = new ObservableCollection<Book>(connect.GetAllBooks());
        }

        private void Update()
        {
            connect = new OperationsWithDatabase();
            var SelectedCategory = new Category();
            var SelectedCategoryToGet = new Category();
            Categories = new ObservableCollection<Category>(connect.GetAllCategory());
            RaisePropertyChanged("Categories");
            Authors = new ObservableCollection<Author>(connect.GetAllAuthors());
            Fillials = new ObservableCollection<Fillial>(connect.GetAllFillials());
            RaisePropertyChanged("Fillials");
            BooksFromCategory = new ObservableCollection<Book>();
            RaisePropertyChanged("Authors");
            var SelectedAuthor = new Author();


        }
        public string AddAuthorName { get; set; }
        public string AddAuthorSurname { get; set; }
        public string AddAuthorNameForGetBook { get; set; }
        public string AddAuthorSurNameForGetBook { get; set; }
        private RelayCommand _addAuthor;
        public RelayCommand AddAuthor
        {
            get
            {
                return _addAuthor ?? (_addAuthor = new RelayCommand(() =>
                {
                    AddAuthorInfo = "Added Author: " + AddAuthorName;
                    var author = new Author()
                    {
                        Name = AddAuthorName,
                        Surname = AddAuthorSurname,
                    };
                    //connect.Initialize();
                    connect.AddAuthor(author);
                    Update();
                    RaisePropertyChanged("AddAuthorInfo");

                }, () => (AddAuthorName != null && AddAuthorSurname != null)));

            }
        }
        public RelayCommand init
        {
            get
            {
                return new RelayCommand(() =>
                {
                    connect.Initialize();
                   
                });

            }
        }
        private string _addAuthorInfo;
        public string AddAuthorInfo
        {
            get { return _addAuthorInfo; }
            set
            {
                if (_addAuthorInfo == value) return;
                _addAuthorInfo = value;

                RaisePropertyChanged("AddAuthorInfo");

            }
        }

        private string _getBookInfo;
        public string GetBookInfo
        {
            get { return _getBookInfo; }
            set
            {
                if (_getBookInfo == value) return;
                _getBookInfo = value;

                RaisePropertyChanged("GetBookInfo");

            }
        }
        private string _getBookPrint;
        public string GetBookPrint
        {
            get { return _getBookPrint; }
            set
            {
                if (_getBookPrint == value) return;
                _getBookPrint = value;

                RaisePropertyChanged("GetBookPrint");

            }
        }

        public string AddBookName { get; set; }
        public int countbook { get; set; }
        private RelayCommand _addBook;
        public RelayCommand AddBook
        {
            get
            {
                return _addBook ?? (_addBook = new RelayCommand(() =>
                {
                    AddBookInfo = "Added Book: " + AddBookName + " "+countbook;
                    var book = new Book()
                    {
                        Id = Guid.NewGuid(),
                        Name = AddBookName,
                        Count = countbook,
                        Category = new Category(),
                        Author = new Author()

                    };
                    book.Category.Id = SelectedCategory.Id;
                    book.Author.Id = SelectedAuthor.Id;
                    connect.AddBook(book);
                    Update();
                    RaisePropertyChanged("AddBookInfo");
                }, () => (AddBookName != null&& countbook>0&& SelectedCategory!=null&& SelectedAuthor!= null)));

            }
        }
        private string _addBookInfo;
        public string AddBookInfo
        {
            get { return _addBookInfo; }
            set
            {
                if (_addBookInfo == value) return;
                _addBookInfo = value;

                RaisePropertyChanged("AddBookInfo");

            }
        }
        private string _readerNamePrint;
        public string ReaderNamePrint
        {
            get { return _readerNamePrint; }
            set
            {
                if (_readerNamePrint == value) return;
                _readerNamePrint = value;
                RaisePropertyChanged("ReaderNamePrint");
            }
        }
        public string ReaderSurNamePrint { get; set; }

        private string _printInfo;
        public string PrintInfo
        {
            get { return _printInfo; }
            set
            {
                if (_printInfo == value) return;
                _printInfo = value;
                RaisePropertyChanged("PrintInfo");
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }

            set
            {
                if (_selectedCategory == value) return;
                _selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");

            }
        }
        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }

            set
            {
                if (_selectedAuthor == value) return;
                _selectedAuthor = value;
                RaisePropertyChanged("SelectedAuthor");

            }
        }
         private Fillial _selectedFillial;
        public Fillial SelectedFillial
        {
            get { return _selectedFillial; }

            set
            {
                if (_selectedFillial == value) return;
                _selectedFillial = value;
                RaisePropertyChanged("SelectedFillial");

            }
        }

        public string AddReaderName { get; set; }
        public string AddReaderSurname { get; set; }
        public string AddReaderCity { get; set; }
        public string AddReaderStreet { get; set; }
        public string AddReaderNumber { get; set; }
        public string AddReaderInfo { get; set; }
        public string AddReaderNameForReturnBook { get; set; }
        public string AddReaderSurNameForReturnBook { get; set; }

        private RelayCommand _addReader;
        public RelayCommand AddReader
        {
            get
            {
                return _addReader ?? (_addReader = new RelayCommand(() =>
                {
                    AddReaderInfo = $"Added Reader : {AddReaderName} "+ Environment.NewLine + $" {AddReaderSurname}";
                    var reader = new Reader()
                    {
                        Name = AddReaderName,
                        Surname = AddReaderSurname,
  
                    };
                    var adress = new Adress()
                    {
                        City = AddReaderCity,
                        Street = AddReaderStreet,
                        House_number = AddReaderNumber,
                    };
                    connect.AddReader(reader,adress,SelectedFillial);
                    Update();
                    RaisePropertyChanged("AddReaderInfo");

                }, () => (AddReaderName != null && AddReaderSurname != null && AddReaderStreet != null && AddReaderNumber != null )));

            }
        }

        public string AddCategoryInfo { get; set; }
        public string AddCategoryName { get; set; }
        public string AddCategoryDescription { get; set; }

        private RelayCommand _addCategory;
        public RelayCommand AddCategory
        {
            get
            {
                return _addCategory ?? (_addCategory = new RelayCommand(() =>
                {
                    AddCategoryInfo = "Added Category: " + AddCategoryName;
                    var cat = new Category()
                    {
                        Id = Guid.NewGuid(),
                        CategoryName = AddCategoryName,
                        Description = AddCategoryDescription
                    };
                   connect.AddCategory(cat);
                    Update();
                    RaisePropertyChanged("AddCategoryInfo");
                }, () => (AddCategoryName != null )));

            }
        }
        private Category _selectedCategoryToGet;
        public Category SelectedCategoryToGet
        {
            get { return _selectedCategoryToGet; }

            set
            {
                if (_selectedCategoryToGet == value) return;
                _selectedCategoryToGet = value;
                RaisePropertyChanged("SelectedCategoryToGet");

            }
        }


        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (_selectedBook == value) return;
                _selectedBook = value;
                RaisePropertyChanged("SelectedBook");

            }
        }

        private RelayCommand _findBookForCriteria;
        public RelayCommand FindBookForCriteria
        {
            get
            {
                return _findBookForCriteria ?? (_findBookForCriteria = new RelayCommand(() =>
                {
                   // Update();
                    Books = new ObservableCollection<Book>(connect.GetBookFromCategory(SelectedCategoryToGet, SelectedAuthor));
                    RaisePropertyChanged("Books");
                }, () => (SelectedCategoryToGet != null|| SelectedAuthor!=null)));

            }
        }

        private RelayCommand _getBook;
        public RelayCommand GetBook
        {
            get
            {
                return _getBook ?? (_getBook = new RelayCommand(() =>
                {


                    AddAuthorInfo = connect.CheckExistUserAndBook(AddAuthorNameForGetBook, AddAuthorSurNameForGetBook, SelectedBook);
                    RaisePropertyChanged("AddAuthorInfo");
                    if (AddAuthorInfo== "Have a nice read")
                    {
                        connect.GiveMeRead(SelectedBook, AddAuthorNameForGetBook, AddAuthorSurNameForGetBook);
                    }

                    Update();

                }, () => (SelectedBook != null )));
            }
        }

        private RelayCommand _returnBook;
        public RelayCommand ReturnBook
        {
            get
            {
                return _returnBook ?? (_returnBook = new RelayCommand(() =>
                {

                    connect.ReturnBook(AddReaderNameForReturnBook, AddReaderSurNameForReturnBook, SelectedBookForGive);
                    Update();

                    GetBookInfo = "Book returned";
                    RaisePropertyChanged("GetBookInfo");
                    BooksWhatReading = new ObservableCollection<Book>();
                    RaisePropertyChanged("BooksWhatReading");
                    SelectedBookForGive = null;
                    RaisePropertyChanged("SelectedBookForGive");

                }, () => (SelectedBookForGive != null)));
            }
        }
        private RelayCommand _pay;
        public RelayCommand Pay
        {
            get
            {
                return _pay ?? (_pay = new RelayCommand(() =>
                {
                    connect.Pay(SelectedPenny);
                    RaisePropertyChanged("PennyNotPay");

                }, () => (SelectedPenny != null)));
            }
        }

        private RelayCommand _findPenny;
        public RelayCommand FindPenny
        {
            get
            {
                return _findPenny ?? (_findPenny = new RelayCommand(() =>
                {
                    PennyNotPay = connect.GetPenny(AddReaderNameForReturnBook, AddReaderSurNameForReturnBook);
                    RaisePropertyChanged("PennyNotPay");


                }));
            }
        }

        private RelayCommand _findBookWhatReading;
        public RelayCommand FindBookWhatReading
        {
            get
            {
                return _findBookWhatReading ?? (_findBookWhatReading = new RelayCommand(() =>
                {
                    if (connect.CheckUser(AddReaderNameForReturnBook, AddReaderSurNameForReturnBook))
                    {
                        BooksWhatReading = connect.GetBookWhatReading(AddReaderNameForReturnBook, AddReaderSurNameForReturnBook);
                        RaisePropertyChanged("BooksWhatReading");
                        GetBookInfo = "Find user";
                    }
                    else
                    {
                        GetBookInfo = "Wrong User";
                    }
                    RaisePropertyChanged("GetBookInfo");
                    Update();

                }, () => (AddReaderNameForReturnBook != null && AddReaderSurNameForReturnBook != null)));
            }
        }

        private Book _selectedBookForGive;
        public Book SelectedBookForGive
        {
            get { return _selectedBookForGive; }
            set
            {
                if (_selectedBookForGive == value) return;
                _selectedBookForGive = value;
                RaisePropertyChanged("SelectedBookForGive");
            }
        }
        private Penny _selectedPenny;
        public Penny SelectedPenny
        {
            get { return _selectedPenny; }
            set
            {
                if (_selectedPenny == value) return;
                _selectedPenny = value;
                RaisePropertyChanged("SelectedPenny");
            }
        }


        private RelayCommand _printCart;
        public RelayCommand PrintCart
        {
            get
            {
                return _printCart ?? (_printCart = new RelayCommand(() =>
                {

                  


                } ));

            }
        }
        private bool _isReading;
        public bool IsFinishRead
        {
            get { return _isReading; }
            set
            {
                if (_isReading == value) return;

                _isReading = value;

                RaisePropertyChanged("IsFinishRead");
            }
        }

        private RelayCommand _getStat;
        public RelayCommand GetStat
        {
            get
            {
                return _getStat ?? (_getStat = new RelayCommand(() =>
                {
                    if (connect.CheckUser(ReaderNamePrint, ReaderSurNamePrint))
                    {
                        CartGrid = connect.GetCartUser(ReaderNamePrint, ReaderSurNamePrint,IsFinishRead);
                        RaisePropertyChanged("CartGrid");
                        GetBookPrint = "Find user";
                    }
                    else
                    {
                        GetBookPrint = "Wrong User";
                    }
                    RaisePropertyChanged("GetStat");
                    Update();

                }, () => (ReaderNamePrint != null)));
            }
        }

    }
}
