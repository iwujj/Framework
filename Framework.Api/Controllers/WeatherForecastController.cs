using Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService _userService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            await _userService.AddUser();
            await _userService.RemoveUser(x => x.Name == "����");
            return "Done";
        }
    }
}