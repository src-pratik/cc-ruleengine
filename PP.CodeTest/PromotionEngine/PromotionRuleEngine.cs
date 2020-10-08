using System;
using System.Collections.Generic;
using System.Linq;

namespace PP.CodeTest
{
    /// <summary>
    /// Engine to verify the business rules against an given input.
    /// </summary>
    public class PromotionRuleEngine : IRuleEngine, IPromotionEngine
    {
        public string Name { get; }

        public List<PromotionRule> Rules { get; }

        public PromotionRuleEngine(string name, List<PromotionRule> rules)
        {
            Name = name;
            Rules = rules.OrderBy(x => x.Order).ToList();
        }

        public void Process(List<CartItem> items, Action<PromotionRule> onRuleHit, Action onComplete = null)
        {
            Rules.OrderBy(x => x.Order).ToList().ForEach(r =>
            {
                while (items.Where(x => r.WhereCondition(x)).Count(x => r.CountCondition(x)) == r.Conditions.Count())
                {
                    foreach (var item in items)
                    {
                        if (r.Conditions.ContainsKey(item.Key))
                            item.Count -= r.Conditions[item.Key];
                    }
                    onRuleHit.Invoke(r);
                }
            });

            onComplete?.Invoke();
        }
    }
}