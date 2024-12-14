using API.Data;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using API.Contract.IRepository;
using API.Repository;
using API.Contract.IService;
using API.Service;


var builder = WebApplication.CreateBuilder(args);

var JWTSetting = builder.Configuration.GetSection("JWTSetting");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryAsset, RepositoryAsset>();
builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryPriceCatalogue, RepositoryPriceCatalogue>();
builder.Services.AddScoped<IRepositoryServices, RepositoryServices>();
builder.Services.AddScoped<IEmailExtractorService, EmailExtractorService>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRepositoryBookingDetail, RepositoryBookingDetail>();
builder.Services.AddScoped<ICarpenterService, CarpenterService>();
builder.Services.AddScoped<IRepositoryRefreshToken, RepositoryRefreshToken>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();


builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlite("Data Source=auth.db"));


builder.Services.AddIdentity<AppUser,IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(opt=> {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt=> {
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,  
            ValidateIssuerSigningKey = true,  
            ValidAudience = JWTSetting["validAudience"],  
            ValidIssuer = JWTSetting["validIssuer"],  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting.GetSection("securityKey").Value!))
        };  

});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
            Description = @"JWT Authorization Example : 'Bearer eyeleieieekeieieie",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"

        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    },
                    Scheme = "outh2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });
         

});

var app = builder.Build();
app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();

app.Run();
