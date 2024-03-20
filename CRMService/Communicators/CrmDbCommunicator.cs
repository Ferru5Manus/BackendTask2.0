using CRMDbService.Communicators;
using Grpc.Net.Client;
using Service = CRMService.Services.Service;
using TechnicalSupportCall = CRMService.Services.TechnicalSupportCall;

namespace CRMService.Communicators;

public class CrmDbCommunicator
{
    private readonly CRMDb.CRMDbClient _crmDbClient;

    public CrmDbCommunicator()
    {
        GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("ABONENTS_DB_URL"));
        _crmDbClient = new CRMDb.CRMDbClient(channel);
    }

    public async Task<Services.ApplicationList> GetApplications()
    {
        var applications = await _crmDbClient.GetAllApplicationsAsync(new EmptyRequest());
        var result = new Services.ApplicationList();
        result.Applications.AddRange(await ConvertToApplicationList(applications.Applications.ToList()));
        return result;
    }

    public async Task<Services.TechnicalSupportCallList> GetTechnicalSupportCalls()
    {
        var technicalSupportCalls = await _crmDbClient.GetAllTechnicalSupportCallsAsync(new EmptyRequest());
        var result = new Services.TechnicalSupportCallList();
        result.TechnicalSupportCalls.AddRange(await ConvertToTechnicalSupportCallList(technicalSupportCalls.TechnicalSupportCalls.ToList()));
        return result;
        
    }

    public async Task<Services.ServiceList> GetServices()
    {
        var services = await _crmDbClient.GetAllServicesAsync(new EmptyRequest());
        var result = new Services.ServiceList();
        result.Services.AddRange(await ConvertToServiceList(services.Services.ToList()));
        return result;
        
    }

    public async Task<Services.ApplicationResponse> CreateApplication(AddApplicationReq request)
    {
        var response = await _crmDbClient.AddApplicationAsync(request);
        var result = new Services.ApplicationResponse()
        {
            Error = response.Error,
            Success = response.Success
        };
        return result;
    }
    private async Task<List<Services.Service>> ConvertToServiceList(List<CRMDbService.Communicators.Service> services)
    {
        var x = new List<Services.Service>();
        foreach (var VARIABLE in services)
        {
            x.Add(new Services.Service()
            {
                ServiceName = VARIABLE.ServiceName,
                AbonentNumber = VARIABLE.AbonentNumber,
                ConnectionDate = VARIABLE.ConnectionDate,
                ServiceId = VARIABLE.ServiceId,
                ServiceType = VARIABLE.ServiceType,
                ServiceView = VARIABLE.ServiceView,
                Status = VARIABLE.Status
                
            });
        }

        return x;
    }
    private async Task<List<Services.TechnicalSupportCall>> ConvertToTechnicalSupportCallList(
        List<CRMDbService.Communicators.TechnicalSupportCall> technicalSupportCalls)
    {
        var x = new List<Services.TechnicalSupportCall>();
        foreach (var VARIABLE in technicalSupportCalls)
        {
            x.Add(new Services.TechnicalSupportCall()
            {
                AbonentNumber = VARIABLE.AbonentNumber,
                CallDate = VARIABLE.CallDate,
                ProblemType = VARIABLE.ProblemType,
                ProblemDescription = VARIABLE.ProblemDescription,
                TechnicalSupportCallID = VARIABLE.TechnicalSupportCallID
            });
        }

        return x;
    }
    private async Task<List<Services.Application>> ConvertToApplicationList(List<Application> applications)
    {
        var x = new List<Services.Application>();
        foreach (var VARIABLE in applications)
        {
          x.Add(new Services.Application()
          {
             AbonentNumber = VARIABLE.AbonentNumber,
             ApplicationNumber = VARIABLE.ApplicationNumber,
             BeginDate = VARIABLE.BeginDate,
             CloseDate = VARIABLE.CloseDate,
             EquipmentType = VARIABLE.EquipmentType,
             Service = new Service()
             {
                 ServiceName = VARIABLE.Service.ServiceName,
                 AbonentNumber = VARIABLE.Service.AbonentNumber,
                 ConnectionDate = VARIABLE.Service.ConnectionDate,
                 ServiceId = VARIABLE.Service.ServiceId,
                 ServiceType = VARIABLE.Service.ServiceType,
                 ServiceView = VARIABLE.Service.ServiceView,
                 Status = VARIABLE.Service.Status
             },
             TechnicalSupportCall = new TechnicalSupportCall()
             {
                 AbonentNumber = VARIABLE.TechnicalSupportCall.AbonentNumber,
                 CallDate = VARIABLE.TechnicalSupportCall.CallDate,
                 ProblemType = VARIABLE.TechnicalSupportCall.ProblemType,
                 ProblemDescription = VARIABLE.TechnicalSupportCall.ProblemDescription,
                 TechnicalSupportCallID = VARIABLE.TechnicalSupportCall.TechnicalSupportCallID
             },
             PersonalAccount = VARIABLE.PersonalAccount
              
              
          });
        }
        return x;
    }

}