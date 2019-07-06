using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace NittyGritty.Data
{
    public static class CacheManager
    {
        /// <summary>
        /// Collection of custom types provided to the <see cref="DataContractSerializer"/> when
        /// reading and writing cache.  Initially empty, additional types may be
        /// added to customize the serialization process.
        /// </summary>
        public static Collection<Type> KnownTypes { get; } = new Collection<Type>();

        public static Dictionary<string, object> LoadCache(string path)
        {
            var cache = new Dictionary<string, object>();
            
            // Get the input stream for the SessionState file
            using (var inStream = File.OpenRead(path))
            {
                // Deserialize the Session State
                var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), KnownTypes);
                cache = (Dictionary<string, object>)serializer.ReadObject(inStream);
            }

            return cache;
        }

        public static async void SaveCache(string path, Dictionary<string, object> cache)
        {
            using (var cacheStream = new MemoryStream())
            {
                // Serialize the session state synchronously to avoid asynchronous access to shared state
                var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), KnownTypes);
                serializer.WriteObject(cacheStream, cache);

                // Get an output stream for the SessionState file and write the state asynchronously
                using (var fileStream = File.OpenWrite(path))
                {
                    cacheStream.Seek(0, SeekOrigin.Begin);
                    await cacheStream.CopyToAsync(fileStream);
                }
            }
        }
    }
}
