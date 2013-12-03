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
        public MainWindow()
        {
            InitializeComponent();
            //base.DataContext = new SongViewModel();
            //_viewModel = (SongViewModel)base.DataContext;
            //DataContext = new MainWindowViewModel();
        }
        //private void ButtonUpdateArtist_Click(object sender, RoutedEventArgs e)
        //{
        //    ++_count;
        //    _viewModel.artist = string.Format("Elvis ({0})", _count);
        //}
    }
}
