using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fotoTeca.Autentication;
using fotoTeca.Models;
using fotoTeca.Models.CitiesAndDepartments;
using fotoTeca.Models.GiftCard;
using fotoTeca.Models.Platform;
using fotoTeca.Models.Product;
using fotoTeca.Models.ShippingUser;
using fotoTeca.Models.TypeClient;
using fotoTeca.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NegronWebApi.Models.Promotion;

namespace fotoTeca
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddDbContext<AplicationDbContex>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();
            services.AddIdentity<AplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<AplicationDbContex>()
              .AddDefaultTokenProviders();
            services.AddScoped<UserDAL>();
            services.AddScoped<StoreDAL>();
            services.AddScoped<ShippingDAL>();
            services.AddScoped<CitiesAndDepartmentsDAL>();
            services.AddScoped<TypeClientDAL>();
            services.AddScoped<PromotionDAL>();
            services.AddScoped<GiftCardDAL>();
            services.AddScoped<ProductDAL>();






            services.AddControllers();
            // se adiciona autenticacion 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                });
            // se adiciona Swagger para generar la documentacion  de los servicios
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Web API de Fototeca" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseCors(builder => builder.WithOrigins("https://cuadrotecanueva.azurewebsites.net", "http://localhost:4200").WithMethods("*").WithHeaders("*"));

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
