﻿#pragma checksum "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E35425E8640F2967236AA39926CFC5D8"
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
    /// FriendListItem
    /// </summary>
    public partial class FriendListItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.FriendListItem ItemElement;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ElementGird;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border PanelBorder;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel TittlePanel;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image UserImage;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UserName;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatusIcon;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UserMenuBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/styles/controls/chat/friendlistitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
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
            this.ItemElement = ((LauncherApp.Styles.Controls.FriendListItem)(target));
            return;
            case 2:
            
            #line 12 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.onMenuClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 16 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.onMenuClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 21 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.onMenuClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ElementGird = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.PanelBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.TittlePanel = ((System.Windows.Controls.StackPanel)(target));
            
            #line 35 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            this.TittlePanel.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseEnter);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            this.TittlePanel.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseLeave);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            this.TittlePanel.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TittlePanel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.UserImage = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.UserName = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.StatusIcon = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.UserMenuBtn = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\..\Styles\Controls\chat\FriendListItem.xaml"
            this.UserMenuBtn.Click += new System.Windows.RoutedEventHandler(this.UserMenuBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

