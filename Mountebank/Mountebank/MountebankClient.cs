using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Mountebank.Configuration;
using Mountebank.Models;

namespace Mountebank
{
    public class MountebankClient : IMountebankClient
    {
        private readonly string _mountebankUrl;
        private readonly Dictionary<int, Imposter> _imposters = new();

        public MountebankClient(string mountebankUrl)
        {
            _mountebankUrl = mountebankUrl;
        }

        public async Task<IImposterConfigurator> ConfigureImposterAsync(int port, string name)
        {
            await LoadExistingImposterDefinitions();

            var imposter = _imposters.GetValueOrDefault(port);
            if (imposter is not null)
            {
                return imposter;
            }

            imposter = await Imposter.StartNew(_mountebankUrl, port, name);
            _imposters[port] = imposter;

            return imposter;
        }

        public Task<ImposterModel> GetImposter(int port) => ApiPaths.Imposter(_mountebankUrl, port)
            .GetJsonAsync<ImposterModel>();

        public Task DeleteImposters() => ApiPaths.Imposters(_mountebankUrl).DeleteAsync();

        private async Task LoadExistingImposterDefinitions()
        {
            var imposters = await ApiPaths.Imposters(_mountebankUrl).GetJsonAsync<ImposterListModel>();

            foreach (var imposter in imposters.Imposters)
            {
                _imposters[imposter.Port] = new Imposter(_mountebankUrl, imposter.Port, imposter.Name);
            }
        }
    }
}