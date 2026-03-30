using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace HBYS.Integration.ENabiz;

public class ENabizPatientData
{
    public string TcNo { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = string.Empty;
    public List<ENabizVisitData> Visits { get; set; } = new();
}

public class ENabizVisitData
{
    public string ProtocolNo { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public List<string> DiagnosisCodes { get; set; } = new();
}

public class ENabizService(HttpClient httpClient, ILogger<ENabizService> logger)
{
    private const string BaseUrl = "https://enabiz.saglik.gov.tr/api"; // Örnek URL

    public async Task<bool> SendPatientDataAsync(ENabizPatientData patient, CancellationToken ct = default)
    {
        try
        {
            logger.LogInformation("e-Nabız'a hasta verisi gönderiliyor. TcNo: {TcNo}", patient.TcNo);

            var json = JsonSerializer.Serialize(patient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Gerçek ortamda e-Nabız servisi çağrılır
            // var response = await httpClient.PostAsync($"{BaseUrl}/patient", content, ct);

            await Task.Delay(100, ct);
            logger.LogInformation("e-Nabız gönderimi başarılı. TcNo: {TcNo}", patient.TcNo);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "e-Nabız gönderimi başarısız. TcNo: {TcNo}", patient.TcNo);
            return false;
        }
    }

    public async Task<string?> ReceiveAcknowledgementAsync(string tcNo, CancellationToken ct = default)
    {
        try
        {
            await Task.Delay(50, ct);
            return "OK";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "e-Nabız acknowledgement alınamadı. TcNo: {TcNo}", tcNo);
            return null;
        }
    }
}
