using FluentValidation.AspNetCore;
using InveonECommerce.Business.Engines.Infrastructure;
using InveonECommerce.Business.Engines.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLibrary.Extensions;

namespace InveonECommerce.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string ApiCorsPolicy = "_apiCorsPolicy";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDAL(Configuration);
            services.AddServices(Configuration);
            services.AddControllers().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<IServiceBase>();
            });
            services.UseCustomValidationResponse();

            
            

            
            services.AddSwaggerDocument();
            services.AddCors(options => options.AddPolicy(ApiCorsPolicy, builder => {
                builder.WithOrigins("http://localhost:46202", "http://localhost:3000").AllowAnyOrigin();
                    //.AllowAnyMethod()
                    //.AllowAnyHeader()
                    //.AllowCredentials();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                
            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            
        }
    }
}
