using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace API {
	public enum eExport {
		JSON,
		XML
	}

	public enum eImport {
		JSON
	}

	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
			ObjectFactory.Init(configuration);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			//Fixes issues with CORS, it's okay as this is not public facing
			services.AddCors(options => {
				options.AddPolicy("AllowAll",
					builder => {
						builder
							.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});

			services.AddControllers();

			//Add the Auth filter so only Authorized people can use the endpoints
			services.AddMvc(options => {
				options.AllowEmptyInputInBodyModelBinding = true;
				options.EnableEndpointRouting = false;
				options.Filters.Add(new Filters.AuthFilter());
			});

			//Storage
			services.AddSingleton<Repository.IItemRepository<DataLogic.Models.ItemDL, Guid>, DataLogic.DataBase.ItemDL>();
			services.AddSingleton<Repository.IInventoryRepository<DataLogic.Models.InventoryDL, Guid>, DataLogic.DataBase.InventoryDL>();
			services.AddSingleton<Repository.IUserRepository<DataLogic.Models.UserDL, Guid>, DataLogic.DataBase.UserDL>();
			services.AddSingleton<Repository.ICheckoutRepository<DataLogic.Models.InventoryDL, Guid>, DataLogic.List.CheckoutDL>(); //Only needs to be a list

			//Logic
			services.AddSingleton<Business.Logic.ItemLogic>();
			services.AddSingleton<Business.Logic.InventoryLogic>();
			services.AddSingleton<Business.Logic.UserLogic>();
			services.AddSingleton<Business.Logic.TransacationLogic>();
			services.AddSingleton<Business.Logic.CheckoutLogic>();

			//Exporters
			services.AddScoped<Export.JsonExport>();
			services.AddScoped<Export.XMLExport>();
			services.AddTransient<Func<eExport, Export.IExport>>(serviceProvider => key => {
				return key switch
				{
					eExport.JSON => serviceProvider.GetService<Export.JsonExport>(),
					eExport.XML => serviceProvider.GetService<Export.XMLExport>(),
					_ => null
				};
			});

			//Importers
			services.AddScoped<Import.JsonImport>();
			services.AddTransient<Func<eImport, Import.IImport>>(serviceProvider => key => {
				return key switch
				{
					eImport.JSON => serviceProvider.GetService<Import.JsonImport>(),
					_ => null
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			app.UseMiddleware<Middleware.LoggingMiddleware>();
			app.UseMiddleware<Middleware.AuthMiddleware>();

			if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseCors("AllowAll");

			app.Use(async (ctx, next) => {
				await next();
				if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted) {

					ctx.Response.StatusCode = 500;
					await ctx.Response.WriteAsync("\"Failure\"");
				}
			});

			app.UseMvc();
		}
	}
}