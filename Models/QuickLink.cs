namespace Backend_Final_Project.Models;
public class QuickLink : BaseEntity<int>
{
    public string Description { get; set; }

    public int TelephoneNumber { get; set; }
}
