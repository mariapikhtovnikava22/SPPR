using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.API.Data
{
    public class DbInitialazer
    {

        public static async Task SeedData(WebApplication app)
        {
            // Получение контекста БД
            using var scope = app.Services.CreateScope();

            var context =
            scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнение миграций
            //context.Database.e
            //await context.Database.MigrateAsync();  //не работает!!!

            await context.Database.EnsureCreatedAsync();

            var appUrl = app.Configuration["AppUrl"];

            //если категории в бд пусты, создаем их
            if (!context.season.Any())
            {
                context.season.AddRange
                    (
                        new SeasonCategory { Name = "Зима", NormalizedName = "winter" },
                        new SeasonCategory { Name = "Весна", NormalizedName = "spring" },
                        new SeasonCategory { Name = "Лето", NormalizedName = "summer" },
                        new SeasonCategory { Name = "Осень", NormalizedName = "autumn" }
                    );
            }

            context.SaveChanges();

            //если продукты в бд пусты, создаем их
            if (context.sneakers.Count() == 0)
            {
                context.sneakers.AddRange
                    (

                    new Sneaker
                    { 
                        Name = "Converse Chuck Taylor 70 Crafted",
                        Description = "Оригинальные кеды Converse Chuck Taylor 70 Crafted лимитированной коллекции",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("summer")).First()!,
       
                        Price = 370,
                        PhotoPath = "Images/Converse Chuck Taylor 70 Crafted HI Patchwork.png",
                    },
                    new Sneaker
                    {
                        Name = "Converse Classic Low Maroon M9691C",
                        Description = "Оригинальные кеды Converse Classic Low Maroon M9691C",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("summer")).First()!,
                        Price = 230,
                        PhotoPath = "Images/Converse Classic Low Maroon M9691C.png",
                    },
                    new Sneaker
                    {
                        
                        Name = "Converse CONS AS-1 Pro Egret",
                        Description = "Опробуйте самую инновационную модель кед от CONS на сегодняшний день, " +
                        "представленную одним из лучших скейтбордистов мира, Алексис Саблоне — AS-1 Pro.",

                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("summer")).First()!,
                        Price = 325,
                        PhotoPath = "Images/Converse CONS AS-1 Pro Egret.png",
                    },
                    new Sneaker
                    {
                       
                        Name = "Converse Chuck Taylor All Star CX Explore HI Black",
                        Description = "Оригинальные черные высокие кеды Converse Chuck Taylor All Star CX Explore High Top Black A02411C —" +
                        " это наш новый шаг к смелому стилю и комфорту.",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("autumn")).First()!,
                        Price = 370,
                        PhotoPath = "Images/Converse Chuck Taylor All Star CX Explore HI Black.png",
                    },
                     new Sneaker
                     {
                         
                         Name = "Converse Malden Street Boot MID Engine Smoke A04479C",
                         Description = "Оригинальные светло-коричневые кеды Converse Chuck Taylor All Star Malden Street Boot MID Engine" +
                        " Smoke/Black A04479C",
                         SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("autumn")).First()!,
                         Price = 370,
                         PhotoPath = "Images/Converse Malden Street Boot MID Engine Smoke A04479C.png",
                     },
                    new Sneaker
                    {
                       
                        Name = "Converse Chuck Taylor 70 AT-CX Platform HI Egret A04581C",
                        Description = "Кеды Converse A04581C Chuck 70 AT-CX в бежевом цвете — это истинное воплощение комфорта и стиля. " +
                        "Особая черта этих кед — это использование технологии AT-CX, " +
                        "которая обеспечивает улучшенную амортизацию и поддержку стопы. " +
                        "Таким образом, Вы получаете максимальный комфорт при каждом шаге и " +
                        "превосходную производительность во время активных занятий спортом.",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("autumn")).First()!,
                        Price = 410,
                        PhotoPath = "Images/Converse Chuck Taylor 70 AT-CX Platform HI Egret A04581C.png",
                    },
                    new Sneaker
                    {
                       
                        Name = "Converse Run Star Legacy CX Platform Mono Suede",
                        Description = "Модель Converse Run Star Legacy A05283C — это ультраудобная и футуристическая платформа, которая возвращается" +
                        " в монохромном дизайне и замши премиум-класса.",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("winter")).First()!,
                        Price = 490,
                        PhotoPath = "Images/Converse Run Star Legacy CX Platform Mono Suede.png",
                    },
                    new Sneaker
                    {
                       
                        Name = "Converse Lugged 2.0 Winter Counter Climate HI Squirmy Worm Brown A04634C",
                        Description = "Оригинальные утепленные кожаные светло коричневые высокие кеды на высокой подошве Converse Chuck Taylor All Star Lugged 2.0 Winter Counter Climate High " +
                        "Top Squirmy Worm Brown/Erget/Nomad Khaki A04634C",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("winter")).First()!,
                        Price = 370,
                        PhotoPath = "Images/Converse Lugged 2.0 Winter Counter Climate HI Squirmy Worm Brown A04634C.png",
                    },
                    new Sneaker
                    {
                        
                        Name = "Converse Run Star Legacy CX HI Egret A00868C",
                        Description = "Последняя версия всеми любимой модели Run Star Hike, Run Star Legacy CX сочетает в себе" +
                        "смелую платформу с исключительным комфортом. ",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("winter")).First()!,
                        Price = 389,
                        PhotoPath = "Images/Converse Run Star Legacy CX HI Egret A00868C.png",
                    },
                    new Sneaker
                    {
                        
                        Name = "Converse Classic HI Red M9621C",
                        Description = "Chuck Taylor All Star High Top — это культовые кеды, которые выделяются своим неповторимым дизайном, " +
                        "логотипом на щиколотке и культурной значимостью.",
                        SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("spring")).First()!,
                        Price = 319,
                        PhotoPath = "Images/Converse Classic HI Red M9621C.png",
                    },
                     new Sneaker
                     {
                        
                         Name = "Converse Chuck Taylor 70 Vintage",
                         Description = "Розовые кеды Converse Chuck Taylor 70 Vintage",
                         SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("spring")).First()!,
                         Price = 319,
                         PhotoPath = "Images/Converse Chuck Taylor 70 Vintage.png",
                     },
                      new Sneaker
                      {
                         
                          Name = "Converse Chuck Taylor 70 HI Midnight Navy",
                          Description = "Оригинальные синие высокие кеды в стиле 70ых Converse Chuck Taylor All Star 70 Recycled Canvas Vinage High Top Midnight Navy/Egret/Black.\r\nМодель изготовлена из переработанного хлопка.",
                          SeasonCategory = context.season.Where(c => c.NormalizedName.Equals("spring")).First()!,
                          Price = 319,
                          PhotoPath = "Images/Converse Chuck Taylor 70 HI Midnight Navy.png",
                      }
                    );
            }
            context.SaveChanges();

        }
    }
}
