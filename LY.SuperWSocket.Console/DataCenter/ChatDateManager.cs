using LY.SuperWSocket.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console.DataCenter
{
    public class ChatDateManager<K, T>
        where T : BaseModel
    {
        private static Dictionary<K, List<T>> Dictionary = new Dictionary<K, List<T>>();

        public static void Add(K k, T t)
        {
            if (Dictionary.ContainsKey(k))
                Dictionary[k].Add(t);
            else
                Dictionary[k] = new List<T> { t };
        }
        public static void Remove(K k, string GuId)
        {
            if (Dictionary.ContainsKey(k))
            {
                Dictionary[k] = Dictionary[k].Where(t => t.GuId != GuId).ToList();
            }
        }

        public static void SendLogin(K k, Action<T> action)
        {
            if (Dictionary.ContainsKey(k))
            {
                foreach (var item in Dictionary[k])
                {
                    action(item);
                }
            }
        }

    }
}
