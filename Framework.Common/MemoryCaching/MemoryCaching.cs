using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common
{
    public class MemoryCaching :ICaching
    {
        private readonly IMemoryCache _cache;
        public MemoryCaching(IMemoryCache memoryCache)
        {
            _cache= memoryCache;
        }
        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue, int timeSpan)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(timeSpan * 60));
        }
    }
}
