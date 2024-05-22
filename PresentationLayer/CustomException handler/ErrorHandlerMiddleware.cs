using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace BusinessLayer.CustomException_handler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                _logger.LogError(error, $"An error occurred during request processing (Status Code: {context.Response.StatusCode})");

                switch (error)
                {

                    case ApplicationException e: // Custom application error
                        _logger.LogError(e, "Custom application error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e: // Not found error
                        _logger.LogError(e, "Key not found error occurred.");
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentException e: // Invalid argument error
                        _logger.LogError(e, "Invalid argument error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InvalidOperationException e: // Invalid operation error
                        _logger.LogError(e, "Invalid operation error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case HttpRequestException e: // HTTP request error
                        _logger.LogError(e, "HTTP request error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        break;
                    case NotImplementedException e: // Not implemented error
                        _logger.LogError(e, "Not implemented error occurred.");
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break;
                    case UnauthorizedAccessException e: // Unauthorized access error
                        _logger.LogError(e, "Unauthorized access error occurred.");
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case FormatException e: // Format error
                        _logger.LogError(e, "Format error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case TimeoutException e: // Timeout error
                        _logger.LogError(e, "Timeout error occurred.");
                        response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                        break;
                    case ValidationException e: // Validation error
                        _logger.LogError(e, "Validation error occurred.");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    // Add more cases as needed...

                    default:
                        _logger.LogError(error, "An unexpected error occurred.");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }

    }
}
