using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScatterGatherDemo
{
    public interface IServiceRequest
    {
    }

    public interface IServiceResponse
    {
    }

    public interface IService<in TRequest, TResponse>
        where TRequest : IServiceRequest
        where TResponse : IServiceResponse
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface IServiceAggregation<TAggregate, TServiceResponse>
        where TServiceResponse : IServiceResponse
    {
        Task<TAggregate> Aggregate(IEnumerable<Task<TServiceResponse>> responses);
    }
}
