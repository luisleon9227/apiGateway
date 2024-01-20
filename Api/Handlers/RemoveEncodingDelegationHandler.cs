namespace Api.Handlers
{
    public class RemoveEncodingDelegationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.AcceptEncoding.Clear();
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
