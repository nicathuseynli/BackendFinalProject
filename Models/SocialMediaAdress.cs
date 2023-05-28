namespace Backend_Final_Project.Models;
public class SocialMediaAdress : BaseEntity<int>
{
    public string FacebookLink { get; set; }
    public string TwitterLink { get; set; }
    public string PinterestLink { get; set; }
    public string DribbbleLink { get; set; }

}
