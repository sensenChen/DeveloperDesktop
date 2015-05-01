using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DeveloperDesktop.Controllers
{
    public partial class WindowsController
    {
        // Setting up important public configuration variables
        static int height = 250, width = 500, count = 0;
        public static List<WebView> windowList = new List<WebView>();
        public static List<Button> buttonList = new List<Button>();

        // Function that creates a web-view UI object, stores a reference and passes back an id
        public static int createWindow(string url, int x, int y, int width, int height)
        {
            WebView window = new WebView();
            window.Height = height;
            window.Width = width;
            //int row = (count%3);
            //int col = count / 3;
            //window.Margin = new Thickness(row*(width)+(row+1)*25, col*height+(col+1)*25, 0, 0);
            window.Margin = new Thickness(x, y, 0, 0);
            window.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            window.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            window.Source = new Uri(url);
            window.DataContext = count;
            windowList.Add(window);

            Button closeButton = new Button();
            closeButton.Content = "X";
            closeButton.Margin = new Thickness(x, y, 0, 0);
            //closeButton.Margin = new Thickness((row)*width + (row + 1) * 25, col * height + (col + 1) * 25, 0, 0);
            closeButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            closeButton.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            closeButton.DataContext = count;
            closeButton.Tapped += closeWindow;
            buttonList.Add(closeButton);

            count++;
            return count-1;
        }
        // Function that removes an active web-view from the desktop and stops it from rendering
        public static void closeWindow(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int index = (int)button.DataContext;
                windowList.Remove(windowList[windowList.Count]);
                buttonList.Remove(buttonList[buttonList.Count]);
            }
            catch (Exception ex)
            {
                var warning = new Windows.UI.Popups.MessageDialog("Can't delete item");
                warning.ShowAsync();
            }
            
        }

        public WindowsController()
        {
            // TODO: Complete member initialization when we have more complex data objects to store

        }
    }
}
