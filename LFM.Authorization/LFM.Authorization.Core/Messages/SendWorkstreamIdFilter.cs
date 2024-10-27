using MassTransit;
using MassTransit.Serialization;
using Microsoft.AspNetCore.Http;

namespace LFM.Authorization.Core.Messages;

public class SendWorkstreamIdFilter<T> : IFilter<SendContext<T>> where T : class
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SendWorkstreamIdFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("send-workstream-id-header");
    }

    public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
    {
        if (_httpContextAccessor.HttpContext != null 
            && _httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("workstreamId", out string workstreamId))
        {
            context.Headers.Set("workstreamId", workstreamId);
        }

        return next.Send(context);
    }
}