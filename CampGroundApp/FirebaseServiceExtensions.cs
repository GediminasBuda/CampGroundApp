using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public static class FirebaseServiceExtensions
    {
        public static IServiceCollection AddFirebaseClient(this IServiceCollection services)
        {
            services.AddHttpClient<IFirebaseClient, FirebaseClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://identitytoolkit.googleapis.com/v1");

            });
            return services;
                
        }
    }
}
