using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.DataAccess.Mongo
{
    class MongoContext
    {
        static MongoContext()
        {
            BsonClassMap.RegisterClassMap<Feedback>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(f => f.DateCreated));
            });
        }

        public MongoCollection<Feedback> Feddback { get; private set; }

        public MongoContext()
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("WeatherAggregator");
            Feddback = database.GetCollection<Feedback>("Feedback");
        }
    }
}
