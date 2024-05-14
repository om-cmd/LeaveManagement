
using BusinessLayer.Middleware;
using BusinessLayer.Repositories;
using DomainLayer.AcessLayer;
using DomainLayer.Data;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.UnitOfWork;
using LeaveManagement.AutoMapperProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Db configuration
builder.Services.AddDbContext<LeaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//autghentication ko ploicy configure haneko 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))

    };
});
//repositories add
builder.Services.AddScoped<ICalanderRepo, CalanderRepository>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeReposotory>();
builder.Services.AddScoped<ILeaveApplyRepo, LeaveApplyRepository>();
builder.Services.AddScoped<ILeaveBalanceRepo, LeaveBalanceRepository>();
builder.Services.AddScoped<ILeaveTypeRepo, LeaveTypeRepository>();
builder.Services.AddScoped<ILoginRepo, LoginRepository>();
builder.Services.AddScoped<IRegisterRepo, RegisterRepository>();
builder.Services.AddSingleton<Authentication>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
//auto mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


