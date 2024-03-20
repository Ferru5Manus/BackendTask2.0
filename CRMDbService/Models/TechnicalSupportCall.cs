using System.ComponentModel.DataAnnotations;

namespace CRMDbService.Models;

public class TechnicalSupportCall
{
    [Key]
    public long TechnicalSupportCallID { get; set; }
    public DateTime CallDate { get; set; }
    public string ProblemType { get; set; }
    public string ProblemDescription { get; set; }
    public string AbonentNumber { get; set; }
}