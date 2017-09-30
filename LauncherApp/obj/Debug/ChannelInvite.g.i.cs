﻿#pragma checksum "..\..\ChannelInvite.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D6DBA7DA6A9F2A409930AAD361FCFA72"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using LauncherApp.Styles.Controls;
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


namespace LauncherApp {
    
    
    /// <summary>
    /// ChannelInvite
    /// </summary>
    public partial class ChannelInvite : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton tMinieBtn;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton tCloseBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label WindowTitle;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LoginLoading;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\ChannelInvite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.FastMessage alertMessage;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/channelinvite.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChannelInvite.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 9 "..\..\ChannelInvite.xaml"
            ((LauncherApp.ChannelInvite)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tMinieBtn = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            return;
            case 3:
            this.tCloseBtn = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            return;
            case 4:
            this.WindowTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\ChannelInvite.xaml"
            this.NameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SendButton = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ChannelInvite.xaml"
            this.SendButton.Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LoginLoading = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.alertMessage = ((LauncherApp.Styles.Controls.FastMessage)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
