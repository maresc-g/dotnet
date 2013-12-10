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
    /// Logique d'interaction pour ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        static public ImageViewModel vm;

        public ImageView()
        {
            InitializeComponent();
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
                        Path = vm.CurrentImageMedia.Path
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
    }
}
