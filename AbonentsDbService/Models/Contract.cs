using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbonentsDbService.Models;

public class Contract
{
    [Key]
    [RegularExpression(@"^\d{1,3}-\d{1,3}-\d{4}$", ErrorMessage = "Invalid contract number format. Example: 123-456-2022")]
    public string ContractNumber { get; set; }
    public DateTime SigningDate { get; set; }
    public int ContractType { get; set; }
    public DateTime ClosingDate { get; set; }
    public string ClosingReason { get; set; }
}
