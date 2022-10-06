using Microsoft.EntityFrameworkCore;
using UserAndMsg.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//�M��޲z�D���x��J���O:Scaffold-DbContext "Server=���A����m;Database=��Ʈw;Trusted_Connection=True;User ID=�b��;Password=�K�X" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

builder.Services.AddDbContext<TestLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestLogDatabase")));

//�]�wCors
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        builder => {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        }
        )
    );

var app = builder.Build();

//allow static file
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();  //����Cors

app.MapControllers();

app.Run();
