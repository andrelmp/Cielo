using Cielo.Enums;

namespace Cielo.Responses
{
    public class CaptureTransactionResponse : CieloResponse<CaptureTransactionResponse>
    {
        public CaptureTransactionResponse(string content)
            : base(content)
        {
            Status = Status.Default;
            Map(c => c.Status, "status", new EnumStatusConverter());
        }

        public Status Status { get; set; }
    }
}
