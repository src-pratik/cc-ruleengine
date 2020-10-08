using System;
using System.Collections.Generic;

namespace PP.CodeTest
{
    /// <summary>
    /// Interface for Promotion Engine
    /// </summary>
    public interface IPromotionEngine
    {
        List<PromotionRule> Rules { get; }

        void Process(List<CartItem> items, Action<PromotionRule> onRuleHit, Action onComplete = null);
    }
}