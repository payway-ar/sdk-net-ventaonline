using Decidir.Clients;

namespace Decidir.Services
{
    internal class Service
    {
        protected string endpoint;
        protected RestClient restClient;

        protected const string CONTENT_TYPE_APP_JSON = "application/json";
        protected const string METHOD_POST = "POST";
        protected const string METHOD_GET = "GET";
        protected const int STATUS_OK = 200;
        protected const int STATUS_CREATED = 201;
        protected const int STATUS_ACCEPTED = 202;
        protected const int STATUS_NOCONTENT = 204;
        protected const int STATUS_ERROR = 500;
        protected const string STATUS_CHALLENGE_PENDING = "CHALLENGE PENDING";
        protected const string STATUS_FINGERPRINT_PENDING = "FINGERPRINT PENDING";

        public Service(string endpoint)
        {
            this.endpoint = endpoint;
        }

        protected bool isErrorResponse(int statusCode)
        {
            if (statusCode == 402)
                return false;
            else
                if (statusCode >= 400 && statusCode < 500)
                    return true;
                else
                    return false;
        }
    }
}
