using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcers.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // Bellekte key'e karşılık gelen datayı ver.
        object Get(string key);
        void Add(string key, object value,int duration);
        // IsAdd() metodu cahce'de var mı ?
        bool IsAdd(string key); // Cache'de varsa, cache'den getiririz.Yoksa veri tabanından getiririz onu da cache ekleriz.
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
