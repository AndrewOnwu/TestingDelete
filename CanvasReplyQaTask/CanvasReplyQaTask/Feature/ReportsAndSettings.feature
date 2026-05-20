@ReportsAndSettings
Feature: ReportsAndSettings

Background:
	Given I have successfully logged in

	@apiLogin
Scenario: Run report
	Given I navigate to "Reports & Settings" and select "Reports"
	When I search for "Project Profitability"
	Then the results are successfully returned
	When I select "Project Profitability" from search table
	And I click button "Run Report"
	Then the results are successfully returned

	@apiLogin
Scenario: Remove events from activity log
	Given I navigate to "Reports & Settings" and select "Activity Log"
	When I select first 3 items in the table
	And click "Actions" -> "Delete"
	Then the items were successfully deleted
