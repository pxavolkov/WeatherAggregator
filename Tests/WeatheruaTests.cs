using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sources;

namespace Tests
{
    [TestFixture]
    public class WeatheruaTests
    {
        [Test]
        public void LoadXml()
        {
            var r = WeatheruaSource.GetCitiesIDs();
        }
    }
}
