using Xunit;

namespace PP.CodeTest.Tests
{
    //Test cases for the basic query helpers defined
    public class Tests_BRE_01
    {
        [Theory]
        [InlineData(ProductTypeEnum.Book)]
        [InlineData(ProductTypeEnum.UpgradeMembership)]
        public void ProductTypeEqual_Found(ProductTypeEnum productType)
        {
            var item = new OrderItem()
            {
                ProductType = productType
            };

            Assert.True(QueryHelpers.ProductTypeEqual(item, productType));
        }

        [Theory]
        [InlineData(ProductTypeEnum.Book)]
        [InlineData(ProductTypeEnum.UpgradeMembership)]
        public void ProductTypeEqual_NotFound(ProductTypeEnum productType)
        {
            var item = new OrderItem()
            {
                ProductType = ProductTypeEnum.ActivateMembership
            };

            Assert.False(QueryHelpers.ProductTypeEqual(item, productType));
        }

        [Theory]
        [InlineData(DeliveryTypeEnum.Online)]
        [InlineData(DeliveryTypeEnum.Physical)]
        public void ProductDeliveryEquals_Found(DeliveryTypeEnum deliveryType)
        {
            var item = new OrderItem()
            {
                DeliveryType = deliveryType
            };

            Assert.True(QueryHelpers.ProductDeliveryEquals(item, deliveryType));
        }

        [Theory]
        [InlineData(DeliveryTypeEnum.Online)]
        public void ProductDeliveryEquals_NotFound(DeliveryTypeEnum deliveryType)
        {
            var item = new OrderItem()
            {
                DeliveryType = DeliveryTypeEnum.Physical
            };

            Assert.False(QueryHelpers.ProductDeliveryEquals(item, deliveryType));
        }

        [Fact]
        public void ProductTypeIn_Found()
        {
            ProductTypeEnum[] productTypes = new ProductTypeEnum[] { ProductTypeEnum.ActivateMembership, ProductTypeEnum.UpgradeMembership };

            var item = new OrderItem()
            {
                ProductType = ProductTypeEnum.ActivateMembership
            };

            Assert.True(QueryHelpers.ProductTypeIn(item, productTypes));
        }

        [Fact]
        public void ProductTypeIn_NotFound()
        {
            ProductTypeEnum[] productTypes = new ProductTypeEnum[] { ProductTypeEnum.ActivateMembership, ProductTypeEnum.UpgradeMembership };

            var item = new OrderItem()
            {
                ProductType = ProductTypeEnum.Book
            };

            Assert.False(QueryHelpers.ProductTypeIn(item, productTypes));
        }

    }
}