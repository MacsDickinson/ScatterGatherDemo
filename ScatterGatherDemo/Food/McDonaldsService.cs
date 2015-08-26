using System.Threading.Tasks;

namespace ScatterGatherDemo.Food
{
    public class McDonaldsService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public async Task<ClosestEateryResponse> Execute(ClosestEateryRequest request)
        {
            var postcode = await PostCodeService.GetLatLong(request.Postcode);
            var stores = await HttpHelper.GetJson($"http://www2.mcdonalds.co.uk/googleapps/GoogleSearchAction.do?method=searchLocation&searchTxtLatlng=({postcode.Latitude}%2C%20{postcode.Longitude})&actionType=filterRestaurant&&language=en&country=uk");

            return new ClosestEateryResponse
            {
                Service = GetType().Name,
                LocationName = stores.results[0].name.ToString(),
                Distance = DistanceCalculator.GetDistance(postcode.Latitude, postcode.Longitude, float.Parse(stores.results[0].latitude.ToString()), float.Parse(stores.results[0].longitude.ToString()))
            };
        }
    }
}