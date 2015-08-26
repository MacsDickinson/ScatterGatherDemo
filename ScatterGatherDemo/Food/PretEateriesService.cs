using System.Threading.Tasks;

namespace ScatterGatherDemo.Food
{
    public class PretEateriesService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public async Task<ClosestEateryResponse> Execute(ClosestEateryRequest request)
        {
            var encodedPostcode = request.Postcode.Replace(" ", "%20");
            var output = await HttpHelper.GetHtmlString($"http://www.pret.co.uk/en-gb/find-a-pret/{encodedPostcode}");

            var marker = "<div class=\"panel-heading\">";
            var closeLocationTag = "</h3>";
            var openLocationTag = "<h3>";
            var closeDistanceTag = " Km away</span>";
            var openDistanceTag = "<span class=\"destination\">Approximately ";

            var foundPos = output.IndexOf(marker);

            if (foundPos < 0)
            {
                return null;
            }

            var location = output.SubstringBetweenStrings(openLocationTag, closeLocationTag, foundPos);
            var distance = output.SubstringBetweenStrings(openDistanceTag, closeDistanceTag, foundPos);

            return new ClosestEateryResponse
            {
                Service = GetType().Name,
                Distance = float.Parse(distance),
                LocationName = $"Pret-a-Manger - {location}"
            };
        }
    }
}