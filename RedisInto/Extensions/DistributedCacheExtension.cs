using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisInto.Extensions
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache distributedCache,
            string key,
            T value,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            //Final absolutime time after which cache will expire irresptive of anything else
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);

            //If cache is not used for 'unusedExpireTime' then remove it from cache.
            options.SlidingExpiration = unusedExpireTime;

            //serialize data to string
            var jsonData = JsonSerializer.Serialize(value);

            await distributedCache.SetStringAsync(key, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache distributedCache,
            string key)
        {
            var value = await distributedCache.GetStringAsync(key);

            if(value is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
