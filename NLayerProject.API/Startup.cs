using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLayerProject.API.Extensions;
using NLayerProject.API.Filters;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data;
using NLayerProject.Data.Repositories;
using NLayerProject.Data.UnitOfWorks;
using NLayerProject.Service.Services;

namespace NLayerProject.API
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

            //services.AddControllers();//�lk hali
            services.AddControllers(o =>//Verilerin gerekli �artlar� sa�lay�p sa�lamad��� kontrol� her method veya controllerda kullanmak yerine her yerde validation kontrol� sa�lar�z. Birden fazla filter ekleyebiliriz bu �ekilde. Global d�zeyde vaildation kontrol�
            {
                o.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options =>//Modeldeki hatalar� normalde api kendisi bir formatta d�n�yordu ama art�k biz filter ile kendimiz d�n�cez buna izin veriyoruz
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NLayerProject.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLayerProject.API v1"));
            }

            app.CustomException();//Extensions olarak Global hata y�netimi ekledik
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
