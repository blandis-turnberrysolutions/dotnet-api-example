C# dotnet core api example

This is a starting point for a discussion about best practices for c#/dotnet apis.  Here are some of the discussions we have had in the past and some of the areas we'd like to investigate:

1.  API return types IActionResult, generic types, etc.
2.  Unit of work pattern.  Is it necessary to have separate unit of work on top of Entity Framework?
3.  Using separate Request and Response objects at the API level
4.  Auto registering dependencies
5.  Do we need the repository pattern?  If so, could it just be a single generic repository?
6.  Versioning
7.  Publishing Events and using the Outbox pattern


Entity Framework

Entity Framework Migrations

Fluent Validations

Fluent Assertions

Automapper

Automapper Projections

DBSaveChangesFilter