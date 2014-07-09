using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Core.Entities
{
    public class SubscriptionResponse
    {
        public string Email { get; set; }
        public string Key { get; set; }
        public bool IsUpdating { get; set; }
        public string AddressText { get; set; }
    }
}
