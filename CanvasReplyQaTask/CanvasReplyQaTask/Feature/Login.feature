@Login
Feature: Login

@webLogin
Scenario: Login
	Given I am on the loginpage
	When I enter the following login details
		| Username | Password |
		| admin    | admin    |
	And I click Login button
	Then I have successfully logged in

@apiLogin
Scenario: Login via API
	Given I have successfully logged in