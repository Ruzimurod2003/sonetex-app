namespace SonetexApp.Models
{
    public class File                                               // Файл
    {
        public int Id { get; set; }                                 // Идентификатор
        public string Name { get; set; }                            // Название файла
        public string Path { get; set; }                            // Путь к файлу
        public string Description { get; set; }                     // Описание 
    }
}
