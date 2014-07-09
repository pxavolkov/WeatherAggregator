using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;

namespace WeatherAggregator.Core.Logic
{
    public class SubscriptionProvider
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionProvider(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionResponse Add(SubscriptionInfo subscription)
        {
            var response = _subscriptionRepository.Add(subscription);
            return response;
        }

        public SubscriptionResponse Confirm(string key)
        {
            var response = _subscriptionRepository.Confirm(key);
            return response;
        }

        public string Unsubscribe(string key)
        {
            string email = _subscriptionRepository.Unsobscribe(key);
            return email;
        }

        public List<SubscriptionInfo> GetConfirmed()
        {
            return _subscriptionRepository.GetConfirmed();
        }

        public void UpdateNotifyDate(SubscriptionInfo subscription)
        {
            _subscriptionRepository.UpdateNotifyDate(subscription);
        }

        public void DeleteExpiredSubscriptions()
        {
            _subscriptionRepository.DeleteExpiredSubscriptions();
        }
    }
}
