using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace HBYS.Integration.Medula;

public class MedulaInvoice
{
    public Guid InvoiceId { get; set; }
    public string TcNo { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal SGKAmount { get; set; }
    public DateTime ServiceDate { get; set; }
    public List<MedulaInvoiceItem> Items { get; set; } = new();
}

public class MedulaInvoiceItem
{
    public string SutCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class MedulaService(HttpClient httpClient, ILogger<MedulaService> logger)
{
    private const string BaseUrl = "https://medula.sgk.gov.tr/api"; // Örnek URL

    public async Task<bool> SendInvoiceAsync(MedulaInvoice invoice, CancellationToken ct = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(invoice);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation("Medula'ya fatura gönderiliyor. InvoiceId: {InvoiceId}", invoice.InvoiceId);

            // Gerçek ortamda Medula SOAP/REST servisi çağrılır
            // var response = await httpClient.PostAsync($"{BaseUrl}/invoice", content, ct);

            // Simülasyon
            await Task.Delay(100, ct);
            logger.LogInformation("Medula fatura gönderimi başarılı. InvoiceId: {InvoiceId}", invoice.InvoiceId);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Medula fatura gönderimi başarısız. InvoiceId: {InvoiceId}", invoice.InvoiceId);
            return false;
        }
    }

    public async Task<string?> ReceiveResponseAsync(Guid invoiceId, CancellationToken ct = default)
    {
        try
        {
            // Gerçek ortamda Medula'dan yanıt sorgulanır
            await Task.Delay(50, ct);
            return "ACCEPTED"; // Örnek yanıt
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Medula yanıt alınamadı. InvoiceId: {InvoiceId}", invoiceId);
            return null;
        }
    }
}
