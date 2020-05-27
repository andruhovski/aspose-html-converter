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
        internal const string AppName = "aspose-html-converter-app";
        internal const string AsposeClientHeaderName = "x-aspose-client";
        internal const string AsposeClientVersionHeaderName = "x-aspose-client-version";
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
            var config = new Aspose.Html.Cloud.Sdk.Client.Configuration()
            {
                AppSid = asposeApiSecrets.AppSid,
                AppKey = asposeApiSecrets.ApiKey,
                ApiBaseUrl = asposeApiSecrets.BasePath,
                AuthUrl = asposeApiSecrets.BasePath,
                ApiVersion = "3.0"
            };
            config.AddDefaultHeader(AsposeClientHeaderName, AppName);
            var version = GetType().Assembly.GetName().Version;
            config.AddDefaultHeader(AsposeClientVersionHeaderName, $"{version.Major}.{version.Minor}");

            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IStorageFileApi, Aspose.Html.Cloud.Sdk.Api.StorageApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.StorageApi(config));
            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IImportApi, Aspose.Html.Cloud.Sdk.Api.HtmlApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.HtmlApi(config));
            services.AddScoped<Aspose.Html.Cloud.Sdk.Api.Interfaces.IConversionApi, Aspose.Html.Cloud.Sdk.Api.HtmlApi>(s =>
                new Aspose.Html.Cloud.Sdk.Api.HtmlApi(config));
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