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
    /// Logique d'interaction pour MusicView.xaml
    /// </summary>
    public partial class MusicView : UserControl
    {
        static public MusicViewModel vm;

        public MusicView()
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
            vm = (MusicViewModel)this.DataContext;
        }

        private void Music_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                vm.handleKeydown(listView.SelectedItems, e);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                foreach (Song song in listView.SelectedItems)
                {
                    vm.removeSong(song);
                }
            }
            else if (listView.SelectedItem != null)
            {
                vm.removeSong(listView.SelectedItem as Song);
            }
        }

        private void ChangeProperties_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItem != null)
            {
                var tmpSong = listView.SelectedItem as Song;
                popupProperties.IsOpen = true;
                vm.SaveSong = listView.SelectedItem as Song;
                vm.CurrentSong = new Song()
                    {
                        Number = tmpSong.Number,
                        Name = tmpSong.Name,
                        Artist = tmpSong.Artist,
                        Album = tmpSong.Album,
                        Path = tmpSong.Path
                    };
            }
        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            string header = menuItem.Header as string;
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Songs = listView.SelectedItems;
                var playlist = new PlaylistViewModel(header, vm.CurrentPlaylistViewModel);
                foreach (Song tmpSong in Songs)
                {
                    playlist.Medias.Add(new Media { Name = tmpSong.Name, Path = tmpSong.Path });
                    playlist.savePlaylist();
                }
            }
            else if (listView.SelectedItem != null)
            {
                var tmpSong = listView.SelectedItem as Song;
                var playlist = new PlaylistViewModel(header, vm.CurrentPlaylistViewModel);
                playlist.Medias.Add(new Media { Name = tmpSong.Name, Path = tmpSong.Path });
                playlist.savePlaylist();
            }
        }

        private void AcceptPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
            var vm = (MusicViewModel)DataContext;
            int i = vm.Songs.IndexOf(vm.SaveSong);
            var song = new Song()
            {
                Number = vm.CurrentSong.Number,
                Name = vm.CurrentSong.Name,
                Artist = vm.CurrentSong.Artist,
                Album = vm.CurrentSong.Album,
                Path = vm.CurrentSong.Path
            };
            vm.Songs.RemoveAt(i);
            vm.Songs.Insert(i, song);
        }

        private void CancelPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
        }

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = Directory.GetFiles("../../Libraries/Playlists/");
            foreach (string fileName in filePaths)
            {
                string tmp = fileName.Substring(fileName.LastIndexOf('/') + 1);
                tmp = tmp.Remove(tmp.LastIndexOf('.'), tmp.Length - tmp.LastIndexOf('.'));
                AddToPlaylist.Items.Add(new MenuItem() { Header = tmp });
            }
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Songs = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Song song in Songs)
                {
                    tmp.Add(new Media { Name = song.Name, Path = song.Path });
                }
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var song = listView.SelectedItem as Song;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = song.Name, Path = song.Path } };
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
        }

        private void AddToCurrentPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MusicViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Songs = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Song song in Songs)
                {
                    tmp.Add(new Media { Name = song.Name, Path = song.Path });
                }
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var song = listView.SelectedItem as Song;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = song.Name, Path = song.Path } };
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
        }
    }
}
