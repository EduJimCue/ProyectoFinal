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
    Credentials = new SessionAWSCredentials("ASIAVRUVQ3VCGIPAQZUO", "hxZOPw47NtNziajOKV22MaMKMFFuhBxC9NQNgY9U", "IQoJb3JpZ2luX2VjEPn//////////wEaCXVzLXdlc3QtMiJIMEYCIQC2YbW8KTWjfKpGfShn3jS/FxaoXkBRqO4BsX7dL2FCcgIhAJIZRcAIZ/xE0esDwHAbiekB2My21pAURriar5A2hfBBKqQCCJL//////////wEQABoMMzgxNDkxOTI4Mzg4Igwz5KmFG+/FLvd2kDgq+AGouDYiNxYWzDHmu3pXmAyjXjKS38OGkYbEzQM/oChcE58v05/PvT2rSwnRKd0Tuq4izBPHo/b2yqCjaeBCQVruK8as7S6y0sIhS9o+SznsdSUsBOLrpPvgLMjo2pY1D+j3so8SoLjXt0kFetDvys9TpCbIZ8HGR66K9pd6INCK7ufLNTIMQrgA2Yg7fleQtjksjof09WLM26BzIjHdL1XfCt7aBWIcikWySkP1oUMDmYH9ksTkSP5LCL7s/we1B4bcQAV3vanq7JdwpyQB5CaPKetj+JjwSOeZWI7enK9KN0NGFbnmhucj3N0nObj55VZfHcLdxBK8IjCkyayzBjqcAQ/gUMiIAtu8iLPrwbrmhZJpKtcPnsU6yicuUJPcCYfmQ/t1kkWqHBbPCJZ0D7Vqb3QPaXERpIeNu1z/OIbYzbW/hdGVFMibNlVs9AHXKHGI+cthakeu99YN2Lg50CYAGCt3QI+BJurZXdvVdcZOVbKD2xu34kqcxPGxg7O+R8mkAM2k/j5kw5fVmFyJnirQpQ/ZBn28/LkFXn5/sw=="),
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