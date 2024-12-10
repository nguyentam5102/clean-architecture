using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azure;
using BookManagement.Domain.Interface.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookManagement.Application.ExceptionHandler
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogService _logger;

        public ExceptionHandler(ILogService logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) 
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = exception.GetType().Name,
                Title = "Internal server error",
                Detail = "Internal server error",
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            if (exception is ValidationException)
            {
                _logger.LogError($"Validation failed");
            }

            _logger.LogError($"Error Message: {exception.Message}");
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
