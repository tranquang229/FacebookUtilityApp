using FM.Application.Constants;
using FM.Application.Wrappers.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;
using System.Text.Json;

namespace FM.API.Middlewares
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (!policyAuthorizationResult.Succeeded || Show401ForForbiddenResult(policyAuthorizationResult))
            {
                var responseModel = new ResponseFail<bool?>(
                    (int)HttpStatusCode.Unauthorized,
                    AppConstant.UNAUTHORIZED,
                    null,
                    new List<string> { AppConstant.UNAUTHORIZED });

                var result = JsonSerializer.Serialize(responseModel);

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(result);

                return;
            }

            await _defaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }

        private bool Show401ForForbiddenResult(PolicyAuthorizationResult policyAuthorizationResult)
        {
            return policyAuthorizationResult.Forbidden ||
                   (policyAuthorizationResult.AuthorizationFailure != null && policyAuthorizationResult.AuthorizationFailure.FailedRequirements.Any());
        }
    }
}