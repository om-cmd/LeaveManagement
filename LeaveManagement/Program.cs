
using BusinessLayer.CustomException_handler;
using BusinessLayer.Extensions;
using BusinessLayer.IService;
using BusinessLayer.Middleware;
using BusinessLayer.RepoService;
using BusinessLayer.SwaggerService;
using DomainLayer.DBSeeding;
using LeaveManagement.AutoMapperProfile;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);


// Db configuration
builder.Services.AddCustomDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));


//autghentication ko ploicy configure haneko  authenticaton
builder.Services.AddCustomAuthentication(builder.Configuration);

//iservice collection yo chai service use garna jastei authorize wala 
builder.Services.AddCustomSwagger();

//repositories add
builder.Services.Repository();

//services add
builder.Services.AddServices();

builder.Services.AddSingleton<Authentication>();

//logging jun global exception handling ma use va xa
builder.Logging.AddConsole().AddDebug();

//refrencehandler
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddSwaggerGen();
//auto mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));
//iunit of work
builder.Services.AddUnitOfWork();
//dbSeed Initializer
builder.Services.AddDbInitializer();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.yesma customSwagger chai use garne swagger ratesko ui lai configure haneko build huda 
if (app.Environment.IsDevelopment())
{
    app.DbSeed();
    app.UseCustomSwagger();

}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
//global exception handler 
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();


