using Microsoft.EntityFrameworkCore;


using ServerFridge.DataContext;
using ServerFridge.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;
using Microsoft.Identity.Client;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;


namespace ServerFridge
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration =builder.Configuration;

            var jwtToken = builder.Configuration.GetSection("JWTToken");
         
            var issuer = jwtToken["Issuer"];
            var audience = jwtToken["Audience"];
            var secretKey = jwtToken["SecretKey"];

            Console.WriteLine($"JWT Configuration:");
            Console.WriteLine($"Issuer: {issuer}");
            Console.WriteLine($"Audience: {audience}");
            Console.WriteLine($"SecretKey: {new string('*', secretKey.Length)} (Length: {secretKey.Length})");

            // Add services to the container.
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("JWT Secret Key is invalid");
            }
            if(secretKey.Length<32)
            {
                throw new ArgumentException("JWT Secret Key is shorter than needed");
            }
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
               
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));



            builder.Services.AddScoped<IFridgeRepository, FridgeRepository>();
            builder.Services.AddScoped<IFridgeProductRepository, FridgeProductRepository>();
            builder.Services.AddScoped<IRegistrateRepository,RegistrateRepository>();
            builder.Services.AddScoped<IProductRepository,ProductsRepository>();
            builder.Services.AddScoped<IModelsRepository,ModelsRepository>();

            builder.Services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })  
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = JwtRegisteredClaimNames.Email,
                        RoleClaimType = ClaimTypes.Role,
                        ValidAlgorithms = new[] { "HS256" },
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnAuthenticationFailed = context =>
                    //    {
                    //        Console.WriteLine($"Authentication failed: {context.Exception}");
                    //        if (context.Exception is SecurityTokenExpiredException)
                    //        {
                    //            Console.WriteLine("Token expired");
                    //        }
                    //        else if (context.Exception is SecurityTokenInvalidSignatureException)
                    //        {
                    //            Console.WriteLine("Invalid signature");
                    //        }
                    //        return Task.CompletedTask;
                    //    },
                    //    OnTokenValidated = context =>
                    //    {
                    //        Console.WriteLine("Token validated successfully");
                    //        return Task.CompletedTask;
                    //    },
                    //    OnChallenge = context =>
                    //    {
                    //        Console.WriteLine($"Challenge: {context.Error}, {context.ErrorDescription}");
                    //        return Task.CompletedTask;
                    //    }
                    //};

                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
                options.AddPolicy("AllUsers", policy => policy.RequireRole("Admin", "User"));
            });
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

       

          //  app.UseStaticFiles();

            app.MapControllers();




            app.Run();

            
            
       
            
        }


    }
}
