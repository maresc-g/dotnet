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
            }
        }
    }
}
