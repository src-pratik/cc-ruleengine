namespace PP.CodeTest
{
    /// <summary>
    /// Sample cart item to mock the input
    /// </summary>
    public class CartItem : IItem
    {
        public string Key { get; set; }
        public int Count { get; set; }
    }
}