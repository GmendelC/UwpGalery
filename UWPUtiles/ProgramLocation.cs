using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Devices.Geolocation;

namespace UWPUtiles
{
    public class ProgramLocation
    {
        public string TextLocation { get; private set; }

        private static ProgramLocation _isntance;

        private Geolocator _geolocator;

        private ProgramLocation()
        {
            InitGeolocation();
        }
        public static ProgramLocation Create()
        {
            if( _isntance == null )
                _isntance = new ProgramLocation();
            return _isntance;
        }

        private async Task InitGeolocation()
        {
            var checkAccess = await Geolocator.RequestAccessAsync();

            if (checkAccess != GeolocationAccessStatus.Denied)
            {
                initGeoocator();

                var location = await _geolocator.GetGeopositionAsync();
                var point = location.Coordinate.Point.Position;
                var res = await GetLocationName(point.Latitude.ToString(), point.Longitude.ToString());
                TextLocation = res;

                _geolocator.PositionChanged += Geo_PositionChanged;
            }
        }

        private void initGeoocator()
        {
            _geolocator = new Geolocator();
            _geolocator.DesiredAccuracy = PositionAccuracy.High;
            _geolocator.DesiredAccuracyInMeters = 5;

            _geolocator.ReportInterval = 3000;
            _geolocator.MovementThreshold = 5;
        }

        private async void Geo_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var point = args.Position.Coordinate.Point.Position;

            var res = await GetLocationName(point.Latitude.ToString(), point.Longitude.ToString());

           TextLocation = res;
                        
        }

        public async Task<string> GetLocationName(string latitude, string longitude)
        {
            //use google api to get location
            string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";

            string location = string.Empty;
            string requestUri = string.Format(baseUri, latitude, longitude);

            // use web service to get location
            using (HttpClient wc = new HttpClient())
            {
                string result = await wc.GetStringAsync(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants()
                              where elm.Name == "status"
                              select elm).FirstOrDefault();

                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    requestUri = res.Value;
                }

            }
            return requestUri;
        }
    }
}
