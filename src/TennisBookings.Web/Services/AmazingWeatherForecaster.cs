namespace TennisBookings.Web.Services
{
    public class AmazingWeatherForecaster : IWeatherForecaster
    {
        public WeatherResult GetCurrentWeather()
        {
            // Amazing
            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun,
            };
        }
    }
}