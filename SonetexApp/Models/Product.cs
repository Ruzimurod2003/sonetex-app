using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Product                                            // Товары 
{
    public int Id { get; set; }
    public string Name { get; set; }                            // Hазвание 
    public string NameRussian { get; set; }                     // Русский нaзвание 
    public string NameEnglish { get; set; }                     // Английский название 
    public string NameUzbek { get; set; }                       // Узбекский название 
    public string Description { get; set; }                     // Описание 
    public string DescriptionUzbek { get; set; }                // Узбекский oписание 
    public string DescriptionRussian { get; set; }              // Русский oписание 
    public string DescriptionEnglish { get; set; }              // Английский oписание 

    public string VendorCode { get; set; }                      // Артикул 
    public string Price { get; set; }                           // Цена
    public int Availability { get; set; }                       // Наличие 
    public DateTime Guarantee { get; set; }                     // Гарантия 
    public string Address { get; set; }                         // Адрес 
    public int CatalogId { get; set; }
    [ForeignKey(nameof(CatalogId))]
    public Catalog Catalog { get; set; }                        // Категория
    public int TypeId { get; set; }
    [ForeignKey(nameof(TypeId))]
    public Type Type { get; set; }                              // Тип
    public int StateId { get; set; }
    [ForeignKey(nameof(StateId))]
    public State State { get; set; }                            // Состояние 
    public List<File> Images { get; set; } = new List<File>();  // Изображение
}