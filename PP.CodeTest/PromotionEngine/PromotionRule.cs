using System;
using System.Collections.Generic;

namespace PP.CodeTest
{
    /// <summary>
    /// Promotion Rule which stores the business rules to be verified if they are satisfied
    /// </summary>
    public class PromotionRule : IRule, IPromotionRule
    {
        /// <summary>
        /// The list of business conditions.
        /// </summary>
        public Dictionary<string, int> Conditions { get; }
        /// <summary>
        /// The where condition by which the system can identify which items to look for
        /// </summary>
        public Func<CartItem, bool> WhereCondition { get; }
        /// <summary>
        /// The count of items that are required for apply the business condition
        /// </summary>
        public Func<CartItem, bool> CountCondition { get; }
        /// <summary>
        /// Friendly code for the rule
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Order in which the rule needs to be executed. 
        /// This can be helpful when we need to carry out rules & actions in dependent sequence
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Promotion rule which includes the conditions to verify 
        /// </summary>
        /// <param name="code">Friendly code for the rule</param>
        /// <param name="conditions">Dictionary of rules which can used to define the rule for apply promo on any SKU</param>
        /// <param name="order">Order in which the rule needs to be executed.</param>
        public PromotionRule(string code, Dictionary<string, int> conditions, int order = 0)
        {
            Conditions = conditions;
            Code = code;
            //build up the conditions based upon the different rules that received
            var whereExpression = PredicateBuilder.False<CartItem>();
            var countExpression = PredicateBuilder.False<CartItem>();
            foreach (var item in Conditions)
            {
                countExpression = countExpression.Or(a => (a.Key == item.Key & a.Count >= item.Value));
                whereExpression = whereExpression.Or(a => a.Key == item.Key);
            }

            WhereCondition = whereExpression.Compile();
            CountCondition = countExpression.Compile();
        }
    }

}