using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using koalunch_api.Repositories;
using koalunch_api.MenuParsers;

namespace koalunch_api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Koalunch API", Version = "v1" });
			});

			services.AddScoped<RestaurantRepository>();
			services.AddScoped<VisitorRepository>();
			services.AddScoped<FeedbackRepository>();
			services.AddScoped<MenuSourceRepository>();
			services.AddSingleton<HtmlDocumentContext>(_provider =>
			{
				return new HtmlDocumentContext(HtmlDocumentContext.CreateDefaultBrowsingContext());
			});
			services.AddHttpClient("zomato", client =>
			{
				client.BaseAddress = new System.Uri("https://developers.zomato.com/api/v2.1/dailymenu");
				client.DefaultRequestHeaders.Add("accept", "application/json");
				client.DefaultRequestHeaders.Add("user_key", "b0a94ba965b2a1bbcdfc59d1632e0a6d");
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Koalunch API v1"));
			}

			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin();
				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
			});
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
