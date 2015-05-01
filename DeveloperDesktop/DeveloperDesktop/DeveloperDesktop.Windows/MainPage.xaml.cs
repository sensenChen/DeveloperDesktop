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
    /// The main desktop view of our application.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        // Instantiating classes for later use
        public MainPage mainPage;
        private PreferencesModel preferences = new PreferencesModel();
        
        // On start, load user preferences and render the windows
        public MainPage()
        {
            this.InitializeComponent();
            int num_windows = preferences.loadWindows();
            openWindows();
        }
        // Function that creates and renders a list of windows
        private void openWindows()
        {
            List<string> urls = new List<string>();
            for (int i = 0; i < preferences.windows.Count; i++)
            {
                PreferencesModel.window window = preferences.windows[i];
                int window_index = WindowsController.createWindow(window.url, window.x, window.y, window.width, window.height);
                RootGrid.Children.Add(WindowsController.windowList[window_index]);
                RootGrid.Children.Add(WindowsController.buttonList[window_index]);
            }
        }
        // Function that is called on button click that generates a new web-view
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = this.url_bar.Text;
            try
            {
                Uri uri = new Uri(url);
                int window_index = WindowsController.createWindow(url, 0, 0, 600, 400);
                RootGrid.Children.Add(WindowsController.windowList[window_index]);
                RootGrid.Children.Add(WindowsController.buttonList[window_index]);
                this.url_bar.Text = "";
            }
            catch (Exception ex)
            {
                var warning = new Windows.UI.Popups.MessageDialog("Invalid URL");
                warning.ShowAsync();
            }
            
        }
        // Removes references of UI elements and un-renders them from the screen
        public void closeWindow(int window_index)
        {
            RootGrid.Children.Remove(WindowsController.windowList[window_index]);
            RootGrid.Children.Remove(WindowsController.buttonList[window_index]);
        }


    }
}
