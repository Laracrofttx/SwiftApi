using Microsoft.Data.Sqlite;
using Serilog;
using SwiftApi.Core.Services;
using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data;
using SwiftApi.Data.Repositories;

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
