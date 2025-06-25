using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Указываем текущую директорию как корневую для статических файлов
builder.Environment.WebRootPath = Directory.GetCurrentDirectory();

var app = builder.Build();

// Настройка статических файлов
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Directory.GetCurrentDirectory()),
    RequestPath = ""
});

// Главная страница
app.MapGet("/", async context =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
    if (File.Exists(filePath))
    {
        await context.Response.SendFileAsync(filePath);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File index.html not found!");
    }
});

app.Run("http://localhost:8080");