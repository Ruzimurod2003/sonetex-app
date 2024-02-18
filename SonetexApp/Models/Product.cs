using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Product                            // Товары 
{
    public int Id { get; set; }
    public string Name { get; set; }            // Hазвание 
    public string NameRussian { get; set; }     // Русский нaзвание 
    public string NameEnglish { get; set; }     // Английский название 
    public string NameUzbek { get; set; }       // Узбекский название 
    public string Description { get; set; }     // Описание 
    public string VendorCode { get; set; }      // Артикул 
    public int Availability { get; set; }       // Наличие 
    public DateTime Guarantee { get; set; }     // Гарантия 
    public int StateId { get; set; }            // Состояние 
    public string Address { get; set; }         // Адрес 
    public Catalog Catalog { get; set; }        // Категория
    public List<File> Images { get; set; }      // Изображение
}