using System.Reflection;
using WebAppAPI.Data;

namespace WebAppAPI.Models.Repositorys
{
    public static class ShirtRepository
    {

        private static List<Shirt> shirtList = new List<Shirt>()
        {
            new Shirt { ShirtID = 1, Brand = "myBrand", Color = "red", Gender = "men", Price = 100, Size = 10},
             new Shirt { ShirtID = 2, Brand = "myBrand", Color = "red", Gender = "men", Price = 100, Size = 10},
              new Shirt { ShirtID = 3, Brand = "myBrand", Color = "red", Gender = "men", Price = 100, Size = 10},
               new Shirt { ShirtID = 4, Brand = "myBrand", Color = "red", Gender = "men", Price = 100, Size = 10}
        };

        public static List<Shirt> GetShirts ()
        {
            return shirtList;
        }
        public static bool ShirtExists(int id)
        {
            return shirtList.Any(x => x.ShirtID == id);
        }

        public static Shirt? GetShirtsById(int id)
        {
            return shirtList.FirstOrDefault(x => x.ShirtID == id);
        }
        public static Shirt? GetShirtByProperties(string? brand, string? gender, string color, int? size)
        {
            return shirtList.FirstOrDefault(x =>
            !string.IsNullOrWhiteSpace(brand) &&
            !string.IsNullOrWhiteSpace(x.Brand) &&
            x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(gender) &&
            !string.IsNullOrWhiteSpace(x.Gender) &&
            x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(color) &&
            !string.IsNullOrWhiteSpace(x.Color) &&
            x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
            size.HasValue &&
            x.Size.HasValue &&
            size.Value == x.Size.Value);

        }
        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirtList.Max(x=> x.ShirtID);
            shirt.ShirtID = maxId + 1;
            shirtList.Add(shirt);
        }

        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirtList.First(x=> x.ShirtID == shirt.ShirtID);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
        }

        public static void DeleteShirt(int shirtId) {
            var shirt = GetShirtsById(shirtId);
            if(shirt != null)
            {
                shirtList.Remove(shirt);
            }
        }
    }
}
