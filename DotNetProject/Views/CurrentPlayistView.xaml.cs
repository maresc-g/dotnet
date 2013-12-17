using DotNetProject.Models;
using DotNetProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DotNetProject.Views
{
    /// <summary>
    /// Logique d'interaction pour CurrentPlaylistView.xaml
    /// </summary>
    public partial class CurrentPlaylistView : UserControl
    {
        static public CurrentPlaylistViewModel vm;

        public CurrentPlaylistView()
        {
            InitializeComponent();
            Height = MainWindow.CurrentHeight;
            Width = MainWindow.CurrentWidth;
            listView.Height = MainWindow.CurrentHeight;
            listView.Width = MainWindow.CurrentWidth;
            MainWindow.ResizeRequested += (sender, e) =>
            {
                Height = MainWindow.CurrentHeight;
                Width = MainWindow.CurrentWidth;
                listView.Height = MainWindow.CurrentHeight;
                listView.Width = MainWindow.CurrentWidth;
            };
            vm = (CurrentPlaylistViewModel)this.DataContext;
        }

        private void CurrentPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (CurrentPlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                vm.handleKeydown(listView.SelectedItems, e);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CurrentPlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                foreach (Media media in listView.SelectedItems)
                {
                    vm.removeCurrentPlaylist(media);
                }
            }
            else if (listView.SelectedItem != null)
            {
                vm.removeCurrentPlaylist(listView.SelectedItem as Media);
            }
        }

        private void ChangeProperties_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CurrentPlaylistViewModel)DataContext;
            if (listView.SelectedItem != null)
            {
                var tmpMedia = listView.SelectedItem as Media;
                popupProperties.IsOpen = true;
                vm.SaveMedia = listView.SelectedItem as Media;
                vm.CurrentMedia = new Media()
                    {
                        Name = tmpMedia.Name,
                        Path = tmpMedia.Path
                    };
            }
        }

        private void AcceptPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
            var vm = (CurrentPlaylistViewModel)DataContext;
            int i = vm.Medias.IndexOf(vm.SaveMedia);
            var media = new Media()
            {
                Name = vm.CurrentMedia.Name,
                Path = vm.CurrentMedia.Path
            };
            vm.Medias.RemoveAt(i);
            vm.Medias.Insert(i, media);
        }

        private void CancelPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
        }
    }
}
