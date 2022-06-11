using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core;
using Api.Core.FakeActors;
using Api.Core.Jwt;
using Application;
using Application.Commands.CategoryCommands;
using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using Application.Email;
using Application.Logger;
using Application.Queries;
using Application.Queries.CategoryQueries;
using Application.Queries.ProductQueries;
using Application.Queries.LogQueries;
using DataAccess;
using Implementation.Commands.CategoryCommands;
using Implementation.Commands.UserCommands;
using Implementation.Email;
using Implementation.Logger;
using Implementation.Queries.CategoryQueries;
using Implementation.Queries.LogQueries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Implementation.Commands.ProductCommands;
using Implementation.Queries.ProductQueries;
using Application.Commands.OrderCommands;
using Implementation.Commands.OrderCommands;
using Application.Queries.OrderQueries;
using Implementation.Queries.OrderQueries;
using Application.Queries.UserQueries;
using Implementation.Queries.UserQueries;

namespace Api
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
            services.AddControllers();

            services.AddTransient<Context>();
            services.AddAutoMapper(typeof(EFGetLogsQuery).Assembly);

            ///validator
            services.AddTransient<UserLoginRequestValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient <CreateOrderValidator>();
            services.AddTransient <UpdateUserValidator>();

            ///users - login...
            services.AddTransient<UseCaseExecutor>();
            services.AddTransient<JwtManager>();
            services.AddTransient<IApplicationActor,FakeApiActor>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            ///users
            services.AddTransient<ICreateUserCommand, EFCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EFUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EFDeleteUserCommand>();
            services.AddTransient<IGetUsersQuery, EFGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EFGetUserQuery>();

            ///logs
            services.AddTransient<IGetLogsQuery, EFGetLogsQuery>();

            ///email
            services.AddTransient<IEmailSender, SmtpEmailSender>();


            //categories
            services.AddTransient<ICreateCategoryCommand, EFCreateCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EFUpdateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EFDeleteCategoryCommand>();
            services.AddTransient<IGetCategoriesQuery, EFGetCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EFGetCategoryQuery>();

            //products
            services.AddTransient<ICreateProductCommand, EFCreateProductCommand>();
            services.AddTransient<IUpdateProductCommand, EFUpdateProductCommand>();
            services.AddTransient<IDeleteProductCommand, EFDeleteProductCommand>();
            services.AddTransient<IGetProductsQuery, EFGetProductsQuery>();
            services.AddTransient<IGetProductQuery, EFGetProductQuery>();

            //orders
            services.AddTransient<ICreateOrderCommand, EFCreateOrderCommand>();
            services.AddTransient<IUpdateOrderCommand, EFUpdateOrderCommand>();
            services.AddTransient<IGetOrdersQuery, EFGetOrdersQuery>();
            services.AddTransient<IGetOrderQuery, EFGetOrderQuery>();



            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new UnauthorizedActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });



            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopy", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
