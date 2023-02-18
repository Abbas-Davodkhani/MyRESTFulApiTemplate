using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTFulApi.Template.Models.Context;
using RESTFulApi.Template.Models.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "testXML.xml"), true);
    option.SwaggerDoc("v1" , new Microsoft.OpenApi.Models.OpenApiInfo { Version = "v1" , Title = "RESTFulApi.Template" });
    option.SwaggerDoc("v2" , new Microsoft.OpenApi.Models.OpenApiInfo { Version = "v2" , Title = "RESTFulApi.Template" });
    option.SwaggerDoc("v3" , new Microsoft.OpenApi.Models.OpenApiInfo { Version = "v3" , Title = "RESTFulApi.Template" });

    option.DocInclusionPredicate((doc, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        var version = methodInfo.DeclaringType
            .GetCustomAttributes<ApiVersionAttribute>(true)
            .SelectMany(attr => attr.Versions);

        return version.Any(v => $"v{v.ToString()}" == doc);
    });

});
string connection = @"Server=.;Database=RESTFulDB;Trusted_Connection=True;TrustServerCertificate=True;";
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connection));
builder.Services.AddScoped<ToDoRepository, ToDoRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();

// Versioning
builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        option.SwaggerEndpoint("/swagger/v3/swagger.json", "v3");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
