﻿#pragma checksum "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58357D4D8202E029C6AB571BEFEEC970"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace LauncherApp.Styles.Controls {
    
    
    /// <summary>
    /// HeaderButton
    /// </summary>
    public partial class HeaderButton : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton ControlElement;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.IconPacks.PackIconMaterial ButtonIcon;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/styles/controls/buttons/headerbutton%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
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
            this.ControlElement = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            
            #line 9 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
            this.ControlElement.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ControlElement_MouseEnter);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
            this.ControlElement.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ControlElement_MouseLeave);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
            this.ControlElement.Loaded += new System.Windows.RoutedEventHandler(this.ControlElement_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\..\..\Styles\Controls\buttons\HeaderButton - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonIcon = ((MahApps.Metro.IconPacks.PackIconMaterial)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
