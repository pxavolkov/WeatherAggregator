namespace WeatherAggregator.Core.Interfaces
{
    public interface ICacheRepository
    {
        object this[string key] { get; set; }
    }
}
