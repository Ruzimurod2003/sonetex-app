
using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;

namespace SonetexApp.Repositories;
public interface ITypeRepository
{
    List<Models.Type> GetTypes(string currentCultureName);
}
public class TypeRepository : ITypeRepository
{
    private readonly ApplicationContext _context;

    public TypeRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<Models.Type> GetTypes(string currentCultureName)
    {
        List<Models.Type> types = new List<Models.Type>();
        var dbTypes = _context.Types
                            .Include(i => i.Products)
                            .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbType in dbTypes)
            {
                var type = new Models.Type();
                type.Name = dbType.NameUzbek;
                type.Id = dbType.Id;
                type.Products = dbType.Products;

                types.Add(type);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbType in dbTypes)
            {
                var type = new Models.Type();
                type.Name = dbType.NameRussian;
                type.Id = dbType.Id;
                type.Products = dbType.Products;

                types.Add(type);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbType in dbTypes)
            {
                var type = new Models.Type();
                type.Name = dbType.NameEnglish;
                type.Id = dbType.Id;
                type.Products = dbType.Products;

                types.Add(type);
            }
        }
        else
        {
            foreach (var dbType in dbTypes)
            {
                var type = new Models.Type();
                type.Name = dbType.Name;
                type.Id = dbType.Id;
                type.Products = dbType.Products;

                types.Add(type);
            }
        }
        return types;
    }
}