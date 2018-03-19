using System.Collections.Generic;

namespace DA.Business.Utiles
{
    class DictionaryEngine
    {
        public static void Add(Dictionary<string, object> dictionary, string Key, object value)
        {
            lock (dictionary)
            {
                if (!dictionary.ContainsKey(Key))
                    dictionary.Add(Key, value);
            }
        }

        public static void Remove(Dictionary<string, object> dictionary, string Key)
        {
            dictionary.Remove(Key);
        }
    }
}
