using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data;
using NLayerProject.Data.Repositories;
using NLayerProject.Data.UnitOfWorks;
using NLayerProject.Service.Services;
using NLayerProject.Web.UsingData.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Veri bulunamad� hata filter kullan�m�
            services.AddScoped<NotFoundFilter>();

            //Gelen verileri Dto s�n�f� ile e�le�tirmek i�in
            services.AddAutoMapper(typeof(Startup));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<NLayerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConStr"), o =>
                {
                    o.MigrationsAssembly("NLayerProject.Data");//Migration yap�lacak dosya yolunu da belirtmek gerekiyor ve ayn� zamanda context ba�lant�s� yap�lm�� projeye entitycore design da eklenmeli ve set as a startup project o proje se�ilmeli. Package Manager Console taraf�ndan MigrationAssembly adresi hangisi verilmi�se oras� se�ilip add-migration demeliyiz. MigrationAssembly de verilen adreste -> core,core.tools,core.SqlServer y�kl� olmal�
                });
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Categories}/{action=Index}/{id?}");
            });
        }
    }
}
