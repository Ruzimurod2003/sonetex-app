using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Manufacturer                                                   // Производители 
{
    public int Id { get; set; }
    public string Name { get; set; }                                        // Hазвание 
    public string NameRussian { get; set; }                                 // Русский нaзвание 
    public string NameEnglish { get; set; }                                 // Английский название 
    public string NameUzbek { get; set; }                                   // Узбекский название 
    public string Description { get; set; }                                 // Описание 
    public string DescriptionUzbek { get; set; }                            // Узбекский oписание 
    public string DescriptionRussian { get; set; }                          // Русский oписание 
    public string DescriptionEnglish { get; set; }                          // Английский oписание 
    public List<Catalog> Catalogs { get; set; } = new List<Catalog>();      // Каталоги
    public int ImageId { get; set; }                                        
    [ForeignKey(nameof(ImageId))]
    public File Image { get; set; }                                          // Изображение
}