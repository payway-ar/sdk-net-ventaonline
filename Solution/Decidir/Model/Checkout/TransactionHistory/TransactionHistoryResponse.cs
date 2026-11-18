using System.Collections.Generic;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class TransactionHistoryResponse : List<TransactionDto>
    {
        public TransactionHistoryResponse() : base() { }

        public TransactionHistoryResponse(IEnumerable<TransactionDto> collection) : base(collection) { }

        public static string toJson(object transactionHistory)
        {
            return JsonConvert.SerializeObject(transactionHistory, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}