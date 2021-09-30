using System.Net;
using System.Threading.Tasks;

namespace Mountebank
{
    public interface IStubResponseConfigurator
    {
        Task RespondWith<T>(T payload, HttpStatusCode status = HttpStatusCode.OK);
    }
}