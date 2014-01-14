﻿#pragma checksum "..\..\..\Views\MusicView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "564C85CD28E7BC54F0747ECE68AEDEA3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34003
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DotNetProject.Views {
    
    
    /// <summary>
    /// MusicView
    /// </summary>
    public partial class MusicView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\MusicView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Filter;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\MusicView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listView;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\MusicView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem AddToPlaylist;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\MusicView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popupProperties;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DotNetProject;component/views/musicview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MusicView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Views\MusicView.xaml"
            ((DotNetProject.Views.MusicView)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Music_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Filter = ((System.Windows.Controls.TextBox)(target));
            
            #line 10 "..\..\..\Views\MusicView.xaml"
            this.Filter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Filter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listView = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            
            #line 13 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ContextMenu_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 14 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Read_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 15 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Remove_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 16 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ChangeProperties_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 17 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddToCurrentPlaylist_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.AddToPlaylist = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\..\Views\MusicView.xaml"
            this.AddToPlaylist.Click += new System.Windows.RoutedEventHandler(this.AddToPlaylist_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.popupProperties = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 11:
            
            #line 69 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AcceptPopup_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 70 "..\..\..\Views\MusicView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelPopup_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
