using Microsoft.Data.Sqlite;
using NLog.Extensions.Logging;
using NLog;
using Serilog;
using SwiftApi.Core.Services;
using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data;
using SwiftApi.Data.Repositories;
using Microsoft.OpenApi.Models;
using NLog.Web;

namespace SwiftApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
			builder.Services.AddTransient(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				var connectionString = configuration.GetConnectionString("SQLiteConnection");
				return new SwiftApiDbContext(connectionString);
			});


			builder.Services.AddScoped<IMessageService, MessageService>();
			builder.Services.AddScoped<IMessageRepository, MessageRepository>();

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddLogging(logginBuilders => 
			{
				logginBuilders.ClearProviders();
				logginBuilders.AddNLogWeb();
			
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}

	}
}
