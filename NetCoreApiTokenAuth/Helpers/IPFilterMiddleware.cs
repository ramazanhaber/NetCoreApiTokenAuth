using NetCoreApiTokenAuth.Entities;

namespace NetCoreApiTokenAuth.Helpers
{
    public class IPFilterMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            if (ipAddress == null) { await next(context); }

            string globalip = ipAddress.ToString();

            using var contextDb = new Context();
            var ipvarmi = contextDb.IpYetki.Where(x=>x.aktif==true && x.ip== globalip).FirstOrDefault();

            if (ipvarmi==null)
            {
                context.Response.StatusCode = 403; 
                await context.Response.WriteAsync(globalip + " ip izniniz yok !");
            }

            await next(context);
        }
    }
}
