using CustomerSupportRefundSystem_API.Data;
using CustomerSupportRefundSystem_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRefundPolicyService, RefundPolicyService>();
builder.Services.AddScoped<IRefundService, RefundService>();
builder.Services.AddScoped<IAiService, OpenAiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CORS
builder.Services.AddCors((options) =>
{
    options.AddPolicy("default", builder =>
    {
        builder.AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
        .WithExposedHeaders("*")
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

app.UseCors("default");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
