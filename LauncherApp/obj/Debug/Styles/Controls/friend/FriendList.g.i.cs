﻿#pragma checksum "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9CB337A0B23A11472992BB8C56A1039E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
    /// FriendList
    /// </summary>
    public partial class FriendList : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LauncherApp.Styles.Controls.FriendList ListElement;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ElementGird;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TittlePanelBorder;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel TittlePanel;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ArrowIcon;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label friendName;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FriendCounter;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ListPanel;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel EmptyFlag;
        
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
            System.Uri resourceLocater = new System.Uri("/LauncherApp;component/styles/controls/friend/friendlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
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
            this.ListElement = ((LauncherApp.Styles.Controls.FriendList)(target));
            return;
            case 2:
            this.ElementGird = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.TittlePanelBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.TittlePanel = ((System.Windows.Controls.StackPanel)(target));
            
            #line 15 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
            this.TittlePanel.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
            this.TittlePanel.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TittlePanel_MouseLeave);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\..\Styles\Controls\friend\FriendList.xaml"
            this.TittlePanel.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TittlePanel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ArrowIcon = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.friendName = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.FriendCounter = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ListPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.EmptyFlag = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

