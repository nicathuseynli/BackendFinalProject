using Backend_Final_Project.Models;

namespace Backend_Final_Project.ViewModels;
public class AboutPageVM
{
    public List<About> AboutPages { get; set; }
    public List<AboutTeam> AboutTeamMembers { get; set; }
    public List<Shipping> Shippings { get; set; }
    public List<AboutCompanySlider> AboutCompanySliders { get; set; }
    public AboutTeam aboutTeam { get; set; }
}
