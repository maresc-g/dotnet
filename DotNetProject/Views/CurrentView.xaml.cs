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
        }

        private void HandleChangeView(object sender, EventArgsStr e)
        {
            Player.Visibility = System.Windows.Visibility.Hidden;
            Music.Visibility = System.Windows.Visibility.Hidden;
            Video.Visibility = System.Windows.Visibility.Hidden;
            Image.Visibility = System.Windows.Visibility.Hidden;
            Playlist.Visibility = System.Windows.Visibility.Hidden;
            CurrentPlaylist.Visibility = System.Windows.Visibility.Hidden;
            if (e.Arg == "Player")
            {
                Player.Visibility = System.Windows.Visibility.Visible;
            }
            else if (e.Arg == "Music")
            {
                Music.Visibility = System.Windows.Visibility.Visible;
            }
            else if (e.Arg == "Video")
            {
                Video.Visibility = System.Windows.Visibility.Visible;
            }
            else if (e.Arg == "Image")
            {
                Image.Visibility = System.Windows.Visibility.Visible;
            }
            else if (e.Arg == "Playlist")
            {
                CurrentPlaylist.Visibility = System.Windows.Visibility.Visible;
            }
            else if (e.Arg.Contains("Playlist"))
            {
                Playlist.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string File in FileList)
            {
                if (Music.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmMusic = Music.Content as MusicViewModel;
                    if (vmMusic != null)
                    {
                        vmMusic.addToLibrary(File);
                    }
                }
                else if (Video.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmVideo = Video.Content as VideoViewModel;
                    if (vmVideo != null)
                    {
                        vmVideo.addToLibrary(File);
                    }
                }
                else if (Image.Visibility == System.Windows.Visibility.Visible)
                {
                    var vmImage = Image.Content as ImageViewModel;
                    if (vmImage != null)
                    {
                        vmImage.addToLibrary(File);
                    }
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
