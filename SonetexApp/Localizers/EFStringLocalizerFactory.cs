using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SonetexApp.Data;
using SonetexApp.Localizers;

namespace FanurApp.Localizers;

public class EFStringLocalizerFactory : IStringLocalizerFactory
{
    string _connectionString;
    public EFStringLocalizerFactory(string connection)
    {
        _connectionString = connection;
    }
    public IStringLocalizer Create(Type resourceSource)
    {
        return CreateStringLocalizer();
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return CreateStringLocalizer();
    }

    private IStringLocalizer CreateStringLocalizer()
    {
        ApplicationContext _db = new ApplicationContext(
                new DbContextOptionsBuilder<ApplicationContext>()
                    .UseSqlite(_connectionString)
                    .Options);
        // инициализация базы данных
        return new EFStringLocalizer(_db);
    }
}