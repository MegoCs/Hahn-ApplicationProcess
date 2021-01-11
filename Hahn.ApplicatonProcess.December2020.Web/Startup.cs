using FluentValidation;
using FluentValidation.AspNetCore;

using Hahn.ApplicatonProcess.December2020.Data.Contracts;
using Hahn.ApplicatonProcess.December2020.Data.Infrastructure;
using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;

namespace Hahn.ApplicatonProcess.December2020.Web
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

            services.AddControllers()
                    .AddFluentValidation();
            services.AddCors(options =>
            {
                options.AddPolicy("all",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });
            services.AddTransient<IValidator<Applicant>, ApplicantValidator>();
            services.AddScoped<IApplicantsRepository, ApplicantsRepository>();
            services.AddDbContext<ApplicantsDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "HahnInMemory"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.December2020.Web", Version = "v1" });
                c.AddFluentValidationRules();
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                        .AddFilter("Microsoft", LogLevel.Information)
                        .AddFilter("System", LogLevel.Error);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("all");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
