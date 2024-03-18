using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbonentsDbService.Models;

public class Passport
{
    [Key]
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    public string Issuer { get; set; }
}