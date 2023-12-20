namespace NetCoreApiTokenAuth.Helpers
{
    public class IPFilterMiddleware : IMiddleware
    {
        private readonly List<string> _allowedIPs;
        public IPFilterMiddleware(List<string> allowedIPs)
        {
            _allowedIPs = allowedIPs;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // burada ip leri sql den çekebilirsin 
            var ipAddress = context.Connection.RemoteIpAddress;
            if(ipAddress == null) { await next(context); }
            string ip = ipAddress.ToString();
            var varmi = _allowedIPs.Where(x=>x.Equals(ip.ToString())).FirstOrDefault();
            if (varmi==null)
            {
                context.Response.StatusCode = 403; // Erişim reddedildi
                await context.Response.WriteAsync(ip.ToString() + " ip izniniz yok !");
                return;
            }
            await next(context);
        }
    }
}
