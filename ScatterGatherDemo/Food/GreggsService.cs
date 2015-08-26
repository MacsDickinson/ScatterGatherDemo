using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScatterGatherDemo.Food
{
    public class GreggsService : IService<ClosestEateryRequest, ClosestEateryResponse>
    {
        public async Task<ClosestEateryResponse> Execute(ClosestEateryRequest request)
        {
            var encodedPostcode = request.Postcode.Replace(" ", "+");
            var html = await HttpHelper.GetHtmlString($"https://www.greggs.co.uk/home/shop-finder/?Distance=5&Location={encodedPostcode}&action_go=Go&Position=&Bounds=&NotIn=");

            var marker = "<div id=\"store_results\">";
            var closeLocationTag = "</a>";
            var openLocationTag = "class=\"show_map_info\" href=\"#\">";
            var closeDistanceTag = "</div>";
            var openDistanceTag = "<div class=\"twelve columns locationDetails\">";

            var foundPos = html.IndexOf(marker);

            if (foundPos < 0)
            {
                return default(ClosestEateryResponse);
            }


            var location = html.SubstringBetweenStrings(openLocationTag, closeLocationTag, foundPos);
            var distanceText = html.SubstringBetweenStrings(openDistanceTag, closeDistanceTag, foundPos);
            var postcode = await PostCodeService.GetLatLong(request.Postcode);
            var value =
                Regex.Match(distanceText,
                    "(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKPSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})")
                    .Groups[2].Value;
            var distancePostcode = await PostCodeService.GetLatLong(value);
            var distance = DistanceCalculator.GetDistance(postcode, distancePostcode);

            return new ClosestEateryResponse
            {
                Service = GetType().Name,
                LocationName = $"Greggs - {location}",
                Distance = distance
            };
        }
    }
}