using System.Collections.Generic;

namespace PP.CodeTest
{
    /// <summary>
    /// This is just used to record the sequence of rule hits.
    /// It will enable to test if the rules are properly executed in the sequence.
    /// </summary>
    public class EventLogTrace
    {
        public Dictionary<int, string> Details { get; set; }
        private int _seqeuence = 1;

        public EventLogTrace()
        {
            Details = new Dictionary<int, string>();
        }

        public void HandleRuleHit(BusinessFunctionRule rule, OrderItem item)
        {
            Details[_seqeuence] = rule.Code;
            _seqeuence++;
        }

        public void HandleComplete()
        {
            Details[_seqeuence] = "Complete";
            _seqeuence++;
        }
    }
}