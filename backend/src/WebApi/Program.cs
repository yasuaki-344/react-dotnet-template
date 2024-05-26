using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Mapper;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;
    // Database setting
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("DbContext"));
    });

    // Add services to the container.
    builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
    builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

    // Auto mapping setting
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApiVersioning(setup =>
    {
        setup.DefaultApiVersion = new ApiVersion(1, 0);
        setup.AssumeDefaultVersionWhenUnspecified = true;
        setup.ReportApiVersions = true;
    });
    builder.Services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'V";
        setup.SubstituteApiVersionInUrl = true;
    });
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Sample API",
            Description = "This is just sample",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        });
        options.CustomSchemaIds(x =>
        {
            var attr = x.GetCustomAttribute<DisplayNameAttribute>();
            return (attr is not null) ? attr.DisplayName : x.Name;
        });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "v1");
            options.RoutePrefix = string.Empty;
        });
    }

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    // Database initialization process
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        DbInitializer.Seed(services);
    }
}

app.Run();
