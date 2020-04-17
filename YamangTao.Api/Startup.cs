using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using YamangTao.Data;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using YamangTao.Model.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YamangTao.Core.Repository;
using YamangTao.Data.Repositories;
using YamangTao.Api.Helpers;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace YamangTao.Api
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
            
            
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("MySQLConnection")));
            // services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("MySQLConnection"), x => x.MigrationsAssembly("YamangTao.Data")));
            // services.AddDbContext<DataContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"), x => x.MigrationsAssembly("YamangTao.Data")));
            services.AddControllers();

            //Swagger Options from SwashBuckle.AspNetCore package
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "YamangTao", Version = "v1" });
            });
            
            IdentityBuilder builder = services.AddIdentityCore<User>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            }); 
            // Create a new instance of the IdentityBuilder for adding EF services in Identity
            // IdentityBuilder builder = services.AddIdentityCore<User>();
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            // builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            // services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<DataContext>()
            //     .AddRoles<Role>()
            //     .AddRoleValidator<RoleValidator<Role>>()
            //     .AddRoleManager<RoleManager<Role>>()
            //     .AddSignInManager<SignInManager<User>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                                .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                                ValidateIssuer = false,
                                                ValidateAudience = false
                        };
                    });
            
              //  auth1 - Add services for to set the aunthentication scheme for the project
            
            // Policy-Based Authorization
            services.AddAuthorization(opt => {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireSupervisorRole", policy => policy.RequireRole("Admin" ,"Supervisor"));
                opt.AddPolicy("RequireDirectorRole", policy => policy.RequireRole("Admin", "Director"));
                opt.AddPolicy("RequireVPRole", policy => policy.RequireRole("Admin", "VP"));
            });
            services.AddCors();
            services.AddMvc();
            // services.AddTransient<SeedUsers>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            
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
                // Global Exception Handler
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });

            
            //Seed Users Run once
            
            // seedUsers.SeedUsersAndRoles("/YamangTao.Data/Seeders/json/UserSeedData.json");

            
            //App swagger configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YamangTao V1");
            });

            
        }
    }
}
