﻿#pragma checksum "..\..\GameOver.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9AA33AE179B8478A97C614C381EA8B04"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace GUI_Project {
    
    
    /// <summary>
    /// GameOver
    /// </summary>
    public partial class GameOver : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPoints;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblHighscoreSaved;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid submitGrid;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbAddName;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewGameGO;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\GameOver.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMenuGO;
        
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
            System.Uri resourceLocater = new System.Uri("/GUI_Project;component/gameover.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GameOver.xaml"
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
            this.lblPoints = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblHighscoreSaved = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.submitGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.txbAddName = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\GameOver.xaml"
            this.txbAddName.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.txbAddName_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 55 "..\..\GameOver.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSubmitHighScore_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnNewGameGO = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\GameOver.xaml"
            this.btnNewGameGO.Click += new System.Windows.RoutedEventHandler(this.btnNewGameGO_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnMenuGO = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\GameOver.xaml"
            this.btnMenuGO.Click += new System.Windows.RoutedEventHandler(this.btnMenuGO_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

