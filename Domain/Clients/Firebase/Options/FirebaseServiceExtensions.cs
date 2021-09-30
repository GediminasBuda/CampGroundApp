using Domain.Clients.Firebase.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Clients.Firebase.Options
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
