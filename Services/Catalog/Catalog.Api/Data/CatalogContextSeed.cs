using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection )
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if( !existProduct )
            {
                productCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>()
            {
            new Product()
            {
                Id= "faa7b621-cfb5-45a0-9bd4-dc09c4a51ccc",
                Category = "Computer",
                Description = "this is first one",
                ImageFile = "Prod1.png",
                Name = "First Laptop",
                Price = 1254800.00m,
                Summry = " this is the first summry"
            },
                        new Product()
            {
                Id= "8fe7e25a-d5d1-4065-a636-5ac95abf87a1",
                Category = "Computer2",
                Description = "this is first two",
                ImageFile = "Prod2.png",
                Name = "second Laptop",
                Price = 1875800.00m,
                Summry = " this is the second summry"
            },

                        new Product()
            {
                Id= "660b014a-1b19-4e7e-816d-fc91e35c04d3",
                Category = "Computer3",
                Description = "this is third one",
                ImageFile = "Prod3.png",
                Name = "third Laptop",
                Price = 8759800.00m,
                Summry = " this is the third summry"
            },
              new Product()
            {
                Id= "a2d6548b-b575-47c0-9530-0e513d0b4629",
                Category = "Computer4",
                Description = "this is forth one",
                ImageFile = "Prod4.png",
                Name = "forth Laptop",
                Price = 4587000.00m,
                Summry = " this is the forth summry"
            },
              new Product()
            {
                Id= "e644a53b-1d01-4a85-8d6b-1262003e657a",
                Category = "Computer5",
                Description = "this is fifth one",
                ImageFile = "Prod5.png",
                Name = "fifth Laptop",
                Price = 5555000.00m,
                Summry = " this is the fifth summry"
            }
            };


        }
    }
}
