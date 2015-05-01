using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeveloperDesktop.Controllers
{
    // StorageController is within PreferenceModel beause it has hidden methods that should only be accessed by PreferenceModel
    // This abstracts interactivity and makes our system more secure
    partial class PreferencesModel
    {
        private class StorageController
        {
            // Function that converts objects into XML for storage
            private string Serialize(object obj)
            {
                using (var sw = new StringWriter())
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj);
                    return sw.ToString();
                }
            }

            // Function that converts XML into objects for retrieval
            private T Deserialize<T>(string xml)
            {
                using (var sw = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(sw);
                }
            }

            // Function that serializes and saves the current configuration to disk
            public void saveWindows(List<window> windows)
            {
                Windows.Storage.ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var serial = Serialize(windows);
                settings.Values["preferences"] = serial;
            }

            // Function that loads the old configuration from disk for rendering
            public List<window> loadWindows()
            {
                Windows.Storage.ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                try
                {
                    List<window> loadedWindows = (List<window>) settings.Values["preferences"];
                    return loadedWindows;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
