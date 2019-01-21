using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RedArbor.Api.MapperProfiles;
using RedArbor.Data;
using RedArbor.Domain.Repositories;
using RedArbor.Domain.Services;
using RedArbor.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace RedArbor.Api.Configurations
{
	public static class ServicesConfigurations
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddTransient<IEmployeeService, EmployeeService>();
			return services;
		}

		public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
		{
			services.AddTransient<IEmployeeRepository, EmployeeRepository>();
			return services;
		}

		public static IServiceCollection ConfigureMapper(this IServiceCollection services)
		{
			services.AddAutoMapper();
			Mapper.Initialize(m => m.AddProfile<EmployeeProfile>());

			return services;
		}

		public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Red Arbor API", Version = "v1" });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			return services;
		}
	}
}
