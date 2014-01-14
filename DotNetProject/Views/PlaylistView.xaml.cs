using DotNetProject.Models;
using DotNetProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        static public PlaylistViewModel vm;

        public PlaylistView()
        {
            InitializeComponent();
            Height = MainWindow.CurrentHeight;
            Width = MainWindow.CurrentWidth;
            listView.Height = MainWindow.CurrentHeight - 30;
            listView.Width = MainWindow.CurrentWidth;
            MainWindow.ResizeRequested += (sender, e) =>
            {
                Height = MainWindow.CurrentHeight;
                Width = MainWindow.CurrentWidth;
                listView.Height = MainWindow.CurrentHeight;
                listView.Width = MainWindow.CurrentWidth;
            };
            vm = (PlaylistViewModel)this.DataContext;
        }

        private void Playlist_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                vm.handleKeydown(listView.SelectedItems, e);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Media media in listView.SelectedItems)
                {
                    tmp.Add(media);
                }
                foreach (Media media in tmp)
                {
                    vm.removeMedia(media);
                }
                vm.savePlaylist();
            }
            else if (listView.SelectedItem != null)
            {
                vm.removeMedia(listView.SelectedItem as Media);
                vm.savePlaylist();
            }
        }

        private void ChangeProperties_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
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
            var vm = (PlaylistViewModel)DataContext;
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

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
            vm.filterList(Filter.Text);
            return;
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Medias = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Media Media in Medias)
                {
                    tmp.Add(new Media { Name = Media.Name, Path = Media.Path });
                }
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var Media = listView.SelectedItem as Media;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = Media.Name, Path = Media.Path } };
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
        }

        private void AddToCurrentPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PlaylistViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Medias = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Media Media in Medias)
                {
                    tmp.Add(new Media { Name = Media.Name, Path = Media.Path });
                }
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var Media = listView.SelectedItem as Media;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = Media.Name, Path = Media.Path } };
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
        }
    }
}
