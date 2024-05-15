
using BusinessLayer.Middleware;
using BusinessLayer.Repositories;
using BusinessLayer.Services;
using DomainLayer.AcessLayer;
using DomainLayer.Data;
using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.UnitOfWork;
using LeaveManagement.AutoMapperProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Db configuration
builder.Services.AddDbContext<LeaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//autghentication ko ploicy configure haneko  authenticaton
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

//services add
builder.Services.AddScoped<ICalanerService, CalanderService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILeaveApplyService, LeaveApplyService>();
builder.Services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();

builder.Services.AddSingleton<Authentication>();

//refrencehandler
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//builder.Services.AddControllers();
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


