namespace Backend_Final_Project.Models;
public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Info { get; set; }
    public decimal Rating { get; set; }
}
