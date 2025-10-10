using Decidir;
using Decidir.Constants;
using Decidir.Model;
using Decidir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecidirTest
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void GetTokenTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, Configuration.PrivateKey, Configuration.PublicKey);
            TokenRequest data = new TokenRequest();
            data.card_number = "4507990000004905";
            data.card_expiration_month = "08";
            data.card_expiration_year = "20";
            data.security_code = "123";
            data.card_holder_name = "John Doe";
            data.card_holder_identification = new Decidir.Model.CardHolderIdentification();
            data.card_holder_identification.type = "dni";
            data.card_holder_identification.number = "25123456";
            
            var result = decidir.GetToken(data);
            Assert.AreEqual("450799", "450799");
        }
    }
}
