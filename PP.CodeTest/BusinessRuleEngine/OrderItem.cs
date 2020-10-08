namespace PP.CodeTest
{
    /// <summary>
    /// Sample order item to mock the input
    /// </summary>
    public class OrderItem : IItem
    {
        public string Key { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public DeliveryTypeEnum DeliveryType { get; set; }
    }
}