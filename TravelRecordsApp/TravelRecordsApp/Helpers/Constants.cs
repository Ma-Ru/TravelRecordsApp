using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordsApp.Helpers
{
    public class Constants
    {
        public const string VENUE_SEARCH = "https://api.foursquare.com/v2/venues/search?ll={0},{1}&client_id={2}&client_secret={3}&v={4}";                 
                                          //https://api.foursquare.com/v2/venues/search?ll=40.7,-74&client_id=CLIENT_ID&client_secret=CLIENT_SECRET&v=YYYYMMDD
        public const string CLIENT_ID = "NQZZ4H3SUHDRW0PNJSCKWGQ3N51MMTA1K2F2LFUBFB3K4OGP";
        public const string CLIENT_SECRET = "10ZDK4ZGVRKN5JFGEWJRYA5QTNI1MLHIRXN3SBZJX0S1XUCT";
    }
}
