﻿using DotNetProject.ViewModels;
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
        public PlayerView()
        {
            InitializeComponent();
            PlayerViewModel vm = new PlayerViewModel();
            this.DataContext = vm;
            vm.PlayRequested += (sender, e) => { this.mediaElement.Play(); };
            vm.PauseRequested += (sender, e) => { this.mediaElement.Pause(); };
            vm.StopRequested += (sender, e) => { this.mediaElement.Stop(); };
        }
    }
}