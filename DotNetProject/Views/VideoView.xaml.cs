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
    /// Logique d'interaction pour VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        static public VideoViewModel vm;

        public VideoView()
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
            vm = (VideoViewModel)this.DataContext;
        }

        private void Video_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                vm.handleKeydown(listView.SelectedItems, e);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                foreach (Video Video in listView.SelectedItems)
                {
                    vm.removeVideo(Video);
                }
            }
            else if (listView.SelectedItem != null)
            {
                vm.removeVideo(listView.SelectedItem as Video);
            }
        }

        private void ChangeProperties_Click(object sender, RoutedEventArgs e)
        {
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItem != null)
            {
                var tmpVideo = listView.SelectedItem as Video;
                popupProperties.IsOpen = true;
                vm.SaveVideo = listView.SelectedItem as Video;
                vm.CurrentVideo = new Video()
                    {
                        Name = tmpVideo.Name,
                        Artist = tmpVideo.Artist,
                        Path = vm.CurrentVideo.Path
                    };
            }
        }

        private void AcceptPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
            var vm = (VideoViewModel)DataContext;
            int i = vm.Videos.IndexOf(vm.SaveVideo);
            var Video = new Video()
            {
                Name = vm.CurrentVideo.Name,
                Artist = vm.CurrentVideo.Artist,
                Path = vm.CurrentVideo.Path
            };
            vm.Videos.RemoveAt(i);
            vm.Videos.Insert(i, Video);
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

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            string header = menuItem.Header as string;
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Videos = listView.SelectedItems;
                var playlist = new PlaylistViewModel(header, vm.CurrentPlaylistViewModel);
                foreach (Video video in Videos)
                {
                    playlist.Medias.Add(new Media { Name = video.Name, Path = video.Path });
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

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Videos = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Video video in Videos)
                {
                    tmp.Add(new Media { Name = video.Name, Path = video.Path });
                }
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var video = listView.SelectedItem as Video;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = video.Name, Path = video.Path } };
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
        }

        private void AddToCurrentPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var vm = (VideoViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Videos = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Video video in Videos)
                {
                    tmp.Add(new Media { Name = video.Name, Path = video.Path });
                }
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var video = listView.SelectedItem as Video;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = video.Name, Path = video.Path } };
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
        }
    }
}
