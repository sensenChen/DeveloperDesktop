using DeveloperDesktop.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DeveloperDesktop
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        WindowsController windowsController = new WindowsController();
        public MainPage mainPage;
        public MainPage()
        {
            this.InitializeComponent();
            mainPage = this;
            openWindows();
        }
        // Function that creates and renders a list of windows
        private void openWindows()
        {
            List<string> urls = new List<string>();
            urls.Add("http://www.youtube.com");
            urls.Add("http://www.twitter.com");
            urls.Add("http://www.github.com");
            for (int i = 0; i < urls.Count; i++)
            {
                int window_index = windowsController.createWindow(urls[i]);
                RootGrid.Children.Add(windowsController.windowList[window_index]);
                RootGrid.Children.Add(windowsController.buttonList[window_index]);
            }
        }
        // Function that is called on button click that generates a new web-view
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = this.url_bar.Text;
            try
            {
                Uri uri = new Uri(url);
                int window_index = windowsController.createWindow(url);
                RootGrid.Children.Add(windowsController.windowList[window_index]);
                RootGrid.Children.Add(windowsController.buttonList[window_index]);
                this.url_bar.Text = "";
            }
            catch (Exception ex)
            {
                var warning = new Windows.UI.Popups.MessageDialog("Invalid URL");
                warning.ShowAsync();
            }
            
        }
    }
}
