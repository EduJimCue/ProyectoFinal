using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Amazon.S3;
using Amazon.Runtime;
using Amazon.Extensions.NETCore.Setup;
using TiendaKarate.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddAWSService<IAmazonS3>(new AWSOptions
{
    Credentials = new SessionAWSCredentials("ASIAVRUVQ3VCNI2CMYWY", "Q5HB/gfs079LNBu743TDIwX2fdMD+kqJq2M74kyD", "IQoJb3JpZ2luX2VjEOn//////////wEaCXVzLXdlc3QtMiJHMEUCIQC/YYw2FxvWo6Dqek78dJvyMUmX+If5s+V3Uc+PGZV9YQIgfGeMNHHb9Tj8Mi/viWm5qQ18K5xst7eQWcWCeYNeW+QqmwIIYhAAGgwzODE0OTE5MjgzODgiDBJF1LfREVWyu3Lwkyr4AVZ4VhAfBqmIOLfs0woRzPcVO9sNDCA5fbInniq0MtkUY7KRsEmAzCIRnS78OSTqfr4EGH4BxI4IX2CtxLcc8oetN9+ClccfBZsHWLYMoFXkbXDxTXl2KiTC8MGFXNodiz5D3YYmJoW+sKx0rVHhqJaDZ9HTBRmvLDTm884dO6BvDuV+4cY1bCZuPRSDbXE5kdZN3k0mmg/zAhPHlnaAj5Eg3Rd6a22sl7AAojkPfsdakAPOI70WWtOXB+QEPeEESWbCwD+3izQUJ0GGOg06AdSA02ttUvrWP+SDcIhF7vR6vWf9dMGeiC+gVioJWJEkqBqUbAkeEuUrMPe7uLIGOp0BJUzuG7N+hNt6tY7MO+4OsD+BL84B0lguP4lIfW2c93ZiCSVpAighy1Kt1Gt3SzbxvSyOss7uYkiUbZYdwuEVXZgrIGreBSAGmIOHjw9MR7Ltf67Czutfhithf8FzWR/lg0AlV8WaPS7+F8kvGfvK7Gmhnc0a34o/Kjx2qu2O/Q/llE3QM9o4yO6h/s5paIhTcCCZ86SQ6drDlMZZdw=="),
    Region = Amazon.RegionEndpoint.USEast1
});

var app = builder.Build();

// Apply any pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();