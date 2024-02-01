Feature: APITests

@/usersEndpoint
Scenario Outline: Validate that the list of users can be received successfully
	Given I send a Get request to <endpoint> endpoint
	When the response is received
	Then the user should receive a <responseCode> response code
	Then the response should contain a list of users with necessary info should count 10
	
	Examples: 
	| endpoint | responseCode |
	| /users   |  OK | 

@/usersEndpoint
Scenario Outline: Validate response header for a list of users 
	Given I send a Get request to <endpoint> endpoint
	When the response is received
	Then the user should receive a <responseCode> response code
	And I should see "Content-Type" in response headers
	Then "Content-Type" header should have value "application/json; charset=utf-8"

	Examples: 
	| endpoint | responseCode |
	| /users   |  OK | 

@/usersEndpoint
Scenario Outline: Validate list of users should be 10
	Given I send a Get request to <endpoint> endpoint
	When  the response is received 
	Then  the user should receive a <responseCode> response code 
	And the response should contain a list of users with necessary info should count 10
	And there are no user id duplicates

	Examples: 
	| endpoint | responseCode |
	| /users   |  OK |  

@/usersEndpoint
Scenario Outline: Validate creation of new user
	Given I send a Post request to <endpoint> endpoint
	When  the response is received 
	Then  the user should receive a <responseCode> response code 
	And the new created user in not empty and contains the id value

	Examples: 
	| endpoint | responseCode |
	| /users   |  Created |

@/invalidendpointEndpoint
Scenario Outline: Validate that user is notified if resource doesn’t exist
	Given I send a Get request to <endpoint> endpoint
	When the response is received
	Then the user should receive a <responseCode> response code
	
	Examples: 
	| endpoint         |  responseCode|
	| /invalidendpoint |  NotFound    |

	