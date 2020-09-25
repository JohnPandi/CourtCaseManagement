using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourtCaseManagement.Api
{
    public class Startup<C> where C : class, IStartupCustom, new()
    {
        public IConfiguration Configuration { get; }

        public C Config { get; set; } 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Config = new C();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Config.AddSwaggerConfigure(services);
            Config.AddDatabaseConfigure(services);
            Config.AddInfrastructureClassDependencyInject(services);
            Config.AddApplicationCoreClassDependencyInject(services);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Court Case Management V1");
                c.RoutePrefix = "swagger";
            });
        }
    }
}