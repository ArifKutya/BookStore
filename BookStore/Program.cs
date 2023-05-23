using System.Text;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MongoDb;
using BookStore.Extensions;
using BookStore.HealthChecks;
using BookStore.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration
        .GetSection(nameof(MongoDbConfiguration)));

// Add services to the container.
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IAuthorRepository, AuthorMongoRepository>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IBookRepository, BookMongoRepository>();
builder.Services.AddSingleton<ILibraryService, LibraryService>();
builder.Services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddSingleton<IUserInfoService, UserInfoService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            
            IssuerSigningKey = 
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** JWT Bearer token in the text box below!",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        },
        
    };

    x.AddSecurityDefinition(
        jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("View", policy =>
    {
        policy.RequireClaim("View", "Employee");
    });
});

builder.Services.AddHealthChecks()
    .AddCheck<MongoHealthCheck>("MongoDB");

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.RegisterHealthChecks();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
