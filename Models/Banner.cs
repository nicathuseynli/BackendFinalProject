namespace Backend_Final_Project.Models;
public class Banner :BaseEntity<int>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string BannerImage { get; set; }
}
