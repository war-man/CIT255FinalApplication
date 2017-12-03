using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DataSettings
    {
        /// <summary>
        /// this key is provided by the Wunderground API and may be updated on their website
        /// </summary>
        public const string APIKey = "034dcef2a59f8dae";
        public const string zipCode = "49684";

        // paste this string into your browser for a quick sample of the data in xml format: http://api.wunderground.com/api/034dcef2a59f8dae/forecast10day/q/CA/49684.xml

        public const string dataFilePathAPI = "http://api.wunderground.com/api/" + APIKey + "/forecast10day/q/CA/" + zipCode + ".xml";

        public const string dataFilePathLocal = "Data.xml";
    }
}
