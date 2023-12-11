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

        public static bool ShirtExists(int id)
        {
            return shirtList.Any(x => x.ShirtID == id);
        }

        public static Shirt? GetShirtsById(int id)
        {
            return shirtList.FirstOrDefault(x => x.ShirtID == id);
        }
    }
}
