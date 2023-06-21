 <img align="left" width="116" height="116" src="logo.png" />
 
 # Clean Architecture Solution Template
 
[![CI](https://github.com/entelect-incubator/.NET-CleanArchitecture/actions/workflows/dotnet.yml/badge.svg)](https://github.com/entelect-incubator/.NET-CleanArchitecture/actions/workflows/dotnet.yml)

<br/>

This is a solution template for creating a backend framework following the principles of Clean Architecture. It provides a layered structure that promotes separation of concerns and modularity, allowing for easier maintenance, scalability, and reusability. The template is designed to be used as a starting point for creating .NET applications and can be reused by different people.

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
- NUnit, FluentAssertions, Moq

## Getting Started

To get started with the solution template, follow these steps:

- [ ] Install the latest .NET 8 SDK
- [ ] Create a folder for your solution and copy the clean code solution into it.
- [ ] Rename the Solution File on the new solution folder to your project.

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

### **Providers**

The Providers layer contains classes for accessing external resources such as file systems, web services, SMTP servers, and more. These classes are based on interfaces defined within the Core layer, enabling loose coupling and dependency inversion.

## **Additional Resources**

For more information about the Command and Query Responsibility Segregation (CQRS) pattern, refer to the [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#:~:text=The%20Command%20and%20Query%20Responsibility,performance%2C%20scalability%2C%20and%20security.)

Feel free to customize the solution template according to your specific requirements and extend it with additional features or technologies as needed.
