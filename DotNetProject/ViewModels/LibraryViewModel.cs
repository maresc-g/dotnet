using DotNetProject.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.ViewModels
{
    public abstract class LibraryViewModel : ViewModelBase
    {
        public CurrentPlaylistViewModel CurrentPlaylistViewModel;

        public abstract void addToLibrary(string path);
    }
}
