using System.Net;

namespace ProjectPractice.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger) {
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError("Exception: {exc}", exception);

            var httpResponse = context.Response;
            httpResponse.ContentType = "application/json";

            var responseModel = new ResponseModel<object>
            {
                Succeeded = false,
                Message = exception.Message
            };

            switch (exception)
            {
                default:
                    httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Errors = new List<string?> { exception.InnerException?.Message };
                    break;
            }

            await httpResponse.WriteAsJsonAsync(responseModel);
        }
    }
}

public class ResponseModel<T>
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public List<string?>? Errors { get; set; }
}
