using BAL.Dto;
using BAL.Mapper;
using BAL.Services.Implementation;
using BAL.Services.Interfaces;
using BAL.Validator;
using DAL;
using DAL.Mapper;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OIC.Mapper;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace OIC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }).ConfigureApiBehaviorOptions(options =>{options.SuppressModelStateInvalidFilter = true;});
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IGuardianService, GuardianService>();
            builder.Services.AddScoped<IGuardianRepo, GuardianRepo>();
            builder.Services.AddScoped<IEducationService, EducationService>();
            builder.Services.AddScoped<IEducationRepo, EducationRepo>();
            builder.Services.AddScoped<IHostelService, HostelService>();
            builder.Services.AddScoped<IHostelRepo, HostelRepo>();
            builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ILibraryRepo, LibraryRepo>();
            builder.Services.AddScoped<IStudentOnboardingService, StudentOnboardingService>();
            builder.Services.AddScoped<IStudentOnboardingRepo, StudentOnboardingRepo>();
            builder.Services.AddScoped<IValidator<OnBoardStudentDto>, StudentOnboardingValidation>();
            builder.Services.AddScoped<IValidator<UpdateStudentDto>, UpdateStudentValidation>();
            builder.Services.AddScoped<IValidator<HostelDto>, HostelValidation>();
            builder.Services.AddScoped<IValidator<LibraryDto>, LibraryValidation>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UIMappingProfile>();
                cfg.AddProfile<BALMappingProfile>();
                cfg.AddProfile<DALMappingProfile>();
                cfg.AddProfile<EntityMappingProfile>();
                cfg.AddProfile<OIC.Mapper.OnBoardStudentProfile>();
                cfg.AddProfile<BAL.Mapper.OnBoardStudentProfile>();
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    builder => builder
                        .WithOrigins("http://localhost:4200")  // your Angular URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()   // needed if using cookies or auth
                );
            });

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAngular");
            app.UseAuthentication();   // MUST be before UseAuthorization
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
