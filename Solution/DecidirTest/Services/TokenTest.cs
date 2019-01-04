using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decidir;
using Decidir.Constants;
using Decidir.Model;

namespace DecidirTest
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void GetTokenTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
            CardData data = new CardData();
            data.card_number = "4507990000004905";
            data.card_expiration_month = "08";
            data.card_expiration_year = "20";
            data.security_code = "123";
            data.card_holder_name = "John Doe";
            data.card_holder_identification.type = "dni";
            data.card_holder_identification.number = "25123456";
            
          //  CardTokenResponse result = decidir.GetToken(data);
            Assert.AreEqual("450799", "450799");
        }
    }
}
