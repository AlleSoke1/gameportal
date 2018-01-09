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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for TheShop.xaml
    /// </summary>
    public partial class TheShop : UserControl
    {
        private string pShopUrl = "https://accounts.arcangames.com/remote/shop";
        private bool ShopLoaded = false;

        public TheShop()
        {
            InitializeComponent();
        }


        private void Url_LoadingFrameComplete(object sender, Awesomium.Core.FrameEventArgs e)
        {
            LoadedIcon.Visibility = Visibility.Hidden;
        }


        public void LoadShopUrl()
        {
            if (!ShopLoaded)
            {
                if (ShopWebUrl.Source.ToString() == "about:blank" || ShopWebUrl.Source.ToString() != pShopUrl)
                {
                    ShopWebUrl.Source = new Uri(pShopUrl);
                    ShopLoaded = true;
                }
            }
            
        }


    }
}
