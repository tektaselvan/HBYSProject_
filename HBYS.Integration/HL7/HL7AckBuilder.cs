namespace HBYS.Integration.HL7;

public class HL7AckBuilder
{
    public string Build(HL7Message originalMessage, string ackCode = "AA", string? errorMessage = null)
    {
        // AA = Application Accept, AE = Application Error, AR = Application Reject
        var now = DateTime.Now.ToString("yyyyMMddHHmmss");
        var msgId = $"ACK{now}";

        var ack = $"MSH|^~\\&|HIS|HOSPITAL|LIS|LAB|{now}||ACK|{msgId}|P|2.5\r" +
                  $"MSA|{ackCode}|{originalMessage.OrderId}";

        if (!string.IsNullOrWhiteSpace(errorMessage))
            ack += $"|{errorMessage}";

        return ack + "\r";
    }
}
