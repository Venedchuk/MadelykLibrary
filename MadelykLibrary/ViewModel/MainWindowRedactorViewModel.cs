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
        public ObservableCollection<Author> Authors { get; set; }


        public MainWindowRedactorViewModel()
        {
            connect = new OperationsWithDatabase();
            var SelectedCategory = new Category();
            Categories = new ObservableCollection<Category>(connect.GetAllCategory());
            Authors = new ObservableCollection<Author>(connect.GetAllAuthors());
            var SelectedAuthor = new Author();
        }
        public string AddAuthorName { get; set; }
        public string AddAuthorSurname { get; set; }
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
                    AddReaderInfo = $"Added Reader : {AddReaderName} {AddReaderSurname}";
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
                 //   reader.Adress = adress;
                    connect.AddReader(reader,adress);
                    RaisePropertyChanged("AddReaderInfo");
                }, () => (AddReaderName != null && AddReaderSurname != null && AddReaderStreet != null && AddReaderNumber != null )));

            }
        }




    }
}
