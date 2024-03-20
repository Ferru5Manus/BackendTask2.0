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

    public async Task<Services.AbonentByNumberReply> GetAbonentsByNumber(string abonentNumber)
    {
        var Result = await _abonentsClient.GetAbonentByPhoneNumberAsync(new AbonentByNumberRequest
        {
            AbonentNumber = abonentNumber
        });
        return new Services.AbonentByNumberReply()
        {
            Abonent = new Services.Abonent()
            {
                AbonentNumber = Result.Abonent.AbonentNumber,
                Address = Result.Abonent.Address,
                Contract = new Services.Contract()
                {
                    ClosingDate = Result.Abonent.Contract.ClosingDate,
                    ClosingReason = Result.Abonent.Contract.ClosingReason,
                    ContractNumber = Result.Abonent.Contract.ContractNumber,
                    ContractType = Result.Abonent.Contract.ContractType,
                    SigningDate = Result.Abonent.Contract.SigningDate
                },
                PersonalAccount = Result.Abonent.PersonalAccount,
                Passport = new Services.Passport()
                {
                    Issuer = Result.Abonent.Passport.Issuer,
                    PassportNumber = Result.Abonent.Passport.PassportNumber,
                    PassportSeries = Result.Abonent.Passport.PassportSeries
                },
                PhoneNumber = Result.Abonent.PhoneNumber,
                FirstName = Result.Abonent.FirstName,
                LastName = Result.Abonent.LastName,
                Patronymic = Result.Abonent.Patronymic


            }
        };
    }

    public async Task<Services.AbonentsReply> GetAbonents()
    {
        var Result = await _abonentsClient.GetAbonentAsync(new AbonentRequest());
        return await ConvertToAbonentsReply(Result.Abonents.ToList());
    } 
    //Знаю что скорее всего работаю неправильно, но как привести разные AbonentsReply в один по-другому не знаю (
    private async Task<Services.AbonentsReply> ConvertToAbonentsReply(List<Abonent> abonentsReply)
    {
        var x = new Services.AbonentsReply();
        foreach (var VARIABLE in abonentsReply)
        {
            x.Abonents.Add(new Services.Abonent()
            {
                AbonentNumber = VARIABLE.AbonentNumber,
                Address = VARIABLE.Address,
                Contract = new Services.Contract()
                {
                    ClosingDate = VARIABLE.Contract.ClosingDate,
                    ClosingReason = VARIABLE.Contract.ClosingReason,
                    ContractNumber = VARIABLE.Contract.ContractNumber,
                    ContractType = VARIABLE.Contract.ContractType,
                    SigningDate = VARIABLE.Contract.SigningDate
                },
                FirstName = VARIABLE.FirstName,
                LastName = VARIABLE.LastName,
                Patronymic = VARIABLE.Patronymic,
                Passport = new Services.Passport()
                {
                    Issuer = VARIABLE.Passport.Issuer,
                    PassportNumber = VARIABLE.Passport.PassportNumber,
                    PassportSeries = VARIABLE.Passport.PassportSeries
                },
                PersonalAccount = VARIABLE.PersonalAccount,
                PhoneNumber = VARIABLE.PhoneNumber
                
                
            });
        }
        return x;
    }
}