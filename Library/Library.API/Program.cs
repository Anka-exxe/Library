using Library.API.Extensions;
using Library.API.Middlewares;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidators();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPolicies();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddServices();
builder.Services.AddControllers();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
//    try
//    {
//        dbContext.Database.Migrate();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
//    }
//}

app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();