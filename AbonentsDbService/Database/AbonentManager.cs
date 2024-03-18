using AbonentsDbService.Services;
using Microsoft.EntityFrameworkCore;

namespace AbonentsDbService.Database;

public class AbonentManager
{
    public async Task<List<Abonent>> GetAbonents()
    {
        using (ApplicationContext  db = new ApplicationContext())
        {
            return await ConvertList(await db.Abonents.ToListAsync());
        }
    }

    private async Task<List<Abonent>> ConvertList(List<Models.Abonent> abonents)
    {
        List<Abonent> abonentList = new List<Abonent>();
        foreach (var VARIABLE in abonents)
        {
            abonentList.Add(new Abonent()
            {
                AbonentNumber = VARIABLE.AbonentNumber,
                FirstName = VARIABLE.FirstName,
                LastName = VARIABLE.LastName,
                Patronymic = VARIABLE.Patronymic,
                PassportSeries = VARIABLE.PassportSeries,
                ContractNumber = VARIABLE.ContractNumber,
                PersonalAccount = VARIABLE.PersonalAccount,
                Address = VARIABLE.Address,
                Contract = new Contract()
                {
                    ContractNumber = VARIABLE.Contract.ContractNumber,
                    SigningDate = Convert.ToInt64(VARIABLE.Contract.SigningDate),
                    ContractType = VARIABLE.Contract.ContractType,
                    ClosingDate = Convert.ToInt64(VARIABLE.Contract.ClosingDate),
                    ClosingReason = VARIABLE.Contract.ClosingReason
                },
                Passport = new Passport()
                {
                    PassportSeries = VARIABLE.Passport.PassportSeries,
                    PassportNumber = VARIABLE.Passport.PassportNumber,
                    Issuer = VARIABLE.Passport.Issuer
                }
                
            });
        }
        return abonentList;
    }
}