using System.ComponentModel.DataAnnotations;

namespace CRMDbService.Models;

public class Applications
{
    [Key]
    public string applicationNumber { get; set; }
    public DateTime beginDate { get; set; }
    public string abonentNumber { get; set; }
    public string personalAccount { get; set; }
    public long serviceId { get; set; }
    
}