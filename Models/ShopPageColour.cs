namespace Backend_Final_Project.Models;
public class ShopPageColour : BaseEntity<int>
{
    public string Colour { get; set; }

    //navigation
    public virtual ICollection<HomeProduct> HomeProducts { get; set; }

}
