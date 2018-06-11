using AutofacDIExample.Resolvers;
using AzureFunctions.Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net;
using System.Net.Http;

namespace AutofacDIExample.GreeterFunction
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public class GreeterFunction
    {
        [FunctionName("GreeterFunction")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage request,
                                              TraceWriter log,
                                              [Inject]IGreeter greeter,
                                              [Inject("Main")]IGoodbyer goodbye,
                                              [Inject("Secondary")]IGoodbyer alternateGoodbye)
        {
            log.Info("C# HTTP trigger function processed a request.");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{greeter.Greet()} {goodbye.Goodbye()} or {alternateGoodbye.Goodbye()}")
            };
        }
    }
}
