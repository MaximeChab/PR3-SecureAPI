using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using PR3_SecureAPI.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SalleContext>(options => options.UseSqlite("DataSource=C:\\Users\\maxim\\Desktop\\PR3\\PR3-SecureAPI\\PR3-SecureAPI\\PR3-database.db"));
builder.Services.AddDbContext<PosteContext>(options => options.UseSqlite("DataSource=C:\\Users\\maxim\\Desktop\\PR3\\PR3-SecureAPI\\PR3-SecureAPI\\PR3-database.db"));
builder.Services.AddDbContext<UtilisateurContext>(options => options.UseSqlite("DataSource=C:\\Users\\maxim\\Desktop\\PR3\\PR3-SecureAPI\\PR3-SecureAPI\\PR3-database.db"));
builder.Services.AddDbContext<EtablissementContext>(options => options.UseSqlite("DataSource=C:\\Users\\maxim\\Desktop\\PR3\\PR3-SecureAPI\\PR3-SecureAPI\\PR3-database.db"));
builder.Services.AddDbContext<IncidentContext>(options => options.UseSqlite("DataSource=C:\\Users\\maxim\\Desktop\\PR3\\PR3-SecureAPI\\PR3-SecureAPI\\PR3-database.db"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Title",
        Description = "Description",
        TermsOfService  = new Uri("https://example.com/terms"),
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Description",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }

        },
        new string[] {}
        }
    });


});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "3iL",
        ValidAudience = "API test",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("96ZP0WzO5W3ZMWWVqZVhsUK0h3lChcdj96ZP0WzO5W3ZMWWVqZVhsUK0h3lChcdj"))
    };
});




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
