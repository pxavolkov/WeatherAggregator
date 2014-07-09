using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.DataAccess.Mongo
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly MongoContext _mongo;

        public SubscriptionRepository(MongoContext context)
        {
            _mongo = context;
        }

        public SubscriptionResponse Add(SubscriptionInfo subscription)
        {
            SubscriptionResponse result = new SubscriptionResponse();
            result.IsUpdating = _mongo.Subscription.AsQueryable().Any(s => s.Email == subscription.Email && s.IsConfirmed);

            var query = Query.And(Query<SubscriptionInfo>.EQ(e => e.Email, subscription.Email),Query<SubscriptionInfo>.EQ(e => e.IsConfirmed, false));
            _mongo.Subscription.Remove(query);

            subscription.CreatedDate = DateTime.Now;
            subscription.IsConfirmed = false;
            subscription.Key = Guid.NewGuid().ToString();

            _mongo.Subscription.Insert(subscription);

            result.Key = subscription.Key;
            result.Email = subscription.Email;
            result.AddressText = subscription.AddressText;

            return result;
        }

        public SubscriptionResponse Confirm(string key)
        {
            var debug = _mongo.Subscription.AsQueryable().ToList();

            SubscriptionResponse result = new SubscriptionResponse();

            SubscriptionInfo notConfirmedSubscription =
                _mongo.Subscription.AsQueryable().FirstOrDefault(s => s.Key == key && s.IsConfirmed == false);
            if (notConfirmedSubscription == null)
                throw new Exception(string.Format("can't find subscription with key {0}", key));

            SubscriptionInfo existConfirmedSubscription =
                _mongo.Subscription.AsQueryable()
                    .FirstOrDefault(s => s.Email == notConfirmedSubscription.Email && s.IsConfirmed);

            if (existConfirmedSubscription != null)
            {
                var removeQuery = Query<SubscriptionInfo>.EQ(e => e.CreatedDate, existConfirmedSubscription.CreatedDate);
                _mongo.Subscription.Remove(removeQuery);
            }

            var updateQuery = Query<SubscriptionInfo>.EQ(e => e.CreatedDate, notConfirmedSubscription.CreatedDate);
            var updateConfirmed = Update<SubscriptionInfo>.Set(e => e.IsConfirmed, true);
            var updateUpdatedTime = Update<SubscriptionInfo>.Set(e => e.UpdateDate, DateTime.Now);
            string newKey = Guid.NewGuid().ToString();
            var updateKey = Update<SubscriptionInfo>.Set(e => e.Key, newKey);
            _mongo.Subscription.Update(updateQuery, updateConfirmed);
            _mongo.Subscription.Update(updateQuery, updateUpdatedTime);
            _mongo.Subscription.Update(updateQuery, updateKey);

            result.Key = newKey;
            result.Email = notConfirmedSubscription.Email;
            result.AddressText = notConfirmedSubscription.AddressText;

            return result;
        }

        public string Unsobscribe(string key)
        {
            SubscriptionInfo subscription = _mongo.Subscription.AsQueryable().FirstOrDefault(s => s.Key == key);
            if (subscription == null)
                throw new Exception(string.Format("can't find subscription with key {0}", key));

            var query = Query<SubscriptionInfo>.EQ(e => e.CreatedDate, subscription.CreatedDate);
            _mongo.Subscription.Remove(query);
            return subscription.Email;
        }

        public List<SubscriptionInfo> GetConfirmed()
        {
            return _mongo.Subscription.AsQueryable().Where(s => s.IsConfirmed).ToList();
        }

        public void UpdateNotifyDate(SubscriptionInfo subscription)
        {
            SubscriptionInfo updatingSubscription =
                _mongo.Subscription.AsQueryable()
                    .FirstOrDefault(s => s.CreatedDate == subscription.CreatedDate && s.IsConfirmed);
            if (updatingSubscription == null)
                throw new Exception(string.Format("can' find subscription for {0} for updating date on notification",
                    subscription.Email));

            var query = Query<SubscriptionInfo>.EQ(e => e.CreatedDate, updatingSubscription.CreatedDate);
            var update = Update<SubscriptionInfo>.Set(e => e.LastNotifyDate, DateTime.Now);
            _mongo.Subscription.Update(query, update);

        }

        public void DeleteExpiredSubscriptions()
        {
            List<SubscriptionInfo> expiredSubscriptions = _mongo.Subscription.AsQueryable().Where(s => !s.IsConfirmed).ToList();

            foreach (SubscriptionInfo expiredSubscription in expiredSubscriptions)
            {
                if ((DateTime.Now - expiredSubscription.CreatedDate) > new TimeSpan(30, 0, 0))
                {
                    var query = Query<SubscriptionInfo>.EQ(e => e.CreatedDate, expiredSubscription.CreatedDate);
                    _mongo.Subscription.Remove(query);
                }
            }
        }
    }
}
