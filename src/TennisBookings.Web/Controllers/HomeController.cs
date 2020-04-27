namespace TennisBookings.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using TennisBookings.Web.Configuration;
    using TennisBookings.Web.Services;
    using TennisBookings.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturedConfiguration _featuredConfiguration;

        public HomeController(
            IWeatherForecaster weatherForecaster,
            IOptions<FeaturedConfiguration> options)
        {
            _weatherForecaster = weatherForecaster;
            _featuredConfiguration = options.Value;
        }

        [Route("")]
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            if (_featuredConfiguration.EnableWeatherForecast)
            {
                var currentWeather = _weatherForecaster.GetCurrentWeather();
                viewModel.DisplayWeatherForecast = true;

                switch (currentWeather.WeatherCondition)
                {
                    case WeatherCondition.Sun:
                        viewModel.WeatherDescription = "It's sunny right now. " +
                                                       "A great day for tennis.";
                        break;

                    case WeatherCondition.Rain:
                        viewModel.WeatherDescription = "We're sorry but it's raining " +
                                                       "here. No outdoor courts in use.";
                        break;

                    default:
                        viewModel.WeatherDescription = "We don't have the latest weather " +
                                                       "information right now, please check again later.";
                        break;
                }
            }

            return View(viewModel);
        }
    }
}