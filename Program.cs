using EjadaTraineesManagementSystem.Data;
using EjadaTraineesManagementSystem.Filters;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EjadaTraineesManagementSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<LogActionFilter>();
        });

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        builder.Services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(type);
            });

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("ar-SA")
            };

            options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        var supportedCultures = new[] { "en-US", "ar-SA" };
        var localizationOption = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOption);

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Trainees}/{action=Trainees}/{id?}");

        app.Run();
    }
}
