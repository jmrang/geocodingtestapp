using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipeTestAppJMRAng.Helpers
{
    public class CommonStrings
    {

        public const string GoogleGeocodingRequestStrOne = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        public const string GoogleApiKey = "<INSERT API KEY HERE>";
        public const string GoogleGeocodingRequestStrTwo = "&key=" + GoogleApiKey;

        public const string PostBindIncludeString = "StreetAddress, City, State, ZipCode";
        public const string PlusSign = "+";
        public const string SingleSpace = " ";

        public const string ParamResult = "results";
        public const string ParamGeometry = "geometry";
        public const string ParamLocation = "location";
        public const string ParamLng = "lng";
        public const string ParamLat = "lat";

        public const string HomeIndexView = "Index";
        public const string Error = "Error";

    }
}