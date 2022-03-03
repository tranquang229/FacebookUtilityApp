using AutoMapper;
using FluentValidation.AspNetCore;
using FM.API.Extensions;
using FM.API.Middlewares;
using FM.Application;
using FM.Application.Interfaces.Repositories;
using FM.Application.Mappers;
using FM.Application.Validations;
using FM.Domain.Entities.Identity;
using FM.Domain.Settings;
using FM.Infrastructure;
using FM.Infrastructure.Contexts;
using FM.Infrastructure.Repositories;
using FM.Infrastructure.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddHttpClient();


    // Add services to the container.
    services.AddApplicationLayer();
    services.AddPersistenceInfrastructure(builder.Configuration);
    services.AddRepositories();
    services.AddSharedInfrastructure(builder.Configuration);

    //Configuration from AppSettings
    services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
    services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

    //Adding DB Context with MSSQL
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    //User Manager Service
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<ApplicationDbContext>();


    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    //Adding Authentication - JWT
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
            };
        });

    //Adding Authorization
    builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
    builder.Services.AddAuthorization();


    services
                  .AddMvc(options =>
                  {
                      options.EnableEndpointRouting = false;
                      options.Filters.Add<ValidationFilter>();
                  })
                  .AddFluentValidation(opt =>
                  {
                      opt.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(RegisterRequestValidator)));
                  })
                  .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

    //To use Fluent Validation with custom response validate
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    //Add repository
    services.AddScoped(typeof(IHealthHistoryRepository), typeof(HealthHistoryRepository));
    services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
    services.AddScoped(typeof(IFbRepository), typeof(FbRepository));

    //Add auto mapper
    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new AutoMapperProfile());
    });

    IMapper mapper = mapperConfig.CreateMapper();

}


var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var connectionString = app.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs" };

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Warning()
        .WriteTo
        .MSSqlServer(connectionString, sinkOpts)
        .CreateLogger();

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        var appContext = services.GetRequiredService<ApplicationDbContext>();

        await appContext.Database.MigrateAsync();

        await SeedData.SeedDefaultAccount(userManager, roleManager);
        //await SeedData.SeedDefaultToken(appContext);

        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred seeding the DB.");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // generated swagger json and swagger ui middleware
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
