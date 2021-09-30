using Mountebank.Models;

namespace Mountebank
{
    public interface IImposterConfigurator
    {
        IStubResponseConfigurator ForRequestsTo(string route, HttpVerb verb = HttpVerb.Get);
    }
}