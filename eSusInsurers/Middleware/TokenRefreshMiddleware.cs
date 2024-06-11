using eSusInsurers.Common.Exceptions;
using eSusInsurers.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace eSusInsurers.Middleware
{
    /// <summary>
    /// Middleware logic for refreshing the token.
    /// </summary>
    public class TokenRefreshMiddleware(IUserService userService, IHttpContextAccessor httpContextAccessor) : IMiddleware
    {
        /// <summary>
        /// Method to read the token and update.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var token = GetTokenFromRequest(context);

                if (!string.IsNullOrEmpty(token))
                {
                    bool tokenNeedsRefresh = TokenNeedsRefresh(context);

                    if (tokenNeedsRefresh)
                    {
                        // Call the token service to refresh the token
                        //var response = await userService.RefreshTokenAsync(token, httpContextAccessor, new CancellationToken());

                        // Set the refreshed token in the response header
                        context.Response.Headers.Append("Access-Control-Expose-Headers", "Authorization");
                        context.Response.Headers.Append("Access-Control-Expose-Headers", "X-Token-Refreshed");
                        //context.Response.Headers.Append("Authorization", response.Token);
                        context.Response.Headers.Append("X-Token-Refreshed", true.ToString());

                    }
                }
            }
            catch (UnauthorizedException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Unauthorized: Invalid or expired token.");
                return;
            }

            await next(context);

        }

        private bool TokenNeedsRefresh(HttpContext context)
        {
            // Get the current token and its expiration time from the request
            string currentToken = GetTokenFromRequest(context);
            DateTime expirationTime = GetTokenExpirationTime(currentToken);

            // Check if the token expiration is within 5 minutes from now
            TimeSpan remainingValidity = expirationTime - DateTime.UtcNow;

            if (remainingValidity.TotalSeconds <= 0)
            {
                throw new UnauthorizedException();
            }

            return remainingValidity.TotalMinutes < 5;
        }

        private string GetTokenFromRequest(HttpContext context)
        {
            return context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        private DateTime GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokens = handler.ReadToken(token) as JwtSecurityToken;
            DateTime expirationTime = tokens.ValidTo;
            return expirationTime;
        }
    }
}
