using System.ComponentModel.DataAnnotations.Schema;

namespace SonetexApp.Models;
public class Image
{
    public int Id { get; set; }
    public string Name { get; set; }                    // Название файла
    public string Path { get; set; }                    // Путь к файлу
    public string Description { get; set; }             // Описание 
    public int? CatalogId { get; set; }                 // Идентификатор каталога
    [ForeignKey(nameof(CatalogId))]
    public Catalog Catalog { get; set; }                // Категория
    public int? CertificateId { get; set; }             // Идентификатор сертификата
    [ForeignKey(nameof(CertificateId))]
    public Certificate Certificate { get; set; }        // Сертификат
    public int? ManufacturerId { get; set; }            // Идентификатор производителя
    [ForeignKey(nameof(ManufacturerId))]
    public Manufacturer Manufacturer { get; set; }      // Производитель
    public int? PartnerId { get; set; }                 // Идентификационный номер партнера
    [ForeignKey(nameof(PartnerId))]
    public Partner Partner { get; set; }                // Партнер
    public int? SpecialistId { get; set; }              // Идентификатор специалиста
    [ForeignKey(nameof(SpecialistId))]
    public Specialist Specialist { get; set; }          // Специалист
    public int? RecommendationId { get; set; }          // Идентификатор рекомендации
    [ForeignKey(nameof(RecommendationId))]
    public Recommendation Recommendation { get; set; }  // Рекомендация
}