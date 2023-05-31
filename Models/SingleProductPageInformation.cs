namespace Backend_Final_Project.Models;
public class SingleProductPageInformation : BaseEntity<int>
{
    public string ShippingInfo { get; set; }

    public string AboutReturnInfo { get; set; }

    public string GuaranteeInfo { get; set; }
}
