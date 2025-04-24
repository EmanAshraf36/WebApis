using Microsoft.EntityFrameworkCore;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);
//Adding the services

//dependency inject the  database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
      options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement"));   
    }
);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();



