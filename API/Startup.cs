using AutoMapper;
using Business.Abstract;
using Business.Services.Auth;
using Business.Services.BitcoinDetail;
using Business.Services.BitcoinWebDetail;
using Business.Utilities.Hashing;
using Business.Utilities.Hashing.Abstract;
using Business.Utilities.Mapping.AutoMapper;
using Business.Utilities.Security;
using Business.Utilities.Security.Abstract;
using Business.Utilities.Security.Helpers;
using Business.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper.Repositories;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
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

            services.AddCors(options =>
                options.AddPolicy("myclients", builder =>
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader())
                );

            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = tokenOptions.Audience,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IBitcoinDetailRepository, BitcoinDetailRepository>();
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddSingleton<IHashingHelper, HashingHelper>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IBitcoinDetailService, BitcoinDetailService>();
            services.AddSingleton<IHostedService, BitcoinWebDetailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("myclients");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
