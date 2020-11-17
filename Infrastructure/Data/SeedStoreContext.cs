using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SeedStoreContext
    {
        public static async Task SeedDataAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await RetrieveDataAsync<ProductBrand>(context, "../Infrastructure/Data/SeedData/brands.json");
                await RetrieveDataAsync<ProductType>(context, "../Infrastructure/Data/SeedData/types.json");
                await RetrieveDataAsync<Product>(context, "../Infrastructure/Data/SeedData/products.json");
            }
            catch(Exception ex)
            {
                ILogger logger = loggerFactory.CreateLogger<SeedStoreContext>();
                logger.LogError(ex.Message);
            }
        }

        private static async Task RetrieveDataAsync<T>(StoreContext context, string jsonFile) where T : class
        {
            var entity = context.Set<T>();

            if (!entity.Any())
            {
                using (Stream stream = new FileStream(jsonFile, FileMode.Open))
                {
                    IEnumerable<T> data = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream);
                    await entity.AddRangeAsync(data);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
