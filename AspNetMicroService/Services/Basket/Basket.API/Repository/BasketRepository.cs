using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _DistributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _DistributedCache = distributedCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var shoppingCart = await _DistributedCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(shoppingCart))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _DistributedCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));

            return await this.GetBasket(shoppingCart.UserName);
        }


        public async Task RemoveBasket(string userName)
        {
            await _DistributedCache.RemoveAsync(userName);
        }

    }
}
