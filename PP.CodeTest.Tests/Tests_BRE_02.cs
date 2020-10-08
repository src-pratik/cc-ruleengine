using System.Collections.Generic;
using System.Linq;
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
                    new BusinessFunctionRule("Book_DuplicatePackSlip_Royalty",x => QueryHelpers.ProductTypeEqual(x, ProductTypeEnum.Book), 2),
                    new BusinessFunctionRule("Membership_Activate",x => QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.ActivateMembership), 2),
                    new BusinessFunctionRule("Membership_Upgrade",x => QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.UpgradeMembership), 2),
                    new BusinessFunctionRule("Membership_Communication",x => QueryHelpers.ProductTypeIn(x,new[]{ProductTypeEnum.ActivateMembership,ProductTypeEnum.UpgradeMembership })  , 3),
                    new BusinessFunctionRule("Video_LearningToSki_AddFirstAid",x => QueryHelpers.WhereKey(x, "Learning To Ski") &  QueryHelpers.ProductTypeEqual(x,ProductTypeEnum.Video), 1),
                    new BusinessFunctionRule("Music_Pay_Royalty",x => QueryHelpers.ProductTypeEqual( x, ProductTypeEnum.Music), 2),
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

        /// <summary>
        // Case :
        //     Phyiscal = Yes
        //     Type = Video
        //     Name = Learning To Ski
        // Output : Rules to trigger
        //     1. Add Free Video to packaging slip (Should be before 2.)
        //     2. Generate packaging slip
        //     3. Commission to agent
        /// </summary>

        [Fact]
        public void Test_Video_LearningToSki_Physical()
        {
            var orderItem = new OrderItem()
            {
                Key = "Learning To Ski",
                ProductType = ProductTypeEnum.Video,
                DeliveryType = DeliveryTypeEnum.Physical
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Video_LearningToSki_AddFirstAid"));
            Assert.True(log.Details.ContainsValue("Physical_Item_Generate_Pack_Slip"));
            Assert.True(log.Details.ContainsValue("Physical_Or_Book_AgentCommission"));

            Assert.True(log.Details.Where(x => x.Value == "Physical_Item_Generate_Pack_Slip").FirstOrDefault().Key >
                log.Details.Where(x => x.Value == "Video_LearningToSki_AddFirstAid").FirstOrDefault().Key);

            Assert.Equal(3, log.Details.Count);
        }

        /// <summary>
        // Case :
        //     Phyiscal = No
        //     Type = Video
        //     Name = Learning To Ski
        // Output :
        //     Only rule to trigger is add First Aid
        /// </summary>

        [Fact]
        public void Test_Video_LearningToSki_Online()
        {
            var orderItem = new OrderItem()
            {
                Key = "Learning To Ski",
                ProductType = ProductTypeEnum.Video,
                DeliveryType = DeliveryTypeEnum.Online
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Video_LearningToSki_AddFirstAid"));
            Assert.Single(log.Details);
        }

        /// <summary>
        // Case :
        //     Phyiscal = No
        //     Type = Video
        // Output :
        //     No rules should be triggered
        /// </summary>

        [Fact]
        public void Test_NoRulesHit_Video_Online()
        {
            var orderItem = new OrderItem()
            {
                Key = "Lord Of Rings",
                ProductType = ProductTypeEnum.Video,
                DeliveryType = DeliveryTypeEnum.Online
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.Empty(log.Details);
        }

        /// <summary>
        // Case :
        //     Phyiscal = No
        //     Type = Membership Activation

        // Output : Rules to trigger (The order should be in sequence)
        //     1. Activate Membership
        //     2. Email the client
        /// </summary>

        [Fact]
        public void Test_Membership_New()
        {
            var orderItem = new OrderItem()
            {
                Key = "Learning To Ski",
                ProductType = ProductTypeEnum.ActivateMembership,
                DeliveryType = DeliveryTypeEnum.Online
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Membership_Activate"));
            Assert.True(log.Details.ContainsValue("Membership_Communication"));

            Assert.True(log.Details.Where(x => x.Value == "Membership_Communication").FirstOrDefault().Key >
                log.Details.Where(x => x.Value == "Membership_Activate").FirstOrDefault().Key);

            Assert.Equal(2, log.Details.Count);
        }

        /// <summary>
        // Case :
        //     Phyiscal = No
        //     Type = Membership Upgrade

        // Output : Rules to trigger (The order should be in sequence)
        //     1. Activate Membership
        //     2. Email the client
        /// </summary>

        [Fact]
        public void Test_Membership_Upgrade()
        {
            var orderItem = new OrderItem()
            {
                Key = "Learning To Ski",
                ProductType = ProductTypeEnum.UpgradeMembership,
                DeliveryType = DeliveryTypeEnum.Online
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Membership_Upgrade"));
            Assert.True(log.Details.ContainsValue("Membership_Communication"));

            Assert.True(log.Details.Where(x => x.Value == "Membership_Communication").FirstOrDefault().Key >
                log.Details.Where(x => x.Value == "Membership_Upgrade").FirstOrDefault().Key);

            Assert.Equal(2, log.Details.Count);
        }

        /// <summary>
        // Case :
        //     Phyiscal = Yes
        //     Type = Music
        //
        // Output : Rules to trigger
        //     1. Generate packaging slip
        //     2. Pay royalty to music company
        //     3. Commission to agent
        /// </summary>
        [Fact]
        public void Test_Physical_Music()
        {
            //Physical Product
            //Product is book
            var orderItem = new OrderItem()
            {
                ProductType = ProductTypeEnum.Music,
                DeliveryType = DeliveryTypeEnum.Physical
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Physical_Item_Generate_Pack_Slip"));
            Assert.True(log.Details.ContainsValue("Physical_Or_Book_AgentCommission"));
            Assert.True(log.Details.ContainsValue("Music_Pay_Royalty"));

            Assert.Equal(3, log.Details.Count);
        }

        /// <summary>
        // Case :
        //     Phyiscal = No
        //     Type = Music
        //
        // Output : Rules to trigger
        //
        //     1. Pay royalty to music company
        //
        /// </summary>
        [Fact]
        public void Test_Online_Music()
        {
            //Physical Product
            //Product is book
            var orderItem = new OrderItem()
            {
                ProductType = ProductTypeEnum.Music,
                DeliveryType = DeliveryTypeEnum.Online
            };

            var log = new EventLogTrace();
            ruleEngine.Process(orderItem, log.HandleRuleHit);

            Assert.True(log.Details.ContainsValue("Music_Pay_Royalty"));
            Assert.Single(log.Details);
        }
    }
}