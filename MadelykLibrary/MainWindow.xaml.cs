using System.Windows;
using System.Windows.Controls;

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
            

        }
        private void label1d1_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(dataGrid, "Grid Printing.");
        }
    }
}
