using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PP.CodeTest.Tests
{
    /// <summary>
    /// Added a new business promo where if there 10 items of C then 
    /// they are given 50% discount on total value of those 10 Items
    /// This will execute last, first priority will be given to C&D pairing
    /// </summary>
    public class Tests_PE_03
    {
        public const string A3 = "3 Of A`s";
        public const string B2 = "2 Of B`s";
        public const string C1D1 = "C & D";
        public const string C10 = "10 Of C`s";

        private PromotionRuleEngine promotionRuleEngine = new PromotionRuleEngine("Promotion Rule Engine", new List<PromotionRule>()
        {
                new PromotionRule(A3, new Dictionary<string, int>() { { "A", 3 } },1),
                new PromotionRule(B2, new Dictionary<string, int>() { { "B", 2 } },2),
                new PromotionRule(C1D1, new Dictionary<string, int>() { { "C", 1 },{ "D", 1 } },3),
                new PromotionRule(C10, new Dictionary<string, int>() { { "C", 10 } },4),
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

        [Fact]
        public void Test_PromoHit_CD_1Time_C10_1Time()
        {
            List<CartItem> shoppingCart = new List<CartItem> {
                new CartItem {  Key = "A", Count = 0 },
                new CartItem {  Key = "B", Count = 0 },
                new CartItem {  Key = "C", Count = 11 },
                new CartItem {  Key = "D", Count = 1 }
            };

            var log = new PromoEngineEventLogTrace(shoppingCart);
            promotionRuleEngine.Process(shoppingCart, log.HandlePromoDiscount, log.CalculateTotal);

            Assert.True(log.Details.Values.Where(x => x.Item1 == C1D1).Count() == 1);
            Assert.True(log.Details.Values.Where(x => x.Item1 == C10).Count() == 1);
            Assert.Equal(2, log.Details.Count);
            Assert.Equal(1 * Pricing.PromoPrice(C1D1) + 1 * Pricing.PromoPrice(C10), log.TotalAmount);
        }

        [Fact]
        public void Test_PromoHit_CD_10Time()
        {
            List<CartItem> shoppingCart = new List<CartItem> {
                new CartItem {  Key = "A", Count = 0 },
                new CartItem {  Key = "B", Count = 0 },
                new CartItem {  Key = "C", Count = 15 },
                new CartItem {  Key = "D", Count = 10 }
            };

            var log = new PromoEngineEventLogTrace(shoppingCart);
            promotionRuleEngine.Process(shoppingCart, log.HandlePromoDiscount, log.CalculateTotal);

            Assert.True(log.Details.Values.Where(x => x.Item1 == C1D1).Count() == 10);
            Assert.Equal(10, log.Details.Count);
            Assert.Equal(10 * Pricing.PromoPrice(C1D1) + 5 * Pricing.Price("C"), log.TotalAmount);
        }
    }
}