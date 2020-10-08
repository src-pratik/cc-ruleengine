namespace PP.CodeTest
{
    /// <summary>
    /// Mock pricing store to get pricing information
    /// </summary>
    public static class Pricing
    {
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

                default:
                    return 0;
            }
        }
    }

}