using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using WeatherAggregator.Core;
using WeatherAggregator.Core.Entities;
using WeatherAggregator.Core.Interfaces;
using WeatherAggregator.Sources.Aspects;
using WeatherAggregator.WebApi.Models;

namespace WeatherAggregator.WebApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly CoreFacade _facade = CreateCoreFacade();

        [HttpGet]
        public List<SourceModel> Sources()
        {
            return _facade.GetSources().Select(SourceModel.Map).ToList();
        }

        [HttpPost]
        public List<Weather> GetWeather(WeatherRequest request)
        {
            var result = _facade.GetWeather(request.Sources, request.DateRange, request.Location);
            return result;
        }

        [HttpPost]
        public bool SubscribeEmailForNotify(string email, double latitude, double longitude, string address)
        {
            bool result = true;
            try
            {
                //Add or update Email with coordinates and text addres to DB with status "Not Confirmed"
                bool isUpdated = IsPresentNotConfirmedEmail(email);
                string key = AddOrUpdateNotConfirmedEmail(email, latitude, longitude, address);

                //Send email with link for confirming  subscription. (If entered email is present in DB with status "Confirmed" text in email should be about changing address)
                SendConfirmationEmail(email, key, address, isUpdated);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        [HttpGet]
        public void ConfirmSubscriptionOrChanging(string email, string key)
        {
            // copy data from not confirmed data to confirmed and delete not confirmed. ofcourse key should be checked before operation
        }


        [HttpGet]
        public void UnsubscribeEmail(string email, string key)
        {
            // check key and delete record from BD if key is correct
        }

        [HttpGet]
        public void NotifySubscribers()
        {
            //Get list of confirmed emails
            // Check if lastForecast date is more than some value (1 day for example) and if is launch forecast for those emails coordinates and send letter if rain is comming
            //after that update lastForecastDate for those emails
        }


        #region Private Methods

        private static CoreFacade CreateCoreFacade()
        {
            var settings = new Settings
            {
                CacheRepositoryFactory = () => DataAccess.InMemory.CacheRepository.Instance,
                Sources = LoadSources()
            };

            return new CoreFacade(settings);
        }

        private static List<ISource> LoadSources()
        {
            var iSource = typeof(ISource);
            var sources = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .SelectMany(s => s.GetTypes())
                .Where(iSource.IsAssignableFrom)
                .Where(t => !t.IsInterface)
                .Select(t => (ISource) Activator.CreateInstance(t))
                .ToList();
            return sources;
        }

        #endregion Private Methods
    }
}