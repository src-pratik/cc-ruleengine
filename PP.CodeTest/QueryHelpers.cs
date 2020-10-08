using System;
using System.Linq;

namespace PP.CodeTest
{
    /// <summary>
    /// Query conditions which are used repeatedly
    /// </summary>
    public static class QueryHelpers
    {
        /// <summary>
        /// The function compares the key in the collection for existence
        /// <para="item">Object that implements IItem</para>
        /// <para="key">Key value for comparision</para>
        /// </summary>
        public static Func<IItem, string, bool> WhereKey = (item, key) => item.Key == key;

        public static Func<OrderItem, ProductTypeEnum, bool> ProductTypeEqual = (item, key) => item.ProductType == key;
        public static Func<OrderItem, ProductTypeEnum[], bool> ProductTypeIn = (item, keys) => keys.Contains(item.ProductType);
        public static Func<OrderItem, DeliveryTypeEnum, bool> ProductDeliveryEquals = (item, deliverytype) => item.DeliveryType == deliverytype;
    }
}