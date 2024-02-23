using Telegram.Bot;

namespace SonetexApp.Commons;
public static class Functions
{
    public static string CheckCurrentUrlLocatedInThis(string currentUrl, string areaName, string controllerName)
    {
        if (currentUrl.ToLower().Contains($"{areaName}/{controllerName}".ToLower()))
        {
            return "active";
        }
        else
        {
            return string.Empty;
        }
    }
    public static async Task SendMessageFromTelegramAsync(string token, string adminId, string name, string email, string subject, string message)
    {
        var bot = new TelegramBotClient(token);
        string messageVM = $"Ismi: <b>{name}</b> \n" +
                            $"Email: <b>{email}</b> \n" +
                            $"Yuboradigan mavzusi: <b>{subject}</b> \n" +
                            $"Habar: <b>{message}</b>";

        await bot.SendTextMessageAsync(adminId, messageVM, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }
}