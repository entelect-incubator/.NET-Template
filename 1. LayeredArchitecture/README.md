 # Layered Architecture Template
 
 [![Layered Architecture](https://github.com/entelect-incubator/.NET-Template/actions/workflows/layered-clean.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/layered-clean.yml)  [![Layered Architecture with DataAccess](https://github.com/entelect-incubator/.NET-Template/actions/workflows/layered.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/layered.yml)

<br/>

Layered architecture organizes a system into distinct, modular layers, each responsible for specific functions, fostering separation of concerns. This design promotes scalability, maintainability, and reusability, making it an excellent choice for projects as it enhances agility and facilitates collaborative development, allowing teams to focus on individual layers without compromising the overall system integrity.

## Technologies

The solution template incorporates the following technologies:

- .NET 8
- Entity Framework Core 8
- MediatR
- FeatureManagement
- Rate limiting
- FluentValidation
- Polly
- NSwag
- NUnit, FluentAssertions, NSubstitute

## Getting Started

There are 3 different architectural pattern templates to choose from

1. Layered Architecture
   1. Clean Template - Commands and Queries include DbContext directly because DbContext is a UnitOfWork and DbSets are Repositories. *Recommended version*
   2. Template with DataAccess Layer to separate Database logic from your business logic.

To get started with the solution template, follow these steps:

- [ ] Install the latest .NET 8 SDK
- [ ] Create a folder for your solution and copy the clean code solution into it.
- [ ] Rename the Solution File on the new solution folder for your project.

## **Overview**

### **Apis**

This layer is dedicated to hosting Apis or other ASP.NET Core projects. It depends on both the Core and Infrastructure layers. However, the dependency on the Infrastructure layer is only for supporting dependency injection. Therefore, only the Startup.cs file should reference the Infrastructure layer.

### **Core**

The Core layer contains all the business logic of the application. It depends on the core/domain layer but has no dependencies on any other layer or project. The Core layer defines interfaces that are implemented by the outside layers. For example, if the application needs to access a notification service, a new interface would be added to the Core layer, and the implementation would be created within the Infrastructure layer.

### **Common**

The Common layer contains entities, enums, exceptions, interfaces, and types that are shared across different layers of the application. It promotes code reuse and consistency.

### **Database**

The Database layer represents the SQL database layer. It contains logic specific to the database layer, such as data access, migrations, and database-specific configurations.

### **Tests**

The Tests layer contains all the unit tests for the application. It ensures the correctness and reliability of the codebase through automated testing.

Also have included Benchmark testing and Load Testing using K6.io under the `LoadTesting` folder.

### **Providers**

The Providers layer contains classes for accessing external resources such as file systems, web services, SMTP servers, and more. These classes are based on interfaces defined within the Core layer, enabling loose coupling and dependency inversion.

## **Additional Resources**

For more information about the Command and Query Responsibility Segregation (CQRS) pattern, refer to the [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#:~:text=The%20Command%20and%20Query%20Responsibility,performance%2C%20scalability%2C%20and%20security.)

Feel free to customize the solution template according to your specific requirements and extend it with additional features or technologies as needed.
