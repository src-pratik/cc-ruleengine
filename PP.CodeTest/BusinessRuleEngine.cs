using System;
using System.Collections.Generic;
using System.Linq;

namespace PP.CodeTest
{
    /// <summary>
    /// Engine to verify the business rules against an given input.
    /// </summary>
    public class BusinessRuleEngine : IRuleEngine, IBusinessRulesEngine
    {
        /// <summary>
        /// Name for the engine
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// List of all rules that are defined
        /// </summary>
        public List<BusinessFunctionRule> Rules { get; }

        public BusinessRuleEngine(string name, List<BusinessFunctionRule> rules)
        {
            Name = name;
            Rules = rules.OrderBy(x => x.Order).ToList();
        }

        /// <summary>
        /// Process the rules on the input
        /// </summary>
        /// <param name="item">Input on which the rules are to be applied</param>
        /// <param name="onRuleHit">Invoke delegate when a particular rules is satisified</param>
        /// <param name="onComplete">Invoke delegate when the engine has completely processed the input</param>
        public void Process(OrderItem item, Action<BusinessFunctionRule, OrderItem> onRuleHit = null, Action onComplete = null)
        {
            Rules.ForEach(r =>
            {
                if (r.WhereCondition(item))
                    onRuleHit?.Invoke(r, item);
            });

            onComplete?.Invoke();
        }
    }
}