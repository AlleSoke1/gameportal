﻿#pragma checksum "..\..\LoginWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "92A6B824084546F24CC668E1043FCFFF"
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
    /// LoginsWindow
    /// </summary>
    public partial class LoginsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.LoginsWindow LoginWindow;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid headerGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LoginLogo;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton MinieBtn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton tFullBtn;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.HeaderButton CloseBtn;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LoginGrid;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginID;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label loginID_libl;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox loginPass;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label loginPass_libl;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.CheckBox RememberCheck;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loginButton;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LoginLoading;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.FastMessage ErrorMessage;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginWindow.xaml"
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
            this.LoginWindow = ((LauncherApp.LoginsWindow)(target));
            
            #line 10 "..\..\LoginWindow.xaml"
            this.LoginWindow.Loaded += new System.Windows.RoutedEventHandler(this.LoginWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.headerGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 26 "..\..\LoginWindow.xaml"
            this.headerGrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LoginWindow_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LoginLogo = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.MinieBtn = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            return;
            case 6:
            this.tFullBtn = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            return;
            case 7:
            this.CloseBtn = ((LauncherApp.Styles.Controls.HeaderButton)(target));
            return;
            case 8:
            this.LoginGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.loginID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.loginID_libl = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.loginPass = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 12:
            this.loginPass_libl = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.RememberCheck = ((LauncherApp.Styles.Controls.CheckBox)(target));
            return;
            case 14:
            
            #line 68 "..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Forgot_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.loginButton = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\LoginWindow.xaml"
            this.loginButton.Click += new System.Windows.RoutedEventHandler(this.loginButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 75 "..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RegisterButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.LoginLoading = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 18:
            this.ErrorMessage = ((LauncherApp.Styles.Controls.FastMessage)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
