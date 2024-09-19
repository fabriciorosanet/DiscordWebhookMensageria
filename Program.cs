using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class DiscordWebhookSender
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task SendToDiscord(string webhookUrl, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine("Mensagem vazia. Não foi possível enviar ao Discord.");
            return;
        }

        var payload = new
        {
            content = message
        };

        var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(webhookUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
        }
    }

    // Lista de webhooks
    public static readonly Dictionary<string, string> Webhooks = new Dictionary<string, string>
    {
        { "CANAL - EVENTOS", "https://discord.com/api/webhooks/1252710100867481616/HPw1Vu8uQF1ZNgrU9N8kkhdXc4FA90GLW_43rFbvJJ8-7lcBXW9gYvITTfw2Je_PuCt1" },
        { "1FRNT - AVISO", "https://discord.com/api/webhooks/1270836670538846239/JdZCekfMxgxaO6UUZdvQIt0WWylVzwrwLrPxg5Lam5unvqyIUuqgj0pJqA2tai8jPesh" },
        { "1FRNT - BATE-PAPO", "https://discord.com/api/webhooks/1270838654050242611/abZ5a0yfNYZetkeXsT254kYePvHpO3XTjVldByQJVnj5fDK8MuGJN5WFTmS0QjWGOKBT" },
        { "1DPTM - AVISO", "https://discord.com/api/webhooks/1270839020749979678/6OCasUnpJDWNbJS3DO0wECSFKk8acjAuy8GFcwE2W9E8ZRh8BPzYuFuEgwm1X3zfaXc8" },
        { "1DPTM - BATE-PAPO", "https://discord.com/api/webhooks/1270839485671669865/GtOxZS7VgkSlAXhua_A4Z8kAU2cwGYMOMVtm6Qz4ugEB4itvyB3E3bDA0_1QRX2LAS9g" },
        // Adicione os outros webhooks aqui da mesma maneira
    };

    public static async Task Main(string[] args)
    {
        // Exemplo de envio de mensagem
        var webhookUrl = Webhooks["CANAL - EVENTOS"];
        var message = "@here\n" +
                    " **Alerta: Seu evento irá começar! 🔔 **\n\n" +
                    "Em 30 minutos iniciaremos nosso encontro de hoje. Clique na seção 'Eventos' no canto superior esquerdo do Discord e acesse sua atividade.\n\n" +
                    "Esperamos você!\n\n";
        await SendToDiscord(webhookUrl, message);
    }
}
