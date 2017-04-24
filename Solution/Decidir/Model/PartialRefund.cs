using Newtonsoft.Json;

namespace Decidir.Model
{
    public class PartialRefund
    {
        public long amount { get; set; }

        public static string toJson(PartialRefund partialRefund)
        {
            return JsonConvert.SerializeObject(partialRefund, Newtonsoft.Json.Formatting.None);
        }
    }
}
