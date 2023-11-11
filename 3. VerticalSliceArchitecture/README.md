# Vertical Slice Template

[![Vertical Slice Architecture](https://github.com/entelect-incubator/.NET-Template/actions/workflows/vertical-slice.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/vertical-slice.yml)

<br/>

Vertical Slice Architecture, often associated with Domain-Driven Design and microservices, advocates for building end-to-end slices of functionality across all layers of the application stack. Unlike traditional horizontal layering, this approach emphasizes delivering a full, working feature set through the entire technology stack. Vertical Slice Architecture fosters a holistic understanding of business features, encourages cross-functional collaboration, and accelerates iterative development by providing a complete user experience with each implemented slice. This methodology promotes rapid feedback loops, enabling teams to iterate quickly and deliver value incrementally.

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

### **Apis**

This layer is dedicated to hosting Apis or other ASP.NET Core projects. It depends on both the Core and Infrastructure layers. However, the dependency on the Infrastructure layer is only for supporting dependency injection. Therefore, only the Startup.cs file should reference the Infrastructure layer.

### **Features**

This layer encompasses domain objects, business logic, specific feature-related command and query operations, and data access, thereby providing a comprehensive vertical slice for the specified feature.

### **Common**

The Common layer contains entities, enums, exceptions, interfaces, and types that are shared across different layers of the application. It promotes code reuse and consistency.

### **Tests**

The Tests layer contains all the unit tests for the application. It ensures the correctness and reliability of the codebase through automated testing.

Also have included Benchmark testing and Load Testing using K6.io under the `LoadTesting` folder.
