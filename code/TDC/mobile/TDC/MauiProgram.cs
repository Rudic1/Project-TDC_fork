using Microsoft.Extensions.Logging;
using TDC.Services;             // <-- Hinzufügen
using TDC.IService;          // <-- Hinzufügen
using TDC.Views.Login;

namespace TDC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUISystemIcons");
                    fonts.AddFont("NicoMoji-Regular.ttf", "Title");
                    fonts.AddFont("Montserrat-Regular.ttf", "Text");
                    fonts.AddFont("Montserrat-Bold.ttf", "Text-Bold");
                });

            #if DEBUG
            builder.Logging.AddDebug();
            #endif

            AddServices(builder.Services);
            AddPages(builder.Services);

            return builder.Build();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IListService, ListService>();
            services.AddTransient<IListItemService, ListItemService>();
            services.AddTransient<ListView>();
        }

        private static void AddPages(IServiceCollection services) {
            services.AddTransient<LoginPage>();
        }
    }
}