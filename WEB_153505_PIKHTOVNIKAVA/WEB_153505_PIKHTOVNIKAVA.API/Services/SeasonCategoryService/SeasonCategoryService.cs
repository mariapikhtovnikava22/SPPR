using WEB_153505_PIKHTOVNIKAVA.API.Data;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.API.Services.SeasonCategoryService;

namespace WEB_153505_PIKHTOVNIKAVA.API.Services.SeasonCategoryService;

public class SeasonCategoryService : ISeasonCategoryService
{
    readonly AppDbContext _appDbContext;
    public SeasonCategoryService(AppDbContext db)
    {
        _appDbContext = db;
    }
    public async Task<ResponseData<List<SeasonCategory>>> GetCategoryListAsync()
    {
        return new ResponseData<List<SeasonCategory>> { Data = await _appDbContext.season.ToListAsync() };
    }
}
