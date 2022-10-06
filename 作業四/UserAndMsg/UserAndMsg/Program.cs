using Microsoft.EntityFrameworkCore;
using UserAndMsg.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//套件管理主控台輸入指令:Scaffold-DbContext "Server=伺服器位置;Database=資料庫;Trusted_Connection=True;User ID=帳號;Password=密碼" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

builder.Services.AddDbContext<TestLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestLogDatabase")));

//設定Cors
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

app.UseCors();  //執行Cors

app.MapControllers();

app.Run();
