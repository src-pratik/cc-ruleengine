# Rule Engine
------------------

### Implementation

1. Define simple generic Rule object to keep all rules
1. Make a processing engine which keeps a list of rules
1. Engine goes through the rule for the given input
1. If the engine hits a rule condition then invoke an action against it. 

The implementation will just focus on the rule engine & inform that the rule has hit. What to do after the rule is hit, the business function will be just mocked.

For eg. If Product is Book, then do some action. 

Rule Engine & Functions to be carried out are two seperate concerns.

## Problem Statement 01 : Promotion Engine
Implementation under `PromotionEngine` folder.

Test Case Implementations :
- Tests_BRE_01 : Some basic functional cases
- Tests_BRE_02 : Functional cases provided as example
- Tests_BRE_03 : Added new promo rule

## Problem Statement 02 : Business Rule Engine
Implementation under `BusinessRuleEngine` folder.

Test Case Implementations :
- Tests_BRE_01 : Some basic for query helpers
- Tests_BRE_02 : Functional Test Cases

---
## PP.CodeTest Code Documentation##

## PP.CodeTest ##

# T:PP.CodeTest.BusinessFunctionRule

 Business Rule which stores the business rules to be verified if they are satisfied 



---
##### P:PP.CodeTest.BusinessFunctionRule.Code

 Code to identify the rule 



---
##### P:PP.CodeTest.BusinessFunctionRule.WhereCondition

 The where condition which defines the business rule to be verified. 



---
# T:PP.CodeTest.BusinessRuleEngine

 Engine to verify the business rules against an given input. 



---
##### P:PP.CodeTest.BusinessRuleEngine.Name

 Name for the engine 



---
##### P:PP.CodeTest.BusinessRuleEngine.Rules

 List of all rules that are defined 



---
##### M:PP.CodeTest.BusinessRuleEngine.Process(PP.CodeTest.OrderItem,System.Action{PP.CodeTest.BusinessFunctionRule,PP.CodeTest.OrderItem},System.Action)

 Process the rules on the input 

|Name | Description |
|-----|------|
|item: |Input on which the rules are to be applied|
|Name | Description |
|-----|------|
|onRuleHit: |Invoke delegate when a particular rules is satisified|
|Name | Description |
|-----|------|
|onComplete: |Invoke delegate when the engine has completely processed the input|


---
# T:PP.CodeTest.DeliveryTypeEnum

 Mode in which the product is bought 



---
# T:PP.CodeTest.ProductTypeEnum

 Type of product 



---
# T:PP.CodeTest.EventLogTrace

 This is just used to record the sequence of rule hits. It will enable to test if the rules are properly executed in the sequence. 



---
# T:PP.CodeTest.OrderItem

 Sample order item to mock the input 



---
# T:PP.CodeTest.IBusinessRulesEngine

 Interface for Business Engine 



---
# T:PP.CodeTest.IItem

 Generic interface that defines for any input 



---
# T:PP.CodeTest.IPromotionEngine

 Interface for Promotion Engine 



---
# T:PP.CodeTest.IPromotionRule

 Interface for Promotion Rules 



---
# T:PP.CodeTest.IRule

 Generic interface that defines a rule for the system 



---
# T:PP.CodeTest.IRuleEngine

 Generic interface for Rule Engine 



---
# T:PP.CodeTest.PredicateBuilder

 This is just a helper class to create dynamic expressions NOTE: Time was not spent on writing this code it was already part of my personal code 



---
# T:PP.CodeTest.CartItem

 Sample cart item to mock the input 



---
# T:PP.CodeTest.Pricing

 Mock pricing store to get pricing information 



---
##### M:PP.CodeTest.Pricing.Price(System.String)

 Provides price of an item. 

|Name | Description |
|-----|------|
|key: |SKU ID|
Returns: 



---
##### M:PP.CodeTest.Pricing.PromoPrice(System.String)

 Provides the pricing when a promo code is applied 

|Name | Description |
|-----|------|
|promoKey: |Promo Code|
Returns: 



---
# T:PP.CodeTest.PromotionRule

 Promotion Rule which stores the business rules to be verified if they are satisfied 



---
##### P:PP.CodeTest.PromotionRule.Conditions

 The list of business conditions. 



---
##### P:PP.CodeTest.PromotionRule.WhereCondition

 The where condition by which the system can identify which items to look for 



---
##### P:PP.CodeTest.PromotionRule.CountCondition

 The count of items that are required for apply the business condition 



---
##### P:PP.CodeTest.PromotionRule.Code

 Friendly code for the rule 



---
##### M:PP.CodeTest.PromotionRule.#ctor(System.String,System.Collections.Generic.Dictionary{System.String,System.Int32},System.Int32)

 Promotion rule which includes the conditions to verify 

|Name | Description |
|-----|------|
|code: |Friendly code for the rule|
|Name | Description |
|-----|------|
|conditions: |Dictionary of rules which can used to define the rule for apply promo on any SKU|
|Name | Description |
|-----|------|
|order: |Order in which the rule needs to be executed.|


---
# T:PP.CodeTest.PromotionRuleEngine

 Engine to verify the business rules against an given input. 



---
# T:PP.CodeTest.QueryHelpers

 Query conditions which are used repeatedly 



---