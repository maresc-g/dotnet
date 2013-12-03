﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetProject.ViewModels
{//  Example code only, feel free to copy and re-use.

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static MainWindowViewModel _main;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            _main = new MainWindowViewModel();
        }

        /// <summary>
        /// Gets the Main property which defines the main viewmodel.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainWindowViewModel Main
        {
            get
            {
                return _main;
            }
        }
    }

}
