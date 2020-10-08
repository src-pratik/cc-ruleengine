using System;
using System.Collections.Generic;

namespace PP.CodeTest
{
    /// <summary>
    /// Interface for Business Engine
    /// </summary>
    public interface IBusinessRulesEngine
    {
        List<BusinessFunctionRule> Rules { get; }

        void Process(OrderItem item, Action<BusinessFunctionRule, OrderItem> onRuleHit = null, Action onComplete = null);
    }
}