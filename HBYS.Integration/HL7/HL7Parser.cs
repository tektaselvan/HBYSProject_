namespace HBYS.Integration.HL7;

public class HL7Message
{
    public string MessageType { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string TestCode { get; set; } = string.Empty;
    public string ResultValue { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string ReferenceRange { get; set; } = string.Empty;
    public DateTime MessageDateTime { get; set; }
    public Dictionary<string, string> RawSegments { get; set; } = new();
}

public class HL7Parser
{
    // HL7 v2.x mesajını parse eder
    // Örnek: MSH|^~\&|LIS|HOSPITAL|HIS|HOSPITAL|20240101120000||ORU^R01|MSG001|P|2.5
    public HL7Message Parse(string rawMessage)
    {
        var message = new HL7Message();
        if (string.IsNullOrWhiteSpace(rawMessage)) return message;

        var segments = rawMessage.Split('\r', '\n')
            .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

        foreach (var segment in segments)
        {
            var fields = segment.Split('|');
            if (fields.Length == 0) continue;

            message.RawSegments[fields[0]] = segment;

            switch (fields[0])
            {
                case "MSH":
                    message.MessageType = fields.Length > 8 ? fields[8] : "";
                    if (fields.Length > 6 && DateTime.TryParseExact(fields[6],
                        "yyyyMMddHHmmss", null,
                        System.Globalization.DateTimeStyles.None, out var dt))
                        message.MessageDateTime = dt;
                    break;

                case "PID":
                    message.PatientId = fields.Length > 3 ? fields[3] : "";
                    message.PatientName = fields.Length > 5 ? fields[5].Replace("^", " ") : "";
                    break;

                case "OBR":
                    message.OrderId = fields.Length > 2 ? fields[2] : "";
                    message.TestCode = fields.Length > 4 ? fields[4].Split('^')[0] : "";
                    break;

                case "OBX":
                    message.ResultValue = fields.Length > 5 ? fields[5] : "";
                    message.Unit = fields.Length > 6 ? fields[6] : "";
                    message.ReferenceRange = fields.Length > 7 ? fields[7] : "";
                    break;
            }
        }

        return message;
    }
}
