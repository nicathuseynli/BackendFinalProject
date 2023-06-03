using Backend_Final_Project.Models;
using Backend_Final_Project.Utilities.Pagination;

namespace Backend_Final_Project.ViewModels
{
    public class ShopVM
    {
        public List<HomeProduct> HomeProducts { get; set; }

        public List<HomeCategory> HomeCategories { get; set; }

        public List<ShopPageColour> ShopPageColours { get; set; }

        public Banner Banner { get; set; }

        public HomeProduct HomeProduct { get; set; }

        public Paginate<HomeProduct> Paginates { get; set; }
    }
}
