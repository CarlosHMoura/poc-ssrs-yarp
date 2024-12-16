using System.Net;
using Yarp.ReverseProxy.Configuration;

namespace POC_SSRS_YARP;

public static class SSRSYarpExtension
{
    public static void AddSSRSYarp(this IServiceCollection services, IConfiguration configuration)
    {

        var reportsRouteConfig = new RouteConfig
        {
            RouteId = "Reports",
            ClusterId = "cluster1",
            Match = new RouteMatch
            {
                Path = "{**catch-all}"
            }
        };

        var clusterConfigs = new ClusterConfig
        {
            ClusterId = "cluster1",
            Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "internal", new DestinationConfig
                    {
                        Address = configuration.GetValue<string>("ReportServerURL")
                    }
                }
            }
        };

        services.AddReverseProxy()
            .ConfigureHttpClient((_, handler) => { handler.Credentials = CredentialCache.DefaultNetworkCredentials; })
            .LoadFromMemory(new[] { reportsRouteConfig }, new[] { clusterConfigs });
    }
}
