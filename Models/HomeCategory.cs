namespace Backend_Final_Project.Models;
public class HomeCategory:BaseEntity<int>
{
    public string Featured { get; set; }
    public string BestSeller { get; set; }
    public string Latest { get; set; }

    public virtual ICollection<HomeProduct> HomeProducts { get; set; }
}
