using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MadelykLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadelykLibrary
{
    class MainWindowRedactorViewModel : ViewModelBase
    {
        private static OperationsWithDatabase connect;
        public MainWindowRedactorViewModel()
        {
            connect = new OperationsWithDatabase();
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










    }
}
