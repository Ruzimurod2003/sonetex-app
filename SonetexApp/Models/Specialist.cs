using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Specialist                         // Cпециалисты 
{
    public int Id { get; set; }
    public string Name { get; set; }            // Hазвание 
    public string NameRussian { get; set; }     // Русский нaзвание 
    public string NameEnglish { get; set; }     // Английский название 
    public string NameUzbek { get; set; }       // Узбекский название 
    public string Description { get; set; }     // Описание 
    public string Email { get;set; }            // Электронная почта
    public string Phone { get; set; }           // Телефон
    public string Position { get;set; }         // Должность
    public int ImageId { get; set; }
    [ForeignKey(nameof(ImageId))]
    public File Image { get; set; }             // Изображение
}