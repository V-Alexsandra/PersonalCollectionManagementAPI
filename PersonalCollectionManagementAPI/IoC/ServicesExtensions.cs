using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalCollectionManagement.Data.Contexts;
using System.Text;

namespace PersonalCollectionManagementAPI.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            return services;
        }


        //public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        //{
        //    services.AddScoped<IUserRepository, UserRepository>();

        //    return services;
        //}

        //public static IServiceCollection ConfigureServices(this IServiceCollection services)
        //{
        //    services.AddScoped<IUserService, UserService>();
        //    services.AddScoped<ITokenService, TokenService>();

        //    return services;
        //}

        //public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        //{
        //    services.AddFluentValidation(options =>
        //    {
        //        options.ImplicitlyValidateChildProperties = true;
        //        options.ImplicitlyValidateRootCollectionElements = true;

        //        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //    });

        //    return services;
        //}

        //public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddAuthentication(auth =>
        //    {
        //        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.SaveToken = true;
        //        options.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidAudience = configuration["AuthSettings:Audience"],
        //            ValidIssuer = configuration["AuthSettings:Issuer"],
        //            RequireExpirationTime = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
        //            ValidateIssuerSigningKey = true,
        //            ValidateLifetime = true
        //        };
        //    });

        //    return services;
        //}
    }
}
