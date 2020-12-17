using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => CheckModelState(actionContext);
            });

            services.AddCors(setup =>
            {
                setup.AddPolicy("corsPolicy", conf => conf.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:4200"));
            });
        }

        private static IActionResult CheckModelState(ActionContext context)
        {
            IEnumerable<string> errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToArray();

            var errorResponse = new ApiValidationResponse { Errors = errors };
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
