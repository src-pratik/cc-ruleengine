using System.Collections.Generic;
using Xunit;

namespace PP.CodeTest.Tests
{
    /// <summary>
    /// Test cases for Solution 2 : Business Rules Engine
    /// </summary>
    public class Tests_BRE_02
    {
        private BusinessRuleEngine ruleEngine = new BusinessRuleEngine("Order Management", new List<BusinessFunctionRule>() {
                    new BusinessFunctionRule("Physical_Item_Generate_Pack_Slip",x => x.DeliveryType == DeliveryTypeEnum.Physical, 2),
                    new BusinessFunctionRule("Physical_Or_Book_AgentCommission",x => x.DeliveryType == DeliveryTypeEnum.Physical ||  QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.Book), 2),
                    new BusinessFunctionRule("Book_DuplicatePackSlip_Royalty",x => x.ProductType == ProductTypeEnum.Book, 2),
                    new BusinessFunctionRule("Membership_Activate",x => QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.ActivateMembership), 2),
                    new BusinessFunctionRule("Membership_Upgrade",x => QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.UpgradeMembership), 2),
                    new BusinessFunctionRule("Membership_Communication",x => QueryHelpers.ProductTypeIn(x,new[]{ProductTypeEnum.ActivateMembership,ProductTypeEnum.UpgradeMembership })  , 3),
                    new BusinessFunctionRule("Video_LearningToSki_AddFirstAid",x => QueryHelpers.WhereKey(x, "Learning To Ski") &  QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.Video), 1),
        });

        /// <summary>
        // Case :
        //     Phyiscal = Yes
        //     Type = Book
        //
        // Output : Rules to trigger
        //     1. Generate packaging slip
        //     2. Generate Royalty
        //     3. Commission to agent
        /// </summary>
        [Fact]
        public void Test_Physical_Book()
        {
            //Physical Product
            //Product is book
            var orderItem = new OrderItem()
            {
                ProductType = ProductTypeEnum.Book,
                DeliveryType = DeliveryTypeEnum.Physical
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Physical_Item_Generate_Pack_Slip"));
            Assert.True(log.Details.ContainsValue("Physical_Or_Book_AgentCommission"));
            Assert.True(log.Details.ContainsValue("Book_DuplicatePackSlip_Royalty"));

            Assert.Equal(3, log.Details.Count);
        }

        /// <summary>
        // Case :
        //     Phyiscal = Yes
        //     Type = Furniture
        //
        // Output : Rules to trigger
        //
        //     1. Generate packaging slip
        //     2. Commission to agent
        /// </summary>

        [Fact]
        public void Test_Physical_NonBook()
        {
            //Physical Product
            var orderItem = new OrderItem()
            {
                ProductType = ProductTypeEnum.Furniture,
                DeliveryType = DeliveryTypeEnum.Physical
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Physical_Item_Generate_Pack_Slip"));
            Assert.True(log.Details.ContainsValue("Physical_Or_Book_AgentCommission"));

            Assert.Equal(2, log.Details.Count);
        }
    }
}