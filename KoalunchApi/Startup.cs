using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using koalunch_api.Repositories;
using koalunch_api.MenuParsers;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using koalunch_api.Models;
using System;
using koalunch_api.Models.Api;

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

			services.AddDbContext<FeedbackContext>();
			services.AddScoped<FeedbackRepository, GoogleFeedbackRepository>(_provicer =>
			{
				return new GoogleFeedbackRepository(new GoogleOptions
				{
					AppName = Configuration["Google:AppName"],
					SpreadsheetId = Configuration["Google:SpreadsheetId"],
					Range = Configuration["Google:Range"],
					CredentialJson = Environment.GetEnvironmentVariable("GOOGLE_SERVICE_CREDENTIAL")
				});
			});
			services.AddScoped<IRepository<Visitors>, VisitorRepository>();
			services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();
			services.AddScoped<IRepository<MenuSource>, MenuSourceRepository>();
			services.AddSingleton<IHtmlDocumentContext, HtmlDocumentContext>(_provider =>
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
			// app.UseHttpsRedirection();

			ConfigureStaticFiles(app, env);

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void ConfigureStaticFiles(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var fileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"client"));
			var options = new DefaultFilesOptions();
			options.FileProvider = fileProvider;
			options.DefaultFileNames.Clear();
			options.DefaultFileNames.Add("public/index.html");

			app.UseDefaultFiles(options);
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = fileProvider,
				RequestPath = new PathString("")
			});
		}
	}
}
