var builder = WebApplication.CreateBuilder(args);
//Adding the services
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();



