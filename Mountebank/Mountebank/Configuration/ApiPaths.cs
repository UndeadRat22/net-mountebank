using Flurl;

namespace Mountebank.Configuration
{
    public static class ApiPaths
    {
        public static string Imposters(string api) => api.AppendPathSegment("imposters");
        public static string Imposter(string api, int port) => Imposters(api).AppendPathSegment(port);
        public static string Stubs(string api, int port) => Imposters(api).AppendPathSegments(port, "stubs");
        public static string Stub(string api, int port, int index) => Stubs(api, port).AppendPathSegment(index);
    }
}