using Microsoft.Extensions.Logging;
using TodoApp.Services;


namespace TodoApp
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
                });

            builder.Services.AddSingleton<DataService>();
            builder.Services.AddSingleton<TodoService>();
            builder.Services.AddSingleton<StatusService>();
            builder.Services.AddSingleton<TagService>();
            builder.Services.AddSingleton<TodoTagService>();



            builder.Services.AddTransient<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
