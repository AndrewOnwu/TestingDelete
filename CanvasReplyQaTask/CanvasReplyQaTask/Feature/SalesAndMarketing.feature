@SalesAndMarketing
Feature: SalesAndMarketing

Background:
	Given I have successfully logged in
	And I navigate to "Sales & Marketing" and select "Contacts"

@apiLogin
#Random to denote a timestamp generated name
Scenario Outline: Create contact
	When I create a new contact
		| Firstname   | Lastname   | Role   | Categories   |
		| <Firstname> | <Lastname> | <Role> | <Categories> |
	Then the contact details has successfully saved

Examples:
	| Firstname      | Lastname | Role | Categories          |
	| TestAutomation | @Random  | CFO  | Customers,Suppliers |