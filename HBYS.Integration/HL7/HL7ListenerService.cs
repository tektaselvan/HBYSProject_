using System.Net;
using System.Net.Sockets;
using System.Text;
using HBYS.Integration.Queue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HBYS.Integration.HL7;

public class HL7ListenerService(
    ILogger<HL7ListenerService> logger,
    QueueService queueService,
    HL7Parser parser,
    HL7AckBuilder ackBuilder) : BackgroundService
{
    private TcpListener? _listener;
    private const int Port = 2575; // Standart HL7 MLLP portu

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _listener = new TcpListener(IPAddress.Any, Port);
        _listener.Start();
        logger.LogInformation("HL7 Listener başlatıldı. Port: {Port}", Port);

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(ct);
                _ = HandleClientAsync(client, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                logger.LogError(ex, "HL7 bağlantı hatası");
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken ct)
    {
        using var stream = client.GetStream();
        var buffer = new byte[4096];

        try
        {
            var bytesRead = await stream.ReadAsync(buffer, ct);
            var rawMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead)
                .Trim('\x0B', '\x1C', '\r'); // MLLP wrapper'ı temizle

            logger.LogInformation("HL7 mesajı alındı ({Length} byte)", bytesRead);

            var message = parser.Parse(rawMessage);
            await queueService.EnqueueAsync(message, ct);

            var ack = ackBuilder.Build(message, "AA");
            var ackBytes = Encoding.UTF8.GetBytes($"\x0B{ack}\x1C\r");
            await stream.WriteAsync(ackBytes, ct);

            logger.LogInformation("HL7 ACK gönderildi. OrderId: {OrderId}", message.OrderId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "HL7 mesaj işleme hatası");
        }
        finally
        {
            client.Close();
        }
    }

    public override void Dispose()
    {
        _listener?.Stop();
        base.Dispose();
    }
}
