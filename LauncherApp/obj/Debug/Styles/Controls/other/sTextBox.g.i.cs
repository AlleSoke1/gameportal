﻿#pragma checksum "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1C8C9206B12D8A45477471BEF993C3B8"
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
    /// sTextBox
    /// </summary>
    public partial class sTextBox : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.sTextBox ControlElement;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Boxorder;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label backLabel;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox boxInput;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox boxInputPassword;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.IconPacks.PackIconMaterial PasswordIcon;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/styles/controls/other/stextbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
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
            this.ControlElement = ((LauncherApp.Styles.Controls.sTextBox)(target));
            
            #line 8 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.ControlElement.Loaded += new System.Windows.RoutedEventHandler(this.ControlElement_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Boxorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.backLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.boxInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInput.KeyDown += new System.Windows.Input.KeyEventHandler(this.boxInput_KeyDown);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInput.LostFocus += new System.Windows.RoutedEventHandler(this.boxInput_LostFocus);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInput.GotFocus += new System.Windows.RoutedEventHandler(this.boxInput_GotFocus);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInput.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.boxInput_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.boxInputPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 15 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInputPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.boxInput_KeyDown);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInputPassword.LostFocus += new System.Windows.RoutedEventHandler(this.boxInput_LostFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInputPassword.GotFocus += new System.Windows.RoutedEventHandler(this.boxInput_GotFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.boxInputPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.boxInputPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PasswordIcon = ((MahApps.Metro.IconPacks.PackIconMaterial)(target));
            
            #line 16 "..\..\..\..\..\Styles\Controls\other\sTextBox.xaml"
            this.PasswordIcon.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.PackIconMaterial_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
