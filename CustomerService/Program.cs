var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
