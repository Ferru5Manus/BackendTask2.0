using CRMDbService.Models;
using CRMDbService.Services;
using Application = CRMDbService.Services.Application;
using Service = CRMDbService.Services.Service;
using TechnicalSupportCall = CRMDbService.Services.TechnicalSupportCall;

namespace CRMDbService.Database;

public class ApplicationsManager
{
    public async Task<List<Service>> GetServices()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return await ConvertSerivceList(db.Services.ToList());
        }
    }

    public async Task<ApplicationResponse> AddApplication(Models.Application application)
    {
        try
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = await db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await db.Applications.AddAsync(application);
                        await db.Services.AddAsync(application.Service);
                        await db.TechnicalSupportCalls.AddAsync(application.TechnicalSupportCall);
                    
                        await db.SaveChangesAsync();
                           
                        await transaction.CommitAsync();
                    }
                    catch(Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return new ApplicationResponse() {Success = false, Error = ex.Message };
                    }
                 
                    
                }
            }
        }
        catch (Exception ex)
        {
            return new ApplicationResponse() {Success = false, Error = ex.Message };
        }

        return new ApplicationResponse() {Success = false, Error = "Unexpected error" };
    }  
    public async Task<List<TechnicalSupportCall>> GetTechnicalSupportCalls()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return await ConvertTechnicalSupportCallList(db.TechnicalSupportCalls.ToList());
        }
    }

    public async Task<List<Application>> GetApplications()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return await ConvertApplicationList(db.Applications.ToList());
        }
        
    }
    private async Task<List<TechnicalSupportCall>> ConvertTechnicalSupportCallList(List<Models.TechnicalSupportCall> technicalSupportCalls)
    {
        List<TechnicalSupportCall> technicalSupportCallsList = new List<TechnicalSupportCall>();

        foreach (var VARIABLE in technicalSupportCalls)
        {
            technicalSupportCallsList.Add(new TechnicalSupportCall()
            {
                TechnicalSupportCallID = VARIABLE.TechnicalSupportCallID,
                CallDate = Convert.ToInt64(VARIABLE.CallDate),
                ProblemType = VARIABLE.ProblemType,
                ProblemDescription = VARIABLE.ProblemDescription,
                AbonentNumber = VARIABLE.AbonentNumber
            });
            
        }
        return technicalSupportCallsList;
    }
    private async Task<List<Service>> ConvertSerivceList(List<Models.Service> services)
    {
        List<Service> serviceList = new List<Service>();
        foreach (var VARIABLE in services)
        {
            serviceList.Add(new Service()
            {
                ServiceId = VARIABLE.ServiceId,
                ServiceName = VARIABLE.ServiceName,
                ConnectionDate = Convert.ToInt64(VARIABLE.ConnectionDate),
                AbonentNumber = VARIABLE.AbonentNumber,
                ServiceType = VARIABLE.ServiceType,
                ServiceView = VARIABLE.ServiceView,
                Status = VARIABLE.Status
            });
        }
        return serviceList;
    }
    private async Task<List<Application>> ConvertApplicationList(List<Models.Application> applications)
    {
        List<Application> applicationsList = new List<Application>();
        foreach (var VARIABLE in applications)
        {
            applicationsList.Add(new Application()
            {
                AbonentNumber = VARIABLE.AbonentNumber,
                Service = new Service()
                {
                    ServiceId = VARIABLE.Service.ServiceId,
                    ServiceName = VARIABLE.Service.ServiceName,
                    ConnectionDate = Convert.ToInt64(VARIABLE.Service.ConnectionDate),
                    AbonentNumber = VARIABLE.Service.AbonentNumber,
                    ServiceType = VARIABLE.Service.ServiceType,
                    ServiceView = VARIABLE.Service.ServiceView,
                    Status = VARIABLE.Service.Status
                    
                },
                BeginDate = Convert.ToInt64(VARIABLE.BeginDate),
                CloseDate = Convert.ToInt64(VARIABLE.CloseDate),
                TechnicalSupportCall = new TechnicalSupportCall()
                {
                    AbonentNumber = VARIABLE.TechnicalSupportCall.AbonentNumber,
                    CallDate = Convert.ToInt64(VARIABLE.TechnicalSupportCall.CallDate),
                    ProblemType = VARIABLE.TechnicalSupportCall.ProblemType,
                    ProblemDescription = VARIABLE.TechnicalSupportCall.ProblemDescription
                    
                },
                PersonalAccount = VARIABLE.PersonalAccount,
                ApplicationNumber = VARIABLE.ApplicationNumber,
                EquipmentType = VARIABLE.EquipmentType,
            });
        }
        return applicationsList;
        
    }
}