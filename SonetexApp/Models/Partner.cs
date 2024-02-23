using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Partner                                        // Партнеры 
{
    public int Id { get; set; }
    public string Name { get; set; }                        // Hазвание 
    public string NameRussian { get; set; }                 // Русский нaзвание 
    public string NameEnglish { get; set; }                 // Английский название 
    public string NameUzbek { get; set; }                   // Узбекский название 
    public string Description { get; set; }                 // Описание 
    public string DescriptionRussian { get; set; }          // Описание на русском 
    public string DescriptionEnglish { get; set; }          // Описание на английском 
    public string DescriptionUzbek { get; set; }            // Описание на узбекском
    public int ImageId { get; set; }
    [ForeignKey(nameof(ImageId))]
    public File Image { get; set; }                         // Изображение
}