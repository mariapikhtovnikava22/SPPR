using WEB_153505_PIKHTOVNIKAVA.API.Data;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.API.Services.ProductService;

namespace WEB_153505_PIKHTOVNIKAVA.API.Services.ProductService;

public class ProductService : IProductService
{

    AppDbContext _context;
    private readonly int _maxPageSize;
    private readonly string _appUrl;


    public ProductService(AppDbContext context, ConfigurationManager configurationManager, ConfigurationService configurationService)
    {
        _context = context;

        // не выльется ли такой подход к получению appUrl и MaxPageSize 
        // в более сложное тестирование и в нагромождение кода
        _appUrl = configurationService.Configuration.GetValue<string>("applicationUrl", "someDefaultValue")!;
        _maxPageSize = Convert.ToInt32(configurationManager["MaxPageSize"]);

    }

    public async Task<ResponseData<Sneaker>> CreateProductAsync(Sneaker product)
    {
        // получаем существующую категорию
        // надо, потому что без этого блока, при передаче id категории в теле запроса
        // бросало исключение
        product.SeasonCategory = _context.season.Where(c => c.NormalizedName == product.SeasonCategory.NormalizedName).FirstOrDefault()!;

        if (product.SeasonCategory == null)
            throw new Exception("невозможно найти категорию при добвалении товара");


        await _context.sneakers.AddAsync(product);

        _context.SaveChanges();

        return new ResponseData<Sneaker> { Data = product };
    }

    public async Task DeleteProductAsync(int id)
    {
        var sneaker = await _context.sneakers.FindAsync(id);

        if (sneaker != null)
            _context.sneakers.Remove(sneaker);

        _context.SaveChanges();
    }

    public async Task<ResponseData<Sneaker>> GetProductByIdAsync(int id)
    {
        var query = _context.sneakers.AsQueryable();


        var data = await query.Where(p => p.Id == id).Include(p => p.SeasonCategory).FirstOrDefaultAsync();
        var response = new ResponseData<Sneaker>();

        if (data != null)
            response.Data = data;
        else
        {
            response.ErrorMessage = "can't find such sneakers";
            response.Success = false;
        }

        return response;
    }

    public async Task<ResponseData<ListModel<Sneaker>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        // объект ответа
        var response = new ResponseData<ListModel<Sneaker>>();
        // данный которые засунутся в объект ответа
        ListModel<Sneaker> listModel = new ListModel<Sneaker>();

        var query = _context.sneakers.AsQueryable();
        // запрос для получения самолетов по категориям
        query = query
         .Where(d => categoryNormalizedName == null
         || d.SeasonCategory.NormalizedName.Equals(categoryNormalizedName)).Include(p => p.SeasonCategory);
        // количество элементов в списке
        var count = query.Count();

        if (count == 0)
        {
            response.ErrorMessage = "no sneakers founded";
            response.Success = false;
            response.Data = listModel;
            return response;
        }
        // проверка на допуститмость номера страницы
        // 
        // иф такой хитрый, чтобы учесть плюс одну страницу, если она не полностью заполнена
        if (pageNo * pageSize - count > pageSize)
        {
            throw new Exception("page number are greater then amount of pages");
        }

        listModel.Items = await query.Skip((pageNo - 1) * 3). // пропускаем элементы, которые не будут отображены
            Take(pageSize). // выбираем столько, сколько поместится на страницу
            ToListAsync(); // конвертируем в список


        // округляем в большую сторону чтобы поместились все элементы
        listModel.TotalPages = (int)Math.Ceiling((double)count / (double)pageSize);
        listModel.CurrentPage = pageNo;

        // если нет самолетов соответствующих данной категории
        // сообщаем об ошибке
        if (listModel.Items.Count == 0)
        {
            response.Success = false;
            response.ErrorMessage = "can't find sneakerss with such engine type";
        }

        // заносим данные в объект ответа
        response.Data = listModel;
        return response;

    }

    public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        // ищем самолет по id
        var responseData = await GetProductByIdAsync(id);
        Sneaker sneakers;

        if (!responseData.Success)
        {
            return new ResponseData<string>
            { Success = false, ErrorMessage = responseData.ErrorMessage };

        }
        else
            sneakers = responseData.Data;


        var photoPath = Path.Combine(_appUrl, "wwwroot", "Imgaes", formFile.FileName);

        sneakers.PhotoPath = photoPath;
        using (Stream fstream = new FileStream(photoPath, FileMode.Create))
        {
            await formFile.CopyToAsync(fstream);
        }

        _context.SaveChanges();
        return new ResponseData<string> { Data = photoPath };
    }

    // в методе update может возникнуть исключение при удалении
    // объекта в момент его изменения. Где его обрабатывать?
    public async Task UpdateProductAsync(int id, Sneaker product)
    {
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DbUpdateConcurrencyException("Entity not found", e);
        }
    }   
}
