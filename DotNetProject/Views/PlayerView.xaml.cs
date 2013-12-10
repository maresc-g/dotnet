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
    /// Logique d'interaction pour PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        static public PlayerViewModel vm = new PlayerViewModel();

        public PlayerView()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.PlayRequested += (sender, e) =>
            {
                this.mediaElement.Play();
            };
            vm.PauseRequested += (sender, e) => { this.mediaElement.Pause(); };
            vm.StopRequested += (sender, e) => { this.mediaElement.Stop(); };
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            if (mediaElement.HasAudio)
            {
                timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            }
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            mediaElement.Stop();
        }

        private void Element_MediaFailed(object sender, EventArgs e)
        {
            mediaElement.Stop();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = e.NewValue;
        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if (mediaElement.HasAudio)
            {
                int SliderValue = (int)timelineSlider.Value;

                // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
                // Create a TimeSpan with miliseconds equal to the slider value.
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                mediaElement.Position = ts;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            return;
        }
        protected void OnHandleDestroyed(EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
