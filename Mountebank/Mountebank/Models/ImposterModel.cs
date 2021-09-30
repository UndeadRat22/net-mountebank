namespace Mountebank.Models
{
    public record ImposterModel(
        int Port,
        string Name,
        int NumberOfRequests,
        RequestModel[] Requests,
        StubModel[] Stubs);
}