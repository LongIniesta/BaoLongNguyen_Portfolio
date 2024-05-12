using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapping;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using DTOs;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Repositories.Interface;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });

    //Add
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

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
                         new string[] {}
                    }
                });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
        .AllowAnyMethod());
});

var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration["jwtKey"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
    };
});
builder.Services.AddAutoMapper
(typeof(AutoMapperProfile).Assembly);

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CategoryDTO>("Categories");
    builder.EntitySet<PostDTO>("Posts");
    builder.EntitySet<GiftDTO>("Gifts");
    builder.EntitySet<ProductDTO>("Products");
    builder.EntitySet<ServiceDTO>("Services");
    builder.EntitySet<ServiceSchedulerDTO>("ServiceSchedulers");
    builder.EntitySet<GiftCommentDTO>("GiftComments");
    builder.EntitySet<OrderDTO>("Orders");
    builder.EntitySet<OrderDetailDTO>("OrderDetails");
    builder.EntityType<ReactDTO>().HasKey(r => r.PostId).HasKey(r => r.AccountId);
    builder.EntitySet<ReactDTO>("Reacts");
    builder.EntityType<ReportDTO>().HasKey(r => r.ReporterId).HasKey(r => r.ReportedPersonId);
    builder.EntitySet<ReportDTO>("Reports");
    return builder.GetEdmModel();
}
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrStaff", policy =>
    {
        policy.RequireRole("admin", "staff");
    });
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAssertion(context =>
    {
        var role = context.User.FindFirst(ClaimTypes.Role);
        if (role == null) return false;
            var idClaimValue = context.User.FindFirst("id")?.Value;

            if (!string.IsNullOrEmpty(idClaimValue))
            {
                IAccountRepository accountRepository = new AccountRepository();
                int versionCheck = (int)accountRepository.GetAccountById(int.Parse(idClaimValue)).Result.Version;
                var versionClaimValue = context.User.FindFirst("version")?.Value;

                if (!string.IsNullOrEmpty(versionClaimValue) && int.Parse(versionClaimValue) == versionCheck)
                {
                    return true;
                }
            }
            return false;

    }).Build();
});

builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lotus.API.Integration v1"));
}
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.MapControllers();

app.Run();
