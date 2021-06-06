using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Music.Brainz.Common.Domain.MusicBrainz;
using Music.Brainz.Common.Settings;
using Music.Brainz.CQRS;
using Music.Brainz.CQRS.Artist.Queries.GetArtist;
using Music.Brainz.Infrastructure;
using Music.Brainz.Infrastructure.Services.MusicBrainz;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Music.Brainz.API
{
    public class Startup
    {
        private const string API_NAME = "Music Brainz API";

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add CORS policy
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add MediatR
            services.AddMediatR(typeof(LookUpArtistQuery).GetTypeInfo().Assembly);

            // Add AppSettings
            services.Configure<AppSettings>(Configuration);

            // Add Services
            services.AddTransient<IApiResponseHandler, ApiResponseHandler>();
            services.AddTransient<IErrorResponseFactory, ErrorResponseFactory>();

            // Add HTTP Services
            services.AddHttpClient();
            services.AddHttpClient<IArtistService, ArtistService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = API_NAME, Version = "V1" });
                c.EnableAnnotations();
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            // Enable CORS
            app.UseCors("SiteCorsPolicy");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add Health Check
            app.UseRouter(a => a.MapGet("healthcheck", context =>
            {
                context.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store";
                context.Response.Headers[HeaderNames.Pragma] = "no-cache";
                return context.Response.WriteAsync($"Success from ({env.EnvironmentName})");
            }));

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", API_NAME); });
        }
    }
}
