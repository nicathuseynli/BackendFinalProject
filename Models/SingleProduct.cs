using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class SingleProduct : BaseEntity<int>
{
    public string Information { get; set; }

    public string ShippingInfo { get; set; }

    public string AboutReturnInfo { get; set; }

    public string GuaranteeInfo { get; set; }

    public int CommentCount { get; set; }

    public string UserFullname { get; set; }

    public string UserComment { get; set; }

    public string UserImage { get; set; }

    [NotMapped]
    public IFormFile UserPhoto { get; set; }

}
