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
using System.IO;

namespace DotNetProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public event EventHandler ClosingWindow;
        static public event EventHandler ResizeRequested;
        static public int CurrentHeight { private set; get; }
        static public int CurrentWidth { private set ; get; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.CurrentHeight = 550;
            MainWindow.CurrentWidth = 600;
            currentView.Height = MainWindow.CurrentHeight;
            currentView.Width = MainWindow.CurrentWidth;
            treeView.Height = CurrentHeight;
            string[] filePaths = Directory.GetFiles("../../Libraries/Playlists/");
            foreach (string fileName in filePaths)
            {
                string tmp = fileName.Substring(fileName.LastIndexOf('/') + 1);
                tmp = tmp.Remove(tmp.LastIndexOf('.'), tmp.Length - tmp.LastIndexOf('.'));
                PlayListItem.Items.Add(new TreeViewItem() { Header = tmp });
            }
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
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainWindow.CurrentWidth = (int)(e.NewSize.Width - 150);
            MainWindow.CurrentHeight = (int)(e.NewSize.Height - 80);
            currentView.Height = MainWindow.CurrentHeight;
            currentView.Width = MainWindow.CurrentWidth;
            treeView.Height = MainWindow.CurrentHeight;
            if (MainWindow.ResizeRequested != null)
            {
                MainWindow.ResizeRequested(null, EventArgs.Empty);
            }
        }
    }
}
