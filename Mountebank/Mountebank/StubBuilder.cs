using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Mountebank.Configuration;
using Mountebank.Models;

namespace Mountebank
{
    public class StubBuilder : IStubResponseConfigurator
    {
        private readonly StubModel _value = new();
        private readonly int _port;
        private readonly string _api;

        public StubBuilder(string api, int port, string route, HttpVerb verb)
        {
            _api = api;
            _port = port;

            var predicate = new Predicate
            {
                DeepEquals = new DeepEquals
                {
                    Method = verb.ToString().ToUpper(),
                    Path = route
                }
            };

            _value.Predicates.Add(predicate);
        }

        public async Task RespondWith<T>(T payload, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new Response
            {
                Is = new Is
                {
                    StatusCode = (int)status,
                    Body = payload
                }
            };
            _value.Responses.Add(response);

            await CreateOrUpdate();
        }

        public async Task CreateOrUpdate()
        {
            var config = _value.Predicates[0].DeepEquals;
            var imposter = await ApiPaths.Imposter(_api, _port).GetJsonAsync<ImposterModel>();

            var existingStub = imposter.Stubs
                .Select((s, index) => new {s, index})
                .FirstOrDefault(pair => pair.s.Predicates
                    .Any(predicate => 
                        predicate.DeepEquals.Method == config.Method && 
                        predicate.DeepEquals.Path == config.Path));

            if (existingStub is not null)
            {
                await ApiPaths.Stub(_api, _port, existingStub.index).DeleteAsync();
            }

            var request = new PostStubRequest
            {
                Stub = _value
            };
            
            await ApiPaths.Stubs(_api, _port).PostJsonAsync(request);
        }
    }
}