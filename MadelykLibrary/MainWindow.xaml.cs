using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MadelykLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var db = new LibraryContext())
            {
                //db.Categorys.Add(new Category() {Id=Guid.NewGuid(),CategoryName="Adventure",Description="Description"});
                db.Authors.Add(new Author() {Id=Guid.NewGuid(),Name="Belyaev" });
                foreach (var item in db.Authors)
                {
                    Console.WriteLine(item.Name);

                }
                
                db.SaveChanges();
            }
        }
    }
}
