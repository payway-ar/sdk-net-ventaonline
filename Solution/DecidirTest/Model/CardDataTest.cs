using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decidir.Model;

namespace DecidirTest.Model
{
    [TestClass]
    public class CardDataTest
    {
        [TestMethod]
        public void CardDataEmptyToJson()
        {
            CardData data = new CardData();
            string result = CardData.toJson(data);
            string expected = "{\"card_number\":null,\"card_expiration_month\":null,\"card_expiration_year\":null,\"security_code\":null,\"card_holder_name\":null,\"card_holder_identification\":{\"type\":null,\"number\":null}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CardDataWithCardNumberToJson()
        {
            CardData data = new CardData();
            data.card_number = "123456789";
            string result = CardData.toJson(data);
            string expected = "{\"card_number\":\"123456789\",\"card_expiration_month\":null,\"card_expiration_year\":null,\"security_code\":null,\"card_holder_name\":null,\"card_holder_identification\":{\"type\":null,\"number\":null}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CardDataWithCardHolderIdentificationToJson()
        {
            CardData data = new CardData();
            data.card_holder_identification = new CardHolderIdentification();

            data.card_holder_identification.type = "dni";
            data.card_holder_identification.number = "12123123";

            data.card_number = "123456789";
            string result = CardData.toJson(data);
            string expected = "{\"card_number\":\"123456789\",\"card_expiration_month\":null,\"card_expiration_year\":null,\"security_code\":null,\"card_holder_name\":null,\"card_holder_identification\":{\"type\":\"dni\",\"number\":\"12123123\"}}";
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void CardDataWithCardHolderIdentificationToObject()
        {
            string expected = "{\"card_number\":\"123456789\",\"card_expiration_month\":null,\"card_expiration_year\":null,\"security_code\":null,\"card_holder_name\":null,\"card_holder_identification\":{\"type\":\"dni\",\"number\":\"12123123\"}}";

            CardData data = CardData.toCardData(expected);
        }
    }
}
