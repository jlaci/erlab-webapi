using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElosztottLabor.Data;
using ElosztottLabor.Interfaces;
using ElosztottLabor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HEM_REST_Server
{
    public class Startup
    {
        readonly string HemAllowSpecificOrigins = "_HemAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB Context
            services.AddDbContext<HEMDbContext>(opt => opt.UseInMemoryDatabase("HEMDb"));

            // Service
            services.AddScoped<IQuestionFormService, QuestionFormService>();

            // Configure the JSON serializer to convert enum values as string
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            // Disable CORS, so we can test our application from Swagger editor online
            services.AddCors(options =>
            {
                options.AddPolicy(HemAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(HemAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}
