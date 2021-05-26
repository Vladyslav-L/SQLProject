@Product
Feature: Product
	In order to interact with Product table 
	As a user
	I want to be changed records in a Product table

Scenario: It is possible to insert table in DB
	Given Connected to BD
	When I send request with valid data to insert in Product table
	Then Inserted row existed in Product table

Scenario: It is possible to update existing records in a table for DB
	Given Connected to BD
	And Row with data exist in Product table
	When I send request with valid data to update existing row in Product table
	Then Updated row existed in Product table

Scenario: It is possible to delete existing records in Product table
	Given Connected to BD
	And Row with data exist in Product table
	When I send request with valid data to delete existing row in Product table
	Then Deleted row not existed in Product table