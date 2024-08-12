# VerticalSlice

Learning project for learning new architecture pattern for building modern .NET web applications.

## Architecture

The architecture is based on the Vertical Slice Architecture pattern. 
The idea is to group all the code related to a feature in a single folder. 
This way, all the code related to a feature is in the same place, making it easier to maintain and understand.

## Patterns

- REPR
- Mediator
- Chains of Responsibility
- CQRS

## Authentication

Generate token from cmd from TravelInspiration.Api folder:

```dotnet user-jwts create --audience travelinspiration-api```

Then you need to replace <Replace> with generated token in TravelInspiration.API.http file.

To generate token for user with `get-itineraries` claim:

```dotnet user-jwts create --audience travelinspiration-api --claim "feature=get-itineraries"```