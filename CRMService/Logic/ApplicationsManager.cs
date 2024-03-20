using CRMDbService.Communicators;
using CRMService.Services;
using Application = CRMService.Services.Application;
using Service = CRMService.Services.Service;

namespace CRMService.Logic;

public class ApplicationsManager
{
    public async Task<AddApplicationReq> AddApplication(AddApplicationRequest request)
    {
        var result = new AddApplicationReq();
        if (DateTime.Now.Day.ToString().Length < 2)
        {
            if (DateTime.Now.Month.ToString().Length < 2)
            {
                result.ApplicationNumber = request.PersonalAccount + "0" + DateTime.Now.Day + "0" + DateTime.Now.Month +
                                           DateTime.Now.Year;
            }

            result.ApplicationNumber = request.PersonalAccount + "0" + DateTime.Now.Day + DateTime.Now.Month +
                                       DateTime.Now.Year;
        }

        result.ApplicationNumber = request.PersonalAccount + DateTime.Now.Day+DateTime.Now.Month+DateTime.Now.Year;
        result.AbonentNumber = request.AbonentNumber;
        result.BeginDate = Convert.ToInt64(DateTime.Now);
        result.CloseDate = 0;
        result.EquipmentType = request.EquipmentType;
        result.PersonalAccount = request.PersonalAccount;
        if (await CheckService(request.Service))
        {
            result.Service = new AddServiceReq()
            {
                AbonentNumber = request.Service.AbonentNumber,
                ServiceName = request.Service.ServiceName,
                ConnectionDate = Convert.ToInt64(DateTime.Now),
                ServiceType = request.Service.ServiceType,
                ServiceView = request.Service.ServiceView,
                Status = request.Service.Status
            };
            result.TechnicalSupportCall = new addTechnicalSupportCallReq()
            {
                AbonentNumber = request.TechnicalSupportCall.AbonentNumber,
                CallDate = Convert.ToInt64(DateTime.Now),
                ProblemType = request.TechnicalSupportCall.ProblemType
            };
            return result;
        }

        return null;

    }

    private async Task<bool> CheckService(AddServiceRequest serviceRequest)
    {
        if (serviceRequest.ServiceView != "Подключение"
            || serviceRequest.ServiceView != "Управление договором/контактными данными"
            || serviceRequest.ServiceView != "Управление тарифом/услугой"
            || serviceRequest.ServiceView != "Диагностика и настройка оборудования/подключения"
            || serviceRequest.ServiceView != "Оплата услуг")
        {
            return false;
        }

        if (serviceRequest.ServiceView == "Подключение")
        {
            if (serviceRequest.ServiceType != "Подключение услуг с новой инфраструктурой"
                || serviceRequest.ServiceType != "Подключение услуг на существующей инфраструктуре")
            {
                return false;
            }  
        }

        if (serviceRequest.ServiceView == "Управление договором/контактными данными")
        {
            if (serviceRequest.ServiceType != "Изменение условий договора"
                || serviceRequest.ServiceType != "Включение в договор дополнительной услуги"
                || serviceRequest.ServiceType != "Изменение контактных данных")
            {
                return false;
            }
               
        }

        if (serviceRequest.ServiceView == "Управление тарифом/услугой")
        {
            if (serviceRequest.ServiceType != "Изменение тарифа" 
                || serviceRequest.ServiceType != "Изменение адреса предоставления услуг"
                || serviceRequest.ServiceType != "Отключение услуги"
                || serviceRequest.ServiceType != "Приостановка предоставления услуги")
            {
                return false;
            }
            
        }

        if (serviceRequest.ServiceView == "Диагностика и настройка оборудования/подключения")
        {
            if (serviceRequest.ServiceType != "Нет доступа к услуге"
                || serviceRequest.ServiceType != "Разрыв соединения"
                || serviceRequest.ServiceType != "Низкая скорость соединения")
            {
                return false;
            }
            
        }

        if (serviceRequest.ServiceView == "Оплата услуг")
        {
            if (serviceRequest.ServiceType != "Выписка по платежам"
                || serviceRequest.ServiceType != "Информация о платежах"
                || serviceRequest.ServiceType != "Получение квитанции на оплату услуги")
            {
                return false;
            }
            
        }

        return true;
    }
}