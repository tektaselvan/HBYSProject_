using System.Collections.Concurrent;
using HBYS.Integration.HL7;
using Microsoft.Extensions.Logging;

namespace HBYS.Integration.Queue;

public class QueueService(ILogger<QueueService> logger)
{
    private readonly ConcurrentQueue<HL7Message> _queue = new();
    private readonly HashSet<string> _processedIds = new(); // Duplicate kontrolü

    public Task EnqueueAsync(HL7Message message, CancellationToken ct = default)
    {
        var messageId = $"{message.OrderId}_{message.MessageDateTime:yyyyMMddHHmmss}";

        if (_processedIds.Contains(messageId))
        {
            logger.LogWarning("Duplicate mesaj tespit edildi, atlanıyor: {MessageId}", messageId);
            return Task.CompletedTask;
        }

        _queue.Enqueue(message);
        _processedIds.Add(messageId);
        logger.LogInformation("Mesaj kuyruğa eklendi. OrderId: {OrderId}, Kuyruk boyutu: {Count}",
            message.OrderId, _queue.Count);

        return Task.CompletedTask;
    }

    public HL7Message? Dequeue()
    {
        _queue.TryDequeue(out var message);
        return message;
    }

    public int Count => _queue.Count;
    public bool IsEmpty => _queue.IsEmpty;
}
