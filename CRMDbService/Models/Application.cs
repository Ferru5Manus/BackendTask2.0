using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMDbService.Models;

public class Application
{
    [Key]
    public string ApplicationNumber { get; set; }
    public DateTime BeginDate { get; set; }
    public string AbonentNumber { get; set; }
    public string PersonalAccount { get; set; }
    public long ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public Service Service { get; set; }
    public string EquipmentType { get; set; }
    public long TechnicalSupportCallID { get; set; }
    [ForeignKey("TechnicalSupportCallID")]
    public TechnicalSupportCall TechnicalSupportCall { get; set; }
    public long CloseDate { get; set; }
}