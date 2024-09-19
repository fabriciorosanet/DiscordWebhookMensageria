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
        // Adicione os outros webhooks aqui da mesma maneira
    };

    public static async Task Main(string[] args)
    {
        // Exemplo de envio de mensagem
        var webhookUrl = Webhooks[ ]; //liste o webhook
        var message = ; // passe sua string de mensagem aqui!
        await SendToDiscord(webhookUrl, message);
    }
}
