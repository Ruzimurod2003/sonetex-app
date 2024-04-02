using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class MProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameRussian { get; set; }
    public string NameEnglish { get; set; }
    public string NameUzbek { get; set; }
    public string Description { get; set; }
    public string DescriptionRussian { get; set; }
    public string DescriptionEnglish { get; set; }
    public string DescriptionUzbek { get; set; }
    public string VendorCode { get; set; }
    public string AlternativeVendorCode { get; set; }
    public string Price { get; set; }
    public int ManufacturerId { get; set; }
    [ForeignKey(nameof(ManufacturerId))]
    public Manufacturer Manufacturer { get; set; }
}