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
using System.Windows.Threading;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for FastMessage.xaml
    /// </summary>
    
    public partial class FastMessage : UserControl
    {

        public enum MessageTypes : int
        {
            Success,
            Warning,
            Info,
        }

        #region Type DP
        public MessageTypes Type
        {
            get { return (MessageTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); SwitchMessageType(value); }
        }

        public static readonly DependencyProperty TypeProperty
            = DependencyProperty.Register(
                  "Type",
                  typeof(MessageTypes),
                  typeof(FastMessage),
                  new PropertyMetadata(default(MessageTypes))
              );
        #endregion

        #region Message DP
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty
            = DependencyProperty.Register(
                  "Message",
                  typeof(string),
                  typeof(FastMessage)
              );
        #endregion

        #region ShowTime DP
        public int ShowTime
        {
            get { return (int)GetValue(ShowTimeProperty); }
            set { SetValue(ShowTimeProperty, value); ChangeShowTime(value); }
        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static readonly DependencyProperty ShowTimeProperty
            = DependencyProperty.Register(
                  "ShowTime",
                  typeof(int),
                  typeof(FastMessage)
              );
        #endregion

        public FastMessage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SwitchMessageType(MessageTypes type)
        {
            LinearGradientBrush gradientColor = new LinearGradientBrush() {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };

            switch (type)
            {
                case MessageTypes.Success:
                    MessageIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;

                    TittlePanel.Background = (Brush)new BrushConverter().ConvertFromString("#FF103812");
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF0DB90D"), 0));
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF048309"), 1));

                    break;
                case MessageTypes.Warning:
                    MessageIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.TimesCircleOutline;

                    TittlePanel.Background = (Brush)new BrushConverter().ConvertFromString("#FF480B0E");
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFDC163B"), 0));
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF910720"), 1));
                    break;
                case MessageTypes.Info:
                    MessageIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.InfoCircle;

                    TittlePanel.Background = (Brush)new BrushConverter().ConvertFromString("#FF773C00");
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFFDAF00"), 0));
                    gradientColor.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFEF8800"), 1));
                    break;
            }


            MessageIcon.Foreground = gradientColor;
        }

        private void ChangeShowTime(int value)
        {
            if (value > 0)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, value);
                dispatcherTimer.Start(); 
            }
            
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Hide();
            dispatcherTimer.Stop();
        }


        public void Show()
        {
            LauncherFactory.ElementAnimation(this, UserControl.OpacityProperty, 0, 1, 0.3, false);
        }

        public void Hide()
        {
            LauncherFactory.ElementAnimation(this, UserControl.OpacityProperty, 1, 0, 0.3, false);
        }

    }


}
