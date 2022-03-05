using Microsoft.EntityFrameworkCore;
using SmartMeterAPI.Data;
using SmartMeterAPI.Helpers;
using SmartMeterAPI.ServiceLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin();
        });
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddTransient<IAccountUploadProcessor, AccountUploadProcessor>();
builder.Services.AddTransient<IReadingUploadProcessor, ReadingUploadProcessor>();
builder.Services.AddTransient<ISaveFile,SaveFile>();
var connectionstring = builder.Configuration.GetConnectionString("AppDB");
builder.Services.AddDbContext<SmartMeterContext>(x => x.UseSqlServer(connectionstring));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
