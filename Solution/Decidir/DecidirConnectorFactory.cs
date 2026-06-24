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
        public readonly DecidirConfig Config;
        public DecidirConnectorFactory(IHttpClientFactory httpClientFactory, DecidirConfig config)
        {
            Config = config;
            Connector = new DecidirConnector(httpClientFactory, Config.AmbienteId, Config.PrivateKey, Config.PublicKey);
        }
    }
}
