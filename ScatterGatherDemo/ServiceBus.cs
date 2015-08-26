using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScatterGatherDemo
{
    public class ServiceBus
    {
        public static readonly ServiceBus Instance = new ServiceBus();

        public async Task<TAggregation> Aggregate<TServiceRequest, TServiceResponse, TAggregation>(TServiceRequest command)
            where TServiceRequest : IServiceRequest
            where TServiceResponse : IServiceResponse
        {
            var aggregatorType = GetTypesImplementing(typeof (IServiceAggregation<TAggregation, TServiceResponse>)).Single();
            var aggregator = (IServiceAggregation<TAggregation, TServiceResponse>) Activator.CreateInstance(aggregatorType);

            var serviceTasks = GetTypesImplementing(typeof (IService<TServiceRequest, TServiceResponse>))
                .Select(t => (IService<TServiceRequest, TServiceResponse>) Activator.CreateInstance(t))
                .Select(s => s.Execute(command));

            //Task.WaitAll(serviceTasks);

            return await aggregator.Aggregate(serviceTasks);
        }

        private IEnumerable<Type> GetTypesImplementing(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Contains(type)));
        }
    }
}