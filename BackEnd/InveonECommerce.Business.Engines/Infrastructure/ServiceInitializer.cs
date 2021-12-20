using InveonECommerce.Business.Engines.Implementations;
using InveonECommerce.Business.Engines.Implementations.JwtServices;
using InveonECommerce.Business.Engines.Interfaces;
using InveonECommerce.Business.Engines.Interfaces.JwtInterfaces;
using InveonECommerce.Data.DAL;
using InveonECommerce.Data.DAL.UnitOfWork;
using InveonECommerce.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Infrastructure
{
    public static class ServiceInitializer
    {
        #region Adding DBContext and UnitofWork to service
        public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyEcommerce");
            services.AddDbContext<EFDBContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }

        #endregion


        #region Service Dependency Injection
        public static void AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            
            //Options pattern
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
           
            
            //Identity 
            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<EFDBContext>();

            //Token & Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Add services that inherited from IServiceBase with Scoped lifetime 
            var baseType = typeof(IServiceBase);
            var assembly = baseType.Assembly;
            var allTypes = assembly.GetTypes();
            var iFaces = allTypes.Where(q => baseType.IsAssignableFrom(q) && q.IsInterface && q != baseType);
            foreach (var iFace in iFaces)
            {
                var implementedClass = allTypes.Where(q => q.IsClass && iFace.IsAssignableFrom(q)).FirstOrDefault();
                if (implementedClass != null)
                {
                    services.AddScoped(iFace, implementedClass);
                }
                else
                {
                    throw new System.Exception("There is no implementation class for " + iFace.Name);
                }

            }
        }

        #endregion

        

    }
}
