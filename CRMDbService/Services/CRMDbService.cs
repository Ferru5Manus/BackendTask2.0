using CRMDbService.Database;
using Grpc.Core;

namespace CRMDbService.Services;

public class CRMDbService : CRMDb.CRMDbBase
{
    private ApplicationsManager _applicationsManager;

    public CRMDbService(ApplicationsManager applicationsManager)
    {
        _applicationsManager = applicationsManager;
    }
    public override async Task<ApplicationResponse> AddApplication(AddApplicationReq request, ServerCallContext context)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return await _applicationsManager.AddApplication(new Models.Application()
        {
            ApplicationNumber = request.ApplicationNumber,
            BeginDate = start.AddMilliseconds(request.BeginDate),
            AbonentNumber = request.AbonentNumber,
            PersonalAccount = request.PersonalAccount,
            Service = new Models.Service()
            {
                AbonentNumber = request.Service.AbonentNumber,
                ServiceName = request.Service.ServiceName,
                ConnectionDate = start.AddMilliseconds(request.Service.ConnectionDate),
                ServiceType = request.Service.ServiceType,
                ServiceView = request.Service.ServiceView,
                Status = request.Service.Status
                
            },
            EquipmentType = request.EquipmentType,
            TechnicalSupportCall = new Models.TechnicalSupportCall()
            {
                CallDate = start.AddMilliseconds(request.TechnicalSupportCall.CallDate),
                ProblemType = request.TechnicalSupportCall.ProblemType,
                ProblemDescription = request.TechnicalSupportCall.ProblemDescription,
                AbonentNumber = request.TechnicalSupportCall.AbonentNumber 
            },
            CloseDate = request.CloseDate
            
        });
    }

    public override async Task<ApplicationList> GetAllApplications(EmptyRequest request, ServerCallContext context)
    {
        List<Application> applications = await _applicationsManager.GetApplications();
        var result = new ApplicationList();
        result.Applications.AddRange(applications);
        
        return result;
    }

    public override async Task<ServiceList> GetAllServices(EmptyRequest request, ServerCallContext context)
    {
        List<Service> services = await _applicationsManager.GetServices();
        var result = new ServiceList();
        result.Services.AddRange(services);
        
        return result;
    }

    public override async Task<TechnicalSupportCallList> GetAllTechnicalSupportCalls(EmptyRequest request, ServerCallContext context)
    {
        List<TechnicalSupportCall> technicalSupportCalls = await _applicationsManager.GetTechnicalSupportCalls();
        var result = new TechnicalSupportCallList();
        result.TechnicalSupportCalls.AddRange(technicalSupportCalls);
        
        return result;
    }
}