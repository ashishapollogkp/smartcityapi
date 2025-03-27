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
		Description = @"JWT Authorization Example : 'Bearer eyeleieieekeieieie",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
		{
			new OpenApiSecurityScheme{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "outh2",
				Name="Bearer",
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
	app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
