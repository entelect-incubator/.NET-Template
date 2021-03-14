 <img align="left" width="116" height="116" src="logo.png" />
 
 # Clean Architecture Solution Template
[![.NET 5](https://github.com/entelect-incubator/.NET-CleanArchitecture/actions/workflows/dotnet5.yml/badge.svg)](https://github.com/entelect-incubator/.NET-CleanArchitecture/actions/workflows/dotnet5.yml)

<br/>

This is a solution template for creating a backend framework following the principles of Clean Architecture. Create a new project based on this template by clicking the above Use this template button or download the solution.

What is [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#:~:text=The%20Command%20and%20Query%20Responsibility,performance%2C%20scalability%2C%20and%20security.)

## Technologies

- .NET 5
- Entity Framework Core 5
- MediatR
- AutoMapper
- FluentValidation
- NUnit, FluentAssertions, Moq

## Getting Started

- [ ] Install the latest .NET 5 SDK

Create a folder for your solution and copy the clean code solution into it.

Run [RenameSolution.ps1](./RenameSolution.ps1) on the new solution folder to rename it to your project.

## **Overview**

### **Tests**

This will contain all unit tests.

### **Common**

This will contain all entities, enums, exceptions, interfaces and types.

### **Database**

SQL database layer.

### **Data Access**

Logic specific to the database layer.

### **Core**

This layer contains all business logic. It is dependent on the core/domain layer but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application needs to access a notification service, a new interface would be added to the application and implementation would be created within the infrastructure.

### **Infrastructure**

This layer contains classes for accessing external resources such as file systems, web services, SMTP, and so on. These classes should be based on interfaces defined within the application layer.

### **Apis**

This layer is for all Apis or other ASP.NET Core projects. This layer depends on both the Core and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Startup.cs should reference Infrastructure.

