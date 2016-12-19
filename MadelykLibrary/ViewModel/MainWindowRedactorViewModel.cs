using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MadelykLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadelykLibrary
{
    class MainWindowRedactorViewModel : ViewModelBase
    {
        private static OperationsWithDatabase connect;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Author> Authors { get; set;}
        public ObservableCollection<Book> BooksFromCategory { get; set; }
        
        public ObservableCollection<Book> Books { get; set; }

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
                        Surname = AddAuthorSurname
                    };
                    connect.AddAuthor(author);
                    Update();
                    RaisePropertyChanged("AddAuthorInfo");

                }, () => (AddAuthorName != null && AddAuthorSurname != null)));

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

        public string AddReaderName { get; set; }
        public string AddReaderSurname { get; set; }
        public string AddReaderCity { get; set; }
        public string AddReaderStreet { get; set; }
        public string AddReaderNumber { get; set; }
        public string AddReaderInfo { get; set; }
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
                        House_number = AddReaderNumber
                    };
                    connect.AddReader(reader,adress);
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


        private Category _selectedBook;
        public Category SelectedBook
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

                    //CheckExistUserAndBook(AddAuthorNameForGetBook,AddAuthorSurNameForGetBook,SelectedBook);
                    AddAuthorInfo = "Have a nice read:"+Environment.NewLine + Books;
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
                    RaisePropertyChanged("AddAuthorInfo");
                }, () => (AddBookName != null )));
            }
        }
    }
}
