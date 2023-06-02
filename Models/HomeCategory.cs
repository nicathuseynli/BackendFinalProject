namespace Backend_Final_Project.Models;
public class HomeCategory : BaseEntity<int>
{
    public string Name { get; set; }

    //nav
    public virtual ICollection<HomeProduct> HomeProducts { get; set; }
}
