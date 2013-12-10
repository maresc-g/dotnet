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
    /// Logique d'interaction pour VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        static public VideoViewModel vm;

        public VideoView()
        {
            InitializeComponent();
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
    }
}
