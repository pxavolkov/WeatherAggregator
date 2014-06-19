using System;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.WebApi.Models
{
    public class SourceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Snippet { get; set; }

        public static SourceModel Map(ISource source)
        {
            return new SourceModel
            {
                Id = source.Id,
                Name = source.Name,
                Snippet = source.Snippet
            };
        }
    }
}