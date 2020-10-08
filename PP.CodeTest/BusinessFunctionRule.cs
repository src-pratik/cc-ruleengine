using System;

namespace PP.CodeTest
{
    /// <summary>
    /// Business Rule which stores the business rules to be verified if they are satisfied
    /// </summary>
    public class BusinessFunctionRule : IRule
    {
        /// <summary>
        /// Code to identify the rule
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Order in which the rule needs to be executed.
        /// This can be helpful when we need to carry out rules & actions in dependent sequence
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// The where condition which defines the business rule to be verified.
        /// </summary>
        public Func<OrderItem, bool> WhereCondition { get; }

        public BusinessFunctionRule(string code, Func<OrderItem, bool> whereCondition, int order = 0)
        {
            WhereCondition = whereCondition;
            Code = code;
            Order = order;
        }
    }
}