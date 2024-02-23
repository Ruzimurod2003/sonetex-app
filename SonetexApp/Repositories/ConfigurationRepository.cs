using Microsoft.AspNetCore.Localization;
using SonetexApp.Data;

namespace SonetexApp.Repositories;
public interface IConfigurationRepository
{
    public string PhoneNumber { get; }
    public string Email { get; }
    public string TelegramLink { get; }
    public string InstagramLink { get; }
    public string FacebookLink { get; }
    public string GithubLink { get; }
    public string GoogleLink { get; }
    public string TwitterLink { get; }
    public string YoutubeLink { get; }
    public string Address { get; }
}
public class ConfigurationRepository : IConfigurationRepository
{
    private readonly ApplicationContext _context;

    public ConfigurationRepository(ApplicationContext context)
    {
        _context = context;
    }

    public string PhoneNumber => _context.Configurations.FirstOrDefault().PhoneNumber;

    public string Email => _context.Configurations.FirstOrDefault().Email;

    public string TelegramLink => _context.Configurations.FirstOrDefault().TelegramLink;

    public string InstagramLink => _context.Configurations.FirstOrDefault().InstagramLink;

    public string FacebookLink => _context.Configurations.FirstOrDefault().FacebookLink;

    public string GithubLink => _context.Configurations.FirstOrDefault().GithubLink;

    public string GoogleLink => _context.Configurations.FirstOrDefault().GoogleLink;

    public string TwitterLink => _context.Configurations.FirstOrDefault().TwitterLink;

    public string YoutubeLink => _context.Configurations.FirstOrDefault().YoutubeLink;

    public string Address => _context.Configurations.FirstOrDefault().Address;
}