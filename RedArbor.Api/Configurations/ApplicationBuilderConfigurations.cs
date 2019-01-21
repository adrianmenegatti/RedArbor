using Microsoft.AspNetCore.Builder;

namespace RedArbor.Api.Configurations
{
	public static class ApplicationBuilderConfigurations
	{
		public static IApplicationBuilder AddSwaggerMiddelware(this IApplicationBuilder app)
		{
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Red Arbor API V1");
			});

			return app;
		}
	}
}
