using DeployApp.Application.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace DeployApp.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {     
            try
            {
                Log.Logger.Information("Starting request [{method}] {path}", context.Request.Method ,context.Request.Path);
                await next.Invoke(context);

            }
            catch (DeployAppException ex) 
            {
                context.Response.Headers.Add("content-type", "application/json");
                context.Response.StatusCode = ex.StatusCode;
                var json = JsonSerializer.Serialize(new { ErrorCode = ex.StatusCode, Message = ex.Message });
                Log.Logger.Error(
                    "ErrorCode: {statusCode} | ErrorMessage: {errorMessage} | InnerErrorMessage: {innerErrorMessage}"
                    , ex.StatusCode, ex.Message, (ex.InnerException != null ? ex.InnerException.Message : "NULL"));
                await context.Response.WriteAsync(json);
            }
            catch(Exception ex)
            {
                context.Response.Headers.Add("content-type", "application/json");
                context.Response.StatusCode = 500;
                var json = JsonSerializer.Serialize(new { ErrorCode = 500, Message ="Something went wrong" });
                Log.Logger.Error(
                    "ErrorCode: {statusCode} | ErrorMessage: {errorMessage} | InnerErrorMessage: {innerErrorMessage}"
                    , 500, ex.Message, (ex.InnerException != null ? ex.InnerException.Message : "NULL"));
                await context.Response.WriteAsync(json);
            }
        }
    }
}
