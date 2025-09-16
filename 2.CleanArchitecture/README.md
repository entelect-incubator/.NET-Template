# Clean Architecture Template

[![Clean Architecture](https://github.com/entelect-incubator/.NET-Template/actions/workflows/clean.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/clean.yml)

<br/>

Clean Architecture, pioneered by software architecture expert Robert C. Martin, is a design philosophy that prioritizes separation of concerns, maintainability, and testability. Enforcing a clear and hierarchical structure minimizes dependencies and allows for interchangeable components within distinct layers. Clean Architecture promotes long-term scalability, adaptability to changing requirements, and a robust foundation for building systems that are easy to understand, extend, and maintain.

## Technologies

The solution template incorporates the following technologies:

-   .NET 9
-   Entity Framework Core 9
-   MediatR
-   FeatureManagement
-   Rate limiting
-   FluentValidation
-   Polly
-   Scalar UI (for OpenAPI visualization at `/scalar`)
-   NSwag (for API client generation)
-   NUnit, FluentAssertions, NSubstitute

## Getting Started

To get started with the solution template, follow these steps:

-   [ ] Install the latest .NET 9 SDK
-   [ ] Create a folder for your solution and copy the clean code solution into it.
-   [ ] Rename the Solution File on the new solution folder for your project.

## **Overview**

### **Apis**

This layer is dedicated to hosting Apis or other ASP.NET Core projects. It depends on both the Core and Infrastructure layers. However, the dependency on the Infrastructure layer is only for supporting dependency injection. Therefore, only the Startup.cs file should reference the Infrastructure layer.

### **Application**

The Application layer contains all the business logic of the application. It depends on the core/domain layer but has no dependencies on any other layer or project. The Application layer defines interfaces that are implemented by the outside layers. For example, if the application needs to access a notification service, a new interface would be added to the Application layer, and the database or external implementation would be created within the Infrastructure layer.

### **Domain**

The Domain layer contains entities, enums, exceptions, interfaces, and types that are shared across different layers of the application. It promotes code reuse and consistency.

### **Infrastructure**

The Database layer represents the SQL database layer. It contains logic specific to the database layer, such as data access, migrations, and database-specific configurations.

The Infrastructure layer also contains classes for accessing external resources such as file systems, web services, SMTP servers, and more. These classes are based on interfaces defined within the Core layer, enabling loose coupling and dependency inversion.

### **Tests**

The Tests layer contains all the unit tests for the application. It ensures the correctness and reliability of the codebase through automated testing.

Also have included Benchmark testing and Load Testing using K6.io under the `LoadTesting` folder.

## **Additional Resources**

For more information about the Command and Query Responsibility Segregation (CQRS) pattern, refer to the [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#:~:text=The%20Command%20and%20Query%20Responsibility,performance%2C%20scalability%2C%20and%20security.)

Feel free to customize the solution template according to your specific requirements and extend it with additional features or technologies as needed.
