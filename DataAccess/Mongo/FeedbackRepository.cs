using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.DataAccess.Mongo
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MongoContext _mongo = new MongoContext();

        public void Add(Feedback feedback)
        {
            _mongo.Feddback.Insert(feedback);
        }

        public IList<Feedback> GetAll()
        {
            return _mongo.Feddback.AsQueryable().ToList();
        }
    }
}
