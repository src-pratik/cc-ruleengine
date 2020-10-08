using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PP.CodeTest.Tests
{
    /// <summary>
    /// Contains all test cases which are provided in sample in the problem statement
    /// </summary>
    public class Tests_PE_02
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

        [Fact]
        public void Test_PromoHit_A3_1Time_B2_2Time()
        {
            List<CartItem> shoppingCart = new List<CartItem> {
                new CartItem {  Key = "A", Count = 5 },
                new CartItem {  Key = "B", Count = 5 },
                new CartItem {  Key = "C", Count = 1 },
                new CartItem {  Key = "D", Count = 0 }
            };

            var log = new PromoEngineEventLogTrace(shoppingCart);
            promotionRuleEngine.Process(shoppingCart, log.HandlePromoDiscount, log.CalculateTotal);

            Assert.True(log.Details.Values.Where(x => x.Item1 == A3).Count() == 1);
            Assert.True(log.Details.Values.Where(x => x.Item1 == B2).Count() == 2);
            Assert.Equal(1 * Pricing.PromoPrice(A3) + 2 * Pricing.PromoPrice(B2) + 2 * Pricing.Price("A") + Pricing.Price("B") + Pricing.Price("C"), log.TotalAmount);
        }

        [Fact]
        public void Test_PromoHit_A3_1Time_B2_2Time_CD_1Time()
        {
            List<CartItem> shoppingCart = new List<CartItem> {
                new CartItem {  Key = "A", Count = 3 },
                new CartItem {  Key = "B", Count = 5 },
                new CartItem {  Key = "C", Count = 1 },
                new CartItem {  Key = "D", Count = 1 }
            };

            var log = new PromoEngineEventLogTrace(shoppingCart);
            promotionRuleEngine.Process(shoppingCart, log.HandlePromoDiscount, log.CalculateTotal);

            Assert.True(log.Details.Values.Where(x => x.Item1 == A3).Count() == 1);
            Assert.True(log.Details.Values.Where(x => x.Item1 == B2).Count() == 2);
            Assert.True(log.Details.Values.Where(x => x.Item1 == C1D1).Count() == 1);
            Assert.Equal(4, log.Details.Count);
            Assert.Equal(1 * Pricing.PromoPrice(A3) + 2 * Pricing.PromoPrice(B2) + 1 * Pricing.PromoPrice(C1D1) + Pricing.Price("B"), log.TotalAmount);
        }
    }
}
