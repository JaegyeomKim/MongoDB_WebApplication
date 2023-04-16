using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_WebApplication.Models;
using MongoDB_WebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ProjectStoreDatabaseSetting>(
                builder.Configuration.GetSection(nameof(ProjectStoreDatabaseSetting)));
builder.Services.AddSingleton<IProjectStoreDatabaseSetting>(sp =>
    sp.GetRequiredService<IOptions<ProjectStoreDatabaseSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("ProjectStoreDatabaseSetting:ConnectionString")));

builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
