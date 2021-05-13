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

            //services.AddControllers();//Ýlk hali
            services.AddControllers(o =>//Verilerin gerekli þartlarý saðlayýp saðlamadýðý kontrolü her method veya controllerda kullanmak yerine her yerde validation kontrolü saðlarýz. Birden fazla filter ekleyebiliriz bu þekilde. Global düzeyde vaildation kontrolü
            {
                o.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options =>//Modeldeki hatalarý normalde api kendisi bir formatta dönüyordu ama artýk biz filter ile kendimiz dönücez buna izin veriyoruz
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

            app.CustomException();//Extensions olarak Global hata yönetimi ekledik
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
