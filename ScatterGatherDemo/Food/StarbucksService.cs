using System.Threading.Tasks;

namespace ScatterGatherDemo.Food
{
    public class StarbucksService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public async Task<ClosestEateryResponse> Execute(ClosestEateryRequest request)
        {
            var postcode = await PostCodeService.GetLatLong(request.Postcode);
            var stores = await HttpHelper.GetJson($"https://testhost.openapi.starbucks.com/location/v2/stores/nearest?latlng={postcode.Latitude},{postcode.Longitude}&format=json");

            return new ClosestEateryResponse
            {
                Service = GetType().Name,
                LocationName = $"Starbucks - {stores.store.name.ToString()}",
                Distance = float.Parse(stores.distance.ToString())
            };
        }
    }
}