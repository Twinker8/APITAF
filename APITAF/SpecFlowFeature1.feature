Feature: APITests

@mytag
Scenario Outline: Validate that the list of users can be received successfully
	Given I send a <verb> request to <endpoint> endpoint
	When the response is received
	Then the user should receive a 200 OK response code
	Then the response should contain a list of users with the following information:
	
	Examples: 
	| verb | endpoint |
	| Get  | /users   |

Scenario Outline: Validate response header for a list of users 
	Given I send a <verb> request to <endpoint> endpoint
	When the response is received
	Then the user should receive a 200 OK response code
	And I should see "Content-Type" in response headers
	Then "Content-Type" header should have value "application/json; charset=utf-8"

	Examples: 
	| verb | endpoint |
	| Get  | /users   |
	