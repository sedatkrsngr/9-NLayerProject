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
            //Veri bulunamadý hata filter kullanýmý
            services.AddScoped<NotFoundFilter>();

            //Gelen verileri Dto sýnýfý ile eþleþtirmek için
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
                    o.MigrationsAssembly("NLayerProject.Data");//Migration yapýlacak dosya yolunu da belirtmek gerekiyor ve ayný zamanda context baðlantýsý yapýlmýþ projeye entitycore design da eklenmeli ve set as a startup project o proje seçilmeli. Package Manager Console tarafýndan MigrationAssembly adresi hangisi verilmiþse orasý seçilip add-migration demeliyiz. MigrationAssembly de verilen adreste -> core,core.tools,core.SqlServer yüklü olmalý
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
