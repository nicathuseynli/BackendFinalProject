namespace Backend_Final_Project.Models;
public class ContactInfo:BaseEntity<int>
{
    public string ContactDescription { get; set; }
    public string MailAddress { get; set; }
    public int PhoneNumber { get; set; }
    public string Address { get; set; }
}
