using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace AbonentsDbService.Models;

public class Abonent
{
    [Key]
    [RegularExpression(@"^78[А-Я]{1}[0-9]{6}$", ErrorMessage = "Invalid Abonent Number format. Example: 78А000001")]
    public string AbonentNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    
    [ForeignKey("Passport")]
    public int PassportSeries { get; set; }
    
    [ForeignKey("Contract")]
    public int ContractNumber { get; set; }
    
    public int PersonalAccount { get; set; }
    public string Address { get; set; }

    public virtual Passport Passport { get; set; }
    public virtual Contract Contract { get; set; }
}