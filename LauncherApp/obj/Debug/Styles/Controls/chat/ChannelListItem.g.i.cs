﻿#pragma checksum "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E0FD39A53466BDF96587BE1653308B27"
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
    /// ChannelListItem
    /// </summary>
    public partial class ChannelListItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.ChannelListItem ItemElement;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ElementGird;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border PanelBorder;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel TittlePanel;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ChannelImage;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ChannelNameLibl;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ChannelID;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChannelMenuBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/styles/controls/chat/channellistitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
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
            this.ItemElement = ((LauncherApp.Styles.Controls.ChannelListItem)(target));
            return;
            case 2:
            
            #line 11 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.onMenuClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 15 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.onMenuClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ElementGird = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.PanelBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.TittlePanel = ((System.Windows.Controls.StackPanel)(target));
            
            #line 29 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            this.TittlePanel.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            this.TittlePanel.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseLeave);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            this.TittlePanel.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TittlePanel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ChannelImage = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.ChannelNameLibl = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.ChannelID = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.ChannelMenuBtn = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\..\..\Styles\Controls\chat\ChannelListItem.xaml"
            this.ChannelMenuBtn.Click += new System.Windows.RoutedEventHandler(this.ChannelMenuBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

