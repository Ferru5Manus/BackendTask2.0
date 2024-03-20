using AbonentsDbService.Database;
using Grpc.Core;

namespace AbonentsDbService.Services;

public class AbonentsService : Abonents.AbonentsBase
{
    private readonly AbonentManager _abonentManager;

    public AbonentsService()
    {
        _abonentManager = new AbonentManager();
    }

    public async override Task<AbonentsReply> GetAbonent(AbonentRequest request, ServerCallContext context)
    {
        AbonentsReply reply = new AbonentsReply();
        var abonents = await _abonentManager.GetAbonents();
        reply.Abonents.AddRange(abonents);
        return await Task.FromResult(reply);
    }

    public async override Task<AbonentByNumberReply> GetAbonentByPhoneNumber(AbonentByNumberRequest request,
        ServerCallContext context)
    {
        AbonentByNumberReply reply = new AbonentByNumberReply();
        var abonent = await _abonentManager.GetAbonentByNumber(request.AbonentNumber);
        reply.Abonent = abonent;
        return await Task.FromResult(reply);
    }
}