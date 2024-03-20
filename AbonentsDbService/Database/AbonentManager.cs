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

    public async Task<Abonent> GetAbonentByNumber(string abonentNumber)
    {
        using (ApplicationContext  db = new ApplicationContext())
        {
            var abonent = await db.Abonents.FirstOrDefaultAsync(x => x.AbonentNumber == abonentNumber);
            return new Abonent()
            {
                AbonentNumber = abonent.AbonentNumber,
                FirstName = abonent.FirstName,
                LastName = abonent.LastName,
                Patronymic = abonent.Patronymic,
                PassportSeries = abonent.PassportSeries,
                ContractNumber = abonent.ContractNumber,
                PersonalAccount = abonent.PersonalAccount,
                Address = abonent.Address,
                Contract = new Contract()
                {
                    ContractNumber = abonent.Contract.ContractNumber,
                    SigningDate = Convert.ToInt64(abonent.Contract.SigningDate),
                    ContractType = abonent.Contract.ContractType,
                    ClosingDate = Convert.ToInt64(abonent.Contract.ClosingDate),
                    ClosingReason = abonent.Contract.ClosingReason
                },
                Passport = new Passport()
                {
                    PassportSeries = abonent.Passport.PassportSeries,
                    PassportNumber = abonent.Passport.PassportNumber,
                    Issuer = abonent.Passport.Issuer
                },
                PhoneNumber = abonent.PhoneNumber
            };
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
                },
                PhoneNumber = VARIABLE.PhoneNumber
            });
        }
        return abonentList;
    }
}