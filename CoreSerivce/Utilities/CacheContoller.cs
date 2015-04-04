using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreSerivce.Utilities
{
    public class CacheContoller
    {
        public static MemcachedClient CacheClient()
        {
            var config = new MemcachedClientConfiguration();
            config.AddServer("192.168.100.249", 11211);
            config.Protocol = MemcachedProtocol.Text;
            config.KeyTransformer = new DefaultKeyTransformer();

            var client = new MemcachedClient(config);
            return client;
        }
        public static object cacheControl(string key, object value, string operation, double cacheTime)
        {
            key = key.Replace("/", "").Replace("\\", "").Replace(".", "").ToLower();

            object retValue = null;
            if (operation == "R")
            {
                try
                {
                    retValue = HttpRuntime.Cache.Get(key);
                }
                catch
                {

                }
            }

            if (operation == "W")
            {
                HttpRuntime.Cache.Insert(key, value,
                        null, DateTime.Now.AddSeconds(cacheTime),
                       new TimeSpan(0));
                retValue = value;
            }

            return retValue;
        }
    }
}
