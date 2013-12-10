using DotNetProject.ViewModels;
using System;
using System.Collections.Generic;
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
                        Path = vm.CurrentSong.Path
                    };
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
    }
}
