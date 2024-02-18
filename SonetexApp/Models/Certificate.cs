using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Certificate                        // Сертификаты 
{
    public int Id { get; set; }                 // Идентификатор
    public string Name { get; set; }            // Hазвание 
    public string NameRussian { get; set; }     // Русский нaзвание 
    public string NameEnglish { get; set; }     // Английский название 
    public string NameUzbek { get; set; }       // Узбекский название 
    public string DocumentPath { get; set; }    // Путь к документу
    public string Description { get; set; }     // Описание 
    public int DocumentId { get; set; }
    [ForeignKey(nameof(DocumentId))]
    public File Document { get; set; }          // Документ
}