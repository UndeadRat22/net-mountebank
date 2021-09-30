using System.Threading.Tasks;
using Flurl.Http;
using Mountebank.Configuration;
using Mountebank.Models;

namespace Mountebank
{
    public class Imposter : IImposterConfigurator
    {
        private readonly string _api;
        private readonly string _name;
        private readonly int _port;

        public Imposter(string api, int port, string name)
        {
            _api = api;
            _port = port;
            _name = name;
        }

        public static async Task<Imposter> StartNew(string api, int port, string name)
        {
            await ApiPaths.Imposters(api).PostJsonAsync(new
            {
                name,
                port,
                protocol = "http",
                recordRequests = true,
            });

            return new Imposter(api, port, name);
        }

        public IStubResponseConfigurator ForRequestsTo(string route, HttpVerb verb = HttpVerb.Get)
        {
            return new StubBuilder(_api, _port, route, verb);
        }
    }
}