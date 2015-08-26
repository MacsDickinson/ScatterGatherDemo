namespace ScatterGatherDemo.Food
{
    public class ClosestEateryResponse : IServiceResponse
    {
        public string Service { get; set; }
        public float Distance { get; set; }
        public string LocationName { get; set; }
    }
}