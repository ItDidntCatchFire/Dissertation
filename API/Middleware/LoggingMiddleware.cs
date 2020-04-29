using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace API.Middleware {
	public class LoggingMiddleware {
		private readonly RequestDelegate _next;

		public LoggingMiddleware(RequestDelegate next) {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context) {
			var request = context.Request.Path;
			var type = context.Request.Method;
			Directory.CreateDirectory("Logging");

			using (var mutex = new Mutex(false, "MyMutex")) {
				mutex.WaitOne();
				File.AppendAllText($"Logging/{DateTime.Now.Date:yy-MM-dd}.txt", $"{DateTime.Now.TimeOfDay:g} {type} {request}{Environment.NewLine}");
				mutex.ReleaseMutex();
			}

			// Call the next delegate/middleware in the pipeline
			await _next(context);
		}

	}
}