using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Controllers.BaseController;
using Api.Controllers.Dto.Mapper;
using AutoMapper;
using Core.Service.Exception;
using Data.Entity;
using Data.Repository;
using Data.Repository.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Service;
using Service.Service;
using Service.Service.Abstract;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = System.Text.Json.JsonSerializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Database
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(opt =>
{
   opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});

// Repositories
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(s => (User)s.GetService<IHttpContextAccessor>().HttpContext.Items["User"]);

builder.Services.Configure<ServiceConfiguration.App>(builder.Configuration.GetSection("App"));
builder.Services.Configure<ServiceConfiguration.Jwt>(builder.Configuration.GetSection("Jwt"));


IdentityModelEventSource.ShowPII = true;
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })    
    .AddJwtBearer(x =>
    {
        x.SaveToken = false;
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters    
        {    
            ValidateIssuer = true,    
            ValidateAudience = true,    
            ValidateLifetime = true,    
            ValidateIssuerSigningKey = true,    
            ValidIssuer = builder.Configuration["Jwt:Issuer"],    
            ValidAudience = builder.Configuration["Jwt:Issuer"],    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromDays(1),
        };    
        
        x.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = delegate { return true; }
        };

        x.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                c.NoResult();

                c.Response.StatusCode = 401;
                c.Response.ContentType = "text/plain";

                return c.Response.WriteAsync(c.Exception.ToString());
            },
            OnTokenValidated = ctx =>
            {
                // Get Login User Id
                // https://www.oauth.com/oauth2-servers/access-tokens/self-encoded-access-tokens/
                var uuid = ((JwtSecurityToken)ctx.SecurityToken).Claims.FirstOrDefault(x => x.Type == "Id")
                    .Value;
                
                var userService = ctx.HttpContext.RequestServices.GetRequiredService<IUserService>();
                var user = userService.GetUser(Guid.Parse(uuid)).Data;

                ctx.HttpContext.Items["User"] = user;
                return Task.CompletedTask;
            }
        };
    });

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthorization(o =>
{
    o.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b => b
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Content-Disposition")
    );
});


// App Build
var app = builder.Build();

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var scope = builder.ApplicationServices.CreateScope();
        var logService = scope.ServiceProvider.GetRequiredService<ILogService>();

        var expResult = new ApiPublicControllerBase.ExceptionResult()
        {
            Message = "İşlem Başarısız",
        };
        
        if (exception is ServiceException serviceException)
        {
            expResult.Message = serviceException.GetServiceMessage();
            expResult.ErrorData = serviceException.GetErrorData();

            if (logService != null)
            {
                logService.CreateLog((ServiceException)exception);
            }
            
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(expResult));
            return;
        }
        else if (exception is ServiceNotAllowedException allowedException)
        {
            expResult.Message = allowedException.GetServiceMessage();
            expResult.ErrorData = allowedException.GetErrorData();

            if (logService != null)
            {
                logService.CreateLog((ServiceNotAllowedException)exception);
            }
            
            context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(expResult));
            return;
        }
        
        if (logService != null)
        {
            logService.CreateLog(exception);
        }
        
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(expResult));
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var publicDir = Path.Combine(builder.Configuration.GetSection("App").GetValue<string>("Disk"), "public");
if (!Directory.Exists(publicDir))
    Directory.CreateDirectory(publicDir);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Configuration.GetSection("App").GetValue<string>("Disk"), "public")
    ),
    RequestPath = "/Public"
});

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

