﻿#pragma checksum "..\..\..\Windows\StatisticsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A530D137F3824C54617DE402731564CADD986210F1E45E6F9E5BCC4CA04ACD68"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FavoritePlayersWPF.Properties;
using FavoritePlayersWPF.Windows;
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


namespace FavoritePlayersWPF.Windows {
    
    
    /// <summary>
    /// StatisticsWindow
    /// </summary>
    public partial class StatisticsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 96 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label name;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label fifa;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label gamesPlayed;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label wins;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label losses;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label draws;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label goals;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label goalsTaken;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\Windows\StatisticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label diff;
        
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
            System.Uri resourceLocater = new System.Uri("/FavoritePlayersWPF;component/windows/statisticswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\StatisticsWindow.xaml"
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
            
            #line 10 "..\..\..\Windows\StatisticsWindow.xaml"
            ((FavoritePlayersWPF.Windows.StatisticsWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.name = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.fifa = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.gamesPlayed = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.wins = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.losses = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.draws = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.goals = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.goalsTaken = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.diff = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
