namespace Backend_Final_Project.Utilities.Pagination;
public class Paginate<T>
{
    public List<T> HomeProducts { get; set; }

    public int CuurentPage { get; set; }

    public int TotalPage { get; set; }

    public Paginate(List<T> homeProducts, int currentPage, int totalPage)
    {
        HomeProducts = homeProducts;
        CuurentPage = currentPage;
        TotalPage = totalPage;
    }
}
