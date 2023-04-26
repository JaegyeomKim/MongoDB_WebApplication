using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_WebApplication.Models;
using MongoDB_WebApplication.Services;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://www.contoso.com");
                      });
});

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
