using AbonentManagementService.Communicators;
using Grpc.Core;

namespace AbonentManagementService.Services;

public class AbonentsManagementService : AbonentsManager.AbonentsManagerBase
{
    private AbonentsDbCommunicator _communicator;
    public AbonentsManagementService(AbonentsDbCommunicator communicator)
    {
        _communicator = communicator;
    }
    public async override Task<AbonentsReply> GetAbonent(AbonentRequest request, ServerCallContext context)
    {
        return await _communicator.GetAbonents();
    }
    
    public async override Task<AbonentByNumberReply> GetAbonentByPhoneNumber(AbonentByNumberRequest request,
        ServerCallContext context)
    {
        return await _communicator.GetAbonentsByNumber(request.AbonentNumber);
    }
 
}