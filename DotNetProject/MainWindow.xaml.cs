using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DotNetProject.ViewModels;

namespace DotNetProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public event EventHandler ClosingWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (MainWindow.ClosingWindow != null)
            {
                MainWindow.ClosingWindow(this, EventArgs.Empty);
            }
        }

        private void AddToLibrary_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)(DataContext);
            vm.addToLibrary();   
        }
    }
}
