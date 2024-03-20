using System.ComponentModel.DataAnnotations;

namespace CRMDbService.Models;

public class Service
{
    [Key]
    public long ServiceId { get; set; }
    public string ServiceName { get; set; }
    public DateTime ConnectionDate { get; set; }
    public string AbonentNumber { get; set; }
    public string ServiceType { get; set; }
    public string ServiceView { get; set; }
    public string Status { get; set; }
}