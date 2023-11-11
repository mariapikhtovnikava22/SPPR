using Microsoft.AspNetCore.Mvc;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Services.SeasonCategoryService;

namespace WEB_153505_PIKHTOVNIKAVA.Services.ProductService
{
    public class MemoryProductService : IProductService
    {

        List<Sneaker> _sneaker;
        List<SeasonCategory> _categories;
        IConfiguration _config;
        public MemoryProductService([FromServices] IConfiguration config,
            ISeasonCategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
            .Result
            .Data;
            _config = config;
            SetupData();
        }

        private void SetupData()
        {
            _sneaker = new List<Sneaker>
            {
                new Sneaker
                {
                    Id=1,
                    Name="Converse Chuck Taylor 70 Crafted",
                    Description="Оригинальные кеды Converse Chuck Taylor 70 Crafted лимитированной коллекции",
                    SeasonCategory= _categories.Find(c => c.NormalizedName.Equals("summer"))!,
                    Price=370,
                    PhotoPath="Images/Converse Chuck Taylor 70 Crafted HI Patchwork.png",
                },
                new Sneaker
                {
                    Id=2,
                    Name="Converse Classic Low Maroon M9691C",
                    Description="Оригинальные кеды Converse Classic Low Maroon M9691C",
                    SeasonCategory= _categories.Find(c => c.NormalizedName.Equals("summer"))!,
                    Price=230,
                    PhotoPath="Images/Converse Classic Low Maroon M9691C.png",
                },
                new Sneaker
                {
                    Id=3,
                    Name="Converse CONS AS-1 Pro Egret",
                    Description="Опробуйте самую инновационную модель кед от CONS на сегодняшний день, " +
                    "представленную одним из лучших скейтбордистов мира, Алексис Саблоне — AS-1 Pro.",
 
                    SeasonCategory= _categories.Find(c => c.NormalizedName.Equals("summer"))!,
                    Price=325,
                    PhotoPath="Images/Converse CONS AS-1 Pro Egret.png",
                },
                new Sneaker
                {
                    Id=4,
                    Name="Converse Chuck Taylor All Star CX Explore HI Black",
                    Description="Оригинальные черные высокие кеды Converse Chuck Taylor All Star CX Explore High Top Black A02411C —" +
                    " это наш новый шаг к смелому стилю и комфорту.",
                    SeasonCategory= _categories.Find(c => c.NormalizedName.Equals("autumn"))!,
                    Price=370,
                    PhotoPath="Images/Converse Chuck Taylor All Star CX Explore HI Black.png",
                },
                 new Sneaker
                 {
                    Id =5,
                    Name = "Converse Malden Street Boot MID Engine Smoke A04479C",
                    Description = "Оригинальные светло-коричневые кеды Converse Chuck Taylor All Star Malden Street Boot MID Engine" +
                    " Smoke/Black A04479C",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("autumn"))!,
                    Price = 370,
                    PhotoPath = "Images/Converse Malden Street Boot MID Engine Smoke A04479C.png",
                },
                new Sneaker
                {
                    Id = 6,
                    Name = "Converse Chuck Taylor 70 AT-CX Platform HI Egret A04581C",
                    Description = "Кеды Converse A04581C Chuck 70 AT-CX в бежевом цвете — это истинное воплощение комфорта и стиля. " +
                    "Особая черта этих кед — это использование технологии AT-CX, " +
                    "которая обеспечивает улучшенную амортизацию и поддержку стопы. " +
                    "Таким образом, Вы получаете максимальный комфорт при каждом шаге и " +
                    "превосходную производительность во время активных занятий спортом.",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("autumn"))!,
                    Price = 410,
                    PhotoPath = "Images/Converse Chuck Taylor 70 AT-CX Platform HI Egret A04581C.png",
                },
                new Sneaker
                {
                    Id = 7,
                    Name = "Converse Run Star Legacy CX Platform Mono Suede",
                    Description = "Модель Converse Run Star Legacy A05283C — это ультраудобная и футуристическая платформа, которая возвращается" +
                    " в монохромном дизайне и замши премиум-класса.",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("winter"))!,
                    Price = 490,
                    PhotoPath = "Images/Converse Run Star Legacy CX Platform Mono Suede.png",
                },
                new Sneaker
                {
                    Id = 8,
                    Name = "Converse Lugged 2.0 Winter Counter Climate HI Squirmy Worm Brown A04634C",
                    Description = "Оригинальные утепленные кожаные светло коричневые высокие кеды на высокой подошве Converse Chuck Taylor All Star Lugged 2.0 Winter Counter Climate High " +
                    "Top Squirmy Worm Brown/Erget/Nomad Khaki A04634C",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("winter"))!,
                    Price = 370,
                    PhotoPath = "Images/Converse Lugged 2.0 Winter Counter Climate HI Squirmy Worm Brown A04634C.png",
                },
                new Sneaker
                {
                    Id = 9,
                    Name = "Converse Run Star Legacy CX HI Egret A00868C",
                    Description = "Последняя версия всеми любимой модели Run Star Hike, Run Star Legacy CX сочетает в себе" +
                    "смелую платформу с исключительным комфортом. ",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("winter"))!,
                    Price = 389,
                    PhotoPath = "Images/Converse Run Star Legacy CX HI Egret A00868C.png",
                },
                new Sneaker
                {
                    Id = 10,
                    Name = "Converse Classic HI Red M9621C",
                    Description = "Chuck Taylor All Star High Top — это культовые кеды, которые выделяются своим неповторимым дизайном, " +
                    "логотипом на щиколотке и культурной значимостью.",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("spring"))!,
                    Price = 319,
                    PhotoPath = "Images/Converse Classic HI Red M9621C.png",
                },
                 new Sneaker
                {
                    Id = 11,
                    Name = "Converse Chuck Taylor 70 Vintage",
                    Description = "Розовые кеды Converse Chuck Taylor 70 Vintage",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("spring"))!,
                    Price = 319,
                    PhotoPath = "Images/Converse Chuck Taylor 70 Vintage.png",
                },
                  new Sneaker
                {
                    Id = 12,
                    Name = "Converse Chuck Taylor 70 HI Midnight Navy",
                    Description = "Оригинальные синие высокие кеды в стиле 70ых Converse Chuck Taylor All Star 70 Recycled Canvas Vinage High Top Midnight Navy/Egret/Black.\r\nМодель изготовлена из переработанного хлопка.",
                    SeasonCategory = _categories.Find(c => c.NormalizedName.Equals("spring"))!,
                    Price = 319,
                    PhotoPath = "Images/Converse Chuck Taylor 70 HI Midnight Navy.png",
                },


            };
        }

        public Task<ResponseData<Sneaker>> CreateProductAsync(Sneaker product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Sneaker>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateProductAsync(int id, Sneaker product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseData<ListModel<Sneaker>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // объект ответа
            var response = new ResponseData<ListModel<Sneaker>>();
            // данный которые засунутся в объект ответа
            ListModel<Sneaker> listModel = new ListModel<Sneaker>();

            var pageSize = int.Parse(_config["ItemsPerPage"]!);

            // проверка на допуститмость номера страницы
            // 
            // иф такой хитрый, чтобы учесть плюс одну страницу, если она не полностью заполнена
            if (pageNo * pageSize - _sneaker.Count > pageSize)
            {
                throw new Exception("page number are greater then amount of pages");
            }

            // filteredAirplanes вынесен в отдельную переменную, только чтобы получить общее количество
            // элементов соответствующих данной категории (надо для корректного отображения номеров страниц)
            var filteredAirplanes = _sneaker.
                Where(d => categoryNormalizedName == null ||
                    d.SeasonCategory.NormalizedName.Equals(categoryNormalizedName)); // фильтр по категории

            listModel.Items = filteredAirplanes.Skip((pageNo - 1) * 3). // пропускаем элементы, которые не будут отображены
                Take(pageSize). // выбираем столько, сколько поместится на страницу
                ToList(); // конвертируем в список


            // округляем в большую сторону чтобы поместились все элементы
            var totalPages = Math.Ceiling((double)filteredAirplanes.Count() / (double)pageSize);
            listModel.TotalPages = (int)totalPages;
            listModel.CurrentPage = pageNo;

            // если нет элементов соответствующих данной категории
            // сообщаем об ошибке
            if (listModel.Items.Count == 0)
            {
                response.Success = false;
                response.ErrorMessage = "can't find sneakers with such season type";
            }

            // заносим данные в объект ответа
            response.Data = listModel;
            return Task.FromResult(response);
        }

    }
}
