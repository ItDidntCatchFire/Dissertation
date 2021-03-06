using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApplication {
	public class AuthStateProvider : AuthenticationStateProvider {
		public bool IsAuthenticated { get; set; }
		public override Task<AuthenticationState> GetAuthenticationStateAsync() {
			var identity = new ClaimsIdentity();

			if (IsAuthenticated) {
				identity = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Sid, Utils.UserId.ToString())
				}, "MyAuthType");
			}

			var user = new ClaimsPrincipal(identity);

			return Task.FromResult(new AuthenticationState(user));
		}

		public void NotifyAuthenticationStateChanged() {
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}
}