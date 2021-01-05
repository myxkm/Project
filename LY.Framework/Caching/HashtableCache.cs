using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Framework.Caching
{
    public class HashtableCache:ICache
    {
        private static IEnumerable<KeyValuePair<string, object>> HTCache = new List<KeyValuePair<string, object>>();

        public T Get<T>(string key)
        {
            return (T)HTCache.Where(t => t.Key == key);
        }

        public void Add(string key, object data, int cacheTime = 30)
        {
           
        }

        public bool Contains(string key)
        {
             return HTCache.Contains(HTCache.FirstOrDefault(t => t.Key == key));
        }

        public void Remove(string key)
        {
           
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public object this[string key]
        {
            get
            {
               return HTCache.Where(t=>t.Key==key);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get { return HTCache.Count(); }
        }
    }
}
