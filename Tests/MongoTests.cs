using System;
using NUnit.Framework;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.DataAccess.Mongo;

namespace Tests
{
    [TestFixture]
    [Ignore]
    public class MongoTests
    {
        [Test]
        public void FeedbackTest()
        {
            var repository = new FeedbackRepository();
            var listBefore = repository.GetAll();
            repository.Add(new Feedback { DateCreated = DateTime.Now, Email = "lol.com", Name = "lol", Text = "Text" });
            var listAfter = repository.GetAll();

            Assert.Greater(listAfter.Count, listBefore.Count, "The item hasn't been added.");
        }
    }
}
