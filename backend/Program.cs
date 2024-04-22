
using AutoMapper;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace backend {
	public class Program {
		public static void Main( string[] args ) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var allowedorsOrigins = builder.Configuration.GetSection( "CORS" ).GetSection( "AllowedOrigins" ).Get<String[]>();
			if ( allowedorsOrigins == null )
				throw new Exception( "CORS:AllowedOrigins is not set in appsettings.json" );

			// Add CORS
			builder.Services.AddCors( options => {
				options.AddDefaultPolicy( policy => {
					policy.WithOrigins( allowedorsOrigins ).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
				} );
			} );

			var jwtSecret = builder.Configuration["Jwt:AccessTokenSecret"];
			if ( jwtSecret == null )
				throw new Exception( "Jwt:AccessTokenSecret is not set in appsettings.json" );

			// Add the authentication service
			builder.Services.AddAuthentication(
				options => {
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				} ).AddJwtBearer( options => {
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( jwtSecret ) )
					};
				}
			);

			builder.Services.AddAuthorization(
					option => {
						option.AddPolicy( "Admin", policy => policy.RequireClaim( "role", "Admin" ) );
						option.AddPolicy( "User", policy => policy.RequireClaim( "role", "User" ) );
					}
				);

			// Add the automapper
			builder.Services.AddSingleton( MapperConfig.Configure().CreateMapper() );

			// Add the database service
			builder.Services.AddScoped<IDbConnection, MySqlConnection>( x => new MySqlConnection( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

			// Add our repositories
			builder.Services.AddScoped<IRepositoryCompany, RepositoryCompany>();
			builder.Services.AddScoped<IRepositoryParticipant, RepositoryParticipant>();
			builder.Services.AddScoped<IRepositorySystemUser, RepositorySystemUser>();
			builder.Services.AddScoped<IRepositoryVendor, RepositoryVendor>();
			builder.Services.AddScoped<IRepositoryVendorItem, RepositoryVendorItem>();
			builder.Services.AddScoped<IRepositoryTask, RepositoryTask>();
			builder.Services.AddScoped<IRepositoryTaskStatus, RepositoryTaskStatus>();
			builder.Services.AddScoped<IRepositoryTaskResource, RepositoryTaskResource>();
			builder.Services.AddScoped<IRepositoryProject, RepositoryProject>();
			builder.Services.AddScoped<IRepositoryProjectItem, RepositoryProjectItem>();
			builder.Services.AddScoped<IRepositoryProjectService, RepositoryProjectService>();
			builder.Services.AddScoped<IRepositoryProjectTest, RepositoryProjectTest>();
			builder.Services.AddScoped<IRepositoryProjectResource, RepositoryProjectResource>();
			builder.Services.AddScoped<IRepositoryProjectCommisionReport, RepositoryProjectCommisionReport>();
			

			// Add our services
			builder.Services.AddScoped<IServiceAuthentication, ServiceAuthentication>();
			builder.Services.AddScoped<IServiceCustomer, ServiceCustomer>();
			builder.Services.AddScoped<IServiceSystemUser, ServiceSystemUser>();
			builder.Services.AddScoped<IServiceVendor, ServiceVendor>();
			builder.Services.AddScoped<IServiceVendorItem, ServiceVendorItem>();
			builder.Services.AddScoped<IServiceTask, ServiceTask>();
			builder.Services.AddScoped<IServiceTaskStatus, ServiceTaskStatus>();
			builder.Services.AddScoped<IServiceTaskResource, ServiceTaskResource>();
			builder.Services.AddScoped<IServiceProject, ServiceProject>();
			builder.Services.AddScoped<IServiceProjectItem, ServiceProjectItem>();
			builder.Services.AddScoped<IServiceProjectService, ServiceProjectService>();
			builder.Services.AddScoped<IServiceProjectTest, ServiceProjectTest>();
			builder.Services.AddScoped<IServiceProjectResource, ServiceProjectResource>();
			builder.Services.AddScoped<IServiceProjectCommisionReport, ServiceProjectCommisionReport>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() ) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCors();

			app.MapControllers();

			app.Run();
		}
	}
}
