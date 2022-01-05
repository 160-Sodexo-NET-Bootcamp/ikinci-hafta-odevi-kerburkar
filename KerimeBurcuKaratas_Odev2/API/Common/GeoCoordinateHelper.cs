using API.Model;
using Data.DataModel;
using GeoCoordinatePortable;
using System;

namespace API.Common
{
 
    public static class GeoCoordinateHelper
    {
        //Container uzaklıkları GeoCoordinate kütüphanesine kullarak hesaplatıldı.
       
        public static double CalculateDistance(ContainerDistanceModel container)
        {
            var startLat = 0;
            var startLong = 0;
            var locA = new GeoCoordinate(startLat, startLong);
            var locB = new GeoCoordinate(Decimal.ToDouble(container.Latitude), Decimal.ToDouble(container.Longitude));
            double distance = locA.GetDistanceTo(locB);
            return distance; //distance metre olarak hesaplandı.
        }
    }
}
