using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.ViewComponents;
public class HeaderViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public HeaderViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var headerPhoneNumber = await _context.HeaderPhoneNumbers.FirstOrDefaultAsync();
        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync();
        HeaderVM headerVM = new()
        {
            HeaderPhoneNumber = headerPhoneNumber,
            HeaderInfo = headerInfo
        };
        return await Task.FromResult(View(headerVM));
    }
}
