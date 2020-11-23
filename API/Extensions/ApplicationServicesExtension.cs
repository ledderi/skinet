using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => CheckModelState(actionContext);
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
