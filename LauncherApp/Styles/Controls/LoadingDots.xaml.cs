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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for CheckBox.xaml
    /// </summary>
    
    public partial class LoadingDots : UserControl
    {

        public LoadingDots()
        {
            InitializeComponent();
            this.DataContext = this;

            Storyboard s = (Storyboard)TryFindResource("mystoryboard");
            s.Begin();
        }



    }

}
