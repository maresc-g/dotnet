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
    /// Logique d'interaction pour ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        static public ImageViewModel vm;

        public ImageView()
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
            vm = (ImageViewModel)this.DataContext;
        }

        private void Image_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                vm.handleKeydown(listView.SelectedItems, e);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                foreach (ImageMedia Image in listView.SelectedItems)
                {
                    vm.removeImageMedia(Image);
                }
            }
            else if (listView.SelectedItem != null)
            {
                vm.removeImageMedia(listView.SelectedItem as ImageMedia);
            }
        }

        private void ChangeProperties_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItem != null)
            {
                var tmpImage = listView.SelectedItem as ImageMedia;
                popupProperties.IsOpen = true;
                vm.SaveImageMedia = listView.SelectedItem as ImageMedia;
                vm.CurrentImageMedia = new ImageMedia()
                    {
                        Name = tmpImage.Name,
                        Path = tmpImage.Path
                    };
            }
        }

        private void AcceptPopup_Click(object sender, RoutedEventArgs e)
        {
            popupProperties.IsOpen = false;
            var vm = (ImageViewModel)DataContext;
            int i = vm.ImageMedias.IndexOf(vm.SaveImageMedia);
            var Image = new ImageMedia()
            {
                Name = vm.CurrentImageMedia.Name,
                Path = vm.CurrentImageMedia.Path
            };
            vm.ImageMedias.RemoveAt(i);
            vm.ImageMedias.Insert(i, Image);
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
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Images = listView.SelectedItems;
                var playlist = new PlaylistViewModel(header, vm.CurrentPlaylistViewModel);
                foreach (ImageMedia img in Images)
                {
                    playlist.Medias.Add(new Media { Name = img.Name, Path = img.Path });
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
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Images = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (ImageMedia img in Images)
                {
                    tmp.Add(new Media { Name = img.Name, Path = img.Path });
                }
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var image = listView.SelectedItem as ImageMedia;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = image.Name, Path = image.Path } };
                vm.CurrentPlaylistViewModel.NewPlaylist(tmp);
            }
        }

        private void AddToCurrentPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ImageViewModel)DataContext;
            if (listView.SelectedItems != null)
            {
                var Images = listView.SelectedItems;
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (ImageMedia img in Images)
                {
                    tmp.Add(new Media { Name = img.Name, Path = img.Path });
                }
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
            else if (listView.SelectedItem != null)
            {
                var image = listView.SelectedItem as ImageMedia;
                ObservableCollection<Media> tmp = new ObservableCollection<Media> { new Media { Name = image.Name, Path = image.Path } };
                vm.CurrentPlaylistViewModel.AddToPlaylist(tmp);
            }
        }
    }
}
