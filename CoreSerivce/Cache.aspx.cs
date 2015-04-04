using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoreSerivce
{
    public partial class Cache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = System.Web.HttpRuntime.Cache.GetEnumerator();
            List<ch> lst = new List<ch>();
            int indx = 1;
            while (enumerator.MoveNext())
            {
                ch chobj=new ch ();
                chobj.Key = (string)enumerator.Key;
               // chobj.value= enumerator.Value.ToString();
                try
                {
                    chobj.expire = GetCacheUtcExpiryDateTime(chobj.Key).AddMinutes(210).ToString();
                }
                catch
                {

                }
                chobj.index = indx.ToString();
                indx++;

                lst.Add(chobj);
            }

            GridView1.DataSource = lst;
            GridView1.DataBind();
        }
        private DateTime GetCacheUtcExpiryDateTime(string cacheKey)
        {
            object cacheEntry = Cache.GetType().GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(Cache, new object[] { cacheKey, 1 });
            PropertyInfo utcExpiresProperty = cacheEntry.GetType().GetProperty("UtcExpires", BindingFlags.NonPublic | BindingFlags.Instance);
            DateTime utcExpiresValue = (DateTime)utcExpiresProperty.GetValue(cacheEntry, null);
            return utcExpiresValue;
        }
    }
    public class ch
    {
        public string Key { get; set; }
        public string value { get; set; }
        public string expire { get; set; }
        public string index { get; set; }
    }
}