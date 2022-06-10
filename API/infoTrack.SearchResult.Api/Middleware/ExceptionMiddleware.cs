using System.Net;
using System.Text.Json;

namespace infoTrack.SearchResult.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (e)
                {
                    case KeyNotFoundException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                //logging is needed here, need to wire up logger
                var result = JsonSerializer.Serialize(new { message = e?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
