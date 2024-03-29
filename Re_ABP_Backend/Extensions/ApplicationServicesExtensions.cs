﻿using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Data.Repositories;
using Re_ABP_Backend.Errors;
using Microsoft.AspNetCore.Mvc;
using Re_ABP_Backend.Data.Services;

namespace Re_ABP_Backend.Exntensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services
            , IConfiguration config)
        {

            services.AddHttpContextAccessor();

            services.AddScoped<IAudioBookService, AudioBookService>();
            services.AddScoped<IUserLibraryService, UserLibraryService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // ErrorsHandler
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
