using Decidir.Constants;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Decidir
{
    public class DecidirConnectorFactory
    {
        public readonly IDecidirConnector Connector;
        public DecidirConnectorFactory(IHttpClientFactory httpClientFactory, DecidirConfig config)
        {
            Connector = new DecidirConnector(httpClientFactory, config.AmbienteId, config.PrivateKey, config.PublicKey);
        }
    }
}
