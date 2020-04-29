using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace WebApplication {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton(async p => {
				var httpClient = p.GetRequiredService<HttpClient>();
				return await httpClient.GetJsonAsync<string>("ip.json")
					.ConfigureAwait(false);
			});

			services.AddAuthorizationCore();
			services.AddScoped<AuthStateProvider>();
			services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());
		}

		public void Configure(IComponentsApplicationBuilder app) {
			app.AddComponent<App>("app");
		}
	}
}