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
        public virtual Penny penny { get; set; } 
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
        public virtual Fillial Fillal { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_number { get; set; }

    }
    public class Fillial
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public virtual StatFillial Stat_Fillial { get; set; }
    }
    public class StatFillial
    {
        public Guid Id { get; set; }
        public string FillialStat { get; set; }
    }
    public class Penny
    {
        public Guid Id { get; set; }
      //  public virtual Cart Cart { get; set; }
        public int Price { get; set; }
        public virtual PennyStat pennyStat { get; set; }
    }
    public class PennyStat
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Count_Day_Min { get; set; }
        public int? Count_Day_Max { get; set; }
    }
}
