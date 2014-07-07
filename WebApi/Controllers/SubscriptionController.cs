using System;
using System.Collections.Generic;
using System.Web.Http;
using WeatherAggregator.Core.Entities;

namespace WeatherAggregator.WebApi.Controllers
{
    public class SubscriptionController : BaseController
    {
        [HttpPost]
        public void Subscribe(SubscriptionInfo subscriber)
        {
            Facade.Subscribe(subscriber);
                //Add or update Email with coordinates and text addres to DB with status "Not Confirmed"
                
                //string key = AddOrUpdateNotConfirmedEmail(email, latitude, longitude, address);

                //Send email with link for confirming  subscription. (If entered email is present in DB with status "Confirmed" text in email should be about changing address)
                //SendConfirmationEmail(email, key, address, isUpdated);
        }

        [HttpGet]
        public void ConfirmSubscription(string key)
        {
            Facade.ConfirmSubscription(key);
            // copy data from not confirmed data to confirmed and delete not confirmed. ofcourse key should be checked before operation
        }


        [HttpGet]
        public void Unsubscribe(string key)
        {
            Facade.Unsubscribe( key);
            // check key and delete record from BD if key is correct
        }

        [HttpGet]
        public void NotifySubscribers()
        {
            Facade.NotifySubscribers();
            ////Delete all emails which are not confirmed for month for example
            //DeleteAllexpiredNotConfirmedEmails();
            ////Get list of confirmed emails
            //List<object> confirmedEmailsToNotify = GetEmailsForNotifiyng();
            //// Check if lastForecast date is more than some value (1 day for example) and if is launch forecast for those emails coordinates and send letter if rain is comming
            //foreach (var notifyInfo in confirmedEmailsToNotify)
            //{

            //}
            ////after that update lastForecastDate for those emails
        }

    }
}
