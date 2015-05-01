using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperDesktop.Controllers
{
    // Model that stores all user's preferenes
    partial class PreferencesModel
    {
        // Structure that organizes and groups window related data
        public struct window
        {
            public string url;
            public int x;
            public int y;
            public int width;
            public int height;
        }
        public List<window> windows;
        private StorageController storage = new StorageController();

        // Function that calls StorageController and loads the user's settings from disk memory.
        public int loadWindows()
        {
            windows = storage.loadWindows();
            try
            {
                if (windows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Nothing");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Val: %s", windows[0].url);
                }
                return windows.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        // Function that saves the current window status on disk for later retrieval
        public void saveWindows()
        {
            storage.saveWindows(windows);
        }

        // Function that loads and returns a specific window by it's id
        public window loadWindow(int id)
        {
            try
            {
                return windows[id];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", ex);
                throw ex;
            }
        }

        // Function that generates a window and stores a reference for later access
        public int createWindow(string url, int x, int y, int width, int height)
        {
            window t = new window();
            t.url = url;
            t.x = x;
            t.y = y;
            t.width = width;
            t.height = height;
            try
            {
                windows.Add(t);
            }
            catch (NullReferenceException ex)
            {
                windows = new List<window>();
                windows.Add(t);
            }
            
            return windows.Count;
        }

        // Function that removes a window reference from access
        public void removeWindow(int id)
        {
            windows.Remove(windows[id]);
        }

        // Function that sets up the preference model on start
        public PreferencesModel()
        {
            this.windows = new List<window>();
        }
    }
}
