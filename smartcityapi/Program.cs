using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using smartcityapi.Context;
using smartcityapi.helperServices;
using smartcityapi.Interface;
using smartcityapi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SmartCityDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MySqlConn")));

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
		{
			builder.AllowAnyOrigin()   // Allows requests from any origin
				   .AllowAnyMethod()   // Allows any HTTP method (GET, POST, PUT, DELETE, etc.)
				   .AllowAnyHeader();  // Allows any headers
		});
});

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IModuleService, ModuleService>();
builder.Services.AddTransient<ISharedService, SharedService>();
builder.Services.AddTransient<IPageService, PageService>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleAccessService, RoleAccessService>();

builder.Services.AddTransient<IUserMgtService, UserMgtService>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();

builder.Services.AddTransient<IDepartmentAccessService, DepartmentAccessService>();

builder.Services.AddTransient<IAssetTypeService, AssetTypeService>();

builder.Services.AddTransient<IDeviceService, DeviceService>();



builder.Services.AddScoped<UserService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};

});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add AddSwagger With JWT Authorization Example;
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "Enter 'Bearer {your_token}' without quotes.",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http, // ✅ Use Http instead of ApiKey
		Scheme = "Bearer" // ✅ Correct Scheme
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
				},
				Scheme = "Bearer", // ✅ Fixed scheme
                Name = "Authorization",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});









builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll"); // Apply CORS policy

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
	app.UseSwagger();
	

	app.UseSwaggerUI(options =>
	{
		options.DefaultModelsExpandDepth(-1); // Hides the Schemas tab
	});
}





app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
