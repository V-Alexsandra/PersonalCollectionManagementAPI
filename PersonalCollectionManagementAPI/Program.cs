using IdentityMS.Data;
using PersonalCollectionManagementAPI.Hubs;
using PersonalCollectionManagementAPI.IoC;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("https://personal-collection-management.vercel.app", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add services to the container.
builder.Services.ConfigureSqlContext(configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication(configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureServices();

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.MapHub<CommentHub>("/commentHub");
app.MapHub<LikeHub>("/likeHub");

await app.SeedDataAsync();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
