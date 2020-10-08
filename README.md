# Rule Engine
------------------

## Problem Statement 02 : Business Rule Engine
-----
### Implementation

1. Define simple generic Rule object to keep all rules
1. Make a processing engine which keeps a list of rules
1. Engine goes through the rule for the given input
1. If the engine hits a rule condition then invoke an action against it. 

The implementation will just focus on the rule engine & inform that the rule has hit. What to do after the rule is hit, the business function will be just mocked.

For eg. If Product is Book, then do some action. 

Rule Engine & Functions to be carried out are two seperate concerns.