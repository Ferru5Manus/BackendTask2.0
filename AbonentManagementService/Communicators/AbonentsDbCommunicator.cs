using AbonentsDbService.Services;
using Grpc.Net.Client;

namespace AbonentManagementService.Communicators;

public class AbonentsDbCommunicator
{
    private readonly Abonents.AbonentsClient _abonentsClient;
    public AbonentsDbCommunicator()
    {
        GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("ABONENTS_DB_URL"));
        _abonentsClient = new Abonents.AbonentsClient(channel);
    }


    public async Task<AbonentsReply> GetAbonents()
    {
        return await _abonentsClient.GetAbonentAsync(new AbonentRequest());
    } 
}