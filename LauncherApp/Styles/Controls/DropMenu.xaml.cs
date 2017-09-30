using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FontAwesome.WPF;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for DropMenu.xaml
    /// </summary>
    public partial class DropMenu : UserControl
    {

        public List<Action> ActionList =  new List<Action>();

        public DropMenu()
        {
            InitializeComponent();

            //addItem((string)App.Current.Resources["LogoMenuAccounts"], FontAwesome.WPF.FontAwesomeIcon.UserCircle, true, new Action(delegate() { MessageBox.Show("asdasd"); }));
            //addItem((string)App.Current.Resources["LogoMenuSupport"], FontAwesome.WPF.FontAwesomeIcon.QuestionCircleOutline, true, new Action(delegate() { MessageBox.Show("asdasd"); }));
            //addItem((string)App.Current.Resources["LogoMenuSettings"], FontAwesome.WPF.FontAwesomeIcon.Cog, false, new Action(delegate() { MessageBox.Show("asdasd"); }));
            //addSpactor();
            //addItem((string)App.Current.Resources["LogoMenuForums"], FontAwesome.WPF.FontAwesomeIcon.CommentsOutline, true, new Action(delegate() { MessageBox.Show("asdasd"); }));
            //addSpactor();
            //addItem((string)App.Current.Resources["LogoMenuLogout"], FontAwesome.WPF.FontAwesomeIcon.PowerOff, false, new Action(delegate() { MessageBox.Show("asdasd"); }));
            //addItem((string)App.Current.Resources["LogoMenuExit"], FontAwesome.WPF.FontAwesomeIcon.Lock, false, new Action(delegate() { MessageBox.Show("asdasd"); }));


            this.DataContext = this;
        }

        public void addItem(string iText, FontAwesomeIcon iIcon, bool iUrlFlag, Action iAction, Brush iIconColor = null){


            StackPanel tempPanel = new StackPanel() {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0),
                Height = 25,
                Background = new SolidColorBrush() { Color = Colors.White, Opacity = 0 }
            };


            if (OptionList.Children.Count == 0) tempPanel.Margin = new Thickness(0, 5, 0, 0);

            tempPanel.Children.Add(new ImageAwesome()
            {
               Icon = iIcon,
               Width = 12,
               Margin = new Thickness(8,0,0,0),
               Height = 25,
               VerticalAlignment = System.Windows.VerticalAlignment.Center,
               Foreground = iIconColor == null ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029") : iIconColor,
           });

           tempPanel.Children.Add(new Label()
           {
               Height= 25,
               FontSize = 12,
               Margin = new Thickness(5,0,0,0),
               Foreground = Brushes.Silver,
               Content = iText,
               HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
               VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
               Padding = new Thickness(0)
           });

           if (iUrlFlag)
           {
               tempPanel.Children.Add(new ImageAwesome()
               {
                   Icon = FontAwesomeIcon.ExternalLink,
                   Width = 12,
                   Margin = new Thickness(5, 0, 0, 0),
                   Height = 25,
                   VerticalAlignment = System.Windows.VerticalAlignment.Center,
                   Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029")
               });
           }

           tempPanel.Tag = ActionList.Count;
           ActionList.Add(iAction);
           tempPanel.MouseDown += onItemClick;
           tempPanel.MouseEnter += onItemHover;
           tempPanel.MouseLeave += onItemHoverOut;

           OptionList.Children.Add(tempPanel);
           OptionList.Height += 25;

        }

        private void onItemHoverOut(object sender, MouseEventArgs e)
        {
            StackPanel sPanel = (StackPanel)sender;
            sPanel.Background.Opacity = 0;
        }

        private void onItemHover(object sender, MouseEventArgs e)
        {
            StackPanel sPanel = (StackPanel)sender;
            sPanel.Background.Opacity = 0.05;
        }

        public void addSpactor() { 

            OptionList.Children.Add(new DockPanel(){
                LastChildFill = false,
                Background = Brushes.White, 
                Height = 1,
                Margin = new Thickness(0, 5, 0, 5), 
                Opacity = 0.15,  
                VerticalAlignment = System.Windows.VerticalAlignment.Top
            });

            OptionList.Height += 11;

        }

        private void onItemClick(object sender, MouseButtonEventArgs e)
        {
            StackPanel objSender = (StackPanel)sender;
            Action nAction = ActionList[(int)objSender.Tag];
            nAction();
        }

        private void ControlElement_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("IS CLIKCED");
        }


    }
}
