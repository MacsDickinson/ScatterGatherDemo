using System;

namespace ScatterGatherDemo
{
    public static class DistanceCalculator
    {
        public static float GetDistance(float latitude, float longitude, float destLatitude, float destLongitude)
        {
            var EarthRadius = 6371; // Radius of the earth in km
            var dLat = DegreeToRadius(latitude - destLatitude);
            var dLon = DegreeToRadius(longitude - destLongitude);
            var a =
                Math.Sin(dLat/2)*Math.Sin(dLat/2) +
                Math.Cos(DegreeToRadius(destLatitude))*Math.Cos(DegreeToRadius(latitude))*
                Math.Sin(dLon/2)*Math.Sin(dLon/2)
                ;
            var c = 2*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (float) ((EarthRadius*c)/1.6); // Distance in miles
        }

        public static float GetDistance(PostCode postcode, PostCode distancePostcode)
        {
            return GetDistance(postcode.Latitude, postcode.Longitude, distancePostcode.Latitude, distancePostcode.Longitude);
        }

        private static double DegreeToRadius(float degree)
        {
            return degree*(Math.PI/180);
        }
    }
}