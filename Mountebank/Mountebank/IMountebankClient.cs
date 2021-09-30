using System.Threading.Tasks;
using Mountebank.Models;

namespace Mountebank
{
    public interface IMountebankClient
    {
        Task<IImposterConfigurator> ConfigureImposterAsync(int port, string name);
        Task<ImposterModel> GetImposter(int port);
        Task DeleteImposters();
    }
}