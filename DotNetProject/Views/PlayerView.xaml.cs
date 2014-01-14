using DotNetProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private int _state;
        private Timer timer;
        private volatile bool _changing;

        public PlayerView()
        {
            InitializeComponent();
            this.DataContext = vm;
            timelineSlider.ApplyTemplate();
            volumeSlider.ApplyTemplate();
            _changing = false;
            volumeSlider.IsMoveToPointEnabled = true;
            timelineSlider.IsMoveToPointEnabled = true;
            Thumb thumb = (timelineSlider.Template.FindName("PART_Track", timelineSlider) as Track).Thumb;
            Thumb volume_thumb = (timelineSlider.Template.FindName("PART_Track", volumeSlider) as Track).Thumb;
            thumb.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(thumb_MouseDown), true);
            thumb.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(thumb_MouseUp), true);
            thumb.Height = 5;
            volume_thumb.Height = 5;

            var time_incButton = (timelineSlider.Template.FindName("PART_Track", timelineSlider) as Track).IncreaseRepeatButton;
            time_incButton.Background = new SolidColorBrush(Color.FromRgb(0x50, 0x50, 0x50));
            var time_decButton = (timelineSlider.Template.FindName("PART_Track", timelineSlider) as Track).DecreaseRepeatButton;
            time_decButton.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            var volume_incButton = (timelineSlider.Template.FindName("PART_Track", volumeSlider) as Track).IncreaseRepeatButton;
            volume_incButton.Background = new SolidColorBrush(Color.FromRgb(0x50, 0x50, 0x50));
            var volume_decButton = (timelineSlider.Template.FindName("PART_Track", volumeSlider) as Track).DecreaseRepeatButton;
            volume_decButton.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Height = MainWindow.CurrentHeight;
            Width = MainWindow.CurrentWidth;
            mediaElement.Height = MainWindow.CurrentHeight - 80;
            mediaElement.Width = MainWindow.CurrentWidth;
            timelineSlider.Width = MainWindow.CurrentWidth - 150;

            CurrentPlaylistViewModel.CurrentRandomState += (sender, e) =>
                {
                    if (!(bool)e)
                        setButtonImg(RandomButton, "RandomButtonImage", "../Resources/random_button.png");
                    else
                        setButtonImg(RandomButton, "RandomButtonImage", "../Resources/random_button_pressed.png");
                };

            CurrentPlaylistViewModel.CurrentRepeatState += (sender, e) =>
            {
                    if ((int)e == 0)
                        setButtonImg(RepeatButton, "RepeatButtonImage", "../Resources/repeat_button.png");
                    else if ((int)e == 1)
                        setButtonImg(RepeatButton, "RepeatButtonImage", "../Resources/repeat_button_pressed.png");
                    else if ((int)e == 2)
                        setButtonImg(RepeatButton, "RepeatButtonImage", "../Resources/repeat_button_one.png");
                };
            MainWindow.ResizeRequested += (sender, e) =>
            {
                Height = MainWindow.CurrentHeight;
                Width = MainWindow.CurrentWidth;
                mediaElement.Height = MainWindow.CurrentHeight - 80;
                mediaElement.Width = MainWindow.CurrentWidth;
                timelineSlider.Width = MainWindow.CurrentWidth - 150;
            };
            currentTime.Content = "--:--";
            totalTime.Content = "--:--";
            _state = 0;
            timer = new Timer(10);
            timer.Elapsed += (sender, e) =>
            {
                this.Dispatcher.Invoke(new Action(() =>
            {
                if (!_changing)
                {
                    timelineSlider.Value = mediaElement.Position.TotalMilliseconds;
                    setDuration(currentTime, mediaElement.Position);
                }
            }));
            };
            vm.PlayRequested += (sender, e) =>
            {
                PauseImage();
                this.mediaElement.Play();
                timer.Start();
            };
            vm.PauseRequested += (sender, e) =>
            {
                PlayImage();
                this.mediaElement.Pause();
                timer.Stop();
            };
            vm.StopRequested += (sender, e) =>
            {
                PlayImage();
                this.mediaElement.Stop();
                timer.Stop();
            };
        }

        private void setButtonImg(Button button, string buttonImgName, string imgPath)
        {
            var image = (Image)button.Template.FindName(buttonImgName, button);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(imgPath, UriKind.Relative);
            bi.EndInit();
            image.Stretch = Stretch.Fill;
            image.Source = bi;
        }

        private void thumb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _changing = true;
            return;
        }

        private void setDuration(Label label, TimeSpan ts)
        {
            label.Content = "";
            if (ts.Days > 0)
                label.Content += ts.Days.ToString().PadLeft(2, '0') + ":";
            if (ts.Hours > 0)
                label.Content += ts.Hours.ToString().PadLeft(2, '0') + ":";
            label.Content += ts.Minutes.ToString().PadLeft(2, '0');
            label.Content += ":" + ts.Seconds.ToString().PadLeft(2, '0');
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            if (mediaElement.HasAudio)
            {
                timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
                setDuration(totalTime, mediaElement.NaturalDuration.TimeSpan);
            }
            else
            {
                timelineSlider.Maximum = 5000;
                totalTime.Content = "05:00";
            }
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            mediaElement.Stop();
            vm.songEnded();
        }

        private void Element_MediaFailed(object sender, EventArgs e)
        {
            mediaElement.Stop();
            PlayImage();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = e.NewValue;
        }

        private void time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_changing)
            {
                if (e.OldValue > e.NewValue || e.OldValue + 150 < e.NewValue)
                {
                    int SliderValue = (int)e.NewValue;
                    TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                    mediaElement.Position = ts;
                }
            }
        }

        private void PlayImage()
        {
            setButtonImg(PlayPauseButton, "PlayPauseButtonImage", "../Resources/play_button.png");
            _state = 0;
        }

        private void PauseImage()
        {
            setButtonImg(PlayPauseButton, "PlayPauseButtonImage", "../Resources/pause_button.png");
            _state = 1;
        }

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            vm.PlayPauseCall(_state);
        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaElement.Position = ts;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            return;
        }

        protected void OnHandleDestroyed(EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void thumb_MouseUp(object sender, MouseEventArgs e)
        {
            _changing = false;
            int SliderValue = (int)timelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaElement.Position = ts;
        }
    }
}
