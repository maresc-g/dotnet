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
using System.Reflection;
using DotNetProject.Models;

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
        static public int CurrentWidth { private set; get; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.CurrentHeight = 550;
            MainWindow.CurrentWidth = 600;
            currentView.Height = MainWindow.CurrentHeight;
            currentView.Width = MainWindow.CurrentWidth;
            treeView.Height = CurrentHeight;
            string[] filePaths = Directory.GetFiles("../../Libraries/Playlists/");
            //Binding myBinding = new Binding("Filename");
            //var tmp2 = new PlayerViewModel();
            //myBinding.Source = tmp2;
            //tmp2.Filename = "test552451";
            //testLabel.SetBinding(Label.ContentProperty, myBinding);
            this.Background = new SolidColorBrush(Color.FromRgb(Globals.backR, Globals.backG, Globals.backB));
            var children = myGrid.Children;
            foreach (Control child in children)
            {
                child.Background = new SolidColorBrush(Color.FromRgb(Globals.backR, Globals.backG, Globals.backB));
            }
            foreach (string fileName in filePaths)
            {
                string tmp = fileName.Substring(fileName.LastIndexOf('/') + 1);
                tmp = tmp.Remove(tmp.LastIndexOf('.'), tmp.Length - tmp.LastIndexOf('.'));
                var item = new TreeViewItem() { Header = tmp };
                item.KeyDown += item_KeyDown;
                PlayListItem.Items.Add(item);
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var vm = (MainWindowViewModel)DataContext;
                vm.changeView("Playlist");
                var item = treeView.SelectedItem as TreeViewItem;
                File.Delete("../../Libraries/Playlists/" + item.Header + ".xml");
                PlayListItem.Items.Remove(treeView.SelectedItem);
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

        private void NewPlaylist_Click(object sender, RoutedEventArgs e)
        {
            addPlaylistBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void Accept_AddPlaylist(object sender, RoutedEventArgs e)
        {
            var item = new TreeViewItem() { Header = playlistName.Text };
            item.KeyDown += item_KeyDown;
            PlayListItem.Items.Add(item);
            addPlaylistBox.Visibility = System.Windows.Visibility.Hidden;
            var file = File.Create("../../Libraries/Playlists/" + playlistName.Text + ".xml");
            file.Close();
        }

        private void Cancel_AddPlaylist(object sender, RoutedEventArgs e)
        {
            playlistName.Text = "";
            addPlaylistBox.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
