using Backend_Final_Project.Models;

namespace Backend_Final_Project.ViewModels
{
    public class ShopVM
    {
        public List<HomeProduct> HomeProducts { get; set; }

        public List<HomeCategory> HomeCategories { get; set; }

        public List<ShopPageColour> ShopPageColours { get; set; }

        public Banner Banner { get; set; }

        public HomeProduct HomeProduct { get; set; }

    }
}
