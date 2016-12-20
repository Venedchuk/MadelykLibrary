using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace MadelykLibrary.Model
{
    public class CategoryObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
    public class BookObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Guid CategotyID { get; set; }
        public Guid AuthorId { get; set; }
    }
    public class CartObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string  BookName { get; set; }
        public DateTime Start_reading { get; set; }
        public DateTime? Finish_reading { get; set; }
        public string Status { get; set; }
    }
    public class AuthorObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class ReaderObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid AdressId { get; set; }
    }
    public class AdressObs : ObservableObject
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? House_number { get; set; }
    }
}
