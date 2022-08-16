using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
namespace Common.Repositories
{
    public interface IMemoryCacheRepository
    {
        void Set<T> (string key , T value ) where T:class;

        /// <summary>
        /// key不存在會如何?
        /// </summary>
        void Remove(string key);



        /// <returns>查不到則回傳null</returns>
        T Get<T> (string key) where T : class;

    }
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IDistributedCache _cache;
        public MemoryCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Set<T>(string key, T value) where T : class
        {
            _cache.Set(key, ObjectToByteArray(value),
                new DistributedCacheEntryOptions { }
            );
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public T Get<T>(string key) where T : class
        {
            return ByteArrayToObject<T>(_cache.Get(key));
        }



        private byte[] ObjectToByteArray(object  obj) //沒有泛型 用obj也行
        {
            //using System.Text.Json;
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private T ByteArrayToObject<T>(byte[] bytes) where T : class
        {
            return bytes is null ? null : JsonSerializer.Deserialize<T>(bytes);
        }

    }
}
