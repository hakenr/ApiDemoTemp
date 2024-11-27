using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
	[ApiController]
	[Route("api/weather")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet("wf", Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet("/dh/{count}/{name?}", Name = "GetWeatherForecast2")]
		public IEnumerable<WeatherForecast> Get2([FromRoute] MyModel model)
		{
			return Enumerable.Range(1, model.Count).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-200, 5),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpPost("/api/product/{id}")]
		public IEnumerable<WeatherForecast> DoSomething(int id, [FromBody] MyModel model)
		{
			System.IO.File.WriteAllText("D:\\Temp\\log.txt", JsonSerializer.Serialize(model));

			if (id == 4)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
			}

			return Enumerable.Range(1, model.Count).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-200, 5),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		public class MyModel
		{
			public int Count { get; set; }
			public string? Name { get; set; } = "Default";
		}
	}

}
