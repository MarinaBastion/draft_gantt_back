using gantt_backend.Data.DBContext;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using gantt_backend.Repositories;
using gantt_backend.Interfaces;
using gantt_backend.Interfaces.Constructor;
using gantt_backend.Repositories.UOF;
using gantt_backend.Interfaces.UOF;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using gantt_backend.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using gantt_backend.Interfaces.Auth;
using gantt_backend.Repositories.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CustomMiddleware.Test.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GanttContext>(options => options.UseLazyLoadingProxies()
                                                              .UseNpgsql(connectionString), 
                                                              ServiceLifetime.Scoped);


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<GanttContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
// builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
//                                 options.SignIn.RequireConfirmedAccount = true)                          
//     .AddEntityFrameworkStores<GanttContext>()
//     .AddDefaultTokenProviders();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy(name : "AllowOrigin",
    builder => {builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();});
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IFlatMapArray<A.Task,List<TaskViewModel>>), typeof(FlatMapArray<A.Task,List<TaskViewModel>>));
//builder.Services.AddScoped<IFlatMapArray, FlatMapArray>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IEntityRepository, EntityRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IEntityFieldRepository, EntityFieldRepository>();
builder.Services.AddScoped<IInstanceRepository, InstanceRepository>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
#endregion

var app = builder.Build();
app.UseCors("AllowOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Gantt Application"));
    app.UseDeveloperExceptionPage();
    
}
app.UseAuthentication();
app.UseJwtTimeValidator();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
