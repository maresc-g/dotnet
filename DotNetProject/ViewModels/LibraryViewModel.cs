using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.ViewModels
{
    public abstract class LibraryViewModel : ViewModelBase
    {
        public static event EventHandler<EventArgsStr> NewMediaRequested;

        protected virtual void OnNewMediaRequested(EventArgsStr e)
        {
            EventHandler<EventArgsStr> handler = LibraryViewModel.NewMediaRequested;

            if (handler != null)
            {
                handler(null, e);
            }
        }
    }
}
