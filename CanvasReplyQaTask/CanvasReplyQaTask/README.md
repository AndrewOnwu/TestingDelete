## Automated tests for Canvas Reply

Based on Net.Core 6.0, Nunit, Specflow and C#.

Test framework contains Selenium.
---

## Default settings
Run on localhost

Run on latest browser Chrome (Default) , Firefox, Edge

Can be updated in appsetting file with values "chrome" or "edge" or "firefox"

Test is set to run in parallel.

Reporting :- A report is generated in the project folder "Report".


## Tests execution
To execute all tests from console command - Ensure you are in project folder

dotnet test

You can also set the following environment variables browser, user and password

For example browser can be set as below


SETLOCAL
--
SET browser='browser'
--
dotnet test
--


if you want to run certain tags

tag values Login, ReportsAndSettings and SalesAndMarketing

dotnet test --filter "Category='tag'"
---