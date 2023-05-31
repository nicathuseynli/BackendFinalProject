using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final_Project.Models;
public class SingleProductReview : BaseEntity<int>
{
    public int CommentCount { get; set; }

    public string UserFullname { get; set; }

    public string UserComment { get; set; }

    public string UserImage { get; set; }
    [NotMapped]
    public IFormFile UserPhoto { get; set; }
}
