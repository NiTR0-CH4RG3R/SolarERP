
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace backend {
	public class Program {
		public static void Main( string[] args ) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Add CORS
			builder.Services.AddCors( options => {
				options.AddDefaultPolicy( policy => {
					policy.WithOrigins( builder.Configuration.GetSection( "CORS" ).GetSection( "AllowedOrigins" ).Get<String[]>()  ).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
				} );
			} );

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
