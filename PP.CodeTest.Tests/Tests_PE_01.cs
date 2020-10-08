using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PP.CodeTest.Tests
{
    public class Tests_PE_01
    {
        public const string A3 = "3 Of A`s";
        public const string B2 = "2 Of B`s";
        public const string C1D1 = "C & D";

        private PromotionRuleEngine promotionRuleEngine = new PromotionRuleEngine("Promotion Rule Engine", new List<PromotionRule>()
        {
                new PromotionRule(A3, new Dictionary<string, int>() { { "A", 3 } },1),
                new PromotionRule(B2, new Dictionary<string, int>() { { "B", 2 } },2),
                new PromotionRule(C1D1, new Dictionary<string, int>() { { "C", 1 },{ "D", 1 } },3),
        });

        [Fact]
        public void Test_No_PromoHit()
        {
            List<CartItem> shoppingCart = new List<CartItem> {
                new CartItem {  Key = "A", Count = 1 },
                new CartItem {  Key = "B", Count = 1 },
                new CartItem {  Key = "C", Count = 1 },
                new CartItem {  Key = "D", Count = 0 }
            };

            var log = new PromoEngineEventLogTrace(shoppingCart);
            promotionRuleEngine.Process(shoppingCart, log.HandlePromoDiscount, log.CalculateTotal);

            Assert.Empty(log.Details);
            Assert.Equal(Pricing.Price("A") + Pricing.Price("B") + Pricing.Price("C"), log.TotalAmount);
        }
    }
}
