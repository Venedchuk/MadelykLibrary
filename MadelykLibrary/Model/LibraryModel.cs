using System;
using System.ComponentModel.DataAnnotations;

namespace MadelykLibrary
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
    }
    public class Cart
    {
        public Guid Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual Reader Reader { get; set; }
        public  DateTime Start_reading { get; set; }
        public DateTime? Finish_reading { get; set; }
        public string Status { get; set; }
    }
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class Reader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual Adress Adress { get; set; }
    }
    public class Adress
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_number { get; set; }
    }
}
