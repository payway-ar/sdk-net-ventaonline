using System;
using System.Collections.Generic;
using Decidir.Model.CyberSource;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class ValidateData
    {
        public SiteInfo site { get; set; }
        public ValidateCustomer customer { get; set; }
        public ValidatePayment payment { get; set; }
        public string success_url { get; set; }
        public string redirect_url { get; set; }
        public string cancel_url { get; set; }
        public FraudDetection fraud_detection { get; set; }
        public static string toJson(ValidateData validateData)
        {
            return JsonConvert.SerializeObject(validateData, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}