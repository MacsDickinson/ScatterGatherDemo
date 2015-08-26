using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScatterGatherDemo.Food;

namespace ScatterGatherDemo.Tests
{
    [TestClass]
    public class FoodServicesAggregationsSpecification
    {
        [TestMethod]
        public async Task Should_return_Greggs_for_LS73NU()
        {
            var closest = await ServiceBus.Instance.Aggregate<ClosestEateryRequest, ClosestEateryResponse, EateryResult>(new ClosestEateryRequest {Postcode = "LS7 3NU"});
            Assert.AreEqual("Greggs - Chapel Allerton", closest.LocationName);
            Assert.AreEqual(0.261534125F, closest.Distance);
        }

        [TestMethod]
        public async Task Check_food_near_LS73NU()
        {
            var closest = (await ServiceBus.Instance.Aggregate<ClosestEateryRequest, ClosestEateryResponse, IEnumerable<EateryResult>>(new ClosestEateryRequest { Postcode = "LS7 3NU" })).ToArray();
            Assert.AreEqual("Greggs - Chapel Allerton", closest[0].LocationName);
            Assert.AreEqual(0.261534125F, closest[0].Distance);

            Assert.AreEqual("McDonald's - Leeds 2", closest[1].LocationName);
            Assert.AreEqual(1.8438977F, closest[1].Distance);

            Assert.AreEqual("Starbucks - Leeds - The Light", closest[2].LocationName);
            Assert.AreEqual(1.89970004558563F, closest[2].Distance);

            Assert.AreEqual("Pret-a-Manger - Leeds, Lands Lane", closest[3].LocationName);
            Assert.AreEqual(3.3F, closest[3].Distance);
        }
    }
}
