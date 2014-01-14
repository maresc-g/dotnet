using DotNetProject.Models;
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
    /// Logique d'interaction pour CurrentView.xaml
    /// </summary>
    public partial class CurrentView : UserControl
    {
        public CurrentView()
        {
            InitializeComponent();
            MainWindowViewModel.ChangeView += HandleChangeView;
            this.Background = new SolidColorBrush(Color.FromRgb(Globals.backR, Globals.backG, Globals.backB));
            var children = myGrid.Children;
            foreach (Control child in children)
            {
                child.Background = new SolidColorBrush(Color.FromRgb(Globals.backR, Globals.backG, Globals.backB));
            }
        }

        public delegate void VisibleDelegate();

        #region VisibleFuncs
        private void PlayerVisible()
        {
            Player.Visibility = System.Windows.Visibility.Visible;
        }
        private void MusicVisible()
        {
            Music.Visibility = System.Windows.Visibility.Visible;
        }
        private void VideoVisible()
        {
            Video.Visibility = System.Windows.Visibility.Visible;
        }
        private void ImageVisible()
        {
            Image.Visibility = System.Windows.Visibility.Visible;
        }
        private void PlaylistVisible()
        {
            Playlist.Visibility = System.Windows.Visibility.Visible;
        }
        private void CurrentPlaylistVisible()
        {
            CurrentPlaylist.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        private void HandleChangeView(object sender, EventArgsStr e)
        {
            Dictionary<string, VisibleDelegate> dict = new Dictionary<string, VisibleDelegate>()
            {
                {"Player", PlayerVisible},
                {"Music", MusicVisible},
                {"Video", VideoVisible},
                {"Image", ImageVisible},
                {"Playlist", CurrentPlaylistVisible},
            };
            Player.Visibility = System.Windows.Visibility.Hidden;
            Music.Visibility = System.Windows.Visibility.Hidden;
            Video.Visibility = System.Windows.Visibility.Hidden;
            Image.Visibility = System.Windows.Visibility.Hidden;
            Playlist.Visibility = System.Windows.Visibility.Hidden;
            CurrentPlaylist.Visibility = System.Windows.Visibility.Hidden;
            if (dict.Keys.Contains(e.Arg))
                dict[e.Arg]();
            else
                PlaylistVisible();
        }
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void addToLibrary(string File, LibraryViewModel library, string[] tab)
        {
            if (library != null)
            {
                foreach (string item in tab)
                {
                    if (File.IndexOf(item) != -1)
                    {
                        library.addToLibrary(File);
                        break;
                    }
                }
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string File in FileList)
            {
                if (Music.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmMusic = Music.Content as MusicViewModel;
                    addToLibrary(File, vmMusic, Globals.tabMusic);
                }
                else if (Video.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmVideo = Video.Content as VideoViewModel;
                    addToLibrary(File, vmVideo, Globals.tabVideo);
                }
                else if (Image.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmImage = Image.Content as ImageViewModel;
                    addToLibrary(File, vmImage, Globals.tabImg);
                }
                else if (Playlist.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmPlaylist = Playlist.Content as PlaylistViewModel;
                    if (vmPlaylist != null)
                    {
                        vmPlaylist.addToLibrary(File);
                    }
                }
                else if (CurrentPlaylist.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmCurrentPlaylist = CurrentPlaylist.Content as CurrentPlaylistViewModel;
                    if (vmCurrentPlaylist != null)
                    {
                        vmCurrentPlaylist.addToLibrary(File);
                    }
                }
            }
        }
    }
}
