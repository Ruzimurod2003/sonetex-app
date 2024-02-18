using Microsoft.Extensions.Localization;
using SonetexApp.Data;
using SonetexApp.Localizers;

namespace FanurApp.Localizers;

public class EFStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly ApplicationContext context;

    public EFStringLocalizerFactory(ApplicationContext _context)
    {
        context = _context;
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
        // инициализация базы данных
        return new EFStringLocalizer(context);
    }
}