using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.DataAccess.Mongo
{
    public class MongoContext
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

        public MongoContext(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            var server = client.GetServer();
            var database = server.GetDatabase(url.DatabaseName);

            Feddback = database.GetCollection<Feedback>("Feedback");
        }
    }
}
