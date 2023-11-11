using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;

namespace WEB_153505_PIKHTOVNIKAVA.Services.SeasonCategoryService
{
    public class MemoryCategoryService : ISeasonCategoryService
    {
        public Task<ResponseData<List<SeasonCategory>>> GetCategoryListAsync()
        {
            var categories = new List<SeasonCategory>
            {
                new SeasonCategory {Id=1, Name="Зима",
                NormalizedName="winter"},
                new SeasonCategory {Id=2,Name="Весна", 
                NormalizedName="spring"},
                new SeasonCategory {Id=3, Name="Лето",
                NormalizedName="summer"},
                new SeasonCategory {Id=4, Name="Осень",
                NormalizedName="autumn"},
            };
            var result = new ResponseData<List<SeasonCategory>>();
            result.Data = categories;
            return Task.FromResult(result); //чтобы соответствовать сигнатуре метода и
                                            //позволить его использование в асинхронном контексте.
        }

    }
}
