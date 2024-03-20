using CRMService.Communicators;
using CRMService.Logic;
using Grpc.Core;

namespace CRMService.Services;

public class CRMService : CRM.CRMBase
{

    private readonly CrmDbCommunicator _crmDbCommunicator;
    private readonly ApplicationsManager _applicationsManager;
    public CRMService(CrmDbCommunicator crmDbCommunicator, ApplicationsManager applicationsManager)
    {
        _crmDbCommunicator = crmDbCommunicator;
        _applicationsManager = applicationsManager;
    }

    public override async Task<ApplicationResponse> AddApplication(AddApplicationRequest request, ServerCallContext context)
    {
        var result = await _applicationsManager.AddApplication(request);
        if (result!=null)
        {
            return await _crmDbCommunicator.CreateApplication(result);
        }

        return new ApplicationResponse()
        {
            Error = "Invalid fields",
            Success = false
        };
    }

    public override async Task<ApplicationList> GetAllApplications(EmptyRequest request, ServerCallContext context)
    {
        return await _crmDbCommunicator.GetApplications();
    }

    public override async Task<ServiceList> GetAllServices(EmptyRequest request, ServerCallContext context)
    {
        return await _crmDbCommunicator.GetServices();
    }

    public override async Task<TechnicalSupportCallList> GetAllTechnicalSupportCalls(EmptyRequest request, ServerCallContext context)
    {
        return await _crmDbCommunicator.GetTechnicalSupportCalls();
    }
}