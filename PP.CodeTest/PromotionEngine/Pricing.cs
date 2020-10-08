namespace PP.CodeTest
{
    /// <summary>
    /// Mock pricing store to get pricing information
    /// </summary>
    public static class Pricing
    {
        /// <summary>
        /// Provides price of an item.
        /// </summary>
        /// <param name="key">SKU ID</param>
        /// <returns></returns>
        public static double Price(string key)
        {
            switch (key)
            {
                case "A":
                    return 50;

                case "B":
                    return 30;

                case "C":
                    return 20;

                case "D":
                    return 10;

                default:
                    return 0;
            }
        }
        /// <summary>
        /// Provides the pricing when a promo code is applied
        /// </summary>
        /// <param name="promoKey">Promo Code</param>
        /// <returns></returns>
        public static double PromoPrice(string promoKey)
        {
            switch (promoKey)
            {
                case "3 Of A`s":
                    return 130;

                case "2 Of B`s":
                    return 45;

                case "C & D":
                    return 30;

                case "10 Of C`s":
                    return (Pricing.Price("C") * 10) / 2;

                default:
                    return 0;
            }
        }
    }
}