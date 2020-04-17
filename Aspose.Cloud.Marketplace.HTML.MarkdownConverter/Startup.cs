using Aspose.Cloud.Marketplace.HTML.Converter.Models;
using Aspose.Cloud.Marketplace.HTML.Converter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Aspose.Cloud.Marketplace.HTML.Converter
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<AsposeApiSecrets>();
            }
            else
            {
                builder.AddEnvironmentVariables();
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            var asposeApiSecrets = Configuration.GetSection("AsposeCloud").Get<AsposeApiSecrets>();
            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IStorageFileApi, Aspose.Html.Cloud.Sdk.Api.StorageApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.StorageApi(asposeApiSecrets.AppSid, asposeApiSecrets.ApiKey, asposeApiSecrets.BasePath, asposeApiSecrets.BasePath));
            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IImportApi, Aspose.Html.Cloud.Sdk.Api.HtmlApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.HtmlApi(asposeApiSecrets.AppSid, asposeApiSecrets.ApiKey, asposeApiSecrets.BasePath, asposeApiSecrets.BasePath));
            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IConversionApi, Aspose.Html.Cloud.Sdk.Api.HtmlApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.HtmlApi(asposeApiSecrets.AppSid, asposeApiSecrets.ApiKey, asposeApiSecrets.BasePath, asposeApiSecrets.BasePath));
            services.AddSingleton<StatisticalService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "status",
                    pattern: "status",
                    defaults: new { controller = "Home", action = "Status" }
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}