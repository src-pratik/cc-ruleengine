using System;

namespace PP.CodeTest
{
    /// <summary>
    /// Interface for Promotion Rules
    /// </summary>
    public interface IPromotionRule
    {
        Func<CartItem, bool> WhereCondition { get; }
        Func<CartItem, bool> CountCondition { get; }
    }
}